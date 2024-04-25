using System.Globalization;
using System.Security.Cryptography.X509Certificates;

public class InterfaceController
{
    public static void ViewMovies(){
        List<Movie> Movies = JsonHandler.Read<Movie>("Model/MovieDB.json");
        Console.WriteLine("┌────┬──────────────────────────────────────────┬─────────────┬─────────────┬──────────────────────────────────────────────────────────────┐");
        Console.WriteLine($"│ ID │ {"Title",-40} │ {"Age Rating",-11} │ {"Genre",-11} │ {"Description",-60} │");
        foreach (Movie movie in Movies)
        {
            Console.WriteLine($"│ {Movies.IndexOf(movie) + 1, -2} │ {movie.Title,-40} │ {movie.AgeRating,-11} │ {movie.Genre, -11} │ {movie.Description,-60} │");
        }
        Console.WriteLine("└────┴──────────────────────────────────────────┴─────────────┴─────────────┴──────────────────────────────────────────────────────────────┘");
        XToGoBack();
    }

    public static void LogIn()
    {
        string username;
        string password;
        string EscorTaborshift;
        User? user = null;

        username = string.Empty;
        password = string.Empty;
        Console.CursorVisible = false;
        Console.Write("\b \b");
        Console.Clear();

        string currentField = "username";

        while (true)
        {
            switch (currentField)
            {
                case "username":
                    (username, EscorTaborshift) = Helper.Catchinput(30, 1, 27, "username", "login", username);
                    if (EscorTaborshift == "ESC")
                    {
                        Console.WriteLine("\b\b");
                        Console.Clear();
                        Interface.GeneralMenu();
                        return;
                    }
                    else if (EscorTaborshift == "TAB") break;
                    else if (EscorTaborshift == "SHIFT") RegisterUser();
                    else if (!string.IsNullOrEmpty(username))
                    {
                        user = User.GetUserWithValue("Name", username);

                        if (user is null)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            char yorn = Helper.ReadInput((char c) => c == 'y' || c == 'n', "username not found", "Username could not be found. Register account? Y/N");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            if (yorn == 'y') RegisterUser();
                            else LogIn();
                        }
                        currentField = "password";
                    }
                    break;
                case "password":
                    (password, EscorTaborshift) = Helper.Catchinput(30, 1, 27, "password", "loginpassword", username, "", "", password);
                    if (EscorTaborshift == "ESC")
                    {
                        Console.WriteLine("\b\b");
                        Console.Clear();
                        Interface.GeneralMenu();
                        return;
                    }
                    else if (EscorTaborshift == "TAB")
                    {
                        currentField = "username";
                    }
                    else if (EscorTaborshift == "SHIFT") RegisterUser();
                    else if (!string.IsNullOrEmpty(password))
                    {
                        if (password == user.Password)
                        {
                            Console.WriteLine("\b\b");
                            Console.Clear();
                            if (user.ID.StartsWith("admin-")) AdminInterface.GeneralMenu(user.ID);
                            else UserInterface.GeneralMenu(user.ID);
                            break;
                        }
                    }
                    break;
            }  
        }
    }


    public static void RegisterUser()
    {
        string username = string.Empty;
        string birthDate = string.Empty;
        string email = string.Empty;
        string password = string.Empty;
        string escapetab = string.Empty;

        Console.CursorVisible = false;
        Console.WriteLine("\b\b");
        Console.Clear();

        // Start with the username input
        string currentField = "username";

        while (true)
        {
            switch (currentField)
            {
                case "username":
                    (username, escapetab) = Helper.Catchinput(30, 1, 27, "username", "register", username, birthDate, email, password);
                    if (escapetab == "ESC")
                    {
                        Console.WriteLine("\b\b");
                        Console.Clear();
                        Interface.GeneralMenu();
                        return;
                    }
                    else if (escapetab == "TAB")
                    {
                        break;
                    }
                    else if (!string.IsNullOrEmpty(username))
                    {
                        currentField = "birthdate";
                    }
                    break;

                case "birthdate":
                    (birthDate, escapetab) = Helper.Catchinput(30, 1, 10, "birthdate", "register", username, birthDate, email, password);
                    if (escapetab == "ESC")
                    {
                        Console.WriteLine("\b\b");
                        Console.Clear();
                        Interface.GeneralMenu();
                        return;
                    }
                    else if (escapetab == "TAB")
                    {
                        currentField = "username";
                    }
                    else if (!string.IsNullOrEmpty(birthDate))
                    {
                        currentField = "email";
                    }
                    break;

                case "email":
                    (email, escapetab) = Helper.Catchinput(30, 1, 30, "email", "register", username, birthDate, email, password);
                    if (escapetab == "ESC")
                    {
                        Console.WriteLine("\b\b");
                        Console.Clear();
                        Interface.GeneralMenu();
                        return;
                    }
                    else if (escapetab == "TAB")
                    {
                        currentField = "birthdate";
                    }
                    else if (!string.IsNullOrEmpty(email))
                    {
                        currentField = "password";
                    }
                    break;

                case "password":
                    (password, escapetab) = Helper.Catchinput(30, 1, 27, "password", "register", username, birthDate, email, password);
                    if (escapetab == "ESC")
                    {
                        Console.WriteLine("\b\b");
                        Console.Clear();
                        Interface.GeneralMenu();
                        return;
                    }
                    else if (escapetab == "TAB")
                    {
                        currentField = "email";
                    }
                    else if (!string.IsNullOrEmpty(password))
                    {
                        // Successfully completed registration
                        Graphics.DrawRegister(username, birthDate, email, password);
                        char yorn = Helper.ReadInput((char c) => c == 'y' || c == 'n', "Complete registration", "Are you happy to register with current details? Y/N");
                        if (yorn == 'y')
                        {
                            User user = new User(username, birthDate, email, password);
                            Console.WriteLine("\b\b");
                            Console.Clear();
                            if (user.ID.StartsWith("admin-")) AdminInterface.GeneralMenu(user.ID);
                            else UserInterface.GeneralMenu(user.ID);

                        }
                    }
                    break;
            }
        }
    }

    public static void XToGoBack(){
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            Interface.GeneralMenu();
        }
    }

    public static void XToGoBack(string id){
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            if (id.StartsWith("admin-")) AdminInterface.GeneralMenu(id);
            else UserInterface.GeneralMenu(id);
        }
    }
}