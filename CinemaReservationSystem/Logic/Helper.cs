using System.Globalization;
public interface ObjectHasID
{
    string ID { get; }
}


public class Helper
{
    //Can be used to filter char or multiple chars and can display menu with header and user options in a box graphic
    

   


    public static (string, string) Catchinput(int maxLength, string type,
        string Case, string username = "", string birthdate = "", string email = "", string password = "")
    {
        string input = GetInputString(type, username, birthdate, email, password);
        string taboresc = string.Empty;
        bool spacebar = false, validated = false;

        DrawUI(Case, username, birthdate, email, password, input, spacebar);

        while (true)
        {
            UpdateUI(Case, type, input, username, birthdate, email, password, spacebar);
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
                        Graphics.DrawRegister(type, input, birthdate, email, password);
                        break;
                    case "birthdate":
                        Graphics.DrawRegister(type, username, input, email, password);
                        break;
                    case "email":
                        Graphics.DrawRegister(type, username, birthdate, input, password);
                        break;
                    case "password":
                        Graphics.DrawRegister(type, username, birthdate, email, input);
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
                Graphics.DrawRegister("username", username, birthdate, email, password);
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

    // everything about getting the valid input.
   
   public static Movie? GetMovieByID(string screeningID)
    {
        List<Movie> MovieList = JsonHandler.Read<Movie>("Data/MovieDB.json");
        foreach(Movie movie in MovieList)
        {
            if (movie.ScreeningIDs.Contains(screeningID)) return movie;
        }
        return null;
    }

    public static Screening? GetScreeningByID(string screeningID)
    {
        List<Screening> ScreeningList = JsonHandler.Read<Screening>("Data/ScreeningDB.json");
        foreach(Screening screening in ScreeningList)
        {
            if (screening.ID == screeningID) return screening;
        }
        return null;
    }
    
}
