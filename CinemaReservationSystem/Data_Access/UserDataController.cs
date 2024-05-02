public static class UserDataController
{
    private static string DBFilePath = "Model/UserDB.csv";
    public static bool AddUser(User user)
    {
        //List<object> records = new()
        CsvHandler.Append(DBFilePath, new List<object>{user});
        //CsvHandler.Write(UserDBFilePath);
        return true;
    }

    public static User GetUserWithValue(string header, object hasValue)
    {
        return CsvHandler.GetRecordWithValue<User>(DBFilePath, header, hasValue);
    }

    //"J" can't be replaced with "object", as MySetProperty in UpdateRecordWithValue needs List<J> rather than List<object>
    public static bool UpdateUserWithValue<J>(User user, string header, J newValue)
    {
        return CsvHandler.UpdateRecordWithValue<User, J>(DBFilePath, user, header, newValue);
    }

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
}