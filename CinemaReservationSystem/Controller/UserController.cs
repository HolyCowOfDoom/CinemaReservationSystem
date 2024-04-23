using System.Collections;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;
using CsvHelper.Configuration.Attributes;

public class UserController
{
    public static void LogOut(){
        // (optioneel) een method voor de logica om te zorgen dat je niet meer bij login only data kan.
        Console.WriteLine("You have been succesfully logged out");
        XToGoBack();
    }

    public static void ViewMovies(string id){
        List<Movie> Movies = JsonHandler.Read<Movie>("Model/MovieDB.json");
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
                    if (id.StartsWith("admin-")) AdminInterface.GeneralMenu(id);
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
                    if (id.StartsWith("admin-")) AdminInterface.GeneralMenu(id);
                    else UserInterface.GeneralMenu(id);    
                }   
            }
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
        Console.WriteLine("┌────────────────────────────────┬───────────────────────────────────────┬────────────────────────┐");
        Console.WriteLine($"│ Username: {user.Name,-20} │ Email: {user.Email,-30} │ Birth date: {user.BirthDate} │");
        Console.WriteLine("└────────────────────────────────┴───────────────────────────────────────┴────────────────────────┘");
        foreach (Reservation reservation in user.Reservations)
        {
            Console.WriteLine($"Reservation Price: {reservation.TotalPrice}; Seats: {string.Join(" ", reservation.SeatIDs)}");
        }
        XToGoBack(id);
    }

        // Console.WriteLine("┌───────────────────────────────┬───────────────────────────────────────┬────────────────────────┬");
        // Console.WriteLine($"│ Username: {user.Name,-20} │ Email: {user.Email,-30} │ Birth date: {user.BirthDate} │ User type: {user.uType} "); uType print welk user een customer of admin is.
        // Console.WriteLine("└───────────────────────────────┴───────────────────────────────────────┴────────────────────────┴");

    private static void WouldYouLikeToSearch(string id)
    {
        Console.WriteLine("Would you like to select a movie? Y / N");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'y' || c == 'n');
        if (specificLetterInput == 'y'){
            SelectMovie(id);
        }
        UserInterface.GeneralMenu(id);
    }

    private static void SelectMovie(string id)
    {
        Movie? movie;
        do{
        string movieName = Helper.GetValidInput("Please type movie title: ", Helper.IsNotNull);
        movie = JsonHandler.GetByMovieName(movieName);
        } while (movie == null);
        if (id.StartsWith("admin-")) AdminController.AdminMovieInterface(movie, id);
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

        if (id.StartsWith("admin-")) AdminController.AdminScreeningInterface(chosenScreening, id);
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
        int seatAmount = Convert.ToInt32(Helper.GetValidInput("Please enter the amount of seats you'd like to reserve: ", Helper.IsNotNull));
        List<string> reservedSeatIDs = [];
        for (int i = 0; i < seatAmount; i++)
        {
            while(true)
            {
                string seatID = Helper.GetValidInput("Please enter valid seat number: ", Helper.IsNotNull);
                bool succesfullReserve = screening.ReserveSeat(seatID);
                if (succesfullReserve) {
                    Console.WriteLine("UserController.cs: Seat was not reserved, but is now");
                    reservedSeatIDs.Add(seatID);
                    break;
                }
                //else if (check if there's even any seats available) break;
                else
                {
                    Console.WriteLine("UserController.cs: Seat was already reserved!");
                }
            }
        }

        //insert price calculation here>
        
        User user = User.GetUserWithValue("ID", id);
        Reservation newReservation = new Reservation(reservedSeatIDs, screening.ID, 20);
        User.UpdateUserWithValue(user, "Reservations", newReservation);

        XToGoBack(id);
    }

    private static void XToGoBack(string id)
    {
        InterfaceController.XToGoBack(id);
    }

    private static void XToGoBack()
    {
        InterfaceController.XToGoBack();
    }

}