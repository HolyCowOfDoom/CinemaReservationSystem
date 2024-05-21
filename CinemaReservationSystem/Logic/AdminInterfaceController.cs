using System.Globalization;

public class AdminInterfaceController
{
    public static void CreateMovie(string id)
    {
        string title = Helper.GetValidInput("Please input movie title:", Helper.IsNotNull);
        string description = Helper.GetValidInput("Please add movie description:", Helper.IsNotNull);
        int ageRating = Convert.ToInt32(Helper.GetValidInput("Please add movie age rating:", Helper.IsNotNull));
        string genre = Helper.GetValidInput("Please add a genre to the movie:", Helper.IsNotNull);

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
        screeningAud = JsonHandler.Get<Auditorium>(auditID, "Data/AuditoriumDB.json");
        } while (screeningAud == null);
        string dateTimeString = Helper.GetValidInput("Please input screening date: <DD-MM-YYYY HH:MM>", Helper.IsValidDT);
        DateTime screeningDT = DateTime.ParseExact(dateTimeString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
        MovieDataController.AddScreening(movie, screeningAud, screeningDT);

        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            AdminInterface.GeneralMenu(id);
        }
    }

    public static void RegisterAdmin(string fid)
    {
        InterfaceController.RegisterUser(admin: true, id: fid);
    }

    public static void HandleAdminSwitch(string fid, string nid)
    {
        User firstuser = UserDataController.GetUserWithValue("ID", fid);
        User newuser = UserDataController.GetUserWithValue("ID", nid);
        while(true){
        char FilterInput = Helper.ReadInput((char c) => c == '1' || c == '2',
        "What account do you want to use?",  $"1. {firstuser.Name} (G)\n2. {newuser.Name}");
        switch(FilterInput)
            {
                case '1':
                    AdminInterface.GeneralMenu(fid);
                    break;
                case '2':
                    AdminInterface.GeneralMenu(nid);
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
        List<Screening> screenings = MovieDataController.GetAllMovieScreenings(movie);
        foreach (Screening screening in screenings)
        {
            Console.WriteLine($"Date and Time: {screening.ScreeningDateTime, -40:dd-MM-yyyy HH:mm} | Auditorium: {screening.AssignedAuditorium.ID}");
        }
        string input = AdminInputMovie(id);
        switch (input) {
            case "Select":
                UserInterfaceController.ScreeningSelect(movie, id);
                break;
            case "Add":
                AddScreening(movie, id);
                break;
            case "Return":
                AdminInterface.GeneralMenu(id);
                break;
            }
    }

    public static string AdminInputMovie(string id)
    {
        while (true) {
        char genreFilterInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3',
            "Movie Options", "1. Select Screening\n2. Add Screening\n3. Go Back");

        switch (genreFilterInput) {
            case '1':
                return "Select";
            case '2':
                return "Add";
            case '3':
                return "Return";
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