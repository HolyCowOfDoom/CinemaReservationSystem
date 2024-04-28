using System.Text;
public class Graphics
{


    public static string auditorium1 = @"A     U U U U U U U U     
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

    public static string auditorium2 = @"A       U U U   U U U U U U   U U U       
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

    public static string auditorium3 = @"A                 U U U   U U U U U U U U   U U U                 
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


    public static void DrawLegend(string Blueprice, string Redprice, string Yellowprice)
    {
        Console.WriteLine("\nSEATS \u001b[35mU\u001b[0m ARE ALREADY RESERVED"  
            + ", \u001b[34mU\u001b[0m PRICE: " + Blueprice 
            + ", \u001b[33mU\u001b[0m PRICE: " + Yellowprice
            + ", \u001b[31mU\u001b[0m PRICE: " + Redprice);
    }

    public static List<string> AuditoriumView(Screening screening, string auditorium)
    {
        //init of auditorium parameters
        int indexPos = 2;
        int width = GetWidth(auditorium);
        int maxindex = GetMaxIndex(auditorium);
        int mintopindex = maxindex - width;
        string auditoriumScreen = GetScreen(GetWidth(auditorium));

        //init lists of necessary info
        List<string> selectedseats = new List<string>();
        List<int> listreservedindex = new List<int>();
        List<int> reservedbyotheruser = new List<int>();


        //alphabet dict creation for seatmapping
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

        //get all the seats and info
        Dictionary<int, (string, bool)> seatIDcolor = new Dictionary<int, (string, bool)>();

        foreach (Seat seat in screening.AssignedAuditorium.Seats)
        {
            seatIDcolor.Add(Convert.ToInt32(seat.ID) - GetAuditoriumOffset(Int32.Parse(screening.AssignedAuditorium.ID)), (seat.Color, seat.IsReserved));
        }

        //add seats to reservedbyotheruser list
        for (int i = 0; i < auditorium.Length; i++)
        {
            if (auditorium[i] == 'U')
            {
                (_, bool reserved) = seatIDcolor[GetSeatNumberFromIndex(auditorium, i, true)];
                if (reserved) reservedbyotheruser.Add(i);
            }
        }
        
        Console.CursorVisible = false;

        //generate coloredauditorium for the first time
        string coloredAuditorium = GetColoredAuditorium(auditorium, indexPos, listreservedindex, seatIDcolor);

        while (true)
        {

            //displaying all necessary info on screen
            Console.WriteLine("\b \b");
            Console.Clear();
            
            Console.WriteLine("Use arrow keys to move the cursor (\u001b[38;5;147mX\u001b[0m), (\u001b[32mSPACEBAR\u001b[0m) to select seat and \u001b[31mESC\u001b[0m to return, max reservable seats: 40");
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

            //user input handling
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

            //reservations check
            else if (key.Key == ConsoleKey.Spacebar)
            {
                if (!selectedseats.Contains($"{numbertoletter[GetRowFromIndex(auditorium, indexPos) + 1]}{GetSeatNumberFromIndex(auditorium, indexPos)}") &&
                    IsSeat(auditorium, indexPos) && !listreservedindex.Contains(indexPos) && !reservedbyotheruser.Contains(indexPos) && selectedseats.Count < 40)
                {
                    selectedseats.Add($"{numbertoletter[GetRowFromIndex(auditorium, indexPos) + 1]}{GetSeatNumberFromIndex(auditorium, indexPos)}");
                    listreservedindex.Add(indexPos);
                }
                else if (selectedseats.Contains($"{numbertoletter[GetRowFromIndex(auditorium, indexPos) + 1]}{GetSeatNumberFromIndex(auditorium, indexPos)}") &&
                         IsSeat(auditorium, indexPos))
                {
                    selectedseats.Remove($"{numbertoletter[GetRowFromIndex(auditorium, indexPos) + 1]}{GetSeatNumberFromIndex(auditorium, indexPos)}");
                    listreservedindex.Remove(indexPos);
                }

            }

            else if (key.Key == ConsoleKey.Enter)
            {
                List<string> reservedseatIDs = new List<string>();
                Console.ForegroundColor = ConsoleColor.Cyan;
                char confirm = Helper.ReadInput((char c) => c == 'y' || c == 'n', "Confirm reservation", "Are you happy with you reservations? Y/N");
                Console.ForegroundColor = ConsoleColor.Gray;
                if (Convert.ToString(confirm).ToLower() == "n") continue;
                else
                {
                    foreach (int index in listreservedindex)
                    {
                        screening.ReserveSeat(Convert.ToString(GetSeatNumberFromIndex(auditorium, index, true) + GetAuditoriumOffset(Int32.Parse(screening.AssignedAuditorium.ID))));
                        reservedseatIDs.Add(Convert.ToString(GetSeatNumberFromIndex(auditorium, index, true) + GetAuditoriumOffset(Int32.Parse(screening.AssignedAuditorium.ID))));
                    }
                    return reservedseatIDs;
                }
            }

            //delete selected reserved seats
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (listreservedindex.Count > 0)
                {
                    listreservedindex.RemoveAt(listreservedindex.Count - 1);
                    selectedseats.RemoveAt(selectedseats.Count - 1);
                }
            }

            //return to previous menu
            else if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }

            //update auditorium visual
            coloredAuditorium = GetColoredAuditorium(auditorium, indexPos, listreservedindex, seatIDcolor);
        }
    }

    public static string GetColoredAuditorium(string auditorium, int indexPos, List<int> listreservedindex, Dictionary<int, (string, bool)> seatIDcolor)
    {
        StringBuilder coloredAuditorium = new StringBuilder();

        for (int i = 0; i < auditorium.Length; i++)
        {
            if (i == indexPos)
                coloredAuditorium.Append("\u001b[38;5;147mX");
            else if (auditorium[i] == 'U' && listreservedindex.Contains(i))
                coloredAuditorium.Append("\u001b[32mU");
            else if (auditorium[i] == 'U' && !listreservedindex.Contains(i) && seatIDcolor.ContainsKey(GetSeatNumberFromIndex(auditorium, i, true)))
            {
                (string color, bool reserved) = seatIDcolor[GetSeatNumberFromIndex(auditorium, i, true)];
                if (reserved == true) coloredAuditorium.Append("\u001b[35mU");
                else if (color == "Yellow") coloredAuditorium.Append("\u001b[33mU");
                else if (color == "Blue") coloredAuditorium.Append("\u001b[34mU");
                else if (color == "Red") coloredAuditorium.Append("\u001b[31mU");
            }
            else coloredAuditorium.Append("\u001b[0m" + auditorium[i]);
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

    public static int GetSeatNumberFromIndex(string auditorium, int index, bool database = false)
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

    public static int GetWidth(string auditorium)
    {
        for (int i = 0; i <= auditorium.Length; i++)
        {
            if (auditorium[i] == '\n') return i - 1;
        }
        return 0;
    }

    public static int GetMaxIndex(string auditorium)
    {
        return auditorium.Length - 2;
    }

    public static string GetScreen(int width)
    {
        string screen = "";

        int screenPadding = (width ) / 40;
        int screenLength = width - screenPadding - 2;

        if (screenLength < 30) screenLength--;

        screen += " ".PadLeft(screenPadding) + "_" + new string('_', screenLength) + "_\n";
        screen += " ".PadLeft(screenPadding) + "\\" + new string('_', screenLength) + "/\n";

        return screen;
    }

    public static int GetAuditoriumOffset(int id)
    {
        switch (id)
        {
            case 1:
                return 0;
            case 2: 
                return 150;
            case 3:
                return 450;
            default:
                return 0;
        }
    }
}