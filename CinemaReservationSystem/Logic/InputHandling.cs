using System.Globalization;
public static class InputHandling
{
    ///////from Helper.cs///////////////
    public static char ReadInput(Func<char, bool> validationCriteria, string header="", string text="")
    {
        Graphics.BoxText(text, header);
        /* validate input examples for char
                        char letterInput = ReadInput(char.IsLetter);

        Validate input to be a digit
        char digitInput = ReadInput(char.IsDigit);

        Validate input to be a specific letter 'X'
        char specificLetterInput = ReadInput((char c) => c == 'X');

        Validate input to be a specific number '5'
        char specificNumberInput = ReadInput((char c) => c == '5');*/
        Console.CursorVisible = false;
        char input;
        bool isValidInput;
        Console.WriteLine();
        do
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            input = keyInfo.KeyChar;
            input = char.ToLower(input);

            isValidInput = validationCriteria(input);

            if (!isValidInput)
            {
                Graphics.WriteErrorMessage("Invalid input. Please try again.");
            }
            ClearLineDo();
        } while (!isValidInput);

        Console.Clear();
        return input;
    }

    public static ConsoleKey ReadInput(Func<ConsoleKey, bool> validationCriteria, string header = "", string text = "")
    {
        Graphics.BoxText(text, header);

        Console.CursorVisible = false;
        ConsoleKey input;
        bool isValidInput;
        Console.WriteLine();
        do
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            input = keyInfo.Key;

            isValidInput = validationCriteria(input);

            if (!isValidInput)
            {
                WriteErrorMessage("Invalid input. Please try again.");
            }
            ClearLineDo();
        } while (!isValidInput);

        Console.Clear();
        return input;
    }
    //Can be used to filter string and displayer menu with header and user options in a box graphic
    // Displays user input key for key, user can backspace input
    //Can be used for login page for input validation
    public static string ReadInput(Func<string, bool> stringValidationCriteria)
    {
        /*
        Validate input to be a string that starts with 'ABC'
        string abcInput = ReadInput(s => s.StartsWith("ABC"));

        Validate input to be a string that contains only digits
        string digitsInput = ReadInput(s => s.All(char.IsDigit));
            */
        Console.CursorVisible = false;
        string input = string.Empty;
        int windowWidth = Console.WindowWidth;
        Console.WriteLine("\n\n");
        do
        {
            int leftPadding = (windowWidth - input.Length) / 2;
            Console.SetCursorPosition(leftPadding, Console.CursorTop);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ClearLineDo();

            if (input.Length == 10)
            {
                ClearLastLine();
                ClearLineDo();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                WriteInCenter("Length exceeded. Please try again.");
                Console.ForegroundColor = ConsoleColor.White;
                input = string.Empty;
            }
            else if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                ClearLineDo();
                input = input.Substring(0, input.Length - 1);
                ClearLineDo();
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                if (!stringValidationCriteria(input) || string.IsNullOrEmpty(input))
                {
                    ClearLastLine();
                    ClearLineDo();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    WriteInCenter("Invalid input. Please try again...");
                    Console.ForegroundColor = ConsoleColor.White;
                    input = string.Empty;
                }
                else
                {
                    break;
                }
            }
            else
            {
                ClearLastLine();
                input += keyInfo.KeyChar;
            }
            Console.WriteLine();
            WriteInCenter(input);
        } while (true);

        ClearLastLine();
        return input;
    }

    private static void HandleInputKey(ref string input, int maxLength, char keyChar, string type, string Case)
    {
        if (input.Length == maxLength)
        {
            WriteErrorMessage("Max input length reached!");
        }
        else if (char.IsLetterOrDigit(keyChar) || char.IsSymbol(keyChar) || char.IsPunctuation(keyChar))
        {
            ConsoleClear();
            {
                if (string.Equals(Case, "register") && string.Equals(type, "birthdate"))
                {
                    if (input.Length == 2 || input.Length == 5) input += "-";
                }
                input += keyChar;
            }
        }
    }

    private static string GetInputString(string type, string username, string birthdate, string email, string password)
    {
        {
            return type switch
            {
                "username" => !string.IsNullOrEmpty(username) ? username : string.Empty,
                "birthdate" => !string.IsNullOrEmpty(birthdate) ? birthdate : string.Empty,
                "email" => !string.IsNullOrEmpty(email) ? email : string.Empty,
                "password" => !string.IsNullOrEmpty(password) ? password : string.Empty,
                _ => string.Empty
            };
        }
    }


    //////From Graphics.cs//////////////////
    ///For AUDITORIUM////////////
    private static void HandleUserInput(Screening screening, User user, string auditorium, int width, int maxindex, int mintopindex, ref int indexPos,
                                        List<string> selectedseats, List<int> listreservedindex, Dictionary<int, char> numbertoletter,
                                        List<int> reservedbyotheruser, List<string> reservedseatIDs, ref bool reservedDone)
    {
        ConsoleKeyInfo key = Console.ReadKey(true);

        switch (key.Key)
        {
            case ConsoleKey.LeftArrow:
            case ConsoleKey.RightArrow:
            case ConsoleKey.DownArrow:
            case ConsoleKey.UpArrow:
                HandleUserMovement(key, ref indexPos, width, maxindex, mintopindex);
                break;
            case ConsoleKey.Spacebar:
                HandleSpacebarKeyPress(auditorium, indexPos, selectedseats, listreservedindex, reservedbyotheruser, numbertoletter);
                break;
            case ConsoleKey.Enter:
                HandleEnterKeyPress(screening, auditorium, listreservedindex, reservedseatIDs, ref reservedDone);
                break;
            case ConsoleKey.Backspace:
                HandleBackspaceKeyPress(listreservedindex, selectedseats);
                break;
            case ConsoleKey.Escape:
                HandleEscapeKeyPress(user, screening);
                break;
            case ConsoleKey.Home:
                Helper.HandleHomeKey(user.ID);
                break;
        }
    }

     private static void HandleSpacebarKeyPress(string auditorium, int indexPos, List<string> selectedseats,
                                                List<int> listreservedindex, List<int> reservedbyotheruser, Dictionary<int, char> numbertoletter)
    {
        string representingseat = $"{numbertoletter[GetRowFromIndex(auditorium, indexPos) + 1]}{GetSeatNumberFromIndex(auditorium, indexPos)}";
        if (!selectedseats.Contains(representingseat) &&
            IsSeat(auditorium, indexPos) && !listreservedindex.Contains(indexPos) &&
            !reservedbyotheruser.Contains(indexPos) && selectedseats.Count < 40)
        {
            selectedseats.Add(representingseat);
            listreservedindex.Add(indexPos);
        }
        else if (selectedseats.Contains(representingseat) &&
                 IsSeat(auditorium, indexPos))
        {
            selectedseats.Remove(representingseat);
            listreservedindex.Remove(indexPos);
        }
    }

    //user input handling
    private static void HandleEnterKeyPress(Screening screening, string auditorium, List<int> listreservedindex,
                                            List<string> reservedseatIDs, ref bool reservedDone)
    {
        if (listreservedindex.Count < 1) return;
        Console.ForegroundColor = ConsoleColor.Cyan;
        char confirm = Helper.ReadInput((char c) => c == 'y' || c == 'n', "Confirm reservation", "Are you happy with your reservations? Y/N");
        Console.ForegroundColor = ConsoleColor.Gray;
        if (string.Equals(Convert.ToString(confirm), "n")) return;
        foreach (int index in listreservedindex)
        {
            ScreeningDataController.ReserveSeat(screening,Convert.ToString(GetSeatNumberFromIndex(auditorium, index, database: true) + GetAuditoriumOffset(Int32.Parse(screening.AssignedAuditorium.ID))));
            reservedseatIDs.Add(Convert.ToString(GetSeatNumberFromIndex(auditorium, index, database: true) + GetAuditoriumOffset(Int32.Parse(screening.AssignedAuditorium.ID))));
            int totalPrice = 999999;
            Reservation reservation = new(reservedseatIDs, screening.ID, totalPrice);
            //UserDataController.UpdateUserWithValue(user?, "Reservations", reservation); //how should i get currentUser here?
        }
        reservedDone = true;
    }

    //user input handling
    private static void HandleBackspaceKeyPress(List<int> listreservedindex, List<string> selectedseats)
    {
        if (listreservedindex.Count > 0)
        {
            listreservedindex.RemoveAt(listreservedindex.Count - 1);
            selectedseats.RemoveAt(selectedseats.Count - 1);
        }
    }

    private static void HandleEscapeKeyPress(User user, Screening screening)
    {
        Console.Write("\f\u001bc\x1b[3J");
        Movie movie = UserInterfaceController.GetMovieByID(screening.ID);
        UserInterfaceController.ScreeningSelect(movie, user.ID);
    }

    //user input handling
    private static void HandleUserMovement(ConsoleKeyInfo key, ref int indexPos, int width, int maxindex, int mintopindex)
    {
        indexPos = key.Key switch
        {
            ConsoleKey.LeftArrow when indexPos - 2 == - 2 => maxindex,
            ConsoleKey.LeftArrow when indexPos - 2 >= 0 && indexPos - 2 <= maxindex => indexPos -= 2,

            ConsoleKey.RightArrow when indexPos + 2 == maxindex + 2 => 0,
            ConsoleKey.RightArrow when indexPos + 2 >= 0 && indexPos + 2 <= maxindex => indexPos += 2,

            ConsoleKey.DownArrow when indexPos + width >= maxindex && indexPos + width <= maxindex + width + 2 => indexPos -= mintopindex,
            ConsoleKey.DownArrow when indexPos + width >= 0 && indexPos + width <= maxindex => indexPos += width + 2,

            ConsoleKey.UpArrow when indexPos - width >= -width && indexPos - width <= 0 => indexPos += mintopindex,
            ConsoleKey.UpArrow when indexPos - width >= 0 && indexPos - width <= maxindex => indexPos -= width + 2,

            _ => throw new ArgumentException(nameof(indexPos), $"Could not find new position for: {indexPos}")
        };
    }




     public static string GetValidInput(string prompt, Func<string, bool> validation)
    {
        string input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
        } while (!validation(input));

        return input;
    }
    public static bool IsNotNull(string input) => !string.IsNullOrWhiteSpace(input);
    public static bool IsValidInt(string input) => input.All(char.IsDigit);
    public static int IsValidUsername(string input)
    {
        List<User> users = CsvHandler.Read<User>("Data/UserDB.csv");
 
        if(!string.IsNullOrWhiteSpace(input) && input.Length >= 3 && input.Length < 28)
        {
            foreach(User user in users)  
            {
                if (user.Name.Equals(input)) return -1;
            }  
            return 1;
        }
        return 0;
    }
    
    public static int IsValidUsernameLog(string input)
    {
        List<User> users = CsvHandler.Read<User>("Data/UserDB.csv");
 
        if(!string.IsNullOrWhiteSpace(input) && input.Length >= 3 && input.Length < 28)
        {
            foreach(User user in users)  
            {
                if (user.Name.Equals(input)) return 1;
            }  
            return -1;
        }
        return -1;
    }
    public static bool IsValidEmail(string input)
    {
        List<User> users = CsvHandler.Read<User>("Data/UserDB.csv");

        if(!string.IsNullOrWhiteSpace(input) && input.Contains('@') && input.Contains('.') && input.Length < 31)
        {
            foreach(User user in users)
            {
                if (user.Email.Equals(input)) return false;
            }
            return true;
        }
        return false;
    }

    public static bool IsValidPassword(string input)
    {
        // test op lege string of string onder de 6 chars.
        if (!string.IsNullOrWhiteSpace(input) && input.Length >= 6)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool IsValidBD(string input)
    {
        string dateFormat = "dd-MM-yyyy";
        if (DateTime.TryParseExact(input, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        {
            if (result.Date <= DateTime.Today)
            {
                return true; // Goede verjaardag
            }
            else
            {
                return false; // Datum is not niet geweest, dus kan niet geboren zijn
            }
        }
        else
        {
            return false; // Verkeerde format
        }
    }


    public static bool IsValidDT(string input)
    {
        string dateFormat = "dd-MM-yyyy HH:mm";
        if (DateTime.TryParseExact(input, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int GetUserAge(User user)
    {
        string DoB = user.BirthDate;
        string DateFormat = "dd-MM-yyyy";
        DateTime birthDate;
        
        DateTime.TryParseExact(DoB, DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out birthDate);
        DateTime today = DateTime.Today;
        int age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age))
        {
            age--; // The user hasn't had their birthday yet this year
        }
        return age;
    }

    public static void HandleHomeKey(string id)
    {
        if (!string.Equals(id, "not logged in"))
        {
            ConsoleClear();
            UserInterface.GeneralMenu(id);
        }
        else
        {
            ConsoleClear();
            Interface.GeneralMenu();
        }
    }
}