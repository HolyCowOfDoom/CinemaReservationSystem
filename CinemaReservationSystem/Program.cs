using System.Formats.Asn1;
using System.Security.AccessControl;

// List<User> tempUserList = new();
// tempUserList = CsvHandler.Read<User>(Path.GetFullPath("UserDB.csv"));
// for(int i = 0; i < tempUserList.Count; i++){
//     Console.WriteLine(tempUserList[i].Name);
// }

// User? tempUser = CsvHandler.GetRecordWithValue<User>("UserDB.csv", "Name", "Utku");
// User? tempUser2 = CsvHandler.GetRecordWithValue<User>("UserDB.csv", "ID", 1);
// Console.WriteLine(tempUser.ID + "   " + tempUser.Name);
// Console.WriteLine(tempUser2.ID + "   " + tempUser2.Name);

// CsvHandler.UpdateRecordOfID<User>("UserDB.csv", tempUser.ID, new User(tempUser) {Name = "Bob!"});
// // foreach(User user in tempUserList){
// //     Console.WriteLine(user.Name);
// // }


// List<Movie> testMovies = new List<Movie>
// {
//     new Movie("The Matrix", 15, "A mind-bending sci-fi classic"),
//     new Movie("Inception", 12, "Explore the depths of the human mind"),
//     new Movie("The Shawshank Redemption", 18, "A tale of hope and redemption"),
//     new Movie("The Godfather", 18, "An epic saga of crime and family"),
//     new Movie("Pulp Fiction", 18, "A stylish journey through the criminal underworld"),
//     new Movie("Fight Club", 18, "An intense exploration of masculinity and identity"),
//     new Movie("Forrest Gump", 12, "Life is like a box of chocolates"),
//     new Movie("The Dark Knight", 12, "The legendary battle between Batman and Joker"),
//     new Movie("The Lord of the Rings: The Fellowship of the Ring", 12, "Embark on an epic adventure"),
//     new Movie("Schindler's List", 18, "A powerful true story of one man's fight against evil"),
//     new Movie("12 Angry Men", 18, "A riveting courtroom drama"),
//     new Movie("Pokemon: The Movie", 6, "Join Ash and Pikachu on an adventure"),
//     // Add more movie entries as needed
// };

// JsonHandler.Update<Auditorium>(new Auditorium("A1", 15), "AuditoriumDB.json");


Console.Clear();
Interface.GeneralMenu();
// Console.ReadLine(); //keeps external terminal open