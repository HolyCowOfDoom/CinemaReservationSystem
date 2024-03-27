public class InterfaceController{
public static void ViewMovies(bool login){
        /* 
        list<Movie> MovieList = JsonHandler.Read<Movie>("MovieDB.json");
        foreach(Movie movie in MovieList){
            // Implement a way to display the movielist
        }
        */
        Console.WriteLine("Press x to go back");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'X');
        if (specificLetterInput == 'X'){
            Interface.GeneralMenu(login);
        }

    }

    public static void LogIn(){
        for(int i = 0; i < 4; i++){
            string passout = "Test"; // weghalen nadat het geimplementeerd is
            Console.WriteLine("Enter your username or press q to quit.");
            string username = Console.ReadLine();
            if(username == "q"){
                Interface.GeneralMenu(false);
            }
            // hier met de csv handler nog een function waar de username gepakt word en de password opgeslagen is.
            Console.WriteLine($"Enter the password associated with the username: {username}");
            string passin = Console.ReadLine();
            // called hier een method van user.cs en krijgt een bool gereturned

            if(passin == passout) // moet anders
            {
                Interface.GeneralMenu(true);
            }
            else{
                Console.WriteLine($"{4 - i} attempts remaining.");
            }
        }
    }

    public static void LogOut(){
        // een method voor de logica om te zorgen dat je niet meer bij login only data kan.
        Interface.GeneralMenu(false);
    }

    public static void RegisterUser()
    {
        Console.WriteLine("User Registration");
        Console.WriteLine("-----------------");
        // passed naar een validator, kan ook nog in ander file als we willen dat dit alleen view is.
        string username = GetValidInput("Enter username: ", IsValidUsername);
        string email = GetValidInput("Enter email: ", IsValidEmail);
        string password = GetValidInput("Enter password: ", IsValidPassword);

        Console.WriteLine("User registration successful!");
        Console.WriteLine($"Username: {username}");
        Console.WriteLine($"Email: {email}");

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
            return !string.IsNullOrWhiteSpace(input) && input.Contains('@');
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


    }
    public static void ViewUser(){
        // Pakt alle data uit de csv en print het voor de user.
        // Dit kan gedaan worden door middel van het ophalen van de ID en daar de gegevens van krijgen.
        // Console.WriteLine($"Username: {username}\n Email: {email}\n Date of birth: {birthdate}\n");
    }

    public static void ReserveSeats(){
        // Hier moet alle view materiaal in van reserving seats.
    }

}

internal class TestMovie{
    public string Title;
    public int AgeRating;
    private static int _lastId = 0;
    public int ID { get; }
    public Movie(string title, int ageRating)
    {
        Title = title;
        AgeRating = ageRating;
        private static int _lastId = 0;
    }

}