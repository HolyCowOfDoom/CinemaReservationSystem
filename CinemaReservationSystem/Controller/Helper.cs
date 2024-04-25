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
        bool isValidInput = false;
        Console.WriteLine();
        do
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            input = keyInfo.KeyChar;

            isValidInput = validationCriteria(input);

            if (!isValidInput)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                WriteInCenter("Invalid input. Please try again.");
                Console.ForegroundColor = ConsoleColor.White;
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

    public static void WriteColoredLetter(string letters)
    {
        Char[] array = letters.ToCharArray();
        char[] yellow = { 'S', 'B', 'G', 'P', 'U', 'V', 'F', 'T', 'H', '1', '2', '3', '4', '5' };

        foreach (Char c in array)
        {
            if (c == 'X')
            {
                Console.ForegroundColor = System.ConsoleColor.DarkRed;
                Console.Write(c);
            }
            else if (yellow.Contains(c))
            {
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.Write(c);
            }
            else
            {
                Console.ForegroundColor = System.ConsoleColor.Gray;
                Console.Write(c);
            }
        }
        Console.WriteLine();
        Console.ForegroundColor = System.ConsoleColor.Gray;
    }

    public static string ReplaceAt(string input, int index, char newChar)
    {
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }

    public static (string, string) Catchinput(int left, int top, int maxLength, string type,
        string Case, string username = "", string birthdate = "", string email = "", string password = "")
    {
        Console.SetCursorPosition(left, top);
        string input = string.Empty;
        string taboresc = string.Empty;
        bool validated = false;
        bool spacebar = false;
        
        switch (type)
        {
            case "username":
                if (!string.IsNullOrEmpty(username)) input = username;
                break;
            case "birthdate":
                if (!string.IsNullOrEmpty(birthdate)) input = birthdate;
                break;
            case "email":
                if (!string.IsNullOrEmpty(email)) input = email;
                break;
            case "password":
                if (!string.IsNullOrEmpty(password)) input = password;
                break;
        }


        if (Case == "register") Graphics.DrawRegister(username, birthdate, email, password);
        else if (Case == "login") Graphics.DrawLogin(input);
        else if (Case == "password")
        {
            if (spacebar)
            {
                Graphics.DrawLogin(username, input);
            }
            else
            {
                Graphics.DrawLogin(username, new string('*', input.Length));
            }
        }


        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                taboresc = "ESC";
                return (input, taboresc);
            }
            else if (key.Key == ConsoleKey.Tab)
            {
                taboresc = "TAB";
                return (input, taboresc);
            }
            else if (key.Key == ConsoleKey.Spacebar && Case == "loginpassword")
            {
                spacebar = !spacebar;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                switch (type)
                {
                    case "username":
                        if (Case == "login")
                        {
                            if (IsValidUsernameLog(input) == -1)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                char yorn = Helper.ReadInput((char c) => c == 'y' || c == 'n', "username not found", "Username could not be found. Register account? Y/N");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                if (yorn == 'y') InterfaceController.RegisterUser();
                                else InterfaceController.LogIn();
                            }
                            else if (IsValidUsernameLog(input) == 1) validated = true;

                        }
                        else if (Case == "register")
                        {
                            if (IsValidUsername(input) == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                WriteInCenter("Invalid username. Must be atleast 3 chars long.");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            else if (IsValidUsername(input) == -1)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                WriteInCenter("Username already in use.");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            else if (IsValidUsername(input) == 1) validated = true;
                        }
                        else if (IsValidUsername(input) == 1) validated = true;
                        break;
                    case "birthdate":
                        if (!IsValidBD(input))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            WriteInCenter("Invalid birthdate. Use format: dd-MM-yyyy.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else validated = true;
                        break;
                    case "email":
                        if (!IsValidEmail(input))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            WriteInCenter("""Invalid email. Must contain "@" and ".".""");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else validated = true;
                        break;
                    case "password":
                        if (!IsValidPassword(input) && Case == "password")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            WriteInCenter("Invalid password. Must be atleast 6 chars long and contain a digit.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else if (!IsValidPassword(input) && Case == "loginpassword")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            WriteInCenter("Invalid password........");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else validated = true;
                        break;
                }
                if (validated is true) return (input, taboresc);
            }
            else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input = input.Remove(input.Length - 1);
                Console.Write("\b \b");
                Console.Clear();
            }
            else if (input.Length == maxLength)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                WriteInCenter("Max input length reached");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (char.IsLetterOrDigit(key.KeyChar) || char.IsSymbol(key.KeyChar) || char.IsPunctuation(key.KeyChar))
            {
                Console.Write("\b \b");
                Console.Clear();
                input += key.KeyChar;
            }



            
            if (Case == "login") Graphics.DrawLogin(input);
            else if (Case == "loginpassword")
            {
                if (spacebar)
                {
                    Graphics.DrawLogin(username, input);
                }
                else
                {
                    Graphics.DrawLogin(username, new string('*', input.Length));
                }
            }
            else if (Case == "register")
            {
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
            }
        }
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
        List<User> users = CsvHandler.Read<User>("Model/UserDB.csv");
 
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
        List<User> users = CsvHandler.Read<User>("Model/UserDB.csv");
 
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
        List<User> users = CsvHandler.Read<User>("Model/UserDB.csv");

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
}
