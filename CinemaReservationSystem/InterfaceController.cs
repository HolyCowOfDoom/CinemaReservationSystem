using System.Globalization;

public class InterfaceController
{
    public static void ViewMovies(){
        List<Movie> Movies = JsonHandler.Read<Movie>("MovieDB.json");
        foreach (Movie movie in Movies)
        {
            Console.WriteLine($"Title: {movie.Title,-50} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
        }
            XToGoBack();
    }

    public static void ViewMovies(int id){
        List<Movie> Movies = JsonHandler.Read<Movie>("MovieDB.json");
        foreach (Movie movie in Movies)
        {
            Console.WriteLine($"Title: {movie.Title,-50} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
        }
        XToGoBack(id);
    }

    public static void LogIn(){
       
        Console.WriteLine("Enter your username or press q to quit.");

        string username = Console.ReadLine(); // !!! word nog niet getest op username
        if(username == "q") Interface.GeneralMenu();
        User? user = CsvHandler.GetRecordWithValue<User>("UserDB.csv", "Name", username);
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
                    int id = user.ID;
                    Interface.GeneralMenu(id);
                }
                else
                {
                    attempts--;
                    Console.WriteLine($"{attempts} attempts remaining.");
                }
            }
        }
        Interface.GeneralMenu();
    }

    public static void LogOut(){
        // (optioneel) een method voor de logica om te zorgen dat je niet meer bij login only data kan.
        Console.WriteLine("You have been succesfully logged out");
        XToGoBack();
    }

    public static void RegisterUser()
    {
        Console.WriteLine("USER REGISTRATION\n-------------------------------------");
        // passed naar een validator, kan ook nog in ander file als we willen dat dit alleen view is.
        string username = GetValidInput("Username needs to be atleast 3 characters or more.\nEnter username: ", IsValidUsername);
        string birthDate = GetValidInput("Birthdate needs to be dd-MM-yyyy.\nEnter birthdate: ", IsValidBD);
        string email = GetValidInput("An email address needs to include a @ & a .\nEnter email: ", IsValidEmail);
        string password = GetValidInput("Password needs to be atleast 6 characters long and have a digit in it.\nEnter password: ", IsValidPassword);
        Console.Clear();
        User user = new User(username, birthDate, email, password);
        Console.WriteLine("User registration successful!");
        Console.WriteLine($"Username: {user.Name}");
        Console.WriteLine($"Birth date: {user.BirthDate}");
        Console.WriteLine($"Email: {user.Email}");
        
        XToGoBack(user.ID);

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

        static bool IsValidUsername(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.Length >= 3;
        }

        static bool IsValidEmail(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.Contains('@') && input.Contains('.');
        }

        static bool IsValidName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words){
                if(word.Length < 2) return false;
            }
            return true;
        }

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
    public static void ViewUser(int id){
        // Pakt alle data uit de csv en print het voor de user.
        // Dit kan gedaan worden door middel van het ophalen van de ID en daar de gegevens van krijgen.
        // Console.WriteLine($"Username: {username}\n Email: {email}\n Date of birth: {birthdate}\n");
        User user = CsvHandler.GetRecordWithValue<User>("UserDB.csv", "ID", id);
            Console.WriteLine($"Username: {user.Name,-10} | Email: {user.Email,-5} | Birth date: {user.BirthDate}\n");
            // Console.WriteLine($"Favourite list");
            // foreach(Movie movie in user.Favlist){
            // Console.WriteLine($"Title: {movie.Title,-50} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
        XToGoBack(id);
    }

    private static void XToGoBack(){
        Console.WriteLine("Press x to go back");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            Interface.GeneralMenu();
        }
    }

    private static void XToGoBack(int id){
        Console.WriteLine("Press x to go back");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            Console.Clear();
            Interface.GeneralMenu(id);
        }
    }
}