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
        char[] yellow = { 'S', 'B', 'G', 'P', 'A', 'V', 'F', 'T', 'H', '1', '2', '3', '4', '5' };

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
                Console.ForegroundColor = System.ConsoleColor.White;
                Console.Write(c);
            }
        }
        Console.WriteLine();
    }

    public static string ReplaceAt(string input, int index, char newChar)
    {
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }

        public static void DrawLogin(string username = "", string password = "")
    {
        Console.SetCursorPosition(0, 0);
        WriteInCenter("╔═════════════════════════╗");
        WriteInCenter("║           LOGIN         ║");
        WriteInCenter("╠═════════════════════════╣");
        WriteInCenter("║USERNAME: " + username.PadRight(15) + "║");
        WriteInCenter("╠═════════════════════════╣");
        WriteInCenter("║PASSWORD: " + password.PadRight(15) + "║");
        WriteInCenter("╚═════════════════════════╝");
    }

    public static string CaptureInput(int left, int top)
    {
        DrawLogin();
        Console.SetCursorPosition(left, top);
        string input = string.Empty;
        int maxLength = 15;
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                // Return to previous menu
                return string.Empty;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                // Validate input against database
                Console.Clear();
                return input;
            }
            else if (key.Key == ConsoleKey.Tab)
            {
                if (!string.IsNullOrEmpty(input))
                {
                    Console.Clear();
                    input = CaptureInputPassword(left, top, input);
                    return input;
                }
            }
            else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input = input.Remove(input.Length - 1);
                Console.Write("\b \b");
                ClearLineDo();
            }
            else if (input.Length == maxLength)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                WriteInCenter("Max input length exceeded");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (char.IsLetterOrDigit(key.KeyChar) || char.IsSymbol(key.KeyChar) || char.IsPunctuation(key.KeyChar))
            {
                input += key.KeyChar;
                Console.Write(key.KeyChar);
            }

            // Clear the input area
            ClearInputArea();
            // Update and display the login form with current input
            DrawLogin(input);
        }
    }
    public static string CaptureInputPassword(int left, int top, string username)
    {
        DrawLogin(username);
        Console.SetCursorPosition(left, top);
        string input = string.Empty;
        int maxLength = 15;
        bool spacebar = false;
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return string.Empty;
            }
            else if (key.Key == ConsoleKey.Spacebar)
            {
                spacebar = !spacebar;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                // Validate input against database
                Console.Clear();
                return input;
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
                WriteInCenter("Max input length exceeded");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (char.IsLetterOrDigit(key.KeyChar) || char.IsSymbol(key.KeyChar) || char.IsPunctuation(key.KeyChar))
            {
                input += key.KeyChar;
            }

            ClearInputArea();
            if (spacebar)
            {
                DrawLogin(username, input);
            }
            else
            {
                DrawLogin(username, new string('*', input.Length));
            }
        }
    }


    public static void ClearInputArea()
    {
        Console.SetCursorPosition(0, 1);
        Console.Write(new string(' ', 100));
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
    public static bool IsValidUsername(string input) => !string.IsNullOrWhiteSpace(input) && input.Length >= 3 && input.Length < 21;
    public static bool IsValidEmail(string input) => !string.IsNullOrWhiteSpace(input) && input.Contains('@') && input.Contains('.') && input.Length < 31;

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
            return true;
        }
        else
        {
            return false;
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
