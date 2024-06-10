using System.Globalization;

public class AdminInterfaceController
{
    public static void CreateMovie(string id)
    {
        string title = Helper.GetValidInput("Please input movie title:", Helper.IsNotNull);
        string description = Helper.GetValidInput("Please add movie description:", Helper.IsNotNull);
        int ageRating = Convert.ToInt32(Helper.GetValidInput("Please add movie age rating:", Helper.IsValidAgeRating));
        string genre = Helper.GetValidInput("Please add a genre to the movie:", Helper.IsNotNull);

        Movie addedMovie = new Movie(title, ageRating, description, genre);

        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            AdminInterface.GeneralMenu(id);
        }
    }

    public static void AddScreening(Movie movie, string id)
    {
        Console.WriteLine("press a to automatically add screenings or c to continue (max 10)");
        ConsoleKeyInfo key = Console.ReadKey();
        switch (key.Key)
        {
            case ConsoleKey.A:
            Console.WriteLine("Enter amount of screenings to add per movie");
            int amountpermovie = Convert.ToInt32(Console.ReadLine());
            AddScreeningsAuto(amountpermovie);
            AdminInterface.GeneralMenu(id);
            break;
            case ConsoleKey.Escape:
            Helper.ConsoleClear();
            AdminInterface.GeneralMenu(id);
            break;
            case ConsoleKey.Home:
            Helper.HandleHomeKey(id);
            break;
            case ConsoleKey:
            Helper.ConsoleClear();
            UserInterfaceController.PrintScreeningsAdmin(movie);
            AddScreening(movie, id);
            break; 
        }
        // char autoInput = Helper.ReadInput((char c) => c == 'a' || c == 'c' || c);
        // if (autoInput == 'a')
        // {
        //     Console.WriteLine("Enter amount of screenings to add per movie");
        //     int amountpermovie = Convert.ToInt32(Console.ReadLine());
        //     AddScreeningsAuto(amountpermovie);
        //     AdminInterface.GeneralMenu(id);
        // }
        Auditorium? screeningAud;
        do
        {
        string auditID = Helper.GetValidInput("Please input valid auditorium ID related to the screening:", Helper.IsNotNull);
        screeningAud = JsonHandler.Get<Auditorium>(auditID, "Data/AuditoriumDB.json");
        } while (screeningAud == null);
        string dateTimeString = Helper.GetValidInput("Please input screening date: <DD-MM-YYYY HH:MM>", Helper.IsValidDT);
        DateTime screeningDT = DateTime.ParseExact(dateTimeString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
        MovieDataController.AddScreening(movie, screeningAud, screeningDT);
        string addToBundle = Helper.GetValidInput("Would you like to add the screening to a bundle? (y/n)", (c) => c == "y" || c == "n");
        if(addToBundle == "y")
        {
            Console.WriteLine("We've no implemented bundles yet!");
            //print bundles
            //allow selecting from bundles
            //add to selected bundle
        }
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            AdminInterface.GeneralMenu(id);
        }
    }
    public static void AddScreeningsAuto(int amountpermovie)
    {
        int progress = 0;
        if (amountpermovie > 10) amountpermovie = 10;
        List<Movie> movies = JsonHandler.Read<Movie>("Data/MovieDB.json");
        Random rand = new Random();

        foreach (Movie movie in movies)
        {
            for (int i = 0; i < amountpermovie; i++)
            {
                List<Screening>? allScreenings = JsonHandler.Read<Screening>("Data/ScreeningDB.json");
                string auditoriumID = rand.Next(1, 4).ToString();
                Auditorium screeningAud = JsonHandler.Get<Auditorium>(auditoriumID, "Data/AuditoriumDB.json");
                DateTime randomDateTime = GetRandomDateTime(rand);
                while (allScreenings.Any(s => s.ScreeningDateTime.Date == randomDateTime.Date &&
                                            s.ScreeningDateTime.Hour == randomDateTime.Hour &&
                                            s.AssignedAuditorium.ID == auditoriumID))
                {
                    randomDateTime = randomDateTime.AddHours(3);

                    // If it goes beyond the allowed time (midnight), adjust it to the next day at 08:00
                    if (randomDateTime.Hour >= 24)
                    {
                        randomDateTime = randomDateTime.Date.AddDays(1).AddHours(8);
                    }
                }

                MovieDataController.AddScreening(movie, screeningAud, randomDateTime);
                
            }    
            progress++;
            Graphics.DrawLoadingBar(progress, movies.Count);
        }
        Thread.Sleep(1000);
        Helper.ConsoleClear();
    }

    public static DateTime GetRandomDateTime(Random random)
    {
        DateTime now = DateTime.Now;
        DateTime twoMonthsFromNow = now.AddMonths(2);

        int totalDaysRange = (twoMonthsFromNow - now).Days;

        int randomDaysOffset = random.Next(0, totalDaysRange + 1);
        DateTime randomDate = now.AddDays(randomDaysOffset);

        // Ensure the time is between 08:00 and 23:59
        int startHour = 8;
        int endHour = 23;
        int hour = random.Next(startHour, endHour + 1);

        DateTime randomDateTime = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day, hour, 0, 0);

        return randomDateTime;
    }

    public static void RegisterAdmin(string fid)
    {
        InterfaceController.RegisterUser(admin: true, id: fid);
    }

    public static void HandleAdminSwitch(string fid, string nid)
    {
        User firstuser = UserDataController.GetUserWithValue("ID", fid);
        User newuser = UserDataController.GetUserWithValue("ID", nid);
        while(true){
        char FilterInput = Helper.ReadInput((char c) => c == '1' || c == '2',
        "What account do you want to use?",  $"1. {firstuser.Name} (Current user)\n2. {newuser.Name} (New user)");
        switch(FilterInput)
            {
                case '1':
                    AdminInterface.GeneralMenu(fid);
                    break;
                case '2':
                    AdminInterface.GeneralMenu(nid);
                    break;
            }
        }
    }
    
    public static void LogOut(){
        Console.WriteLine("You have been succesfully logged out");
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            Interface.GeneralMenu();
        }
    }

    public static void AdminMovieInterface(Movie movie, string id)
    {
        Console.Clear();
        Console.WriteLine($"Title: {movie.Title,-40} | Age Rating: {movie.AgeRating,-3} | Description: {movie.Description}");
        List<Screening> screenings = MovieDataController.GetAllMovieScreenings(movie);
        foreach (Screening screening in screenings)
        {
            Console.WriteLine($"Date and Time: {screening.ScreeningDateTime, -40:dd-MM-yyyy HH:mm} | Auditorium: {screening.AssignedAuditorium.ID}");
        }
        string input = AdminInputMovie(id);
        switch (input) {
            case "Select":
                UserInterfaceController.ScreeningSelect(movie, id);
                break;
            case "Add":
                AddScreening(movie, id);
                break;
            case "Return":
                AdminInterface.GeneralMenu(id);
                break;
            }
    }



    public static string AdminInputMovie(string id)
    {
        while (true) {
        char genreFilterInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3',
            "Movie Options", "1. Select Screening\n2. Add Screening\n3. Go Back");

        switch (genreFilterInput) {
            case '1':
                return "Select";
            case '2':
                return "Add";
            case '3':
                return "Return";
            }
        }
    }

    public static void AdminScreeningInterface(Screening screening, string id)
    {
        Console.WriteLine("Reserve seats and display audit plan in this menu...");
        Console.WriteLine("Press x to go back to the main menu");
        char specificLetterInput = Helper.ReadInput((char c) => c == 'x');
        if (specificLetterInput == 'x'){
            AdminInterface.GeneralMenu(id);
        }
    }

    
}