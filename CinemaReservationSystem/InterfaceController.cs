public class InterfaceController
{
    public static void ViewMovies(){

    List<TestMovie> testMovies = CreateOrGetTestMovies(); // Moet weg nadat database gekoppeld staat
        foreach(TestMovie movie in testMovies){
            Console.WriteLine($"ID: {movie.ID}, Title: {movie.Title}, Age Rating: {movie.AgeRating}");
        }
        XToGoBack();
    }

    public static void ViewMovies(int id){

    List<TestMovie> testMovies = CreateOrGetTestMovies(); // Moet weg nadat database gekoppeld staat
        foreach(TestMovie movie in testMovies){
            Console.WriteLine($"ID: {movie.ID}, Title: {movie.Title}, Age Rating: {movie.AgeRating}");
        }
        XToGoBack(id);
    }

    public static void LogIn(){
        for(int i = 0; i < 4; i++){
            string passout = "Test"; // weghalen nadat het geimplementeerd is
            Console.WriteLine("Enter your username or press q to quit.");
            string username = Console.ReadLine();
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
        // een method voor de logica om te zorgen dat je niet meer bij login only data kan.
        Console.WriteLine("You have been succesfully logged out");
        XToGoBack();
    }

    public static void RegisterUser()
    {
        Console.WriteLine("User Registration");
        Console.WriteLine("-----------------");
        // passed naar een validator, kan ook nog in ander file als we willen dat dit alleen view is.
        string username = GetValidInput("Username needs to be atleast 3 characters or more.\nEnter username: ", IsValidUsername);
        string email = GetValidInput("An email address needs to include a @ & a .\nEnter email: ", IsValidEmail);
        string password = GetValidInput("Password needs to be atleast 6 characters long and have a digit in it.\nEnter password: ", IsValidPassword);
        TestUser user = new(username, email, password);
        CreateOrGetTestUser(user);

        Console.WriteLine("User registration successful!");
        Console.WriteLine($"Username: {user._username}");
        Console.WriteLine($"Email: {user._email}");
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
                Console.WriteLine($"Username: {user._username} \nEmail: {user._email}\n");
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

// voor demo
    private static List<TestMovie> _testMovies;
    private static List<TestMovie> CreateOrGetTestMovies() {
        _testMovies ??= CreateTestMovies();
        return _testMovies;
    }
    private static List<TestMovie> CreateTestMovies()
    {
        List<TestMovie> testMovies =
        [
            new TestMovie("The Matrix", 15),
            new TestMovie("Inception", 12),
            new TestMovie("The Shawshank Redemption", 18),
        ];

        return testMovies;
    }

    private static List<TestUser> _testUsers;

    private static List<TestUser> CreateOrGetTestUser(TestUser user) {
        _testUsers ??= new List<TestUser>();
        _testUsers.Add(user); // Add user to the list
        return _testUsers;
    }
}

internal class TestMovie{
    public string Title;
    public int AgeRating;
    private static int _lastId = 0;
    public int ID { get; }
    public TestMovie(string title, int ageRating)
    {
        Title = title;
        AgeRating = ageRating;
        ID = _lastId++;
    }
}

internal class TestUser{
    public readonly string _username;
    public readonly string _email;
    private readonly string _password;
    public readonly int ID;
    private static int _latestID = 0;
    public TestUser(string user, string email, string password){
        _username = user;
        _email = email;
        _password = password;
        ID = ++_latestID;
    }
    public int UserID => ID;
}