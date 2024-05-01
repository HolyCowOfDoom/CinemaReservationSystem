namespace TestCinemaReservationSystem;

[TestClass]
public class TestUser
{
    [TestMethod]
    public void TestConstructor()
    {
        User user = new("TestUser1", "01-01-2000", "test.user@gmail.com", "testpassword");
        Assert.AreEqual(user.Name, "TestUser1");
        Assert.AreEqual(user.BirthDate, "01-01-2000");
        Assert.AreEqual(user.Email, "test.user@gmail.com");
        Assert.AreEqual(user.Password, "testpassword");
    }

    [TestMethod]
    public void TestAddUser()
    {
        
    }

    [TestMethod]
    public void GetUserWithValue()
    {

    }

    [TestMethod]
    public void UpdateUserWithValue()
    {

    }

}