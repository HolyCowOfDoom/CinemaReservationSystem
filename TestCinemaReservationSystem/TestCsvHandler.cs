using System.IO;
using System.Text;

namespace TestCinemaReservationSystem;
[TestClass]
public class TestCsvHandler : TestHelperMethods //TestMethods has general methods used for testing
{
    [TestMethod]
    public void TestRead() //equivalance class testing. tests if method works with User, assumes equivalence with other classes
    {
        string fileName = "TestFiles/TestCSvHandler_TestRead.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(10, true, fileName);
        CsvHandler.Write(fileName, testUsers);
        List<User> readUsers = CsvHandler.Read<User>(fileName);
        for(int i = 0; i <= readUsers.Count - 1; i++)
        {
            Assert.IsTrue(readUsers[i] == testUsers[i]);
        }
    }

    [TestMethod]
    public void TestWrite() //equivalance class testing. tests if method works with User, assumes equivalence with other classes
    {
        string fileName = "TestFiles/TestCSvHandler_TestWrite.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(10, false, fileName); //no reservations
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
    public void TestAppend() //equivalance class testing. tests if method works with User, assumes equivalence with other classes
    {
        string fileName = "TestFiles/TestCSvHandler_TestAppend.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(2, true, fileName);
        List<User> testUsersAppend = CreateTestUsers(2, true, fileName);
        CsvHandler.Append(fileName, testUsersAppend);
        CsvHandler.Write(fileName, testUsers);
        List<User> readUsers = CsvHandler.Read<User>(fileName);
        for(int i = 0; i <= testUsers.Count -1; i++)
        {
            Assert.IsTrue(testUsers[i] == readUsers[i]);
        }
        List<User> testUsers2 = CreateTestUsers(2, true, fileName);
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
    public void TestGetRecordWithValue() //equivalance class testing. tests if method works with User, assumes equivalence with other classes
    {
        string fileName = "TestFiles/TestCSvHandler_TestGetRecordWithValue.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(5, true, fileName);
        CsvHandler.Write(fileName, testUsers);
        for(int i = 0; i < testUsers.Count; i++)
        {
            User foundUser0 = CsvHandler.GetRecordWithValue<User>(fileName, "Name", testUsers[i].Name);
            User foundUser1 = CsvHandler.GetRecordWithValue<User>(fileName, "ID", testUsers[i].ID);
            User foundUser2 = CsvHandler.GetRecordWithValue<User>(fileName, "BirthDate", testUsers[i].BirthDate);
            User foundUser3 = CsvHandler.GetRecordWithValue<User>(fileName, "Email", testUsers[i].Email);
            User foundUser4 = CsvHandler.GetRecordWithValue<User>(fileName, "Password", testUsers[i].Password);
            User foundUser5 = CsvHandler.GetRecordWithValue<User>(fileName, "Reservations", testUsers[i].Reservations);
            User foundUser6 = CsvHandler.GetRecordWithValue<User>(fileName, "Reservations", testUsers[i].Reservations[0]);
            // Console.WriteLine(testUsers[i].BirthDate);
            // Console.WriteLine(foundUser2.BirthDate);
            // Console.WriteLine(testUsers[i].Name);
            // Console.WriteLine(foundUser2.Name);
            // Console.WriteLine(testUsers[i].ID);
            // Console.WriteLine(foundUser2.ID);
            // Console.WriteLine(testUsers[i].Password);
            // Console.WriteLine(foundUser2.Password);
            // Console.WriteLine(testUsers[i].Equals(foundUser2));
            //Console.WriteLine(CsvHandler.ListInfo(testUsers[i].Reservations) );
            //Console.WriteLine(foundUser2.BirthDate);
            Assert.AreEqual(testUsers[i], foundUser0);
            Assert.AreEqual(testUsers[i], foundUser1);
            Assert.AreEqual(testUsers[i], foundUser2);
            Assert.AreEqual(testUsers[i], foundUser3);
            Assert.AreEqual(testUsers[i], foundUser4);
            Assert.AreEqual(testUsers[i], foundUser5); //searching for an entire list works now,
            Assert.AreEqual(testUsers[i], foundUser6); // searching item in list now doesn't! - fixed now
        }
    }

    [TestMethod]
    public void TestUpdateRecordWithValue() //equivalance class testing. tests if method works with User, assumes equivalence with other classes
    {
        string fileName = "TestFiles/TestCSvHandler_TestUpdateRecordWithValue.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(5, true, fileName);
        //CsvHandler.Write(fileName, new List<User>() {new User($"testUser{-1}", "01-01-2000", $"test.user{-1}@gmail.com", $"testPassword{-1}")});
        CsvHandler.Write(fileName, testUsers);
        for(int i = 0; i <= 4; i++)
        {
            User startUser = new User(testUsers[i]); //keep start value for later comparison

            //update testUsers after every change, as the User in DB becomes different from the one in memory (testUsers[i])
            List<Reservation> testReservations = new() {new Reservation(new List<string>() {$"new{i}"}, $"new{i}", i * 30)};
            CsvHandler.UpdateRecordWithValue(fileName, testUsers[i], "Name", "NewName" + testUsers[i].Name);
            testUsers = CsvHandler.Read<User>(fileName);
            CsvHandler.UpdateRecordWithValue(fileName, testUsers[i], "ID", "NewID" + testUsers[i].ID);
            testUsers = CsvHandler.Read<User>(fileName);
            CsvHandler.UpdateRecordWithValue(fileName, testUsers[i], "BirthDate", "NewBirthDate" + testUsers[i].BirthDate);
            testUsers = CsvHandler.Read<User>(fileName);
            CsvHandler.UpdateRecordWithValue(fileName, testUsers[i], "Email", "NewEmail" + testUsers[i].Email);
            testUsers = CsvHandler.Read<User>(fileName);
            CsvHandler.UpdateRecordWithValue(fileName, testUsers[i], "Password", "NewPassword" + testUsers[i].Password);
            testUsers = CsvHandler.Read<User>(fileName);
            CsvHandler.UpdateRecordWithValue(fileName, testUsers[i], "Reservations", testReservations);
            testUsers = CsvHandler.Read<User>(fileName);

            //applies all changes to a new User in memory based on start values of user
            User newUser = new User("NewID" + startUser.ID, "NewName" + startUser.Name, 
            "NewBirthDate" + startUser.BirthDate, "NewEmail" + startUser.Email, 
            "NewPassword" + startUser.Password, false, testReservations, null); //null for FavMovies for now
            //newUser.Reservations.InsertRange(0, testUsers[i].Reservations); //make sure old Reservations are in the new User to compare too
            User foundUser = CsvHandler.GetRecordWithValue<User>(fileName, "Name", testUsers[i].Name);
            Assert.AreEqual(foundUser, newUser);
            //Assert.IsTrue(foundUser == newUser);
        }
        
    }

    // [TestMethod]
    // public void TestMySetProperty<T>(T objToChange, string propertyToChange, object newValue)
    // {
    //     CsvHandler.MySetProperty<T>
    // }

    // [TestMethod]
    // public void TestMyGetProperty()
    // {

    // }
}