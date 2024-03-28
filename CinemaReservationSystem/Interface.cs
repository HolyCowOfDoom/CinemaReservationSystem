using System.Collections;

class Interface{
    // Als het menu word geroepen moet er gekeken worden of er al is ingelogd. Zo ja, id erbij callen.
    // ^ dus GeneralMenu(id), makkelijkste manier is om te kijken of je een id meekrijgt met de call.
    // ViewMovies is nu nog hetzelfde bij wel of niet inloggen.
    public static void GeneralMenu(){
        char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3', 
        "General Menu",  "1. View all movies\n 2. Register\n 3. Log in");
        switch (DigitInput) 
            {
            case '1':
                InterfaceController.ViewMovies();
                break;
            case '2':
                InterfaceController.RegisterUser();
                break;     
            case '3':
                InterfaceController.LogIn();
                break;
            default:
                GeneralMenu();
                break;
            }
        }

    public static void GeneralMenu(int id){
        char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4' || c == '5',
        "General Menu",  "1. View all movies / Reserve seats\n 2. See profile\n 3. Log out\n 4. Create Movie (ADMIN)\n 5. Add Screening (ADMIN)");
        switch(DigitInput) 
        {
            case '1':
                InterfaceController.ViewMovies(id);
                break;
                // called overloaded versie van ViewMovies, kan dus gebruikt worden om
                // seats te reserveren en op te slaan in de id van een employee. 
            case '2':
                InterfaceController.ViewUser(id);
                break;
            case '3':
                InterfaceController.LogOut();
                break;
                // Heeft geen id nodig, want called GeneralMenu zonder id.
            case '4':
                InterfaceController.CreateMovie(id);
                break;
            case '5':
                InterfaceController.AddScreening(id);
                break;
            default:
                GeneralMenu(id);
                break;
        }
    }   
}