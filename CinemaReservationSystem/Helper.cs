public class Helper
    {
        public static char ReadInput(Func<char, bool> validationCriteria)
        {
            /* validate input examples
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

            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                input = keyInfo.KeyChar;

                // Check if the input satisfies the validation criteria
                isValidInput = validationCriteria(input);

                if (!isValidInput)
                {
                    WriteInCenter("Invalid input. Please try again.");
                }
                ClearLineDo();
            } while (!isValidInput);

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
    }
}