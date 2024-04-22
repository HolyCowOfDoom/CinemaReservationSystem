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

        XToGoBack(id);
    }

    public static void AddScreening(Movie movie, string id)
    {
        Auditorium? screeningAud;
        do
        {
        string auditID = Helper.GetValidInput("Please input valid auditorium ID related to the screening:", Helper.IsNotNull);
        screeningAud = JsonHandler.Get<Auditorium>(auditID, "AuditoriumDB.json");
        } while (screeningAud == null);

        string dateTimeString = Helper.GetValidInput("Please input screening date: <DD-MM-YYYY HH:MM>", Helper.IsValidDT);
        DateTime screeningDT = DateTime.ParseExact(dateTimeString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
        movie.AddScreening(screeningAud, screeningDT);

        XToGoBack(id);
    }

    public static void RegisterAdmin(string id)
    {
        Console.WriteLine("ADMIN REGISTRATION\n─────────────────────────────────");
        // passed naar een validator, kan ook nog in ander file als we willen dat dit alleen view is.
        string username = Helper.GetValidInput("Username needs to be atleast 3 characters and not more than 20 characters.\nEnter username: ", Helper.IsValidUsername);
        string birthDate = Helper.GetValidInput("Birthdate needs to be dd-MM-yyyy.\nEnter birthdate: ", Helper.IsValidBD);
        string email = Helper.GetValidInput("An email address needs to include (@) and (.).\nEnter email: ", Helper.IsValidEmail);
        string password = Helper.GetValidInput("Password needs to be atleast 6 characters long and have a digit in it.\nEnter password: ", Helper.IsValidPassword);
        Console.Clear();
        User user = new User(username, birthDate, email, password, true);
        Console.WriteLine("User registration successful!");
        Console.WriteLine($"Username: {user.Name}");
        Console.WriteLine($"Birth date: {user.BirthDate}");
        Console.WriteLine($"Email: {user.Email}");
        
        XToGoBack(id);
    }

    public static void LogOut(){
        Console.WriteLine("You have been succesfully logged out");
        XToGoBack();
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

        XToGoBack(id);
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
        XToGoBack(id);
    }

    private static void XToGoBack(string id)
    {
        InterfaceController.XToGoBack(id);
    }

    private static void XToGoBack()
    {
        InterfaceController.XToGoBack();
    }
}