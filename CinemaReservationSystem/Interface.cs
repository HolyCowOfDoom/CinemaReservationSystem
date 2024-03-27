class Interface{
    // Eerste keer dat Menu word geroepen moet de bool op False beginnen.
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
            }
            else if (DigitInput == '2'){
                InterfaceController.ViewUser(id);
            }
            else if (DigitInput == '3'){
                InterfaceController.LogOut();
            }
            else{
                GeneralMenu(id);
        }
    }   
}