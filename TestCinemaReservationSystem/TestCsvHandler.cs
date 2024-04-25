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

    public List<User> CreateTestUsers(int count)
    {
        List<User> testUsers = new();
        for(int i = 1; i <= count; i++ )
        {
            testUsers.Add(new User($"testUser{i}", "01-01-2000", $"test.user{i}@gmail.com", $"testPassword{1}"));
        }
        return testUsers;
    }

    [TestMethod]
    public void TestRead()
    {
        string fileName = "CSvHandler_TestRead.csv";
        CreateTestFile(fileName);
        List<User> testUsers = CreateTestUsers(10);
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
        List<User> testUsers = CreateTestUsers(10);
        CsvHandler.Write(fileName, testUsers);
        TextReader r = File.OpenText(fileName);
        string headers = r.ReadLine();
        List<string> users = new();
        // while((string Line = r.ReadLine()) == true)
        // {
        //     usersData.Add(Line);
        // }
        List<string[]> usersData = new();
        foreach(string data in users)
        {
            usersData.Add(data.Split(","));
        }
        //string firstuser = r.ReadLine();
        //string[] userData = firstuser.Split(",");
        for(int i = 0; i <= 9; i++)
        {
            Assert.AreEqual(usersData[i][0], testUsers[i].ID);
        }
        
    }

    [TestMethod]
    public void TestAppend()
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

    public void GetRecordWithValue()
    {
        
    }

    public void UpdateRecordWithValue()
    {

    }

    public void MySetProperty()
    {

    }

    public void MyGetProperty()
    {

    }
}