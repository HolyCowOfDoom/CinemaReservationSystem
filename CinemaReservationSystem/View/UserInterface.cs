using System.Runtime.InteropServices;

public class UserInterface
{
    public static void GeneralMenu(string id){
        char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4',
        "General Menu",  "1. View all movies / Reserve seats\n2. Filter Movies\n3. See profile\n4. Log out");
        switch(DigitInput) 
        {
            case '1':
                UserInterfaceController.ViewMovies(id);
                break;
                // called overloaded versie van ViewMovies, kan dus gebruikt worden om
                // seats te reserveren en op te slaan in de id van een employee.
            case '2':
                char FilterOption = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4',
                "Filter options",  "1. Filter movies by the Age Rating\n2. Filter Movies by Genre\n3. Filter Movies by both\n4. Exit Menu");
                if(FilterOption == '1')
                {
                    string option = "Age"; 
                    UserInterfaceController.FilterMovies(id, option);
                    break; 
                }
                else if(FilterOption == '2')
                {
                    string option = "Genre";
                    UserInterfaceController.FilterMovies(id, option);
                    break;
                }
                else if(FilterOption == '3')
                {
                    string option = "Both";
                    UserInterfaceController.FilterMovies(id, option);
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
                UserInterfaceController.ViewUser(id);
                break;
            case '4':
                UserInterfaceController.LogOut();
                break;
                // Heeft geen id nodig, want called GeneralMenu zonder id.
            default:
                GeneralMenu(id);
                break;
        }
    }  
}