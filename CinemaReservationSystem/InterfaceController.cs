public class InterfaceController
{
    public static void ViewMovies(){

    List<TestMovie> testMovies = CreateOrGetTestMovies(); // Moet weg nadat database gekoppeld staat
    foreach (TestMovie movie in testMovies){
        Console.WriteLine($"Title: {movie.Title,-50} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
    }
        XToGoBack();
    }

    public static void ViewMovies(int id){

    List<TestMovie> testMovies = CreateOrGetTestMovies(); // Moet weg nadat database gekoppeld staat
        foreach (TestMovie movie in testMovies){
            Console.WriteLine($"Title: {movie.Title,-50} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
        }
        XToGoBack(id);
    }

    public static void LogIn(){
        for(int i = 0; i < 4; i++){
            string passout = "Test"; // !!! weghalen nadat het geimplementeerd is
            Console.WriteLine("Enter your username or press q to quit.");
            string username = Console.ReadLine(); // !!! word nog niet getest op username
            if(username == "q"){
                Interface.GeneralMenu();
            }
            // hier CSV handler die username krijgt en ID + Password returned om in te loggen.
            Console.WriteLine($"Enter the password associated with the username: {username}");
            string passin = Console.ReadLine();
            // called hier een method van user.cs en krijgt een user ID gereturned

            if(passin == passout) // !!! moet anders
            {
                int id = 100;
                Interface.GeneralMenu(id);
            }
            else{
                Console.WriteLine($"{4 - i} attempts remaining.");
            }
        }
    }

    public static void LogOut(){
        // (optioneel) een method voor de logica om te zorgen dat je niet meer bij login only data kan.
        Console.WriteLine("You have been succesfully logged out");
        XToGoBack();
    }

    public static void RegisterUser()
    {
        Console.WriteLine("User Registration");
        Console.WriteLine("-----------------");
        // passed naar een validator, kan ook nog in ander file als we willen dat dit alleen view is.
        string username = GetValidInput("Username needs to be atleast 3 characters or more.\nEnter username: ", IsValidUsername);
        string fullname = GetValidInput("Full name is atleast two words or more\nEnter Fullname: ", IsValidName);
        string email = GetValidInput("An email address needs to include a @ & a .\nEnter email: ", IsValidEmail);
        string password = GetValidInput("Password needs to be atleast 6 characters long and have a digit in it.\nEnter password: ", IsValidPassword);
        TestUser user = new(username, email, fullname, password);
        CreateOrGetTestUser(user);
        AddingFavmovies(user);

        Console.WriteLine("User registration successful!");
        Console.WriteLine($"Username: {user._username}");
        Console.WriteLine($"Email: {user._email}");
        Console.WriteLine($"Full name: {user.Fullname}");
        XToGoBack(user.UserID);

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
    }
    public static void ViewUser(int id){
        // Pakt alle data uit de csv en print het voor de user.
        // Dit kan gedaan worden door middel van het ophalen van de ID en daar de gegevens van krijgen.
        // Console.WriteLine($"Username: {username}\n Email: {email}\n Date of birth: {birthdate}\n");

        // Hieronder is voor de demo, moet aangepast worden door middel van CSV/Json info te verkrijgen en displayen.
        foreach(TestUser user in _testUsers){
            if(id == user.ID){
                Console.WriteLine($"Username: {user._username,-10} | Email: {user._email,-5} | Full name: {user.Fullname}\n");
                Console.WriteLine($"Favourite list");
                foreach(TestMovie movie in user.Favlist){
                Console.WriteLine($"Title: {movie.Title,-50} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
                }
            }
            else{
                Console.WriteLine($"User not found");
            }
        XToGoBack(id);
        }
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
            Interface.GeneralMenu(id);
        }
    }

// voor demo \ kan misschien gebruikt worden om eerst met csv en json te testen. Maar moet later eruit.
    private static List<TestMovie> _testMovies;
    private static List<TestMovie> _testFavMovies;
    
    private static List<TestMovie> CreateOrGetTestMovies() {
        _testMovies ??= CreateTestMovies();
        return _testMovies;
    }
    private static List<TestMovie> CreateTestMovies()
    {
        List<TestMovie> testMovies =
        [
            new TestMovie("The Matrix", 15, "A mind-bending sci-fi classic where reality is not what it seems."),
            new TestMovie("Inception", 12, "Explore the depths of the human mind in this mind-bending thriller."),
            new TestMovie("The Shawshank Redemption", 18, "A tale of hope and redemption in the most unlikely of places."),
            new TestMovie("The Godfather", 18, "An epic saga of crime, family, and power."),
            new TestMovie("Pulp Fiction", 18, "A stylish and unforgettable journey through the criminal underworld."),
            new TestMovie("Fight Club", 18, "An intense exploration of masculinity and identity."),
            new TestMovie("Forrest Gump", 12, "Life is like a box of chocolates in this heartwarming tale."),
            new TestMovie("The Dark Knight", 12, "The legendary battle between the Batman and the Joker."),
            new TestMovie("The Lord of the Rings: The Fellowship of the Ring", 12, "Embark on an epic adventure to save Middle-earth."),
            new TestMovie("The Lord of the Rings: The Two Towers", 12, "The fellowship divides as the forces of darkness gather."),
            new TestMovie("The Lord of the Rings: The Return of the King", 12, "The final showdown for the fate of Middle-earth."),
            new TestMovie("The Godfather: Part II", 18, "A gripping continuation of the Corleone family saga."),
            new TestMovie("Schindler's List", 18, "A powerful true story of one man's fight against evil."),
            new TestMovie("12 Angry Men", 18, "A riveting courtroom drama that challenges your perceptions."),
            new TestMovie("Pokemon: The movie", 6, "Join Ash and Pikachu on an adventure to save the world of Pokémon.")
        ];

        return testMovies;
    }


    private static List<TestMovie> CreateOrGetFav() {
        _testFavMovies ??= CreateFav();
        return _testFavMovies;
    }

    private static List<TestMovie> CreateFav()
    {
        List<TestMovie> testFavMovies =
        [
            new TestMovie("The Matrix", 15, "A mind-bending sci-fi classic where reality is not what it seems."),
            new TestMovie("Inception", 12, "Explore the depths of the human mind in this mind-bending thriller."),
            new TestMovie("Fight Club", 18, "An intense exploration of masculinity and identity."),
            new TestMovie("The Dark Knight", 12, "The legendary battle between the Batman and the Joker."),
            new TestMovie("The Lord of the Rings: The Fellowship of the Ring", 12, "Embark on an epic adventure to save Middle-earth."),
            new TestMovie("Pokemon: The movie", 6, "Join Ash and Pikachu on an adventure to save the world of Pokémon.")
        ];

        return testFavMovies;
    }

    private static List<TestUser> _testUsers;

    private static List<TestUser> CreateOrGetTestUser(TestUser user) {
        _testUsers ??= new List<TestUser>();
        _testUsers.Add(user); // Add user to the list
        return _testUsers;
    }

    private static void AddingFavmovies(TestUser user){
        CreateOrGetFav();
            foreach(TestMovie movie in _testFavMovies){
                Console.WriteLine($"Title: {movie.Title,-50} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
                // Prompt for 'x' or 'y' after displaying each movie
                Console.WriteLine("Press x to continue to the next movie or press y to add the movie to your favorites list.");
                char specificLetterInput = Helper.ReadInput((char c) => c == 'x' || c == 'y');
                if (specificLetterInput == 'y'){
                    // If user presses 'y', add the movie to the favorites list
                    user.AddToFavorites(movie);
            }
        }   
    }
    
}

internal class TestMovie{
    public string Title;
    public int AgeRating;
    public string Description;
    private static int _lastId = 0;
    public int ID { get; }
    public TestMovie(string title, int ageRating, string description)
    {
        Title = title;
        AgeRating = ageRating;
        Description = description;
        ID = ++_lastId;
    }
}

internal class TestUser{
    public readonly string _username;
    public readonly string _email;
    public readonly string Fullname;
    private readonly string _password;
    public readonly int ID;
    private static int _latestID = 0;
    public List<TestMovie> Favlist;
    public TestUser(string user, string email, string fullname, string password){
        _username = user;
        _email = email;
        _password = password;
        Fullname = fullname;
        ID = ++_latestID;
        Favlist = new List<TestMovie>();
    }
    public int UserID => ID;
    public void AddToFavorites(TestMovie movie){
        Favlist.Add(movie);
    }
}