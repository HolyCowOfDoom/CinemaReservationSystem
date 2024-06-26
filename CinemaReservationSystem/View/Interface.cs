public abstract class Interface
{
    public static void GeneralMenu(){
        Helper.WriteInCenter(Graphics.cinemaVintage);
        char DigitInput = Helper.ReadInput((char c) => c == '1' || c == '2' || c == '3', 
        "General Menu",  "1. View all movies\n2. Register\n3. Log in");
        switch (DigitInput) 
        {
        case '1':
            UserInterfaceController.ViewMovies();
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
}