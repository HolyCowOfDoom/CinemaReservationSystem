public static class UserDataController
{
    private static string DBFilePath = "Data/UserDB.csv";
    //optional altFilePath parameters added solely to allow testing 
    public static bool AddUser(User user, string altFilePath = "")
    {
        if(altFilePath == "") CsvHandler.Append(DBFilePath, new List<object>{user});
        else CsvHandler.Append(altFilePath, new List<object>{user});
        return true;
    }

    public static User GetUserWithValue(string header, object hasValue, string altFilePath = "")
    {
        if(altFilePath == "") return CsvHandler.GetRecordWithValue<User>(DBFilePath, header, hasValue);
        else return CsvHandler.GetRecordWithValue<User>(altFilePath, header, hasValue);
    }

    //"J" can't be replaced with "object", as MySetProperty in UpdateRecordWithValue needs List<J> rather than List<object>
    public static bool UpdateUserWithValue<J>(User user, string header, J newValue, string altFilePath = "")
    {
        if(altFilePath == "") return CsvHandler.UpdateRecordWithValue<User, J>(DBFilePath, user, header, newValue);
        else return CsvHandler.UpdateRecordWithValue<User, J>(altFilePath, user, header, newValue);
    }

    public static bool AddValueToUser<J>(User user, string header, J addValue, string altFilePath = "")
    {
        if(altFilePath == "") return CsvHandler.AddValueToRecord<User, J>(DBFilePath, user, header, addValue);
        else return CsvHandler.AddValueToRecord<User, J>(altFilePath, user, header, addValue);
    }

    public static void AddFavoriteMovie(User user, Movie movie)
    {
        user.FavMovies.Add(movie);
        UserDataController.AddValueToUser(user, "FavMovies", movie);
        // Code to update user database
    }

    public static void RemoveFavoriteMovie(User user, int movieIndex)
    {
        user.FavMovies.Remove(user.FavMovies[movieIndex]);
        UserDataController.UpdateUserWithValue(user, "FavMovies", user.FavMovies);
        // Code om user database te updaten
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