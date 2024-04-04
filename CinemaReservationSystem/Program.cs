using System.Security.AccessControl;

User Utku = new("Utku", "08-04-2000", "ut.ozyurt@gmail.com", "moo");
User Utku2 = new("Utku2", "09-04-2000", "ut.ozyurt@gmail.com", "moo2");
User Oektoek = new("Oektoek", "01-02-1999", "Oektoek@gmail.com", "ok");

List<User> tempUserList = new();
tempUserList = CsvHandler.Read<User>(Path.GetFullPath("UserDB.csv"));
for(int i = 0; i < tempUserList.Count; i++){
    Console.WriteLine(tempUserList[i].Name);
}

User? tempUser = CsvHandler.GetRecordWithValue<User>("UserDB.csv", "Name", "Utku");
User? tempUser2 = CsvHandler.GetRecordWithValue<User>("UserDB.csv", "ID", 1);
User? tempUser3 = CsvHandler.GetRecordWithValue<User>("UserDB.csv", "Name", "Oektoek");
Console.WriteLine(tempUser.ID + "   " + tempUser.Name);
Console.WriteLine(tempUser2.ID + "   " + tempUser2.Name);

CsvHandler.UpdateRecordOfID<User>("UserDB.csv", tempUser.ID, new User(tempUser) {Name = "Bob!"});
CsvHandler.UpdateRecordOfID<User>("UserDB.csv", 1, new User(tempUser2) {Name = "Steve"});
CsvHandler.UpdateRecordWithValue("UserDB.csv", tempUser3, "Email", "Oektoek@live.nl");
// foreach(User user in tempUserList){
//     Console.WriteLine(user.Name);
// }

Interface.GeneralMenu();
Console.ReadLine(); //keeps external terminal open

