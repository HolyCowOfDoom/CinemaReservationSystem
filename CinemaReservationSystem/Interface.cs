class Interface{
    // Eerste keer dat Menu word geroepen moet de bool op False beginnen.
    public static void GeneralMenu(bool login){
        if(!login){
            Console.WriteLine("General Menu\n 1. View all movies\n 2. Register \n 3. Log in\n");
            char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3', "Menu Options: 123");
            if(DigitInput == 1){
                InterfaceController.ViewMovies(login);
            }
            else if(DigitInput == 2){
                InterfaceController.RegisterUser();
            }
            else if(DigitInput == 3){
                InterfaceController.LogIn();
            }
            else{
                GeneralMenu(login);
            }
        }

        else{
            Console.WriteLine("General Menu\n 1. View all movies\n 2. Log out\n 3. View profile\n 4. Reserve seats");
            char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3' || c == '4', "Menu Options: 1234");
            if(DigitInput == 1){
                InterfaceController.ViewMovies(login);
            }
            else if (DigitInput == 2){
                InterfaceController.LogOut();
            }
            else if (DigitInput == 3){
                InterfaceController.ViewUser();
            }
            else if(DigitInput == 4){
                InterfaceController.ReserveSeats();
            }
            else{
                GeneralMenu(login);
            }
        }
    }
}