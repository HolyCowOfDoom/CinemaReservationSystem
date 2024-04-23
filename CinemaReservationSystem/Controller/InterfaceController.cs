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

    public static void LogIn(){
        bool quit = false;
        do{
        Console.WriteLine("Enter your username or press q to quit.");

        string username = Console.ReadLine();
        if(username.ToLower() == "q") quit = true;
        User? user = User.GetUserWithValue("Name", username);
        if (user != null)
        {
            // hier CSV handler die username krijgt en ID + Password returned om in te loggen.
            int attempts = 3;
            while (attempts > 0)
            {
                Console.WriteLine($"Enter the password associated with the username: {username}");
                string passin = Console.ReadLine();
                // called hier een method van user.cs en krijgt een user ID gereturned

                if (passin == user.Password)
                {
                    string id = user.ID;
                    Console.WriteLine($"Succesfully logged into {user.Name}");
                    XToGoBack(id);
                    break;
                }
                else
                {
                    attempts--;
                    Console.WriteLine($"{attempts} attempts remaining.");
                }
            }
        }
        Console.WriteLine("Attempt limit reached on trying passwords.");
        XToGoBack();
        break;
        } while (!quit);
        XToGoBack();
    }

    public static void Login()
    {
        Console.CursorVisible = false;
        Console.Clear();

        string username = Helper.CaptureInput(30, 1);
        string password = Helper.CaptureInputPassword(30, 1, username);

        //test
        Helper.DrawLogin(username, password);

        Console.ReadKey();
    }

    public static void RegisterUser()
    {
        Console.WriteLine("USER REGISTRATION\n-------------------------------------");
        string username = Helper.GetValidInput("Username needs to be atleast 3 characters and not more than 20 characters.\nEnter username: ", Helper.IsValidUsername);
        string birthDate = Helper.GetValidInput("Birthdate needs to be dd-MM-yyyy.\nEnter birthdate: ", Helper.IsValidBD);
        string email = Helper.GetValidInput("An email address needs to include (@) and (.).\nEnter email: ", Helper.IsValidEmail);
        string password = Helper.GetValidInput("Password needs to be atleast 6 characters long and have a digit in it.\nEnter password: ", Helper.IsValidPassword);
        Console.Clear();
        User user = new User(username, birthDate, email, password);
        Console.WriteLine("User registration successful!");
        Console.WriteLine($"Username: {user.Name}");
        Console.WriteLine($"Birth date: {user.BirthDate}");
        Console.WriteLine($"Email: {user.Email}");
        
        XToGoBack(user.ID);
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