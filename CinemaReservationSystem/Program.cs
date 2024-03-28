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

// Interface.GeneralMenu();
// Console.ReadLine(); //keeps external terminal open

List<Movie> demoMovies = JsonHandler.Read<Movie>("MovieDB.json");
List<User> demoUsers = CsvHandler.Read<User>("UserDB.csv");
List<Auditorium> demoAuds = JsonHandler.Read<Auditorium>("AuditoriumDB.json");


