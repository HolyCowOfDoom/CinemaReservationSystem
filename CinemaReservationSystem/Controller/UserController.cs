using System.Collections;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;
using CsvHelper.Configuration.Attributes;

public class UserController
{
    public static void LogOut(){
        Console.WriteLine("You have been succesfully logged out");
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            Interface.GeneralMenu();
        }
    }

    public static void ViewMovies(string id){
        List<Movie> Movies = JsonHandler.Read<Movie>("Model/MovieDB.json");
        Console.WriteLine("┌────┬──────────────────────────────────────────┬─────────────┬─────────────┬──────────────────────────────────────────────────────────────┐");
        Console.WriteLine($"│ ID │ {"Title",-40} │ {"Age Rating",-11} │ {"Genre",-11} │ {"Description",-60} │");
        foreach (Movie movie in Movies)
        {
            Console.WriteLine($"│ {Movies.IndexOf(movie) + 1, -2} │ {movie.Title,-40} │ {movie.AgeRating,-11} │ {movie.Genre, -11} │ {movie.Description,-60} │");
        }
        Console.WriteLine("└────┴──────────────────────────────────────────┴─────────────┴─────────────┴──────────────────────────────────────────────────────────────┘");
        WouldYouLikeToSearch(id);
        Console.Clear();
    }
    public static void FilterMovies(string id, string option)
    {
        // char genreFilterInput;
        List<Movie> Movies = JsonHandler.Read<Movie>("Model/MovieDB.json");
        User user = User.GetUserWithValue("ID", id);
        int age = Helper.GetUserAge(user);
        List<Movie> sortedMovies = Movies.OrderBy(movie => movie.AgeRating).ToList(); // kan ook in de JsonHandler.cs
        int rating = 0;
        string genre = "";
        if(option == "Age")
        {
            rating = InputAge();
            if(age >= rating)
            {
                FilterMoviesAge(rating, sortedMovies);
            }
            else
            {
                Console.WriteLine("You are too young too see these movies. Press X to go back.");
                char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
                if (specificLetterInput == 'x'){
                    User IdCheck = User.GetUserWithValue("ID", id);
                    if (IdCheck.Admin) AdminInterface.GeneralMenu(id); 
                    else UserInterface.GeneralMenu(id);   
                }
            }
        }
        if(option == "Genre")
        {
            genre = InputGenre();
            FilterMoviesGenre(genre, sortedMovies, age);
        }
        if(option == "Both")
        {
            rating = InputAge();
            if(age >= rating)
            {
                genre = InputGenre();
                FilterMoviesAgeAndGenre(genre, rating, sortedMovies);
            }
            else
            {
                Console.WriteLine("You are too young too see these movies. Press X to go back.");
                char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
                if (specificLetterInput == 'x'){
                    User IdCheck = User.GetUserWithValue("ID", id);
                    if (IdCheck.Admin) AdminInterface.GeneralMenu(id); 
                    else UserInterface.GeneralMenu(id);   
                }   
            }
        }
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
                Console.WriteLine("Age Rating Descriptions:\n1. All Ages (G): General Audiences - All ages admitted. There is no content that would be objectionable to most parents.\n2. Parental Guidance Suggested (PG): Some material may not be suitable for children. Parents urged to give parental guidance until the age of 9.\n3. Parental Guidance 13+ (PG-13): Some material may be inappropriate for children under 13. Parents are urged to be cautious.\n4. Restricted (R): Restricted – Only Under 17 requires accompanying parent or adult guardian. Contains some adult material.\n5. No One 17 and Under (NC-17): Adults Only – No one 17 and under admitted.");
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
                Console.WriteLine($"│ Title: {movie.Title,-40} │ Age Rating: {movie.AgeRating,-3} │ Genre: {movie.Genre,-10} │ Description: {movie.Description}");
            }
        }
    }

    public static void FilterMoviesGenre(string genre, List<Movie> sortedMovies, int age){
        int currentrating = 0;
        foreach (Movie movie in sortedMovies)
        {
                if (genre == movie.Genre) 
                {
                    if (currentrating < movie.AgeRating)
                    {
                        currentrating = movie.AgeRating;
                        if(age < currentrating) break;
                        else Console.WriteLine($"\n│ Suitable for an audience under {currentrating,-2} years old.");
                    }
                    Console.WriteLine($"│ Title: {movie.Title,-40} │ Age Rating: {movie.AgeRating,-3} │ Genre: {movie.Genre,-10} │ Description: {movie.Description}");
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
            Console.WriteLine($"│ Title: {movie.Title,-40} │ Age Rating: {movie.AgeRating,-3} │ Genre: {movie.Genre,-10} │ Description: {movie.Description}");
        }
    }
    }


    public static void ViewUser(string id){
        User user = User.GetUserWithValue( "ID", id);
        Console.WriteLine("┌────────────────────────────────┬───────────────────────────────────────┬────────────────────────┬─────────┐");
        Console.WriteLine($"│ Username: {user.Name,-20} │ Email: {user.Email,-30} │ Birth date: {user.BirthDate} │ Age: {Helper.GetUserAge(user),-2} │");
        Console.WriteLine("└────────────────────────────────┴───────────────────────────────────────┴────────────────────────┴─────────┘");
        Console.WriteLine("ALL RESERVATIONS");
        int index = 0;
        Console.WriteLine("┌───┬──────────────────────────────────────────────────────┬────────────────────────────────────┬───────────────┬───────────────────────┬───────────────────┐");
        foreach (Reservation reservation in user.Reservations)
        {
            index++;
            string movietitle = GetMovieByID(reservation.ScreeningID);
            Screening screening = GetScreeningByID(reservation.ScreeningID);
            Console.WriteLine($"│ {index} │ Movie name: {movietitle,-40} │ Screening Date: {screening.ScreeningDateTime, -16} │ Auditorium: {screening.AssignedAuditorium.ID} │ Reservation Price: {reservation.TotalPrice} │ Seats: {string.Join(" ", reservation.SeatIDs), -10} │");
        }
        Console.WriteLine("└───┴──────────────────────────────────────────────────────┴────────────────────────────────────┴───────────────┴───────────────────────┴───────────────────┘");
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            UserInterface.GeneralMenu(id);
        }
    }

    public static string? GetMovieByID(string screeningID)
    {
        List<Movie> MovieList = JsonHandler.Read<Movie>("Model/MovieDB.json");
        foreach(Movie movie in MovieList)
        {
            if (movie.ScreeningIDs.Contains(screeningID)) return movie.Title;
        }
        return null;
    }

    public static Screening? GetScreeningByID(string screeningID)
    {
        List<Screening> ScreeningList = JsonHandler.Read<Screening>("Model/ScreeningDB.json");
        foreach(Screening screening in ScreeningList)
        {
            if (screening.ID == screeningID) return screening;
        }
        return null;
    }

    private static void WouldYouLikeToSearch(string id)
    {
        Console.WriteLine("Would you like to select a movie? Y / N");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'y' || c == 'n');
        if (specificLetterInput == 'y'){
            SelectMovie(id);
        }
        else
        {
            User IdCheck = User.GetUserWithValue("ID", id);
            if (IdCheck.Admin) AdminInterface.GeneralMenu(id); 
            else UserInterface.GeneralMenu(id);
        }
    }

    private static void SelectMovie(string id)
    {
        List<Movie> movies = JsonHandler.Read<Movie>("Model/MovieDB.json");
        Movie? movie;
        do{
        int movieNbr = Convert.ToInt32(Helper.GetValidInput("Please type movie number: ", Helper.IsValidInt));
        try
        {
            movie = movies[movieNbr - 1];
        }
        catch (IndexOutOfRangeException)
        {
            movie = null;
        }
        } while (movie == null);
        User IdCheck = User.GetUserWithValue("ID", id);
        if (IdCheck.Admin) AdminController.AdminMovieInterface(movie, id); 
        else MovieInterface(movie, id);
    }

    private static void MovieInterface(Movie movie, string id)
    {
        Console.Clear();
        Console.WriteLine($"Title: {movie.Title,-40} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
        List<Screening> screenings = movie.GetAllMovieScreenings();
        foreach (Screening screening in screenings)
        {
            int screeningNbr = screenings.IndexOf(screening) + 1;
            Console.WriteLine($"{screeningNbr, -2} | Date and Time: {screening.ScreeningDateTime, -40:dd-MM-yyyy HH:mm} | Auditorium: {screening.AssignedAuditorium.ID}");
        }
        string input = InputMovie(id);
        if (input == "Select")
        {
            ScreeningSelect(screenings, id);
        }
        if (input == "Return")
        {
            UserInterface.GeneralMenu(id);
        }
    }

    public static string InputMovie(string id)
    {
        Helper.WriteInCenter(Graphics.cinemacustom);
        while (true) {
        char genreFilterInput = Helper.ReadInput((char c) => c == '1' || c == '2',
            "Movie Options", "1. Select Screening\n2. Go Back");

        switch (genreFilterInput) {
            case '1':
                return "Select";
            case '2':
                return "Return";
            }
        }
    }
    
    public static void ScreeningSelect(List<Screening> screenings, string id)
    {
        Console.Clear();

        Screening? chosenScreening = null;
        do{
        string screeningIndex = Helper.GetValidInput("Please type screening number: ", Helper.IsNotNull);
        foreach (Screening screening in screenings)
        {
            if (screenings.IndexOf(screening) + 1 == Convert.ToInt32(screeningIndex))
            {
                chosenScreening = screening;
            }
        }
        } while (chosenScreening == null);

        User IdCheck = User.GetUserWithValue("ID", id);
        if (IdCheck.Admin) AdminController.AdminScreeningInterface(chosenScreening, id); 
        else ScreeningInterface(chosenScreening, id);
    }

    private static void ScreeningInterface(Screening screening, string id)
    {
        Console.WriteLine($"Date and Time: {screening.ScreeningDateTime, -40:dd-MM-yyyy HH:mm} | Auditorium: {screening.AssignedAuditorium.ID}");
        string input = InputScreening(id);
        if (input == "Return")
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Movie movie = JsonHandler.Get<Movie>(screening.MovieID, "Model/MovieDB.json");
#pragma warning disable CS8604 // Possible null reference argument.
            MovieInterface(movie, id);
        }
        else
        {
            ReserveSeats(screening, id);
        } 
    }

    public static string InputScreening(string id)
    {
        while (true) {
        char genreFilterInput = Helper.ReadInput((char c) => c == '1' || c == '2',
            "Movie Options", "1. Reserve Seats\n2. Go Back");

        switch (genreFilterInput) {
            case '1':
                return "Reserve";
            case '2':
                return "Return";
            }
        }
    }

    public static void ReserveSeats(Screening screening, string id)
    {
        User user = User.GetUserWithValue("ID", id);


        IEnumerable<string> reservedSeatIDs = Graphics.AuditoriumView(screening, user);
        //priceCalc will be done through AuditoriumView

        
        Reservation newReservation = new Reservation(reservedSeatIDs.ToList(), screening.ID, 20);
        User.UpdateUserWithValue(user, "Reservations", newReservation);

        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            Interface.GeneralMenu();
        }
    }


}