using System.Text;
public static class Graphics
{

    private static readonly string auditorium1 = @"A     U U U U U U U U     
B   U U U U U U U U U U   
C   U U U U U U U U U U   
D U U U U U U U U U U U U 
E U U U U U U U U U U U U 
F U U U U U U U U U U U U 
G U U U U U U U U U U U U 
H U U U U U U U U U U U U 
I U U U U U U U U U U U U 
J U U U U U U U U U U U U 
K U U U U U U U U U U U U 
L   U U U U U U U U U U   
M     U U U U U U U U     
N     U U U U U U U U     
";

    private static readonly string auditorium2 = @"A       U U U   U U U U U U   U U U       
B       U U U   U U U U U U   U U U       
C     U U U U   U U U U U U   U U U U     
D     U U U U   U U U U U U   U U U U     
E     U U U U   U U U U U U   U U U U     
F   U U U U U   U U U U U U   U U U U U   
G   U U U U U   U U U U U U   U U U U U   
H   U U U U U   U U U U U U   U U U U U   
I U U U U U U   U U U U U U   U U U U U U 
J U U U U U U   U U U U U U   U U U U U U 
K U U U U U U   U U U U U U   U U U U U U 
L U U U U U U   U U U U U U   U U U U U U 
M U U U U U U   U U U U U U   U U U U U U 
N   U U U U U   U U U U U U   U U U U U   
O   U U U U U   U U U U U U   U U U U U   
P   U U U U U   U U U U U U   U U U U U   
Q   U U U U U   U U U U U U   U U U U U   
R   U U U U U   U U U U U U   U U U U U   
S   U U U U U   U U U U U U   U U U U U   
";

    private static readonly string auditorium3 = @"A                 U U U   U U U U U U U U   U U U                 
B               U U U U   U U U U U U U U   U U U U               
C           U U U U U U   U U U U U U U U   U U U U U U           
D       U U U U U U U U   U U U U U U U U   U U U U U U U U       
E       U U U U U U U U   U U U U U U U U   U U U U U U U U       
F     U U U U U U U U U   U U U U U U U U   U U U U U U U U U     
G     U U U U U U U U U   U U U U U U U U   U U U U U U U U U     
H   U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U   
I U U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U U 
                                                                  
J U U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U U 
K U U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U U 
L U U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U U 
M U U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U U 
N   U U U U U U U U U U   U U U U U U U U   U U U U U U U U U U   
                                                                  
O     U U U U U U U U U   U U U U U U U U   U U U U U U U U U     
P       U U U U U U U U   U U U U U U U U   U U U U U U U U       
Q       U U U U U U U U   U U U U U U U U   U U U U U U U U       
R       U U U U U U U U   U U U U U U U U   U U U U U U U U       
S       U U U U U U U U   U U U U U U U U   U U U U U U U U       
T         U U U U U U U   U U U U U U U U   U U U U U U U         
";

    public static readonly string cinemaVintage = @"
                        |
                          ___________I____________
                         ( _____________________ ()
                       _.-'|                    ||
                   _.-'   ||       VIDEO        ||
  ______       _.-'       ||                    ||
 |      |_ _.-'           ||      KILLED        ||
 |      |_|_              ||                    ||
 |______|   `-._          ||     THE RADIO      ||
    /\          `-._      ||                    ||
   /  \             `-._  ||       STAR         ||
  /    \                `-.I____________________||
 /      \                 ------------------------
 /________\___________________/________________\______";

    public static readonly string cinemacustom = @" _____________________________
|                             |
|                             |
|                             |
|                             |
|                             |
|                             |
`-----------------------------'

       (~~) (~~) (~~) (~~)
       _)(___)(___)(___)(_
    (~~) (~~) (~~) (~~) (~~)
    _)(___)(___)(___)(___)(_
 (~~) (~~) (~~) (~~) (~~) (~~)
 _)(___)(___)(___)(___)(___)(_
|    |    |    |    |    |    |
|    |    |    |    |    |    |
||~~~~~||~~~~~||~~~~~||~~~~~~|| 
`'     `'     `'     `'      `'";
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
        Console.SetCursorPosition((Console.WindowWidth - 50) / 2, 10);
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
        int usernamerightpadding, birthdaterightpadding, emailrightpadding, passwordrightpadding;
        int usernameleftpadding, birthdateleftpadding, emailleftpadding, passwordleftpadding;
        (username, usernamerightpadding, usernameleftpadding) = GetInstructionString(username, "username");
        (birthdate, birthdaterightpadding, birthdateleftpadding) = GetInstructionString(birthdate, "birthdate");
        (email, emailrightpadding, emailleftpadding) = GetInstructionString(email, "email");
        (password, passwordrightpadding, passwordleftpadding) = GetInstructionString(password, "password");

        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.Blue;
        Helper.WriteInCenter("Register or press ESC to go to back to menu.");
        Console.ForegroundColor = ConsoleColor.Gray;
        
        Console.SetCursorPosition((Console.WindowWidth - 50) / 2, 8);
        Helper.WriteInCenter("╔═════════════════════════════════════╗");
        Helper.WriteInCenter("║               REGISTER              ║");
        Helper.WriteInCenter("╠═════════════════════════════════════╣");
        Helper.WriteInCenter("║USERNAME: ".PadLeft(usernameleftpadding) + username.PadRight(usernamerightpadding) + "║");
        Helper.WriteInCenter("╠═════════════════════════════════════╣");
        Helper.WriteInCenter("║BIRTHDATE: ".PadLeft(birthdateleftpadding) + birthdate.PadRight(birthdaterightpadding) + "║");
        Helper.WriteInCenter("╠═════════════════════════════════════╣");
        Helper.WriteInCenter("║EMAIL: ".PadLeft(emailleftpadding) + email.PadRight(emailrightpadding) + "║");
        Helper.WriteInCenter("╠═════════════════════════════════════╣");
        Helper.WriteInCenter("║PASSWORD: ".PadLeft(passwordleftpadding) + password.PadRight(passwordrightpadding) + "║");
        Helper.WriteInCenter("╚═════════════════════════════════════╝");
    }
    public static (string, int, int) GetInstructionString(string registerinfo, string type)
    {
        if (string.IsNullOrEmpty(registerinfo))
        {
            return (type) switch
            {

                "username" => (Colorize("username > 3 chars", "gray"), 36, 20),
                "birthdate" => (Colorize("format: dd-MM-yyyy", "gray"), 35, 21),
                "email" => (Colorize("email has '@' and '.'", "gray"), 39, 17),
                "password" => (Colorize("password has digit", "gray"), 36, 20),
                _ => throw new ArgumentException(nameof(registerinfo), $"Could not find type for: {registerinfo}")
            };
        }

        return (type) switch
        {

        "username" => (registerinfo, 27, 0),
        "birthdate" => (registerinfo, 26, 0),
        "email" => (registerinfo, 30, 0),
        "password" => (registerinfo, 27, 0),
        _ => throw new ArgumentException(nameof(registerinfo), $"Could not find type for: {registerinfo}")
        };
    }

    private static void DrawLegend(string Blueprice, string Redprice, string Yellowprice)
    {
        Console.WriteLine($"\nSEATS {Colorize("U", "magenta")} ARE ALREADY RESERVED"
            + $", {Colorize("U", "blue")} PRICE: " + Blueprice
            + $", {Colorize("U", "yellow")} PRICE: " + Yellowprice
            + $", {Colorize("U", "red")} PRICE: " + Redprice);
    }

    public static IEnumerable<string> AuditoriumView(Screening screening, User user)
    {
        //init all data needed for display and backend
        int indexPos, width, maxindex, mintopindex;
        string auditorium, auditoriumScreen;
        bool reservedDone = false;
        List<string> selectedseats;
        List<string> reservedseatIDs = new List<string>();
        List<int> listreservedindex, reservedbyotheruser;
        Dictionary<int, char> numbertoletter;
        Dictionary<int, (string, bool)> seatIDcolor;
        
        InitAuditorium(screening, user, out indexPos, out width, out maxindex, out mintopindex, out auditoriumScreen, out auditorium,
                       out selectedseats, out listreservedindex, out reservedbyotheruser, out numbertoletter, out seatIDcolor);

        Console.CursorVisible = false;

        //generate coloredauditorium for the first time
        string coloredAuditorium = GetColoredAuditorium(auditorium, indexPos, reservedbyotheruser, listreservedindex, seatIDcolor);

        while (!reservedDone)
        {
            DrawAuditoriumInfo(auditorium, auditoriumScreen, coloredAuditorium, indexPos, selectedseats, numbertoletter);

            //user input handling
            HandleUserInput(screening, user, auditorium, width, maxindex, mintopindex, ref indexPos, selectedseats, listreservedindex, 
                            numbertoletter, reservedbyotheruser, reservedseatIDs, reservedDone);

            //update auditorium visual
            coloredAuditorium = GetColoredAuditorium(auditorium, indexPos, reservedbyotheruser, listreservedindex, seatIDcolor);
        }
        return reservedseatIDs;
    }

    private static void HandleUserInput(Screening screening, User user, string auditorium, int width, int maxindex, int mintopindex, ref int indexPos,
                                        List<string> selectedseats, List<int> listreservedindex, Dictionary<int, char> numbertoletter,
                                        List<int> reservedbyotheruser, List<string> reservedseatIDs, bool reservedDone)
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
                HandleEnterKeyPress(screening, auditorium, listreservedindex, reservedseatIDs, reservedDone);
                break;
            case ConsoleKey.Backspace:
                HandleBackspaceKeyPress(listreservedindex, selectedseats);
                break;
            case ConsoleKey.Escape:
                HandleEscapeKeyPress(user);
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

    private static void HandleEnterKeyPress(Screening screening, string auditorium, List<int> listreservedindex,
                                            List<string> reservedseatIDs, bool reservedDone)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        char confirm = Helper.ReadInput((char c) => c == 'y' || c == 'n', "Confirm reservation", "Are you happy with your reservations? Y/N");
        Console.ForegroundColor = ConsoleColor.Gray;
        if (string.Equals(Convert.ToString(confirm), "n")) return;
        foreach (int index in listreservedindex)
        {
            screening.ReserveSeat(Convert.ToString(GetSeatNumberFromIndex(auditorium, index, database: true) + GetAuditoriumOffset(Int32.Parse(screening.AssignedAuditorium.ID))));
            reservedseatIDs.Add(Convert.ToString(GetSeatNumberFromIndex(auditorium, index, database: true) + GetAuditoriumOffset(Int32.Parse(screening.AssignedAuditorium.ID))));
        }
        reservedDone = true;
    }

    private static void HandleBackspaceKeyPress(List<int> listreservedindex, List<string> selectedseats)
    {
        if (listreservedindex.Count > 0)
        {
            listreservedindex.RemoveAt(listreservedindex.Count - 1);
            selectedseats.RemoveAt(selectedseats.Count - 1);
        }
    }

    private static void HandleEscapeKeyPress(User user)
    {
        Console.Write("\f\u001bc\x1b[3J");
        if (user.Admin is true) AdminInterface.GeneralMenu(user.ID);
        else UserInterface.GeneralMenu(user.ID);
    }

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

            _ => _ => throw new ArgumentException(nameof(indexPos), $"Could not find new position for: {indexPos}")
        };
    }

    private static void InitAuditorium(Screening screening, User user, out int indexPos, out int width, out int maxindex,
        out int mintopindex, out string auditoriumScreen, out string auditorium, out List<string> selectedseats, 
        out List<int> listreservedindex, out List<int> reservedbyotheruser, out Dictionary<int, char> numbertoletter, 
        out Dictionary<int, (string, bool)> seatIDcolor)
    {
        auditorium = screening.AssignedAuditorium.ID switch
        {
            "1" => auditorium1,
            "2" => auditorium2,
            "3" => auditorium3,
            _ => ""
        };
        //init of auditorium parameters
        indexPos = 2;
        width = GetWidth(auditorium);
        maxindex = GetMaxIndex(auditorium);
        mintopindex = maxindex - width;
        auditoriumScreen = GetScreen(GetWidth(auditorium));
        selectedseats = new List<string>();
        listreservedindex = new List<int>();
        reservedbyotheruser = new List<int>();
        numbertoletter = new Dictionary<int, char>();
        seatIDcolor = new Dictionary<int, (string, bool)>();

        //alphabet dict creation for seatmapping
        List<char> alphabet = new List<char>();
        for (char c = 'A'; c <= 'Z'; c++)
        {
            alphabet.Add(c);
        }
        for (int i = 0; i < alphabet.Count; i++)
        {
            numbertoletter.Add(i + 1, alphabet[i]);
        }
        //get all the seats and info
        foreach (Seat seat in screening.AssignedAuditorium.Seats)
        {
            seatIDcolor.Add(Convert.ToInt32(seat.ID) - GetAuditoriumOffset(Int32.Parse(screening.AssignedAuditorium.ID)), (seat.Color, seat.IsReserved));
        }
        //get user specific seats and add them if they are present from last session
        foreach (var item in user.Reservations)
        {
            if (string.Equals(item.ScreeningID, screening.ID))
            {
                foreach (string seatid in item.SeatIDs)
                {
                    int offsetseatid = GetIndexFromSeat(auditorium, Int32.Parse(seatid) - GetAuditoriumOffset(Int32.Parse(screening.AssignedAuditorium.ID)));
                    listreservedindex.Add(offsetseatid);
                    selectedseats.Add($"{numbertoletter[GetRowFromIndex(auditorium, offsetseatid) + 1]}{GetSeatNumberFromIndex(auditorium, offsetseatid)}");
                }
            }
        }
        //add seats to reservedbyotheruser list
        for (int i = 0; i < auditorium.Length; i++)
        {
            if (auditorium[i] == 'U')
            {
                (_, bool reserved) = seatIDcolor[GetSeatNumberFromIndex(auditorium, i, database: true)];
                if (reserved && !listreservedindex.Contains(i)) reservedbyotheruser.Add(i);
            }
        }
    }
    

    private static void DrawAuditoriumInfo(string auditorium, string auditoriumScreen, string coloredAuditorium, int indexPos, IEnumerable<string> selectedseats, IDictionary<int, char> numbertoletter)
    {
        Console.WriteLine("\b \b");
        Console.Clear();

        Console.WriteLine($"Use arrow keys to move the cursor ({Colorize("X", "lavender")}), ({Colorize("SPACEBAR", "green")}) to select seat and {Colorize("ESC", "red")} to return, max reservable seats: 40");
        if (IsSeat(auditorium, indexPos)) Console.WriteLine($"Seat: {numbertoletter[GetRowFromIndex(auditorium, indexPos) + 1]}{GetSeatNumberFromIndex(auditorium, indexPos)}");
        else Console.WriteLine();

        Console.Write("Selected seats: ");
        foreach (string seat in selectedseats)
        {
            Console.Write($"{seat} ");
        }
        Console.WriteLine();
        Console.WriteLine(auditoriumScreen);


        Console.Write(coloredAuditorium);
        DrawLegend("10", "15", "12");
    }

    private static string GetColoredAuditorium(string auditorium, int indexPos, IEnumerable<int> reservedbyotheruser, IEnumerable<int> listreservedindex, IDictionary<int, (string, bool)> seatIDcolor)
    {
        StringBuilder coloredAuditorium = new StringBuilder();
        int auditoriumLength = auditorium.Length;

        for (int i = 0; i < auditoriumLength; i++)
        {
            coloredAuditorium.Append(auditorium[i] switch
            {
                _ when i == indexPos => Colorize("X", "lavender"),
                'U' when listreservedindex.Contains(i) => Colorize("U", "green"),
                'U' when !listreservedindex.Contains(i) &&
                         seatIDcolor.ContainsKey(GetSeatNumberFromIndex(auditorium, i, true)) =>
                         GetColorizedUnchangingU(auditorium, i, seatIDcolor, reservedbyotheruser),
                _ => auditorium[i].ToString()
            });
        }
        return coloredAuditorium.ToString();
    }
    private static string GetColorizedUnchangingU(string auditorium, int i, IDictionary<int, (string, bool)> seatIDcolor, IEnumerable<int> reservedbyotheruser)
    {
        (string color, bool reserved) = seatIDcolor[GetSeatNumberFromIndex(auditorium, i, true)];
        return (reserved, reservedbyotheruser.Contains(i), color) switch
        {
            (true, true, _) => Colorize("U", "magenta"),
            (_, _, "Yellow") => Colorize("U", "yellow"),
            (_, _, "Blue") => Colorize("U", "blue"),
            (_, _, "Red") => Colorize("U", "red"),
            _ => throw new ArgumentException(nameof(i), $"Could not find color for seat: {GetSeatNumberFromIndex(auditorium, i, true)}")
        };
    }
    public static string Colorize(string character, string color)
    {
        return color.ToLower() switch
        {
            "black" => $"\u001b[30m{character}\u001b[0m",
            "red" => $"\u001b[31m{character}\u001b[0m",
            "green" => $"\u001b[32m{character}\u001b[0m",
            "yellow" => $"\u001b[33m{character}\u001b[0m",
            "blue" => $"\u001b[34m{character}\u001b[0m",
            "magenta" => $"\u001b[35m{character}\u001b[0m",
            "cyan" => $"\u001b[36m{character}\u001b[0m",
            "white" => $"\u001b[37m{character}\u001b[0m",
            "lavender" => $"\u001b[38;5;147m{character}\u001b[0m",
            "gray" => $"\u001b[90m{character}\u001b[0m",
            _ => throw new ArgumentException(nameof(color), $"Invalid color: {color}")
        };
    }

    private static bool IsSeat(string auditorium, int index)
    {
        return auditorium[index] == 'U';
    }

    private static int GetIndexFromSeat(string auditorium, int seatnr)
    {
        int seatNumber = 0;

        for (int i = 0; i <= auditorium.Length; i++)
        {
            
            if (auditorium[i] == 'U')
            {
                seatNumber++;
            }
            if (seatNumber == seatnr) return i;
        }
        return 0;
    }

    private static int GetRowFromIndex(string auditorium, int index)
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

    private static int GetSeatNumberFromIndex(string auditorium, int index, bool database = false)
    {
        int seatNumber = 0;
        int seatDBnumber = 0;

        for (int i = 0; i <= index; i++)
        {
            if (auditorium[i] == '\n') seatNumber = 0;
            else if (auditorium[i] == 'U')
            {
                seatNumber++;
                seatDBnumber++;
            } 
        }
        if (database is true) return seatDBnumber;
        else return seatNumber;
    }

    private static int GetWidth(string auditorium)
    {
        for (int i = 0; i <= auditorium.Length; i++)
        {
            if (auditorium[i] == '\n') return i - 1;
        }
        return 0;
    }

    private static int GetMaxIndex(string auditorium)
    {
        return auditorium.Length - 2;
    }

    private static string GetScreen(int width)
    {
        string screen = "";

        int screenPadding = (width ) / 40;
        int screenLength = width - screenPadding - 2;

        if (screenLength < 30) screenLength--;

        screen += " ".PadLeft(screenPadding) + "_" + new string('_', screenLength) + "_\n";
        screen += " ".PadLeft(screenPadding) + "\\" + new string('_', screenLength) + "/\n";

        return screen;
    }

    private static int GetAuditoriumOffset(int id)
    {
        return id switch
        {
            1 => 0,
            2 => 150,
            3 => 450,
            _ => 0
        };
    }
}
