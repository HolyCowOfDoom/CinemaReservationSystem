using System.Security.AccessControl;

User Utku = new("Utku", "08-04-2000", "ut.ozyurt@gmail.com", "moo");
User Utku2 = new("Utku2", "09-04-2000", "ut.ozyurt@gmail.com", "moo2");

List<User> tempUserList = new();
tempUserList = CsvHandler.Read<User>(Path.GetFullPath("UserDB.csv"));
for(int i = 0; i < tempUserList.Count; i++){
    Console.WriteLine(tempUserList[i].Name);
}

User? tempUser = CsvHandler.GetRecordWithValue<User>("UserDB.csv", "Name", "Utku");
User? tempUser2 = CsvHandler.GetRecordWithValue<User>("UserDB.csv", "ID", 1);
Console.WriteLine(tempUser.ID + "   " + tempUser.Name);
Console.WriteLine(tempUser2.ID + "   " + tempUser2.Name);

CsvHandler.UpdateRecordOfID<User>("UserDB.csv", tempUser.ID, new User(tempUser) {Name = "Bob!"});
// foreach(User user in tempUserList){
//     Console.WriteLine(user.Name);
// }

Interface.GeneralMenu();
Console.ReadLine(); //keeps external terminal open

