using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

public class User : IEquatable<User>
{
    static string UserDBFilePath = Path.GetFullPath("UserDB.csv");
    private string _id;
    //private string _name; 
    //private string _birthDate;
    //private string _email;
    private string _password;
    [Name("ID")]
    public string ID {get => _id; set => _id = value;}
    //Name set is set to public for testing in Program.cs. change back to private when done testing, as all changes should be done via User.cs
    [Name("Name")]
    public string Name {get; set;} //we don't want _name to be able to be changed outside of this class
    [Name("BirthDate")]
    public string BirthDate {get; private set;} //could use a DateTime obj
    [Name("Email")]
    public string Email {get; private set;}
    [Name("Admin")]
    public bool Admin { get; }
    //public string Password {get => EncryptPassword(_password); private set => _password = value;}
    [Name("Password")]
    public string Password {get => _password; private set => _password = value;}
    //add custom set if allowing user to change password
    [Name("Reservations")]
    public List<Reservation> Reservations {get; set;}

    // public User(string name, string birthDate, string email, string password, bool admin, List<Reservation> reservations = null)
    // {
    //     // ID = CsvHandler.CountRecords(UserDBFilePath);

    //     ID = admin ? "admin-" + Guid.NewGuid().ToString() : Guid.NewGuid().ToString();
    //     Name = name;
    //     BirthDate = birthDate;
    //     Email = email;
    //     _password = password;
    //     Admin = admin;
    //     //Reservations = new() {new Reservation(new List<string>{"1","2", "3"}, "1", 30)};
    //     Reservations = reservations;
    //     AddUser(this);
        
    // }
    public User(string name, string birthDate, string email, string password, bool admin = false, List<Reservation> reservations = null)
    {
        ID = Guid.NewGuid().ToString();
        Name = name;
        BirthDate = birthDate;
        Email = email;
        _password = password;
        Admin = admin;
        if(reservations != null) Reservations = reservations;
        else Reservations = new() {new Reservation(new List<string>() {"-1"}, "-1", -1)};
        AddUser(this);
    }
    //for use by CsVHandler.Read(), copies ID rather than generating a new one
    public User(string id, string name, string birthDate, string email,string password, bool admin, List<Reservation> reservations)//, string reservations)
    {
        ID = id;
        Name = name;
        BirthDate = birthDate;
        Email = email;
        _password = password;
        Admin = admin;
        Reservations = reservations;
        // if(reservations != null) Reservations = reservations;
        // else Reservations = new();
        //Reservations = new() {new Reservation(new List<string>{"1","2", "3"}, "1", 30)};
        //Reservations = reservations;
        //AddUser(this);
        //_password = DecryptPassword(password); //passwords in csv are encrypted
    }
    //for user by CsvHandler.WriteValueToRecordExtension() //is this still used?
    public User(User user){
        ID = user.ID;
        Name = user.Name;
        BirthDate = user.BirthDate;
        Email = user.Email;
        _password = user._password;
        Admin = user.Admin;
        Reservations = user.Reservations;
    }
    public static bool AddUser(User user)
    {
        //List<object> records = new()
        CsvHandler.Append(UserDBFilePath, new List<object>{user});
        //CsvHandler.Write(UserDBFilePath);
        return true;
    }

    public static User GetUserWithValue(string header, object value)
    {
        return CsvHandler.GetRecordWithValue<User>(UserDBFilePath, header, value);
    }

    //"J" can't be replaced with "object", as MySetProperty in UpdateRecordWithValue needs List<J> rather than List<object>
    public static bool UpdateUserWithValue<J>(string csvFile, User user, string header, J value)
    {
        return CsvHandler.UpdateRecordWithValue<User, J>(UserDBFilePath, user, header, value);
    }


    public static bool ValidatePassword(User user, string password)
    {
        User userInDB = GetUserWithValue("ID", user.ID);
        string userPassword = userInDB.Password;
        if(password == userPassword) return true;
        else return false;
    }

    //makes object1.Equals(object2) return true if their fields match. default returns false as they ae different obects.
    //https://stackoverflow.com/questions/25461585/operator-overloading-equals
    public static bool operator== (User user1, User user2)
    {
        return (user1.Name == user2.Name 
                    && user1.ID == user2.ID 
                    && user1.BirthDate == user2.BirthDate
                    && user1.Email == user2.Email
                    && user1.Password == user2.Password)
                    && user1.Reservations.SequenceEqual(user2.Reservations);
    }

    public static bool operator!= (User user1, User user2)
    {
        return !(user1.Name == user2.Name 
                    && user1.ID == user2.ID 
                    && user1.BirthDate == user2.BirthDate
                    && user1.Email == user2.Email
                    && user1.Password == user2.Password)
                    && user1.Reservations.SequenceEqual(user2.Reservations);
    }
    public bool Equals(User other)
    {
        return (Name == other.Name 
                    && ID == other.ID 
                    && BirthDate == other.BirthDate
                    && Email == other.Email
                    && Password == other.Password)
                    && Reservations.SequenceEqual(other.Reservations);
    }
    //this last line is because the one above can't override Equals due to a signature mismatch due to "User other"
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
    // public static bool ChangeName(int id, string newName){
    //     string currentName = GetNameFromID(id);
    //     if(newName == currentName){
    //         Console.WriteLine($"User's name and entered name({newName}) are identical");
    //         return false;
    //     }
    //     else if(string.IsNullOrWhiteSpace(newName))
    //     {
    //         Console.WriteLine("Entered name was null or whitespace");
    //         return false;
    //     }
    //     else{
    //         //CsvHandler.Write(UserDBFilePath);
    //         //SetNameOfID(id, newName);
    //         return true;
    //     }
    // }

    // public static bool ChangeBirthDate(int id, string birthDate){
    //     string currentBirthDate = GetBirthDateFromID(id);
    //     if(birthDate == currentBirthDate){
    //         Console.WriteLine($"User's birth date and entered birth date({birthDate}) are identical");
    //         return false;
    //     }
    //     else if(string.IsNullOrWhiteSpace(birthDate))
    //     {
    //         Console.WriteLine("Entered birth date was null or whitespace");
    //         return false;
    //     }
    //     else{
    //         //CsvHandler.Write(UserDBFilePath);
    //         //SetBirthDateOfID(id, birthDate);
    //         return true;
    //     }
    // }

    // public static bool ChangeEmail(int id, string email){
    //     string currentEmail = GetEmailFromID(id);
    //     if(email == currentEmail){
    //         Console.WriteLine($"User's email and entered email({email}) are identical");
    //         return false;
    //     }
    //     else if(string.IsNullOrWhiteSpace(email))
    //     {
    //         Console.WriteLine("Entered email was null or whitespace");
    //         return false;
    //     }
    //     else{
    //         //CsvHandler.Write(UserDBFilePath);
    //         //SetEmailOfID(id, email);
    //         return true;
    //     }
    // }

    // public static int GetIDFromName(string name)
    // {
    //     try{
    //         int id = 0;
    //         //load from csv/Users
    //         //CsvHandler.Read(UserDBFilePath);
    //         return id;
    //     } catch(Exception e){ //catch specific csv exceptions
    //         Console.WriteLine(e);
    //     }
    //     return -1;
    // }
    // //add try-catch to functions below (or is try-catch not even necessary at all?)

    // public static string GetNameFromID(int id)
    // {
    //     string name = "";
    //     //load from csv/Users
    //     //CsvHandler.Read(UserDBFilePath);
    //     return name;
    // }

    // public static string GetBirthDateFromID(int id)
    // {
    //     string birthDate = "";
    //     //load from csv/Users
    //     //CsvHandler.Read(UserDBFilePath);
    //     return birthDate;
    // }

    // public static string GetEmailFromID(int id)
    // {
    //     string email = "";
    //     //load from csv/Users
    //     //CsvHandler.Read(UserDBFilePath);
    //     return email;
    // }

    // public static string GetPasswordFromID(int id) //is this secure? rn callers must handle security/validation
    // {
    //     string password = "";
    //     //load from csv/Users
    //     //CsvHandler.Read(UserDBFilePath);
    //     return password;
    // }

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

