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

    public static void AddScreening(string id)
    {
        Console.WriteLine("Please enter movie ID");
        string movieID = Console.ReadLine();
        Movie movie = JsonHandler.Get<Movie>(movieID, "MovieDB.json");
        Console.WriteLine("Please input auditorium ID related to the screening:");
        string auditID = Console.ReadLine();
        movie.AddScreening(JsonHandler.Get<Auditorium>(auditID, "AuditoriumDB.json"), null);

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



    private static void XToGoBack(string id)
    {
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            Console.Clear();
            UserInterface.GeneralMenu(id);
        }
    }

    private static void XToGoBack(){
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            Console.Clear();
            Interface.GeneralMenu();
        }
    }
}