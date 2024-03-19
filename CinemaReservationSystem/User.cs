using System.Reflection.Metadata.Ecma335;
using CsvHelper.Configuration.Attributes;

public class User
{
    private readonly int _id;
    //private string _name; 
    //private string _birthDate;
    //private string _email;
    private string _password;

    public int ID {get => _id; init => _id = value;}
    public string Name {get; private set;} //we don't want _name to be able to be changed outside of this class
    public string BirthDate {get; private set;}
    public string Email {get; private set;}
    //private string Password {get; set;}
    //add custom set if allowing user to change password

   
    public User(string name, string birthDate, string email, string password)
    {
        //ID = CsvHandler.GetAmountOfUsers();
        _password = password;
        Name = name;
        BirthDate = birthDate;
        Email = email;
        CsvHandler.AddUser(this);
    }
    public User(int id, string name, string birthDate, string email, string password) : this(name, birthDate, email, password)
    {
        ID = id;
    }

    public bool ValidatePassword(string name, string password)
    {
        int id = CsvHandler.GetIDFromName(name);
        string userPassword = CsvHandler.GetPasswordFromID(id); //gets directly from DB rather than Users List
        if(password == userPassword ) return true;
        else return false;
    }

    public bool ChangeName(string newName){
        if(newName == Name){
            Console.WriteLine($"User's name and entered name({newName}) are identical");
            return false;
        }
        else if(string.IsNullOrWhiteSpace(newName))
        {
            Console.WriteLine("Entered name was null or whitespace");
            return false;
        }
        else{

            Name = newName;
            CsvHandler.UpdateUser(this);
            //int id = CsvHandler.GetIDFromName(currentName);
            //CsvHandler.SetNameOfID(id, newName);
            return true;
        }
    }

    public bool ChangeBirthDate(string birthDate){
        if(birthDate == BirthDate){
            Console.WriteLine($"User's birth date and entered birth date({birthDate}) are identical");
            return false;
        }
        else if(string.IsNullOrWhiteSpace(birthDate))
        {
            Console.WriteLine("Entered birth date was null or whitespace");
            return false;
        }
        else{
            BirthDate = birthDate;
            CsvHandler.UpdateUser(this);
            return true;
        }
    }

    public bool ChangeEmail(string email){
        if(email == Email){
            Console.WriteLine($"User's email and entered email({email}) are identical");
            return false;
        }
        else if(string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Entered email was null or whitespace");
            return false;
        }
        else{
            Email = email;
            CsvHandler.UpdateUser(this);
            return true;
        }
    }
}