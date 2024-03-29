public class AdminInterface
{
    public static void GeneralMenu(int id){
        char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4' || c == '5',
        "General Menu",  "1. View all movies / Reserve seats\n 2. See profile\n 3. Log out\n 4. Create Movie (ADMIN)\n 5. Add Screening (ADMIN)");
        switch(DigitInput) 
        {
            case '1':
                UserController.ViewMovies(id);
                break;
                // called overloaded versie van ViewMovies, kan dus gebruikt worden om
                // seats te reserveren en op te slaan in de id van een employee. 
            case '2':
                UserController.ViewUser(id);
                break;
            case '3':
                UserController.LogOut();
                break;
                // Heeft geen id nodig, want called GeneralMenu zonder id.
            case '4':
                AdminController.CreateMovie(id);
                break;
            case '5':
                AdminController.AddScreening(id);
                break;
            default:
                GeneralMenu(id);
                break;
        }
    }  
}