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
        string username = string.Empty, birthDate = string.Empty, email = string.Empty, password = string.Empty, escapetab = string.Empty;
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
                    currentField = InterfaceController.HandleRegisterinput(currentField, username, escapetab, "birthdate", lastfield: null);
                    break;
                case "birthdate":
                    (birthDate, escapetab) = Helper.Catchinput(10, "birthdate", "register", username, birthDate, email, password);
                    currentField = InterfaceController.HandleRegisterinput(currentField, birthDate, escapetab, "email", "username");
                    break;
                case "email":
                    (email, escapetab) = Helper.Catchinput(30, "email", "register", username, birthDate, email, password);
                    currentField = InterfaceController.HandleRegisterinput(currentField, email, escapetab, "password", "birthdate");
                    break;
                case "password":
                    (password, escapetab) = Helper.Catchinput(27, "password", "register", username, birthDate, email, password);
                    currentField = InterfaceController.HandleRegisterinput(currentField, password, escapetab, nextfield: null, "email");
                    if (string.Equals(currentField, "validated")) registercomplete = true;
                    break;
            }
        }
        User user = new User(username, birthDate, email, password, admin: true);
        Helper.ConsoleClear();
        AdminInterface.GeneralMenu(fid);
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