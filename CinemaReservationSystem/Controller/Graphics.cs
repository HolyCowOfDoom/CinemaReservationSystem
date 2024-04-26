public class Graphics
{

    public static string auditorium1 = @"    U U U U U U U U     
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

    public static string auditorium2 = @"  U U U U U   U U U U U U   U U U U U   
  U U U U U   U U U U U U   U U U U U   
  U U U U U   U U U U U U   U U U U U   
  U U U U U   U U U U U U   U U U U U   
  U U U U U   U U U U U U   U U U U U   
  U U U U U   U U U U U U   U U U U U   
U U U U U U   U U U U U U   U U U U U U 
U U U U U U   U U U U U U   U U U U U U 
U U U U U U   U U U U U U   U U U U U U 
U U U U U U   U U U U U U   U U U U U U 
U U U U U U   U U U U U U   U U U U U U 
  U U U U U   U U U U U U   U U U U U   
  U U U U U   U U U U U U   U U U U U   
  U U U U U   U U U U U U   U U U U U   
    U U U U   U U U U U U   U U U U     
    U U U U   U U U U U U   U U U U     
    U U U U   U U U U U U   U U U U     
      U U U   U U U U U U   U U U       
      U U U   U U U U U U   U U U       
";

    public static string auditorium3 = @"        U U U U U U U   U U U U U U U U   U U U U U U U         
      U U U U U U U U   U U U U U U U U   U U U U U U U U       
      U U U U U U U U   U U U U U U U U   U U U U U U U U       
      U U U U U U U U   U U U U U U U U   U U U U U U U U       
    U U U U U U U U U   U U U U U U U U   U U U U U U U U U     
                                                                
  U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U   
U U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U U 
U U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U U 
U U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U U 
U U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U U 
                                                                
U U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U U 
  U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U   
    U U U U U U U U U   U U U U U U U U   U U U U U U U U U     
    U U U U U U U U U   U U U U U U U U   U U U U U U U U U     
      U U U U U U U U   U U U U U U U U   U U U U U U U U       
      U U U U U U U U   U U U U U U U U   U U U U U U U U       
          U U U U U U   U U U U U U U U   U U U U U U           
              U U U U   U U U U U U U U   U U U U               
                U U U   U U U U U U U U   U U U                 
";
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
        int indexPos = 0;
        List<string> selectedseats = new List<string>();
        List<int> listreservedindex = new List<int>();

        List<char> alphabet = new List<char>();
        for (char c = 'A'; c <= 'Z'; c++)
        {
            alphabet.Add(c);
        }

        Dictionary<int, char> numbertoletter = new Dictionary<int, char>();
        for (int i = 0; i < alphabet.Count; i++)
        {
            numbertoletter.Add(i + 1, alphabet[i]);
        }

        Console.CursorVisible = false;

        while (true)
        {
            Console.WriteLine("\b \b");
            Console.Clear();

            Console.WriteLine("Use arrow keys to move the cursor (X), (SPACEBAR) to select seat and ESC to quit, max reservable seats: 40");
            if (IsSeat(auditorium, indexPos)) Console.WriteLine($"Seat: {numbertoletter[GetRowFromIndex(auditorium, indexPos) + 1]} {GetSeatNumberFromIndex(auditorium, indexPos)}");
            else Console.WriteLine();

            Console.Write("Selected seats: ");
            foreach (string seat in selectedseats)
            {
                Console.Write($"{seat} ");
            }
            Console.WriteLine();


            string coloredAuditorium = GetColoredAuditorium(auditorium, indexPos, listreservedindex);

            Console.Write(coloredAuditorium);

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.LeftArrow)
            {
                if (indexPos - 2 == -2) indexPos = maxindex;
                else if (indexPos - 2 >= 0 && indexPos - 2 <= maxindex)
                {
                    indexPos -= 2;
                }
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                if (indexPos + 2 == maxindex + 2) indexPos = 0;
                else if (indexPos + 2 >= 0 && indexPos + 2 <= maxindex)
                {
                    indexPos += 2;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                if (indexPos + width >= maxindex && indexPos + width <= maxindex + width + 2) indexPos -= mintopindex;
                else if (indexPos + width >= 0 && indexPos + width <= maxindex)
                {
                    indexPos += width + 2;
                }
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                if (indexPos - width >= -width && indexPos - width <= 0) indexPos += mintopindex;
                else if (indexPos - width >= 0 && indexPos - width <= maxindex)
                {
                    indexPos -= width + 2;
                }
            }


            else if (key.Key == ConsoleKey.Spacebar)
            {
                if (!selectedseats.Contains($"{numbertoletter[GetRowFromIndex(auditorium, indexPos) + 1]} {GetSeatNumberFromIndex(auditorium, indexPos)}") &&
                    IsSeat(auditorium, indexPos) && !listreservedindex.Contains(indexPos) && selectedseats.Count < 40)
                {
                    selectedseats.Add($"{numbertoletter[GetRowFromIndex(auditorium, indexPos) + 1]} {GetSeatNumberFromIndex(auditorium, indexPos)}");
                    listreservedindex.Add(indexPos);
                }
                else if (selectedseats.Contains($"{numbertoletter[GetRowFromIndex(auditorium, indexPos) + 1]} {GetSeatNumberFromIndex(auditorium, indexPos)}") &&
                    IsSeat(auditorium, indexPos))
                {
                    selectedseats.Remove($"{numbertoletter[GetRowFromIndex(auditorium, indexPos) + 1]} {GetSeatNumberFromIndex(auditorium, indexPos)}");
                    listreservedindex.Remove(indexPos);
                }

            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                Environment.Exit(0);
            }
        }
    }

    public static string GetColoredAuditorium(string auditorium, int indexPos, List<int> listreservedindex)
    {
        StringBuilder coloredAuditorium = new StringBuilder();

        for (int i = 0; i < auditorium.Length; i++)
        {
            if (i == indexPos)
                coloredAuditorium.Append("\u001b[31mX");
            else if (auditorium[i] == 'U' && listreservedindex.Contains(i))
                coloredAuditorium.Append("\u001b[32mU");
            else if (auditorium[i] == 'U' && !listreservedindex.Contains(i))
                coloredAuditorium.Append("\u001b[33mU");
            else
                coloredAuditorium.Append(auditorium[i]);
        }

        coloredAuditorium.Append("\u001b[0m");

        return coloredAuditorium.ToString();
    }

    public static bool IsSeat(string auditorium, int index)
    {
        return auditorium[index] == 'U';
    }

    public static int GetRowFromIndex(string auditorium, int index)
    {
        int row = 0;
        bool seat = false;

        for (int i = 0; i < index && i < auditorium.Length; i++)
        {
            if (auditorium[i] == 'U') seat = true;
            else if (auditorium[i] == '\n' && seat is true)
            {
                row++;
                seat = false;
            }
        }
        return row;
    }

    public static int GetSeatNumberFromIndex(string auditorium, int index)
    {
        int seatNumber = 0;

        for (int i = 0; i <= index; i ++)
        {
            if (auditorium[i] == '\n') seatNumber = 0;
            else if (auditorium[i] == 'U') seatNumber++;
        }
        return seatNumber;
    }
}