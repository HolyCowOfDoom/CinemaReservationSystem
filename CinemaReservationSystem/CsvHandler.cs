using CsvHelper;
public static class CsvHandler{
    static string UserDBFilePath = "../UserDB.csv";
    private static List<User> Users; //we could avoid using this list altogether by using static methods in User.cs together with methods below (I already tried, it does make things more complicated)
    //but this list essentially doubles the size of our db so it might be worth it? 
    //(we could manipulate the db directly with user input in static methods in User.cs rather than through a User obj)
    public static bool LoadUsers(){
        //Users =   ;
        return true;
    }
    public static bool AddUser(User user){

        return true;
    }

    public static bool UpdateUser(User user){
        //rewrite csv.user -> user
        return true;
    }

    public static User GetUserByID(int id)
    {
        // string name = GetNameFromID(id);
        // string birthDate = GetBirthDateFromID(id);
        // string email = GetEmailFromID(id);
        // string password = GetPasswordFromID(id);
        //User user = new User(id, name, birthDate, email, password);
        return Users.Find(user => user.ID == id);
    }

    public static int GetIDFromName(string name)
    {
        try{
            int id = 0;
            //load from csv/Users
            return id;
        } catch(Exception e){ //catch specific csv exceptions
            Console.WriteLine(e);
        }
        return -1;
    }
    //add try-catch to functions below (or is try-catch not even necessary at all?)

    public static string GetNameFromID(int id)
    {
        string name = "";
        //load from csv/Users
        return name;
    }

    public static string GetBirthDateFromID(int id)
    {
        string birthDate = "";
        //load from csv/Users
        return birthDate;
    }

    public static string GetEmailFromID(int id)
    {
        string email = "";
        //load from csv/Users
        return email;
    }

    public static string GetPasswordFromID(int id) //is this secure? rn callers must handle security/validation
    {
        string password = "";
        //load from csv/Users
        return password;
    }

    public static bool SetNameOfID(int id, string name)
    {
        //update Users(?)
        //write to csv
        return true;
    }

    public static bool SetBirthDateOfID(int id, string birthDate)
    {
        //update Users(?)
        //write to csv
        return true;
    }

    public static bool SetEmailOfID(int id, string email)
    {
        //update Users(?)
        //write to csv
        return true;
    }

    public static bool SetPasswordOfID(int id, string password)
    {
        //update Users(?)
        //write to csv
        return true;
    }
   
}