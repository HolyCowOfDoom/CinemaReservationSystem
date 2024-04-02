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
        int movieID = Convert.ToInt32(Console.ReadLine());
        Movie movie = JsonHandler.Get<Movie>(movieID, "MovieDB.json");
        Console.WriteLine("Please input auditorium ID related to the screening:");
        int auditID = Convert.ToInt32(Console.ReadLine());
        movie.AddScreening(JsonHandler.Get<Auditorium>(auditID, "AuditoriumDB.json"), null);

        XToGoBack(id);
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
}