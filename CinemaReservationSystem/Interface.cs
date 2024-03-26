class Interface{
    // Eerste keer dat Menu word geroepen moet de bool op False beginnen.
    public static void GeneralMenu(bool login){
        if(!login){
            char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3', 
            "General Menu",  "1. View all movies\n 2. Register\n 3. Log in");
            if(DigitInput == '1'){
                InterfaceController.ViewMovies(login);
            }
            else if(DigitInput == '2'){
                InterfaceController.RegisterUser();
            }
            else if(DigitInput == '3'){
                InterfaceController.LogIn();
            }
            else{
                GeneralMenu(login);
            }
        }

        else{
            char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4',
            "General Menu\n 1. View all movies\n 2. Register \n 3. Log in\n", "Menu Options: 1234");
            if(DigitInput == '1'){
                InterfaceController.ViewMovies(login);
            }
            else if (DigitInput == '2'){
                InterfaceController.LogOut();
            }
            else if (DigitInput == '3'){
                InterfaceController.ViewUser();
            }
            else if(DigitInput == '4'){
                InterfaceController.ReserveSeats();
            }
            else{
                GeneralMenu(login);
            }
        }
    }
}