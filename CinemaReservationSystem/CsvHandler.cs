using CsvHelper;
public static class CsvHandler{
    static string UserDBFilePath = "../UserDB.csv";
    //private static List<User> Users; //we could avoid using this list altogether by using static methods in User.cs together with methods below (I already tried, it does make things more complicated)
    //but this list essentially doubles the size of our db so it might be worth it? 
    //(we could manipulate the db directly with user input in static methods in User.cs rather than through a User obj)
    public static bool LoadUsers(){
        //Users =   ;
        return true;
    }
 
    public static bool Write()
    {
        return true;
    }

    public static bool Read()
    {
        return true;
    }

   
}