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
    public static void WriteInCenter(string data)
    {
        foreach (var model in data.Split('\n'))
        {
            Console.SetCursorPosition((Console.WindowWidth - model.Length) / 2, Console.CursorTop);
            Console.WriteLine(model);
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
}
