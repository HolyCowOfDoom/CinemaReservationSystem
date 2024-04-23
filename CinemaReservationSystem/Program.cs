using System.Diagnostics;
using System.Formats.Asn1;
using System.Reflection;
using System.Security.AccessControl;

// User Steve = new("Steve", "08-04-2000", "steve.harrington@gmail.com", "password123", true);
// List<string> testSeatsIDs = new() {"1", "2"};
// //CsvHandler.UpdateRecordWithValue<User>("UserDB.csv", Steve, "Reservations", new Reservation(testSeatsIDs, "1", 30));
// //CsvHandler.UpdateRecordWithValue<User>("UserDB.csv", Steve, "Name", "Bob");
// CsvHandler.UpdateRecordWithValue<User, Reservation>("UserDB.csv", Steve, "Reservations",  new Reservation(testSeatsIDs, "1", 30));
// User record = CsvHandler.GetRecordWithValue<User>("UserDB.csv", "Reservations", new Reservation(testSeatsIDs, "1", 30));
// Console.WriteLine(record.Reservations[1].ScreeningID);
// Debug.Assert(User.ValidatePassword(Steve, "password123"));



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


// Console.Clear();
// Auditorium A = new Auditorium("1");
// Auditorium B = new Auditorium("2");
// Auditorium C = new Auditorium("3");
// A.UpdateAuditoriumJson();
// B.UpdateAuditoriumJson();
// C.UpdateAuditoriumJson();
Interface.GeneralMenu();
// Console.ReadLine(); //keeps external terminal open