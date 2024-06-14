namespace TestCinemaReservationSystem;

[TestClass]
public class TestUser : TestHelperMethods
{
    [TestMethod]
    public void TestConstructor()
    {
        User user = new("TestUser1", "01-01-2000", "test.user@gmail.com", "testpassword", false);
        Assert.AreEqual(user.Name, "TestUser1");
        Assert.AreEqual(user.BirthDate, "01-01-2000");
        Assert.AreEqual(user.Email, "test.user@gmail.com");
        Assert.AreEqual(user.Password, "testpassword");
    }

    [TestMethod]
    public void TestAddUser()
    {
        string fileName = "TestFiles/TestUser_TestAddUser.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(3, true, fileName);
        for(int i = 0; i > testUsers.Count; i++) UserDataController.AddUser(testUsers[i], fileName);
        List<User> readUsers = new();
        for(int i = 0; i < testUsers.Count; i++)
        {
            //maybe change to using StreamReader so we don't have dependency on GetUserWithValue() here
            readUsers.Add(UserDataController.GetUserWithValue("ID", testUsers[i].ID, fileName));
            
            Assert.AreEqual(testUsers[i], readUsers[i]);
        }
    }

    [TestMethod]
    public void TestGetUserWithValue() //exact same test as above
    {
        string fileName = "TestFiles/TestUser_TestGetUserWithValue.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(3, true, fileName);
        for(int i = 0; i > testUsers.Count; i++) UserDataController.AddUser(testUsers[i], fileName);
        List<User> readUsers = new();
        for(int i = 0; i < testUsers.Count; i++)
        {
            //maybe change to using StreamReader so we don't have dependency on GetUserWithValue() here
            readUsers.Add(UserDataController.GetUserWithValue("ID", testUsers[i].ID, fileName)); 
            
            Assert.AreEqual(testUsers[i], readUsers[i]);
        }
    }

    [TestMethod]
    public void TestUpdateUserWithValue()
    {
        //first part same as above, assert that Users have given values
        string fileName = "TestFiles/TestUser_TestUpdateUserWithValue.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(3, true, fileName);
        for(int i = 0; i > testUsers.Count; i++) UserDataController.AddUser(testUsers[i], fileName);
        List<User> readUsers = new();
        for(int i = 0; i < testUsers.Count; i++)
        {
            //maybe change to using StreamReader so we don't have dependency on GetUserWithValue() here
            readUsers.Add(UserDataController.GetUserWithValue("ID", testUsers[i].ID, fileName)); 
            
            Assert.AreEqual(testUsers[i], readUsers[i]);
        } 
        List<User> newReadUsers = new();
        for(int i = 0; i < testUsers.Count; i++)
        {
            UserDataController.UpdateUserWithValue(testUsers[i], "ID", "new" + testUsers[i].ID, fileName);
            //read updatred user back from database to a new list
            newReadUsers.Add(UserDataController.GetUserWithValue("Name", testUsers[i].Name, fileName));
            //readUsers list still contains original version, so we can use those as a base for comparison
            Assert.AreEqual(newReadUsers[i].ID, "new" + readUsers[i].ID);
        }
    }

    [TestMethod]
    public void TestAddValueToUser()
    {
        //first part same as above, assert that Users have given values
        string fileName = "TestFiles/TestUser_TestUpdateUserWithValue.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(3, true, fileName);
        for(int i = 0; i > testUsers.Count; i++) UserDataController.AddUser(testUsers[i], fileName);
        List<User> readUsers = new();
        for(int i = 0; i < testUsers.Count; i++)
        {
            //maybe change to using StreamReader so we don't have dependency on GetUserWithValue() here
            readUsers.Add(UserDataController.GetUserWithValue("ID", testUsers[i].ID, fileName)); 
            
            Assert.AreEqual(testUsers[i], readUsers[i]);
        } 
        for(int i = 0; i < testUsers.Count; i++)
        {
            Reservation reservation = new Reservation(new List<string>() {"1"}, $"{i}", 20);
            UserDataController.AddValueToUser(testUsers[i], "Reservations", reservation, fileName);
            User readUser = UserDataController.GetUserWithValue("ID", testUsers[i].ID);
            Assert.IsTrue(readUser.Reservations.Contains(reservation));
        }
    }

}