using System.Globalization;

public class AdminController
{
    public static void CreateMovie(string id)
    {
        Console.WriteLine("Please input movie title:");
        string title = Console.ReadLine();
        Console.WriteLine("Please add movie description:");
        string description = Console.ReadLine();
        Console.WriteLine("Please add movie age rating:");
        int ageRating = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Please add a genre to the movie:");
        string genre = Console.ReadLine();

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
        string username = GetValidInput("Username needs to be atleast 3 characters and not more than 20 characters.\nEnter username: ", IsValidUsername);
        string birthDate = GetValidInput("Birthdate needs to be dd-MM-yyyy.\nEnter birthdate: ", IsValidBD);
        string email = GetValidInput("An email address needs to include (@) and (.).\nEnter email: ", IsValidEmail);
        string password = GetValidInput("Password needs to be atleast 6 characters long and have a digit in it.\nEnter password: ", IsValidPassword);
        Console.Clear();
        User user = new User(username, birthDate, email, password, true);
        Console.WriteLine("User registration successful!");
        Console.WriteLine($"Username: {user.Name}");
        Console.WriteLine($"Birth date: {user.BirthDate}");
        Console.WriteLine($"Email: {user.Email}");
        
        XToGoBack(id);

        static string GetValidInput(string prompt, Func<string, bool> validation)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
            } while (!validation(input));

            return input;
        }

        static bool IsValidUsername(string input) => !string.IsNullOrWhiteSpace(input) && input.Length >= 3 && input.Length < 21;
        static bool IsValidEmail(string input) => !string.IsNullOrWhiteSpace(input) && input.Contains('@') && input.Contains('.') && input.Length < 31;

        // static bool IsValidName(string input)
        // {
        //     if (string.IsNullOrWhiteSpace(input))
        //     {
        //         return false;
        //     }
        //     string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        //     foreach (string word in words){
        //         if(word.Length < 2) return false;
        //     }
        //     return true;
        // }

        static bool IsValidPassword(string input)
        {
            // test op lege string of string onder de 6 chars.
            if (!string.IsNullOrWhiteSpace(input) && input.Length >= 6)
            {
                foreach (char c in input)
                {
                    if (char.IsDigit(c))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static bool IsValidBD(string input)
        {
            string dateFormat = "dd-MM-yyyy";
            if (DateTime.TryParseExact(input, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
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