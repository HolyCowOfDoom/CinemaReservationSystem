using System.Collections;
using System.Globalization;

public class UserController
{
    public static void LogOut(){
        // (optioneel) een method voor de logica om te zorgen dat je niet meer bij login only data kan.
        Console.WriteLine("You have been succesfully logged out");
        XToGoBack();
    }

    public static void ViewMovies(int id){
        List<Movie> Movies = JsonHandler.Read<Movie>("MovieDB.json");
        foreach (Movie movie in Movies)
        {
            Console.WriteLine($"Title: {movie.Title,-40} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
        }
        XToGoBack(id);
    }

    public static void FilterMovies(int id){
        char FilterInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4' || c == '5',
        "Filter Options",  "1. General Audience (G)\n2. Parental Guidance Suggested (PG)\n3. (PG-13)\n4. Restricted (R)\n5. No One 17 and Under (NC-17)");
        List<Movie> Movies = JsonHandler.Read<Movie>("MovieDB.json");
        List<Movie> sortedMovies = Movies.OrderBy(movie => movie.AgeRating).ToList(); // kan ook in de JsonHandler.cs
        int rating = 0;
        switch(FilterInput)
        {
            case '1':
                rating = 6;
                break;
            case '2':
                rating = 9;
                break;
            case '3':
                rating = 12;
                break;
            case '4':
                rating = 15;
                break;
            case '5':
                rating = 18;
                break;
        }
        int currentrating = 0;
        foreach (Movie movie in sortedMovies)
        {
            if (rating >= movie.AgeRating) 
            {
                if (currentrating < movie.AgeRating)
                {
                    currentrating = movie.AgeRating;
                    Console.WriteLine($"\n│ Suitable for an audience under {currentrating,-2} years old.");
                }
                Console.WriteLine($"│ Title: {movie.Title,-40} │ Age Rating: {movie.AgeRating,-3} │ Description: {movie.Description} │");
            }
        }
        XToGoBack(id);
    }

    public static void ViewUser(int id){
        User user = CsvHandler.GetRecordWithValue<User>("UserDB.csv", "ID", id);
        Console.WriteLine("┌────────────────────────────────┬───────────────────────────────────────┬────────────────────────┐");
        Console.WriteLine($"│ Username: {user.Name,-20} │ Email: {user.Email,-30} │ Birth date: {user.BirthDate} │");
        Console.WriteLine("└────────────────────────────────┴───────────────────────────────────────┴────────────────────────┘");
        XToGoBack(id);
    }

        // Console.WriteLine("┌───────────────────────────────┬───────────────────────────────────────┬────────────────────────┬");
        // Console.WriteLine($"│ Username: {user.Name,-20} │ Email: {user.Email,-30} │ Birth date: {user.BirthDate} │ User type: {user.uType} "); uType print dat user een customer of admin is.
        // Console.WriteLine("└───────────────────────────────┴───────────────────────────────────────┴────────────────────────┴");
       

    private static void XToGoBack()
    {
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            Console.Clear();
            Interface.GeneralMenu();
        }
    }
    private static void XToGoBack(int id)
    {
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            Console.Clear();
            UserInterface.GeneralMenu(id);
        }
    }
}