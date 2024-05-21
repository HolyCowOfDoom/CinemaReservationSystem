using System.Globalization;
using System.Security.Cryptography.X509Certificates;

public class InterfaceController
{
    public static void ViewMovies(){
        UserInterfaceController.ViewMovies();
    }

    public static void LogIn(bool reservemovie=false, Movie movie = null)
    {
        string username = string.Empty, password = string.Empty;
        string EscorTaborshift;
        User? user = null;
        Console.CursorVisible = false;
        Helper.ConsoleClear();

        string currentField = "username";

        while (true)
        {
            switch (currentField)
            {
                case "username":
                    (username, EscorTaborshift) = Helper.Catchinput(27, "username", "login", username);
                    switch (EscorTaborshift)
                    {
                        case "ESC":
                            Helper.ConsoleClear();
                            Interface.GeneralMenu();
                            break;
                        case "TAB":
                            RegisterUser();
                            break;
                        default:
                            break;
                    }
                    if (!string.IsNullOrEmpty(username))
                    {
                        user = UserDataController.GetUserWithValue("Name", username);
                        currentField = "password";
                    }
                    break;
                case "password":
                    (password, EscorTaborshift) = Helper.Catchinput(27, "password", "loginpassword", username, "", "", password);
                    switch (EscorTaborshift)
                    {
                        case "ESC":
                            Helper.ConsoleClear();
                            Interface.GeneralMenu();
                            break;
                        case "TAB":
                            RegisterUser();
                            break;
                        default:
                            break;
                    }
                    if (!string.IsNullOrEmpty(password))
                    {
                        if (string.Equals(password, user.Password))
                        {
                            Helper.ConsoleClear();
                            if (user.Admin is true) AdminInterface.GeneralMenu(user.ID);
                            else
                            {
                                if (reservemovie is true) UserInterfaceController.ScreeningSelect(movie ,user.ID);
                                UserInterface.GeneralMenu(user.ID);
                            }
                            break;
                        }
                    }
                    break;
            }  
        }
    }


    public static void RegisterUser(string? username = null, bool admin = false, string? id = null)
    {
        if (username is null) username = string.Empty;
        string birthDate = string.Empty, email = string.Empty, password = string.Empty, escapetab = string.Empty;
        bool registercomplete = false;

        Console.CursorVisible = false;
        Helper.ConsoleClear();

        // Start with the username input
        string currentField = "username";

        while (!registercomplete)
        {
            switch (currentField)
            {
                case "username":
                    (username, escapetab) = Helper.Catchinput(27, "username", "register", username, birthDate, email, password);
                    currentField = HandleRegisterinput(currentField, username, escapetab, "birthdate", lastfield: null);
                    break;
                case "birthdate":
                    (birthDate, escapetab) = Helper.Catchinput(10, "birthdate", "register", username, birthDate, email, password);
                    currentField = HandleRegisterinput(currentField, birthDate, escapetab, "email", "username");
                    break;
                case "email":
                    (email, escapetab) = Helper.Catchinput(30, "email", "register", username, birthDate, email, password);
                    currentField = HandleRegisterinput(currentField, email, escapetab, "password", "birthdate");
                    break;
                case "password":
                    (password, escapetab) = Helper.Catchinput(27, "password", "register", username, birthDate, email, password);
                    currentField = HandleRegisterinput(currentField, password, escapetab, nextfield: null, "email");
                    if (string.Equals(currentField, "validated")) registercomplete = true;
                    break;
            }
        }
        User? user;
        if (admin is true) user = new User(username, birthDate, email, password, admin : true);
        else user = new User(username, birthDate, email, password);
        Helper.ConsoleClear();
        if (user.Admin is true && id is not null) AdminInterfaceController.HandleAdminSwitch(id, user.ID);
        else UserInterface.GeneralMenu(user.ID);
    }

    public static string HandleRegisterinput(string currentField, string userinfo, string escapetab, string nextfield, string lastfield)
    {
        switch (escapetab)
        {
            case "ESC":
                Helper.ConsoleClear();
                Interface.GeneralMenu();
                break;
            case "TAB":
                if (nextfield is not null) currentField = nextfield;
                return currentField;
            case "SHIFTTAB":
                if (lastfield is not null) currentField = lastfield;
                return currentField;
        }
        if (!string.IsNullOrEmpty(userinfo))
        {
            if (string.Equals(currentField, "password"))
            {
                char yorn = Helper.ReadInput((char c) => c == 'y' || c == 'n', "Complete registration",
                                             "Are you happy to register with current details? Y/N");
                if (yorn == 'y')
                {
                    return "validated";
                }
            }
            currentField = nextfield;
        }
        return currentField;
    }

}