using System.Runtime.InteropServices;

public class UserInterface
{
    public static void GeneralMenu(string id){
        char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4',
        "General Menu",  "1. View all movies / Reserve seats\n2. Filter Movies\n3. See profile\n4. Log out");
        switch(DigitInput) 
        {
            case '1':
                UserController.ViewMovies(id);
                break;
                // called overloaded versie van ViewMovies, kan dus gebruikt worden om
                // seats te reserveren en op te slaan in de id van een employee.
            case '2':
                char FilterOption = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4',
                "Filter options",  "1. Filter movies by the Age Rating\n 2. Filter Movies by Genre\n 3. Filter Movies by both\n 4. Exit Menu");
                if(FilterOption == '1')
                {
                    string option = "Age"; 
                    UserController.FilterMovies(id, option);
                    break; 
                }
                else if(FilterOption == '2')
                {
                    string option = "Genre";
                    UserController.FilterMovies(id, option);
                    break;
                }
                else if(FilterOption == '3')
                {
                    string option = "Both";
                    UserController.FilterMovies(id, option);
                    break;
                }
                else if(FilterOption == '4') 
                {
                    GeneralMenu(id);
                    break;
                }
                else{
                    break;
                }
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