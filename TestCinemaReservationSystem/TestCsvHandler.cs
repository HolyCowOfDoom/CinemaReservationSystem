using System.IO;
using System.Text;

namespace TestCinemaReservationSystem;
[TestClass]
public class TestCsvHandler
{
    public void CreateTestFile(string fileName)
    {
       using var file = File.Create(fileName); //overwrites file with same name, to avoid duplicates
    }

    public List<User> CreateTestUsers(int count, bool addReservations)
    {
        List<User> testUsers = new();
        for(int i = 1; i <= count; i++ )
        {
            User newUser = new User($"testUser{i}", "01-01-2000", $"test.user{i}@gmail.com", $"testPassword{1}");
            if (addReservations) newUser.Reservations.Add(new Reservation(new List<string>() {$"{i}"}, $"{i}", i*20));
            testUsers.Add(newUser);
        }
        return testUsers;
    }

    [TestMethod]
    public void TestRead()
    {
        string fileName = "CSvHandler_TestRead.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(10, true);
        CsvHandler.Write(fileName, testUsers);
        List<User> readUsers = CsvHandler.Read<User>(fileName);
        for(int i = 0; i <= readUsers.Count - 1; i++)
        {
            Assert.IsTrue(readUsers[i] == testUsers[i]);
        }
    }

    [TestMethod]
    public void TestWrite()
    {
        string fileName = "CSvHandler_TestWrite.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(10, false); //no reservations
        //the comma delimiter of reservations messes with the tests below
        //so maybe make a seperate test for that using CsvHelper, testing the Reservation TypeConverter as well
        CsvHandler.Write(fileName, testUsers);
        TextReader r = File.OpenText(fileName);
        string headers = r.ReadLine();
        List<string> usersData = new();
        while(true)
        {
            string line = r.ReadLine();
            if(line is null) break;
            else usersData.Add(line);
        }
        List<string[]> usersDataSplit = new(); //string[] is the return type of Split()
        foreach(string data in usersData)
        {
            usersDataSplit.Add(data.Split(","));
        }
        //string firstuser = r.ReadLine();
        //string[] userData = firstuser.Split(",");
        for(int i = 0; i <= 9; i++)
        {
            Assert.AreEqual(usersDataSplit[i][0], testUsers[i].ID);
            Assert.AreEqual(usersDataSplit[i][1], testUsers[i].Name);
            Assert.AreEqual(usersDataSplit[i][2], testUsers[i].BirthDate);
            Assert.AreEqual(usersDataSplit[i][3], testUsers[i].Email);
            Assert.AreEqual(usersDataSplit[i][5], testUsers[i].Password);
        }
    }

    [TestMethod]
    public void TestAppend()
    {
        string fileName = "CSvHandler_TestAppend.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(2, true);
        CsvHandler.Write(fileName, testUsers);
        List<User> readUsers = CsvHandler.Read<User>(fileName);
        for(int i = 0; i <= testUsers.Count -1; i++)
        {
            Assert.IsTrue(testUsers[i] == readUsers[i]);
        }
        List<User> testUsers2 = CreateTestUsers(2, true);
        int testUsersCount = testUsers.Count;
        CsvHandler.Append<User>(fileName, testUsers2);
        testUsers.AddRange(testUsers2);
        Assert.AreEqual(testUsers.Count, testUsersCount + testUsers2.Count);
        readUsers = CsvHandler.Read<User>(fileName);
        for(int i = 0; i <= testUsers.Count -1; i++)
        {
            Assert.IsTrue(testUsers[i] == readUsers[i]);
        }
    }

    [TestMethod]
    public void TestGetRecordWithValue()
    {
        string fileName = "CSvHandler_TestGetRecordWithValue.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(5, true);
        CsvHandler.Write(fileName, testUsers);
        for(int i = 0; i <= 4; i++)
        {
            User foundUser0 = CsvHandler.GetRecordWithValue<User>(fileName, "Name", testUsers[i].Name);
            User foundUser1 = CsvHandler.GetRecordWithValue<User>(fileName, "ID", testUsers[i].ID);
            User foundUser2 = CsvHandler.GetRecordWithValue<User>(fileName, "BirthDate", testUsers[i].BirthDate);
            User foundUser3 = CsvHandler.GetRecordWithValue<User>(fileName, "Email", testUsers[i].Email);
            User foundUser4 = CsvHandler.GetRecordWithValue<User>(fileName, "Password", testUsers[i].Password);
            User foundUser5 = CsvHandler.GetRecordWithValue<User>(fileName, "Reservations", testUsers[i].Reservations);
            User foundUser6 = CsvHandler.GetRecordWithValue<User>(fileName, "Reservations", testUsers[i].Reservations[0]);
            Assert.AreEqual(testUsers[i], foundUser0);
            Assert.AreEqual(testUsers[i], foundUser1);
            Assert.AreEqual(testUsers[i], foundUser2);
            Assert.AreEqual(testUsers[i], foundUser3);
            Assert.AreEqual(testUsers[i], foundUser4);
            Assert.AreEqual(testUsers[i], foundUser5); //searching for an entire list doesn't work!, leave this for now to show failure
            Assert.AreEqual(testUsers[i], foundUser6);
        }
    }

    [TestMethod]
    public void TestUpdateRecordWithValue()
    {
        string fileName = "CSvHandler_TestUpdateRecordWithValue.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(5, true);
        CsvHandler.Write(fileName, new List<User>() {new User($"testUser{-1}", "01-01-2000", $"test.user{-1}@gmail.com", $"testPassword{-1}")});
        CsvHandler.Write(fileName, testUsers);
        // for(int i = 0; i <= 4; i++)
        // {
        //     List<Reservation> testReservations = new() {new Reservation(new List<string>() {$"new{i}"}, $"new{i}", i * 30)};
        //     CsvHandler.UpdateRecordWithValue(fileName, testUsers[i], "Name", "NewName" + testUsers[i].Name);
        //     CsvHandler.UpdateRecordWithValue(fileName, testUsers[i], "ID", "NewID" + testUsers[i].ID);
        //     CsvHandler.UpdateRecordWithValue(fileName, testUsers[i], "BirthDate", "NewBirthDate" + testUsers[i].BirthDate);
        //     CsvHandler.UpdateRecordWithValue(fileName, testUsers[i], "Email", "NewEmail" + testUsers[i].Email);
        //     CsvHandler.UpdateRecordWithValue(fileName, testUsers[i], "Password", "NewPassword" + testUsers[i].Password);
        //     CsvHandler.UpdateRecordWithValue(fileName, testUsers[i], "Reservations", testReservations[0]);

        //     User newUser = new User("NewID" + testUsers[i].ID, "NewName" + testUsers[i].Name, 
        //     "NewBirthDate" + testUsers[i].BirthDate, "NewEmail" + testUsers[i].Email, 
        //     "NewPassword" + testUsers[i].Password, false, testReservations);
        //     newUser.Reservations.InsertRange(0, testUsers[i].Reservations); //make sure old Reservations are in the new User to compare too
        //     User foundUser = CsvHandler.GetRecordWithValue<User>(fileName, "Name", "NewName" + testUsers[i].Name);
        //     //Assert.AreEqual(foundUser, newUser);
        //     Assert.IsTrue(foundUser == newUser);
        // }
        
    }

    [TestMethod]
    public void MySetProperty()
    {

    }

    [TestMethod]
    public void MyGetProperty()
    {

    }
}