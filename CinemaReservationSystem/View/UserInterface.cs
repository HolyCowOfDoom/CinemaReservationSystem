public class UserInterface : Interface
{
    public static void GeneralMenu(string id){
        char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4' || c == '5',
        "General Menu",  "1. View all movies / Reserve seats\n2. Filter Movies\n3. See profile\n4. Magnetar\n5. Log out");
        switch(DigitInput) 
        {
            case '1':
                UserInterfaceController.ViewMovies(id);
                break;
            case '2':
                char FilterOption = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4' || c == '5',
                "Filter options", "1. Filter movies by the Age Rating\n2. Filter Movies by Genre\n3. Filter Movies by both\n4. Filter Movies by Favorites\n5. Exit Menu");
                if (FilterOption == '1')
                {
                    string option = "Age";
                    UserInterfaceController.FilterMovies(id, option);
                    break;
                }
                else if (FilterOption == '2')
                {
                    string option = "Genre";
                    UserInterfaceController.FilterMovies(id, option);
                    break;
                }
                else if (FilterOption == '3')
                {
                    string option = "Both";
                    UserInterfaceController.FilterMovies(id, option);
                    break;
                }
                else if (FilterOption == '4')
                {
                    string option = "Favorites";
                    UserInterfaceController.FilterMovies(id, option);
                    break;
                }
                else if (FilterOption == '5')
                {
                    GeneralMenu(id);
                    break;
                }
                else
                {
                    break;
                }
            case '3':
                UserInterfaceController.ViewUser(id);
                break;
            case '4':
                Magnetar game = new Magnetar(100);
                game.Play();
                Helper.ConsoleClear();
                GeneralMenu(id);
                break;
            case '5':
                UserInterfaceController.LogOut();
                break;
            default:
                GeneralMenu(id);
                break;
        }
    }  
}