using System.Collections;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;

public class UserController
{
    public static void LogOut(){
        // (optioneel) een method voor de logica om te zorgen dat je niet meer bij login only data kan.
        Console.WriteLine("You have been succesfully logged out");
        XToGoBack();
    }

    public static void ViewMovies(string id){
        List<Movie> Movies = JsonHandler.Read<Movie>("MovieDB.json");
        Console.WriteLine($"{"Title:",-40} | {"Age Rating:",-11} | {"Genre:",-11} | {"Description:"}");
        foreach (Movie movie in Movies)
        {
            Console.WriteLine($"{movie.Title,-40} | {movie.AgeRating,-11} | {movie.Genre, -11} | {movie.Description}");
        }
        WouldYouLikeToSearch(id);
        Console.Clear();
    }
    public static void FilterMovies(string id, string option)
    {
        char genreFilterInput;
        List<Movie> Movies = JsonHandler.Read<Movie>("MovieDB.json");
        List<Movie> sortedMovies = Movies.OrderBy(movie => movie.AgeRating).ToList(); // kan ook in de JsonHandler.cs
        int rating = 0;
        string genre = "";
        if(option == "Age")
        {
            rating = InputAge();
            FilterMoviesAge(rating, sortedMovies);
        }
        if(option == "Genre")
        {
            genre = InputGenre();
            FilterMoviesGenre(genre, sortedMovies);
        }
        if(option == "Both")
        {
            rating = InputAge();
            genre = InputGenre();
            FilterMoviesAgeAndGenre(genre, rating, sortedMovies);
        }

        XToGoBack(id);
    }

    public static int InputAge()
    {
        while(true){
        char FilterInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6',
        "Filter Options",  "1. All Ages (G)\n2. Parental Guidance Suggested (PG)\n3. Parental Guidance 13+ (PG-13)\n4. Restricted (R)\n5. No One 17 and Under (NC-17)\n6. Show Age Rating Descriptions");
        switch(FilterInput)
        {
            case '1':
                return 6;
            case '2':
                return 9;
            case '3':
                return 13;
            case '4':
                return 15;
            case '5':
                return 18;
            case '6':
                Console.WriteLine("Age Rating Descriptions:\n1. All Ages (G): General Audiences - All ages admitted. There is no content that would be objectionable to most parents.\n2. Parental Guidance Suggested (PG): Some material may not be suitable for children. Parents urged to give parental guidance.\n3. Parental Guidance 13+ (PG-13): Some material may be inappropriate for children under 13. Parents are urged to be cautious.\n4. Restricted (R): Restricted – Under 17 requires accompanying parent or adult guardian. Contains some adult material.\n5. No One 17 and Under (NC-17): Adults Only – No one 17 and under admitted.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
        }
    }

    public static string InputGenre()
    {
        while (true) {
        char genreFilterInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6',
            "Filter Options", "1. Action\n2. Adventure\n3. Comedy\n4. Drama\n5. Horror\n6. Display Genre Descriptions");

        switch (genreFilterInput) {
            case '1':
                return "Action";
            case '2':
                return "Adventure";
            case '3':
                return "Comedy";
            case '4':
                return "Drama";
            case '5':
                return "Horror";
            case '6':
                Console.WriteLine("Genre Descriptions:\n1. Action: Exciting and fast-paced movies filled with thrilling stunts and intense action sequences.\n2. Adventure: Films that take audiences on a journey to explore new worlds, embark on quests, and face unknown challenges.2. Adventure: Films that take audiences on a journey to explore new worlds, embark on quests, and face unknown challenges.\n3. Comedy: Light-hearted and humorous movies that aim to entertain and make audiences laugh with witty jokes and funny situations.\n4. Drama: Engaging and emotionally impactful stories that delve into complex human relationships and personal struggles.\n5. Horror: Tense and chilling movies that aim to scare and thrill audiences with suspenseful plots and terrifying scenarios.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                continue;
        }
    }
    }
    public static void FilterMoviesAge(int rating, List<Movie> sortedMovies){
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
                Console.WriteLine($"│ Title: {movie.Title,-40} │ Age Rating: {movie.AgeRating,-3} │ Genre: {movie.Genre,-10} │ Description: {movie.Description} │");
            }
        }
    }

    public static void FilterMoviesGenre(string genre, List<Movie> sortedMovies){
        int currentrating = 0;
        foreach (Movie movie in sortedMovies)
        {
            if (genre == movie.Genre) 
            {
                if (currentrating < movie.AgeRating)
                {
                    currentrating = movie.AgeRating;
                    Console.WriteLine($"\n│ Suitable for an audience under {currentrating,-2} years old.");
                }
                Console.WriteLine($"│ Title: {movie.Title,-40} │ Age Rating: {movie.AgeRating,-3} │ Genre: {movie.Genre,-10} │ Description: {movie.Description} │");
            }
        }
    }

    public static void FilterMoviesAgeAndGenre(string genre, int rating, List<Movie> sortedMovies) { 
    int currentRating = 0;
    foreach (Movie movie in sortedMovies) {
        if (rating >= movie.AgeRating && genre == movie.Genre) {
            if (currentRating < movie.AgeRating) {
                currentRating = movie.AgeRating;
                Console.WriteLine($"\n│ Suitable for an audience under {currentRating,-2} years old.");
            }
            Console.WriteLine($"│ Title: {movie.Title,-40} │ Age Rating: {movie.AgeRating,-3} │ Genre: {movie.Genre,-10} │ Description: {movie.Description} │");
        }
    }
    }


    public static void ViewUser(string id){
        User user = CsvHandler.GetRecordWithValue<User>("UserDB.csv", "ID", id);
        Console.WriteLine("┌────────────────────────────────┬───────────────────────────────────────┬────────────────────────┐");
        Console.WriteLine($"│ Username: {user.Name,-20} │ Email: {user.Email,-30} │ Birth date: {user.BirthDate} │");
        Console.WriteLine("└────────────────────────────────┴───────────────────────────────────────┴────────────────────────┘");
        XToGoBack(id);
    }

        // Console.WriteLine("┌───────────────────────────────┬───────────────────────────────────────┬────────────────────────┬");
        // Console.WriteLine($"│ Username: {user.Name,-20} │ Email: {user.Email,-30} │ Birth date: {user.BirthDate} │ User type: {user.uType} "); uType print welk user een customer of admin is.
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
    private static void XToGoBack(string id)
    {
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            Console.Clear();
            UserInterface.GeneralMenu(id);
        }
    }

    private static void WouldYouLikeToSearch(string id)
    {
        Console.WriteLine("Would you like to select a movie? Y / N");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'y' || c == 'n');
        if (specificLetterInput == 'y'){
            SelectMovie(id);
        }
        XToGoBack(id);
    }

    private static void SelectMovie(string id)
    {
        Movie? movie;
        do{
        string MovieName = Helper.GetValidInput("Please type movie title: ", Helper.IsNotNull);
        movie = JsonHandler.GetByMovieName(MovieName);
        } while (movie == null);
        MovieInterface(movie, id);
    }

    private static void MovieInterface(Movie movie, string id)
    {
        Console.Clear();
        Console.WriteLine($"Title: {movie.Title,-40} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
        List<Screening> screenings = movie.GetAllMovieScreenings();
        foreach (Screening screening in screenings)
        {
            Console.WriteLine($"Date and Time: {screening.ScreeningDateTime, -40:dd-MM-yyyy HH:mm} | Auditorium: {screening.AssignedAuditorium.ID}");
        }
        XToGoBack(id);
    }
}