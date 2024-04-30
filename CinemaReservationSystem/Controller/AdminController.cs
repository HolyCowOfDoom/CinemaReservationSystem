using System.Globalization;

public class AdminController
{
    public static void CreateMovie(string id)
    {
        string title = Helper.GetValidInput("Please input movie title:", Helper.IsNotNull);
        string description = Helper.GetValidInput("Please add movie description:", Helper.IsNotNull);
        int ageRating = Convert.ToInt32(Helper.GetValidInput("Please add movie age rating:", Helper.IsNotNull));
        Console.WriteLine("Please add a genre to the movie:");
        string genre = Helper.GetValidInput("Please input movie title:", Helper.IsNotNull);

        Movie addedMovie = new Movie(title, ageRating, description, genre);

        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            AdminInterface.GeneralMenu(id);
        }
    }

    public static void AddScreening(Movie movie, string id)
    {
        Auditorium? screeningAud;
        do
        {
        string auditID = Helper.GetValidInput("Please input valid auditorium ID related to the screening:", Helper.IsNotNull);
        screeningAud = JsonHandler.Get<Auditorium>(auditID, "Model/AuditoriumDB.json");
        } while (screeningAud == null);

        string dateTimeString = Helper.GetValidInput("Please input screening date: <DD-MM-YYYY HH:MM>", Helper.IsValidDT);
        DateTime screeningDT = DateTime.ParseExact(dateTimeString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
        movie.AddScreening(screeningAud, screeningDT);

        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            AdminInterface.GeneralMenu(id);
        }
    }

    public static void RegisterAdmin(string fid)
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
                        AdminInterface.GeneralMenu(fid);
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
                        AdminInterface.GeneralMenu(fid);
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
                        AdminInterface.GeneralMenu(fid);
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
                        AdminInterface.GeneralMenu(fid);
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
                            User user = new User(username, birthDate, email, password, true);
                            Console.WriteLine("\b\b");
                            Console.Clear();
                            AdminInterface.GeneralMenu(fid);
                        }
                    }
                    break;
            }
        }
    }
    public static void LogOut(){
        Console.WriteLine("You have been succesfully logged out");
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            Interface.GeneralMenu();
        }
    }

    public static void AdminMovieInterface(Movie movie, string id)
    {
        Console.Clear();
        Console.WriteLine($"Title: {movie.Title,-40} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
        List<Screening> screenings = movie.GetAllMovieScreenings();
        foreach (Screening screening in screenings)
        {
            Console.WriteLine($"Date and Time: {screening.ScreeningDateTime, -40:dd-MM-yyyy HH:mm} | Auditorium: {screening.AssignedAuditorium.ID}");
        }
        string input = AdminInputMovie(id);
        if (input == "Select")
        {
            UserController.ScreeningSelect(screenings, id);
        }
        if (input == "Add")
        {
            AddScreening(movie, id);
        }

        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            AdminInterface.GeneralMenu(id);
        }
    }

    public static string AdminInputMovie(string id)
    {
        while (true) {
        char genreFilterInput = Helper.ReadInput((char c) => c == '1' || c == '2',
            "Movie Options", "1. Select Screening\n2. Add Screening");

        switch (genreFilterInput) {
            case '1':
                return "Select";
            case '2':
                return "Add";
            }
        }
    }

    public static void AdminScreeningInterface(Screening screening, string id)
    {
        Console.WriteLine("Reserve seats and display audit plan in this menu...");
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            AdminInterface.GeneralMenu(id);
        }
    }
}