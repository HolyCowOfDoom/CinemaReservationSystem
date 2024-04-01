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
//     new Movie("The Matrix", 15, "A mind-bending sci-fi classic", "Placeholder"),
//     new Movie("Inception", 12, "Explore the depths of the human mind", "Placeholder"),
//     new Movie("The Shawshank Redemption", 18, "A tale of hope and redemption", "Placeholder"),
//     new Movie("The Godfather", 18, "An epic saga of crime and family", "Placeholder"),
//     new Movie("Pulp Fiction", 18, "A stylish journey through the criminal underworld", "Placeholder"),
//     new Movie("Fight Club", 18, "An intense exploration of masculinity and identity", "Placeholder"),
//     new Movie("Forrest Gump", 12, "Life is like a box of chocolates", "Placeholder"),
//     new Movie("The Dark Knight", 12, "The legendary battle between Batman and Joker", "Placeholder"),
//     new Movie("The Lord of the Rings: The Fellowship of the Ring", 12, "Embark on an epic adventure", "Placeholder"),
//     new Movie("Schindler's List", 18, "A powerful true story of one man's fight against evil", "Placeholder"),
//     new Movie("12 Angry Men", 18, "A riveting courtroom drama", "Placeholder"),
//     new Movie("Pokemon: The Movie", 6, "Join Ash and Pikachu on an adventure", "Placeholder"),
//     // Add more movie entries as needed
// };

// List<Auditorium> auditoriums = JsonHandler.Read<Auditorium>("AuditoriumDB.json");
// JsonHandler.Write<Auditorium>(auditoriums, "AuditoriumDB.json");

List<Auditorium> auditoriums = 
[
    new Auditorium("1", 20),
    new Auditorium("2", 20)
];
JsonHandler.Write<Auditorium>(auditoriums, "AuditoriumDB.json");

List<Movie> movies = JsonHandler.Read<Movie>("MovieDB.json");
movies[3].AddScreening(auditoriums[0], null);




Interface.GeneralMenu();
// Console.ReadLine(); //keeps external terminal open