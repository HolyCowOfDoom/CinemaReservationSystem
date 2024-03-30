public class UserInterface
{
    public static void GeneralMenu(int id){
        char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4',
        "General Menu",  "1. View all movies / Reserve seats\n 2. Filter Movies\n 3. See profile\n 4. Log out");
        switch(DigitInput) 
        {
            case '1':
                UserController.ViewMovies(id);
                break;
                // called overloaded versie van ViewMovies, kan dus gebruikt worden om
                // seats te reserveren en op te slaan in de id van een employee.
            case '2':
                UserController.FilterMovies(id);
                break; 
            case '3':
                UserController.ViewUser(id);
                break;
            case '4':
                UserController.LogOut();
                break;
                // Heeft geen id nodig, want called GeneralMenu zonder id.
            default:
                GeneralMenu(id);
                break;
        }
    }  
}