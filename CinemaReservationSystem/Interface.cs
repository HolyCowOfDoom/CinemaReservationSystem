class Interface{

    public static void GeneralMenu(bool login){
        if(login){
            Console.WriteLine("General Menu\n 1. View all movies\n 2. Register \n 3. Log in\n");
            char digitInput = ReadInput(char.IsDigit);
            if(digitInput == "1"){
                ViewMovies();
            }
            else if(digitInput == "2"){
                RegisterUser();
            }
            else if(digitInput == "3"){
                LogIn();
            }
            else{
                Console.WriteLine("Please enter a correct digit which corresponds to an option");
                GeneralMenu(login);
            }
        }

        else{
            Console.WriteLine("General Menu\n 1. View all movies\n 2. Log out\n 3. View profile\n 4. Reserve seats");
            char digitInput = ReadInput(char.IsDigit)
            if(digitInput == "1"){
                ViewMovies();
            }
            else if (digitInput == "2"){
                LogOut();
            }
            else if (digitInput == "3"){
                ViewUser();
            }
            else if(digitInput == "4"){
                ReserveSeats();
            }
            else{
                Console.WriteLine("Please enter a correct digit which corresponds to an option");
                GeneralMenu(login);
            }
        }
    }
    public static void ViewMovies(){
        /* 
        list<Movie> MovieList = DBJson.GetMoviesList()
        foreach(Movie movie in MovieList){
            // Implement a way to display the movielist
        }
        */
    }

    public static void LogIn(){
        for(int i = 0; i < 4; i++){
            string passout = "Test"; // weghalen nadat het geimplementeerd is
            Console.WriteLine("Enter your username or press q to quit.");
            string username = Console.ReadLine();
            if(username == "q"){
                GeneralMenu(false);
            }
            Console.WriteLine($"Enter the password associated with the username: {username}");
            string passin = Console.ReadLine();
                // called hier een method van user.cs en krijgt een bool gereturned

            if(passin == passout){
                GeneralMenu(true);
            }
            else{
                Console.WriteLine($"{4 - i} attempts remaining.");
            }
        }
    }

    public static void LogOut(){
        // een method voor de logica om te zorgen dat je niet meer bij login only data kan.
        GeneralMenu(false);
    }

    static void RegisterUser()
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
        Console.WriteLine($"Username: {username}\n Email: {email}\n Date of birth: {birthdate}\n");
    }

    private static void ReserveSeats(){
        // Hier moet alle view materiaal in van reserving seats.
    }
}