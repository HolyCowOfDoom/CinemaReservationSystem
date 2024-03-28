class Interface{
    // Als het menu word geroepen moet er gekeken worden of er al is ingelogd. Zo ja, id erbij callen.
    // Reserve seats is een wip
    // Inloggen kan alleen dmv Register User. Bij inloggen word id 100 toegekend, hierdoor werkt viewuser niet en kan je verder weinig laten zien
    // ViewMovies is nu nog hetzelfde bij wel of niet inloggen.
    public static void GeneralMenu(){
        char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3', 
        "General Menu",  "1. View all movies\n 2. Register\n 3. Log in");
            if(DigitInput == '1'){
                InterfaceController.ViewMovies();
            }
            else if(DigitInput == '2'){
                InterfaceController.RegisterUser();
            }
            else if(DigitInput == '3'){
                InterfaceController.LogIn();
            }
            else{
                GeneralMenu();
            }
        }

    public static void GeneralMenu(int id){
        char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4',
        "General Menu",  "1. View all movies / Reserve seats\n 2. See profile\n 3. Log out");
            if(DigitInput == '1'){
                InterfaceController.ViewMovies(id); 
                // called overloaded versie van ViewMovies, kan dus gebruikt worden om
                // seats te reserveren en op te slaan in de id van een employee. 
            }
            else if (DigitInput == '2'){
                InterfaceController.ViewUser(id);
            }
            else if (DigitInput == '3'){
                InterfaceController.LogOut();
                // Heeft geen id nodig, want called GeneralMenu zonder id.
            }
            else{
                GeneralMenu(id);
        }
    }   
}