// See https://aka.ms/new-console-template for more information
using System.Security.AccessControl;

Console.WriteLine("Hello, World!");
User Utku = new("Utku", "08-04-2000", "ut.ozyurt@gmail.com", "moo");
User Utku2 = new("Utku2", "09-04-2000", "ut.ozyurt@gmail.com", "moo2");

List<User> tempUserList = new();
CsvHandler.Read(Path.GetFullPath("UserDB.csv"), out tempUserList);
for(int i = 0; i < tempUserList.Count; i++){
    Console.WriteLine(tempUserList[i].Name);
}
// foreach(User user in tempUserList){
//     Console.WriteLine(user.Name);
// }
Console.ReadLine(); //keeps external terminal open

