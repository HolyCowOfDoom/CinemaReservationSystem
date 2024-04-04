using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using CsvHelper.Configuration.Attributes;

public class User : IEquatable<User>
{
    static string UserDBFilePath = Path.GetFullPath("UserDB.csv");
    private readonly string _id;
    //private string _name; 
    //private string _birthDate;
    //private string _email;
    private string _password;
    private bool _fromCSV;
    [Name("ID")]
    public string ID {get => _id; init => _id = value;}
    [Name("Name")]
    
    //Name set is set to public for testing in Program.cs. change back to private when done testing, as all changes should be done via User.cs
    public string Name {get; set;} //we don't want _name to be able to be changed outside of this class
    [Name("BirthDate")]
    public string BirthDate {get; private set;} //could use a DateTime obj
    [Name("Email")]
    public string Email {get; private set;}
    [Name("Password")]
    public bool Admin { get; }
    [Name("Admin")]
    //public string Password {get => EncryptPassword(_password); private set => _password = value;}
    public string Password {get => _password; private set => _password = value;}
    //add custom set if allowing user to change password
    

    public User(string name, string birthDate, string email, string password, bool admin)
    {
        // ID = CsvHandler.CountRecords(UserDBFilePath);

        ID = admin ? "admin-" + Guid.NewGuid().ToString() : Guid.NewGuid().ToString();
        Name = name;
        BirthDate = birthDate;
        Email = email;
        _password = password;
        Admin = admin;
        AddUser(this);
    }
    public User(string name, string birthDate, string email, string password)
    {
        // ID = CsvHandler.CountRecords(UserDBFilePath);
        
        ID = Guid.NewGuid().ToString();
        Name = name;
        BirthDate = birthDate;
        Email = email;
        _password = password;
        Admin = false;
        AddUser(this);
    }
    //for use by CsVHandler.Read() (it instantiates User objects)
    public User(string id, string name, string birthDate, string email, string password, bool admin)
    {
        ID = id;
        Name = name;
        BirthDate = birthDate;
        Email = email;
        _password = password;
        Admin = admin;
        //_password = DecryptPassword(password); //passwords in csv are encrypted
    }
    //for user by CsvHandler.WriteValueToRecordExtension()
    public User(User user){
        ID = user.ID;
        Name = user.Name;
        BirthDate = user.BirthDate;
        Email = user.Email;
        _password = user._password;
        Admin = user.Admin;
    }
    public static bool AddUser(User user)
    {
        //List<object> records = new()
        CsvHandler.Append(UserDBFilePath, new List<object>{user});
        //CsvHandler.Write(UserDBFilePath);
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
        //string userPassword = DecryptPassword(GetPasswordFromID(id)); //gets directly from DB rather than Users List
        string userPassword = GetPasswordFromID(id);
        if(password == userPassword) return true;
        else return false;
    }


    //https://stackoverflow.com/questions/25461585/operator-overloading-equals
    public static bool operator== (User user1, User user2)
    {
        return (user1.Name == user2.Name 
                    && user1.ID == user2.ID 
                    && user1.BirthDate == user2.BirthDate
                    && user1.Email == user2.Email
                    && user1.Password == user2.Password);
    }

    public static bool operator!= (User user1, User user2)
    {
        return !(user1.Name == user2.Name 
                    && user1.ID == user2.ID 
                    && user1.BirthDate == user2.BirthDate
                    && user1.Email == user2.Email
                    && user1.Password == user2.Password);
    }
    public bool Equals(User other)
    {
        return (Name == other.Name 
                    && ID == other.ID 
                    && BirthDate == other.BirthDate
                    && Email == other.Email
                    && Password == other.Password);
    }
    public override bool Equals(object obj) => obj is User && Equals(obj as User);

    // private static string EncryptPassword(string password)
    // {
    //     string encrypted = "";
    //     foreach(char letter in password)
    //     {
    //         if(letter % 2 == 0) encrypted += letter + 13; //even -> odd
    //         else encrypted += (char)(letter + 9); //odd -> even 7 + 9 -> 16
    //     }
    //     return encrypted;
    // }

    // private static string DecryptPassword(string password)
    // {
    //     string decrypted = "";
    //     foreach(char letter in password)
    //     {
    //         if(letter % 2 != 0) decrypted += (char)(letter - 13); //odd -> even
    //         else decrypted += (char)(letter - 9); //even -> odd 16 - 9 => 7
    //     }
    //     return decrypted;
    // }
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
            //CsvHandler.Write(UserDBFilePath);
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
            //CsvHandler.Write(UserDBFilePath);
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
            //CsvHandler.Write(UserDBFilePath);
            //SetEmailOfID(id, email);
            return true;
        }
    }

    public static int GetIDFromName(string name)
    {
        try{
            int id = 0;
            //load from csv/Users
            //CsvHandler.Read(UserDBFilePath);
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
        //CsvHandler.Read(UserDBFilePath);
        return name;
    }

    public static string GetBirthDateFromID(int id)
    {
        string birthDate = "";
        //load from csv/Users
        //CsvHandler.Read(UserDBFilePath);
        return birthDate;
    }

    public static string GetEmailFromID(int id)
    {
        string email = "";
        //load from csv/Users
        //CsvHandler.Read(UserDBFilePath);
        return email;
    }

    public static string GetPasswordFromID(int id) //is this secure? rn callers must handle security/validation
    {
        string password = "";
        //load from csv/Users
        //CsvHandler.Read(UserDBFilePath);
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