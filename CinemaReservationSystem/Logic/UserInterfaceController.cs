using System.Collections;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using CsvHelper.Configuration.Attributes;

public class UserInterfaceController
{
    private const int batchSize = 20;
    private static List<Movie>? Movies;
    private static List<Screening>? Screenings;
    private static List<Screening>? loadedScreenings;
    private static int totalcount;
    private static int currentIndex = 0;
    private static int selectedIndex = 0;
    public static void LogOut(){
        Interface.GeneralMenu();
    }

    public static void ViewMovies(string id="not logged in")
    {
        Helper.ConsoleClear();
        Console.CursorVisible = false;
        totalcount = JsonHandler.Read<Movie>("Data/MovieDB.json").Count;
        LoadNextMovies();

        ConsoleKeyInfo key;
        do
        {
            PrintMovies();

            key = Console.ReadKey(true);

            key = HandleUserViewMovieInput(key);

            Helper.ConsoleClear();
        } while (key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Home);
        if (key.Key == ConsoleKey.Escape)
        {
            ResetFields();
            if (!string.Equals(id, "not logged in"))
                UserInterface.GeneralMenu(id);
            else Interface.GeneralMenu();
        }
        else if (key.Key == ConsoleKey.Enter)
        {
            Movie movie = Movies[selectedIndex];
            if (string.Equals(id, "not logged in"))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                char confirm = Helper.ReadInput((char c) => c == 'y' || c == 'n', "Not logged in",
                                                "Login to reserve seats for a movie? Y/N");
                Console.ResetColor();

                if (confirm == 'y')
                {
                    ResetFields();
                    InterfaceController.LogIn(true, movie);
                }
                else ViewMovies();
            }
            ResetFields();
            ScreeningSelect(movie, id);
        }
        else if (key.Key == ConsoleKey.Home)
        {
            ResetFields();
            Helper.HandleHomeKey(id);
        }
    }

    private static ConsoleKeyInfo HandleUserViewMovieInput(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (selectedIndex > 0)
                    selectedIndex--;
                else
                {
                    selectedIndex = totalcount - 1;
                    LeftArrowPress();
                    LoadNextMovies();
                }
                break;
            case ConsoleKey.DownArrow:
                if (selectedIndex < Movies.Count - 1)
                    selectedIndex++;
                else
                {
                    selectedIndex = 0;
                    RightArrowPress();
                    LoadNextMovies();
                }
                break;
            case ConsoleKey.LeftArrow:
                LeftArrowPress();
                LoadNextMovies();
                break;
            case ConsoleKey.RightArrow:
                RightArrowPress();
                LoadNextMovies();
                break;
            case ConsoleKey.Home:
                return key;
            case ConsoleKey.Enter:
                return key;
            case ConsoleKey.Escape:
                return key;
        }
        if (selectedIndex >= Movies.Count)
            selectedIndex = Movies.Count - 1;
        return key;
    }

    private static void LeftArrowPress()
    {
        if (currentIndex > 0)
        {
            currentIndex -= batchSize;
            if (currentIndex < 0)
                currentIndex = 0;
        }
        else
        {
            currentIndex = totalcount - (totalcount % batchSize);
        }
    }

    private static void RightArrowPress()
    {
        currentIndex += batchSize;
        if (currentIndex >= totalcount)
            currentIndex = 0;
    }

    private static void ResetFields()
    {
        Movies = null;
        Screenings = null;
        loadedScreenings = null;
        currentIndex = 0;
        selectedIndex = 0;
    }

    private static void LoadNextMovies()
    {
        Movies = JsonHandler.Read<Movie>("Data/MovieDB.json").Skip(currentIndex).Take(batchSize).ToList();
    }

    private static void PrintMovies()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Use arrow keys to move up, down and use left, right to load the next batch of movies.");
        Console.ResetColor();
        Console.WriteLine("┌──────┬──────────────────────────────────────────┬─────────────┬─────────────┬──────────────────────────────────────────────────────────────┐");
        Console.WriteLine($"│ ID   │ {"Title",-40} │ {"Age Rating",-11} │ {"Genre",-11} │ {"Description",-60} │");
        Console.WriteLine("├──────┼──────────────────────────────────────────┼─────────────┼─────────────┼──────────────────────────────────────────────────────────────┤");
        for (int i = 0; i < Movies.Count; i++)
        {
            if (i == selectedIndex)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.WriteLine($"│ {currentIndex + i + 1,-4} │ {Movies[i].Title,-40} │ {Movies[i].AgeRating,-11} │ {Movies[i].Genre,-11} │ {Movies[i].Description,-60} │");
            Console.ResetColor();
        }
        Console.WriteLine("└──────┴──────────────────────────────────────────┴─────────────┴─────────────┴──────────────────────────────────────────────────────────────┘");
        Console.WriteLine($"Page {(currentIndex / batchSize) + 1} of {totalcount/batchSize + 1}");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("ESC to go back, HOME to return to main menu, or ENTER to select movie.");
        Console.ResetColor();
    }
    public static void FilterMovies(string id, string option)
    {
        // char genreFilterInput;
        List<Movie> Movies = JsonHandler.Read<Movie>("Data/MovieDB.json");
        User user = UserDataController.GetUserWithValue("ID", id);
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
                    User IdCheck = UserDataController.GetUserWithValue("ID", id);
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
                    User IdCheck = UserDataController.GetUserWithValue("ID", id);
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
        User user = UserDataController.GetUserWithValue( "ID", id);
        Console.WriteLine("┌────────────────────────────────┬───────────────────────────────────────┬────────────────────────┬─────────┐");
        Console.WriteLine($"│ Username: {user.Name,-20} │ Email: {user.Email,-30} │ Birth date: {user.BirthDate} │ Age: {Helper.GetUserAge(user),-2} │");
        Console.WriteLine("└────────────────────────────────┴───────────────────────────────────────┴────────────────────────┴─────────┘");
        Console.WriteLine("ALL RESERVATIONS");
        int index = 0;
        Console.WriteLine("┌───┬──────────────────────────────────────────────────────┬───────────────────────────┬───────────────┬───────────────────────┬───────────────────┐");
        foreach (Reservation reservation in user.Reservations)
        {
            index++;
            Movie movie = GetMovieByID(reservation.ScreeningID);
            Screening screening = GetScreeningByID(reservation.ScreeningID);
            Console.WriteLine($"│ {index} │ Movie name: {movie.Title,-40} │ Date: {screening.ScreeningDateTime, -16} │ Auditorium: {screening.AssignedAuditorium.ID} │ Reservation Price: {reservation.TotalPrice} │ Seats: {string.Join(" ", reservation.SeatIDs), -10} │");
        }
        Console.WriteLine("└───┴──────────────────────────────────────────────────────┴───────────────────────────┴───────────────┴───────────────────────┴───────────────────┘");
        string userInput = UserMenu();
        if (userInput == "Return"){
            UserInterface.GeneralMenu(id);
        }
        else if (userInput == "Cancel")
        {
            CancelReservation(id, user);
        }
    }

    public static string UserMenu()
    {
        while(true)
        {
            char FilterInput = Helper.ReadInput((char c) => c == '1' || c == '2',
            "User Options",  "1. Cancel Reservation\n2. Go Back");
            switch(FilterInput)
            {
                case '1':
                    return "Cancel";
                case '2':
                    return "Return";
            }
        }
    }

    public static void CancelReservation(string id, User user)
    {
         int index = 0;
        Console.WriteLine("┌───┬──────────────────────────────────────────────────────┬────────────────────────────────────┬───────────────┬───────────────────────┬───────────────────┐");
        foreach (Reservation reservation in user.Reservations)
        {
            index++;
            Movie movie = GetMovieByID(reservation.ScreeningID);
            Screening screening = GetScreeningByID(reservation.ScreeningID);
            Console.WriteLine($"│ {index} │ Movie name: {movie.Title,-40} │ Screening Date: {screening.ScreeningDateTime, -16} │ Auditorium: {screening.AssignedAuditorium.ID} │ Reservation Price: {reservation.TotalPrice} │ Seats: {string.Join(" ", reservation.SeatIDs), -10} │");
        }
        Console.WriteLine("└───┴──────────────────────────────────────────────────────┴────────────────────────────────────┴───────────────┴───────────────────────┴───────────────────┘");
        Console.WriteLine("Please choose the reservation you'd like to cancel: ");
        int userInput = Convert.ToInt32(Console.ReadLine());
        Reservation reservationToCancel = user.Reservations[userInput - 1];
        Console.Clear();
        char userConfirmation = Helper.ReadInput((char c) => c == 'y' || c == 'n', "Are you sure?", "Y/N");
        if (userConfirmation == 'n')
        {
            ViewUser(id);
        }
        else if (userConfirmation == 'y')
        {
            Screening screening = GetScreeningByID(reservationToCancel.ScreeningID);
            foreach (string seatID in reservationToCancel.SeatIDs)
            {
                ScreeningDataController.CancelSeat(screening, seatID);
            }
            List<Reservation> newReservations = new List<Reservation>(user.Reservations);
            newReservations.Remove(reservationToCancel);
            UserDataController.UpdateUserWithValue<List<Reservation>>(user, "Reservations", newReservations);

            ViewUser(id);
        }
    }

    public static Movie? GetMovieByID(string screeningID)
    {
        List<Movie> MovieList = JsonHandler.Read<Movie>("Data/MovieDB.json");
        foreach(Movie movie in MovieList)
        {
            if (movie.ScreeningIDs.Contains(screeningID)) return movie;
        }
        return null;
    }

    public static Screening? GetScreeningByID(string screeningID)
    {
        List<Screening> ScreeningList = JsonHandler.Read<Screening>("Data/ScreeningDB.json");
        foreach(Screening screening in ScreeningList)
        {
            if (screening.ID == screeningID) return screening;
        }
        return null;
    }

    public static void ScreeningSelect(Movie movie, string id)
    {
        Helper.ConsoleClear();
        Console.CursorVisible = false;

        Screenings = MovieDataController.GetAllMovieScreenings(movie);
        totalcount = Screenings.Count;

        ConsoleKeyInfo key;
        do
        {
            LoadScreenings();
            PrintScreenings(movie);

            key = Console.ReadKey(true);

            key = HandleUserSelectScreeningInput(key);

            Helper.ConsoleClear();
        } while (key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Home);
        if (key.Key == ConsoleKey.Escape)
        {
            ResetFields();
            ViewMovies(id);
        }
        else if (key.Key == ConsoleKey.Enter)
        {
            HandleScreeningSelectEnter(movie, id);
        }
        else if (key.Key == ConsoleKey.Home)
        {
            ResetFields();
            Helper.HandleHomeKey(id);
        }     
    }

    private static void LoadScreenings()
    {
        loadedScreenings = Screenings.Skip(currentIndex).Take(batchSize).ToList();
    }

    private static void HandleScreeningSelectEnter(Movie movie, string id)
    {
        try
        {
            Screening? chosenScreening = null;
            foreach (Screening screening in Screenings)
            {
                if (Screenings.IndexOf(screening) + 1 == selectedIndex + 1)
                {
                    chosenScreening = screening;
                }
            }
            ResetFields();
            ReserveSeats(chosenScreening, id);
        }
        catch (NullReferenceException)
        {
            ScreeningSelect(movie, id);
        }
    }

    private static void PrintScreenings(Movie movie)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Use arrow keys to move up, down and use left, right to load the next batch of screenings.");
        Console.ResetColor();
        Console.WriteLine("┌──────────────────────────────────────────┬────────────────┬─────────────┬───────────────────────────────────────────────────────────────────────────┐");
        Console.WriteLine($"│ {movie.Title,-40} │ {"Age rating: " + movie.AgeRating,-15}│ {movie.Genre,-11} │ {movie.Description,-74}│");
        Console.WriteLine("├──────┬───────────────────────────────────┼────────────────┼─────────────┴───────────────────────────────────────────────────────────────────────────┤");
        for (int i = 0; i < loadedScreenings?.Count; i++)
        {
            if (i == selectedIndex)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.WriteLine($"│ {currentIndex + i + 1,-4} | Date and Time: {loadedScreenings[i].ScreeningDateTime,-18:dd-MM-yyyy HH:mm} | Auditorium: {loadedScreenings[i].AssignedAuditorium.ID, -2} | {"|", 89}");
            Console.ResetColor();
        }
        Console.WriteLine("└──────┴───────────────────────────────────┴────────────────┴─────────────────────────────────────────────────────────────────────────────────────────┘");
        Console.WriteLine($"Page {(currentIndex / batchSize) + 1} of {totalcount / batchSize + 1}");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("ESC to go back, HOME to return to main menu, or ENTER to select screening to reserve seats.");
        Console.ResetColor();
    }
    private static ConsoleKeyInfo HandleUserSelectScreeningInput(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (selectedIndex > 0)
                    selectedIndex--;
                else
                {
                    selectedIndex = totalcount - 1;
                    LeftArrowPress();
                    LoadScreenings();
                }
                break;
            case ConsoleKey.DownArrow:
                if (selectedIndex < loadedScreenings.Count - 1)
                    selectedIndex++;
                else
                {
                    selectedIndex = 0;
                    RightArrowPress();
                    LoadScreenings();
                }
                break;
            case ConsoleKey.LeftArrow:
                LeftArrowPress();
                LoadScreenings();
                break;
            case ConsoleKey.RightArrow:
                RightArrowPress();
                LoadScreenings();
                break;
            case ConsoleKey.Home:
                return key;
            case ConsoleKey.Enter:
                return key;
            case ConsoleKey.Escape:
                return key;
        }
        if (selectedIndex >= loadedScreenings.Count)
            selectedIndex = loadedScreenings.Count - 1;
        return key;
    }

    public static void ReserveSeats(Screening screening, string id)
    {
        User user = UserDataController.GetUserWithValue("ID", id);


        IEnumerable<string> reservedSeatIDs = Graphics.AuditoriumView(screening, user);
        //priceCalc will be done through AuditoriumView

        
        Reservation newReservation = new Reservation(reservedSeatIDs.ToList(), screening.ID, 20);
        UserDataController.AddValueToUser(user, "Reservations", newReservation);

        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            UserInterface.GeneralMenu(user.ID);
        }
    }
}