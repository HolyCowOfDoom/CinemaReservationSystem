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
            Console.WriteLine($"Title: {movie.Title,-50} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
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