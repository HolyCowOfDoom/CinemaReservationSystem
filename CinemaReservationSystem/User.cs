using System.Reflection.Metadata.Ecma335;
using CsvHelper.Configuration.Attributes;

public static class User
{
    static string UserDBFilePath = "../UserDB.csv";
    // private readonly int _id;
    // //private string _name; 
    // //private string _birthDate;
    // //private string _email;
    // private string _password;

    // public int ID {get => _id; init => _id = value;}
    // public string Name {get; private set;} //we don't want _name to be able to be changed outside of this class
    // public string BirthDate {get; private set;}
    // public string Email {get; private set;}
    //private string Password {get; set;}
    //add custom set if allowing user to change password

   public static bool AddUser(string name, string birthDate, string email, string password)
    {
        CsvHandler.Write(UserDBFilePath);
        return true;
    }
    // public User(string name, string birthDate, string email, string password)
    // {
    //     //ID = CsvHandler.GetAmountOfUsers();
    //     _password = password;
    //     Name = name;
    //     BirthDate = birthDate;
    //     Email = email;
    //     CsvHandler.AddUser(this);
    // }
    // public User(int id, string name, string birthDate, string email, string password) : this(name, birthDate, email, password)
    // {
    //     ID = id;
    // }

    public static bool ValidatePassword(int id, string name, string password)
    {
        //int id = GetIDFromName(name);
        string userPassword = GetPasswordFromID(id); //gets directly from DB rather than Users List
        if(password == userPassword) return true;
        else return false;
    }

    public static bool ChangeName(int id, string newName){
        string currentName = GetNameFromID(id);
        if(newName == currentName){
            Console.WriteLine($"User's name and entered name({newName}) are identical");
            return false;
        }
        else if(string.IsNullOrWhiteSpace(newName))
        {
            Console.WriteLine("Entered name was null or whitespace");
            return false;
        }
        else{
            CsvHandler.Write(UserDBFilePath);
            //SetNameOfID(id, newName);
            return true;
        }
    }

    public static bool ChangeBirthDate(int id, string birthDate){
        string currentBirthDate = GetBirthDateFromID(id);
        if(birthDate == currentBirthDate){
            Console.WriteLine($"User's birth date and entered birth date({birthDate}) are identical");
            return false;
        }
        else if(string.IsNullOrWhiteSpace(birthDate))
        {
            Console.WriteLine("Entered birth date was null or whitespace");
            return false;
        }
        else{
            CsvHandler.Write(UserDBFilePath);
            //SetBirthDateOfID(id, birthDate);
            return true;
        }
    }

    public static bool ChangeEmail(int id, string email){
        string currentEmail = GetEmailFromID(id);
        if(email == currentEmail){
            Console.WriteLine($"User's email and entered email({email}) are identical");
            return false;
        }
        else if(string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Entered email was null or whitespace");
            return false;
        }
        else{
            CsvHandler.Write(UserDBFilePath);
            //SetEmailOfID(id, email);
            return true;
        }
    }

    public static int GetIDFromName(string name)
    {
        try{
            int id = 0;
            //load from csv/Users
            CsvHandler.Read(UserDBFilePath);
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
        CsvHandler.Read(UserDBFilePath);
        return name;
    }

    public static string GetBirthDateFromID(int id)
    {
        string birthDate = "";
        //load from csv/Users
        CsvHandler.Read(UserDBFilePath);
        return birthDate;
    }

    public static string GetEmailFromID(int id)
    {
        string email = "";
        //load from csv/Users
        CsvHandler.Read(UserDBFilePath);
        return email;
    }

    public static string GetPasswordFromID(int id) //is this secure? rn callers must handle security/validation
    {
        string password = "";
        //load from csv/Users
        CsvHandler.Read(UserDBFilePath);
        return password;
    }

    // sets maybe not necessary anymore
    // public static bool SetNameOfID(int id, string name)
    // {
    //     //update Users(?)
    //     //write to csv
    //     return true;
    // }

    // public static bool SetBirthDateOfID(int id, string birthDate)
    // {
    //     //update Users(?)
    //     //write to csv
    //     return true;
    // }

    // public static bool SetEmailOfID(int id, string email)
    // {
    //     //update Users(?)
    //     //write to csv
    //     return true;
    // }

    // public static bool SetPasswordOfID(int id, string password)
    // {
    //     //update Users(?)
    //     //write to csv
    //     return true;
    // }
}