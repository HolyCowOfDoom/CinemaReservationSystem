using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

public class User : IEquatable<User>
{
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
    [Name("Reservations"), Optional]
    public List<Reservation> Reservations {get; set;}// = new();

    [Name("FavMovies"), Optional]
    public List<Movie> FavMovies { get; set; } = new List<Movie>();

    public User(string name, string birthDate, string email, string password, bool admin = false, List<Reservation> reservations = null)
    {
        Directory.CreateDirectory("Data");
        using (StreamWriter w = File.AppendText("Data/UserDB.csv")) //create file if it doesn't already exist
        ID = Guid.NewGuid().ToString();
        Name = name;
        BirthDate = birthDate;
        Email = email;
        _password = password;
        Admin = admin;
        if(reservations != null) Reservations = reservations;
        else Reservations = new(); //
        UserDataController.AddUser(this);
    }
    //for use by CsVHandler.Read(), copies ID rather than generating a new one
    public User(string id, string name, string birthDate, string email,string password, bool admin, List<Reservation> reservations, List<Movie> favMovies)//, string reservations)
    {
        ID = id;
        Name = name;
        BirthDate = birthDate;
        Email = email;
        _password = password;
        Admin = admin;
        Reservations = reservations;
        FavMovies = favMovies;
        // if(reservations != null) Reservations = reservations;
        // else Reservations = new();
        //Reservations = new() {new Reservation(new List<string>{"1","2", "3"}, "1", 30)};
        //Reservations = reservations;
        //AddUser(this);
        //_password = DecryptPassword(password); //passwords in csv are encrypted
    }
    //for use by CsvHandler.WriteValueToRecordExtension() //is this still used?
    public User(User user){
        ID = user.ID;
        Name = user.Name;
        BirthDate = user.BirthDate;
        Email = user.Email;
        _password = user._password;
        Admin = user.Admin;
        Reservations = user.Reservations;
    }
    

    //makes object1.Equals(object2) return true if their fields match. default returns false as they ae different obects.
    //https://stackoverflow.com/questions/25461585/operator-overloading-equals
    public static bool operator== (User? user1, User? user2)
    {
        if(user1 is null)
        {
            if(user2 is null) return true;
            else return false;
        }
        // Equals handles case of null on right side.
        return user1.Equals(user2);
    }

    public static bool operator!= (User? user1, User? user2)
    {
        return !(user1 == user2);
    }
    public bool Equals(User other)
    {
        bool compareList = true;
        if(other is null) return false;

        // Reservations can not be null anymore, they're just empty, which these checks don't cover
        // still leaving this here for a little bit untill I'm sure we won't need these checks
        // if(Reservations == null && other.Reservations == null){
        //     compareList = false;
        // }
        // else if (Reservations == null || other.Reservations == null)
        // {
        //     return false; //if one of them is null and the other isn't
        // }
        // else if (Reservations.Contains(null) || other.Reservations.Contains(null))
        // {
        //     return false; //if any of them contains a null reference
        // }
        //some debugging:
        // Console.WriteLine($"In User.Equals(): {Reservations.SequenceEqual(other.Reservations)}");
        // for(int i = 0; i < Reservations.Count; i++) Console.WriteLine($"{Reservations[i].ScreeningID}, {Reservations[i].TotalPrice}");
        // Console.WriteLine("////");
        // for(int i = 0; i < other.Reservations.Count; i++) Console.WriteLine($"{other.Reservations[i].ScreeningID}, {other.Reservations[i].TotalPrice}");

        //if neither is/has null then compare as usual
        bool returnValue = Name == other.Name 
                    && ID == other.ID 
                    && BirthDate == other.BirthDate
                    && Email == other.Email
                    && Password == other.Password
                    && (compareList ? Reservations.SequenceEqual(other.Reservations) : true);
        return returnValue;
                    
    }
    //this last line is because the one above can't override Equals due to a signature mismatch due to "User other"
    public override bool Equals(object obj) => obj is User && Equals(obj as User);

}

