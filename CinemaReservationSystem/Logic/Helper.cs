using System.Globalization;
public interface ObjectHasID
{
    string ID { get; }
}


public class Helper
{
    //Can be used to filter char or multiple chars and can display menu with header and user options in a box graphic
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
                WriteErrorMessage("Invalid input. Please try again.");
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

    public static void ClearLastLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.Write(new string(' ', Console.BufferWidth));
        Console.SetCursorPosition(0, Console.CursorTop - 1);
    }

    public static void ClearLineDo()
    {
        Console.Write(new string(' ', Console.BufferWidth));
        Console.SetCursorPosition(0, Console.CursorTop - 1);

    }

    public static void WriteInCenter(string data, int length=0, bool offset=false)
    {
        if (offset is true)
        {
            foreach (var model in data.Split('\n'))
            {
                Console.SetCursorPosition((Console.WindowWidth - length) / 2, Console.CursorTop);
                Console.WriteLine(model);
            }
        }
        else if (offset is false)
        {
            foreach (var model in data.Split('\n'))
            {
                Console.SetCursorPosition((Console.WindowWidth - model.Length - length) / 2, Console.CursorTop);
                Console.WriteLine(model);
            }
        }

    }


    public static (string, string) Catchinput(int maxLength, string type,
        string Case, string username = "", string birthdate = "", string email = "", string password = "")
    {
        string input = GetInputString(type, username, birthdate, email, password);
        string taboresc = string.Empty;
        bool spacebar = false, validated = false;

        DrawUI(Case, username, birthdate, email, password, input, spacebar);

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    taboresc = "ESC";
                    return (input, taboresc);
                case ConsoleKey.Tab when (key.Modifiers & ConsoleModifiers.Shift) != 0:
                    taboresc = "SHIFTTAB";
                    return (input, taboresc);
                case ConsoleKey.Tab:
                    taboresc = "TAB";
                    return (input, taboresc);
                case ConsoleKey.Spacebar when string.Equals(Case, "loginpassword"):
                    spacebar = !spacebar;
                    break;
                case ConsoleKey.Backspace when input.Length > 0:
                    ConsoleClear();
                    input = input.Remove(input.Length - 1);
                    break;
                case ConsoleKey.Enter:
                    validated = ValidateUserInput(Case, type, input);
                    break;
                default:
                    HandleInputKey(ref input, maxLength, key.KeyChar, type, Case);
                    break;
            }
            UpdateUI(Case, type, input, username, birthdate, email, password, spacebar);
            if (validated is true) return (input, taboresc);
        }
    }
    private static bool ValidateUserInput(string Case, string type, string input)
    {
        switch (type)
        {
            case "username":

                switch (Case)
                {
                    case "login":
                        if (IsValidUsernameLog(input) == -1)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            char confirm = Helper.ReadInput((char c) => c == 'y' || c == 'n', "username not found",
                                                            "Username could not be found. Register account? Y/N");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            if (confirm == 'y') InterfaceController.RegisterUser(input);
                            else InterfaceController.LogIn();
                        }
                        else if (IsValidUsernameLog(input) == 1) return true;
                        break;
                    case "register":
                        if (IsValidUsername(input) == 0)
                        {
                            WriteErrorMessage("Invalid username. Must be atleast 3 chars long.");
                        }
                        else if (IsValidUsername(input) == -1)
                        {
                            WriteErrorMessage("Username already in use.");
                        }
                        else if (IsValidUsername(input) == 1) return true;
                        break;
                }
                break;
            case "birthdate":
                if (!IsValidBD(input))
                {
                    WriteErrorMessage("Invalid birthdate. Use format: dd-MM-yyyy.");
                }
                else return true;
                break;
            case "email":
                if (!IsValidEmail(input))
                {
                    WriteErrorMessage("""Invalid email. Must contain "@" and ".".""");
                }
                else return true;
                break;
            case "password":
                if (!IsValidPassword(input) && string.Equals(Case, "password") || !IsValidPassword(input) && string.Equals(Case, "register"))
                {
                    WriteErrorMessage("Invalid password. Must be atleast 6 chars long and contain a digit.");
                }
                else if (!IsValidPassword(input) && string.Equals(Case, "loginpassword"))
                {
                    WriteErrorMessage("Invalid password........");
                }
                else return true;
                break;
        }
        return false;
    }

    private static void WriteErrorMessage(string error)
    {
        int cursortop;
        
        Console.WriteLine(new string(' ', Console.WindowWidth));
        (_, cursortop) = Console.GetCursorPosition();
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.SetCursorPosition(0, cursortop -1);
        WriteInCenter(error);
        Console.ResetColor();
    }

    private static void HandleInputKey(ref string input, int maxLength, char keyChar, string type, string Case)
    {
        if (input.Length == maxLength)
        {
            WriteErrorMessage("Max input length reached!");
        }
        else if (char.IsLetterOrDigit(keyChar) || char.IsSymbol(keyChar) || char.IsPunctuation(keyChar))
        {
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
    private static void UpdateUI(string Case, string type, string input, string username, string birthdate, string email, string password, bool spacebar)
    {
        switch (Case)
        {
            case "login":
                Graphics.DrawLogin(input);
                break;
            case "loginpassword":
                if (spacebar)
                {
                    Graphics.DrawLogin(username, input);
                }
                else
                {
                    Graphics.DrawLogin(username, new string('*', input.Length));
                }
                break;
            case "register":
                switch (type)
                {
                    case "username":
                        Graphics.DrawRegister(input, birthdate, email, password);
                        break;
                    case "birthdate":
                        Graphics.DrawRegister(username, input, email, password);
                        break;
                    case "email":
                        Graphics.DrawRegister(username, birthdate, input, password);
                        break;
                    case "password":
                        Graphics.DrawRegister(username, birthdate, email, input);
                        break;
                }
                break;
        }
    }
    private static void DrawUI(string Case, string username, string birthdate, string email, string password, string input, bool spacebar)
    {
        switch (Case)
        {
            case "register":
                Graphics.DrawRegister(username, birthdate, email, password);
                break;
            case "login":
                Graphics.DrawLogin(input);
                break;
            case "password":
                if (spacebar)
                {
                    Graphics.DrawLogin(username, input);
                }
                else
                {
                    Graphics.DrawLogin(username, new string('*', input.Length));
                }
                break;
        }
    }
    public static void ConsoleClear()
    {
        Console.WriteLine("\b \b");
        Console.Clear();
    }

    // everything about getting the valid input.
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
