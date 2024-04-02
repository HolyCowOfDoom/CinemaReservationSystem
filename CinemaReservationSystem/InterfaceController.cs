using System.Globalization;

public class InterfaceController
{
    public static void ViewMovies(){
        List<Movie> Movies = JsonHandler.Read<Movie>("MovieDB.json");
        foreach (Movie movie in Movies)
        {
            Console.WriteLine($"Title: {movie.Title,-40} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
        }
            XToGoBack();
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
                    string id = user.ID;
                    Console.WriteLine($"Succesfully logged into {user.Name}");
                    XToGoBack(id);
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
    }

    public static void RegisterUser()
    {
        Console.WriteLine("USER REGISTRATION\n-------------------------------------");
        // passed naar een validator, kan ook nog in ander file als we willen dat dit alleen view is.
        string username = GetValidInput("Username needs to be atleast 3 characters and not more than 20 characters.\nEnter username: ", IsValidUsername);
        string birthDate = GetValidInput("Birthdate needs to be dd-MM-yyyy.\nEnter birthdate: ", IsValidBD);
        string email = GetValidInput("An email address needs to include (@) and (.).\nEnter email: ", IsValidEmail);
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
    
    private static void XToGoBack(){
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            Interface.GeneralMenu();
        }
    }

    private static void XToGoBack(string id){
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            UserInterface.GeneralMenu(id);
        }
    }
}