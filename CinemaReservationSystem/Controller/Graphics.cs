public class Graphics
{
    public static void BoxText(string text, string header = "")
    {

        string upperHeader = header.ToUpper();
        string upperText = text.ToUpper();

        if (string.IsNullOrEmpty(upperText)) return;
                
        string[] lines = upperText.Split('\n');

        int maxLineLength = lines.Max(line => line.Length);
        int totalWidth = Math.Max(maxLineLength, upperHeader.Length) + 4;
        int RightPadding = (totalWidth - maxLineLength) / 2;
        int headerLeftPadding = (totalWidth - upperHeader.Length) / 2 - 1;

        string topBorder = !string.IsNullOrEmpty(upperHeader) ? "╔" + new string('═', totalWidth) + "╗" : "";

        string middleBorder = "";
        if (!string.IsNullOrEmpty(upperHeader))
        {
            middleBorder = $"╔{new string('═', totalWidth)}╗\n║ {new string(' ', headerLeftPadding)}{upperHeader}{new string(' ', totalWidth - upperHeader.Length - headerLeftPadding - 2)} ║\n╠{new string('═', totalWidth)}╣";
        }
        else
        {
            middleBorder = "╠" + new string('═', totalWidth) + "╣";
        }

        string bottomBorder = "╚" + new string('═', totalWidth) + "╝";

        if (!string.IsNullOrEmpty(upperHeader))
        {
            Helper.WriteInCenter(middleBorder);
        }
        else
        {
            Helper.WriteInCenter("╔" + new string('═', totalWidth) + "╗");
        }

        foreach (string line in lines)
        {
            RightPadding = (totalWidth - line.Length - 2);
            Helper.WriteInCenter($"║ {line}{new string(' ', totalWidth - line.Length - RightPadding - 2)}{new string(' ', RightPadding)} ║");
        }

        Helper.WriteInCenter(bottomBorder);
    }

    public static void DrawLogin(string username = "", string password = "")
    {
        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.Blue;
        Helper.WriteInCenter("Login or press ESC to go back to menu, press TAB to register an account.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Helper.WriteInCenter("╔═════════════════════════════════════╗");
        Helper.WriteInCenter("║                LOGIN                ║");
        Helper.WriteInCenter("╠═════════════════════════════════════╣");
        Helper.WriteInCenter("║USERNAME: " + username.PadRight(27) + "║");
        Helper.WriteInCenter("╠═════════════════════════════════════╣");
        Helper.WriteInCenter("║PASSWORD: " + password.PadRight(27) + "║");
        Helper.WriteInCenter("╚═════════════════════════════════════╝");
    }

    public static void DrawRegister(string username = "", string birthdate = "", string email = "", string password = "")
    {
        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.Blue;
        Helper.WriteInCenter("Register, press TAB to go back a field or press ESC to go to back to menu.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Helper.WriteInCenter("╔═════════════════════════════════════╗");
        Helper.WriteInCenter("║               REGISTER              ║");
        Helper.WriteInCenter("╠═════════════════════════════════════╣");
        Helper.WriteInCenter("║USERNAME: " + username.PadRight(27) + "║");
        Helper.WriteInCenter("╠═════════════════════════════════════╣");
        Helper.WriteInCenter("║BIRTHDATE: " + birthdate.PadRight(26) + "║");
        Helper.WriteInCenter("╠═════════════════════════════════════╣");
        Helper.WriteInCenter("║EMAIL: " + email.PadRight(30) + "║");
        Helper.WriteInCenter("╠═════════════════════════════════════╣");
        Helper.WriteInCenter("║PASSWORD: " + password.PadRight(27) + "║");
        Helper.WriteInCenter("╚═════════════════════════════════════╝");
    }

    public static void AuditoriumView(string auditorium, int width, int maxindex, int mintopindex)
    {
        Console.CursorVisible = false;

        int indexPos = 0;
        var newmap = Helper.ReplaceAt(auditorium, indexPos, 'X');

        Console.WriteLine(indexPos);
        Helper.WriteColoredLetter(newmap);

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            Console.Write("\b \b");
            Console.Clear();

            if (key.Key == ConsoleKey.LeftArrow)
            {
                if (indexPos - 2 == - 2) indexPos = maxindex;
                else if (indexPos - 2 >= 0 && indexPos - 2 <= maxindex)
                {
                    indexPos -= 2;
                }
                Console.WriteLine(indexPos);

            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                if (indexPos + 2 == maxindex + 2) indexPos = 0;
                else if (indexPos + 2 >= 0 && indexPos + 2 <= maxindex)
                {
                    indexPos += 2;
                }
                Console.WriteLine(indexPos);
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                if (indexPos + width >= maxindex && indexPos + width <= maxindex + width + 2) indexPos -= mintopindex;
                else if (indexPos + width >= 0 && indexPos + width <= maxindex)
                {
                    indexPos += width + 2;
                }
                Console.WriteLine(indexPos);
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                if (indexPos - width >= - width && indexPos - width <= 0) indexPos += mintopindex;
                else if (indexPos - width >= 0 && indexPos - width <= maxindex)
                {
                    indexPos -= width + 2;
                }
                Console.WriteLine(indexPos);
            }
            newmap = Helper.ReplaceAt(auditorium, indexPos, 'X');
            Helper.WriteColoredLetter(newmap);
        }
    }

    public static void AudiVisual()
    {
        string Auditorium1 = @"
  U U U U U U U U
  U U U U U U U U U U
  U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U U U
  U U U U U U U U U U
  U U U U U U U U
  U U U U U U U U
";

        string Auditorium2 = @"
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U U  U U U U U U  U U U U U U
  U U U U U U  U U U U U U  U U U U U U
  U U U U U U  U U U U U U  U U U U U U
  U U U U U U  U U U U U U  U U U U U U
  U U U U U U  U U U U U U  U U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U U  U U U U U U  U U U U U
  U U U U  U U U U U U  U U U U
  U U U U  U U U U U U  U U U U
  U U U U  U U U U U U  U U U U
  U U U  U U U U U U  U U U
  U U U  U U U U U U  U U U
";

    string Auditorium3 = @"
  U U U U U U U  U U U U U U U U  U U U U U U U
  U U U U U U U U  U U U U U U U U  U U U U U U U U
  U U U U U U U U  U U U U U U U U  U U U U U U U U
  U U U U U U U U  U U U U U U U U  U U U U U U U U
  U U U U U U U U U  U U U U U U U U  U U U U U U U U U

  U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U
  U U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U U
  U U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U U
  U U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U U
  U U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U U

  U U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U U
  U U U U U U U U U U  U U U U U U U U  U U U U U U U U U U
  U U U U U U U U U  U U U U U U U U  U U U U U U U U U
  U U U U U U U U U  U U U U U U U U  U U U U U U U U U
  U U U U U U U U  U U U U U U U U  U U U U U U U U
  U U U U U U U U  U U U U U U U U  U U U U U U U U
  U U U U U U  U U U U U U U U  U U U U U U
  U U U U  U U U U U U U U  U U U U
  U U U  U U U U U U U U  U U U
";
    }
}