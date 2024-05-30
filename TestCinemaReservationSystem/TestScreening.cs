namespace TestCinemaReservationSystem;

[TestClass]
public class TestScreening : TestHelperMethods
{
    [TestMethod]
    public void TestConstructor()
    {
        List<Seat> seats = new() {new Seat("blue", 5), new Seat("blue", 5), new Seat("Red", 10)};
        Auditorium auditorium = new("auditorium1", seats);
        DateTime dateTime = new DateTime(2000, 1, 1, 0, 0, 0);
        Screening screening = new Screening(auditorium, dateTime, "movie1", "screening1");
        Assert.AreEqual(screening.AssignedAuditorium, auditorium);
        Assert.AreEqual(screening.ScreeningDateTime, dateTime);
        Assert.AreEqual(screening.MovieID, "movie1");
        Assert.AreEqual(screening.ID, "screening1");

    }

    [TestMethod]
    public void TestAdjustDateTime()
    {
        string fileName = "TestFiles/TestScreening_TestAdjustDateTime.json";
        CreateTestFile(fileName);
        List<Screening> testScreenings = CreateTestScreenings(3, fileName);
        for(int i = 0; i < 3; i++)
        {
            string dateTimeString = $"0{i}-01-2000 0{i}:0{i}";
            ScreeningDataController.AdjustDateTime(testScreenings[i], dateTimeString);

            DateTime dateTime = new DateTime(2000, 1, i, i, i, 0); //should match string above
            Assert.AreEqual(testScreenings[i].ScreeningDateTime, dateTime); //test if instance in memory is changed succesfully
            //test instance in ScreeningDB.json. Can test against instance in memory if previous test is succesful.
            Screening readScreening = JsonHandler.Get<Screening>(testScreenings[i].ID, fileName);
            Assert.AreEqual(readScreening.ScreeningDateTime, testScreenings[i].ScreeningDateTime);

        }
    }

    [TestMethod]
    public void TestAdjustTime()
    {
        string fileName = "TestFiles/TestScreening_TestAdjustTime.json";
        CreateTestFile(fileName);
        List<Screening> testScreenings = CreateTestScreenings(3, fileName);

        for(int i = 0; i < 3; i++)
        {
            string timeString = $"0{i}:0{i}";
            ScreeningDataController.AdjustTime(testScreenings[i], timeString);
            
            TimeSpan time = new TimeSpan(i, i, 0); //should match string above
            Assert.AreEqual(testScreenings[i].ScreeningDateTime.TimeOfDay, time); //test if instance in memory is changed succesfully
            //test instance in ScreeningDB.json. Can test against instance in memory if previous test is succesful.
            Screening readScreening = JsonHandler.Get<Screening>(testScreenings[i].ID, fileName);
            Assert.AreEqual(readScreening.ScreeningDateTime.TimeOfDay, testScreenings[i].ScreeningDateTime.TimeOfDay);
        }
    }

    [TestMethod]
    public void TestAdjustDate()
    {
        string fileName = "TestFiles/TestScreening_TestAdjustDate.json";
        CreateTestFile(fileName);
        List<Screening> testScreenings = CreateTestScreenings(3, fileName);

        for(int i = 0; i < 3; i++)
        {
            string dateString = $"0{i}-01-2000";
            ScreeningDataController.AdjustDate(testScreenings[i], dateString);
            
            DateTime date = new DateTime(2000, 1, i); //should match string above
            Assert.AreEqual(testScreenings[i].ScreeningDateTime.Date, date); //test if instance in memory is changed succesfully
            //test instance in ScreeningDB.json. Can test against instance in memory if previous test is succesful.
            Screening readScreening = JsonHandler.Get<Screening>(testScreenings[i].ID, fileName);
            Assert.AreEqual(readScreening.ScreeningDateTime.Date, testScreenings[i].ScreeningDateTime.Date);
        }
    }

    [TestMethod]
    public void TestAdjustAuditorium()
    {
        string fileName = "TestFiles/TestScreening_TestAdjustAuditorium.json";
        CreateTestFile(fileName);
        List<Screening> testScreenings = CreateTestScreenings(3, fileName);
        List<Auditorium> newAuditoriums = CreateTestAuditoriums(3);
        for(int i = 0; i < 3; i++)
        {
            ScreeningDataController.AdjustAuditorium(testScreenings[i], newAuditoriums[i], fileName);
            Assert.AreEqual(testScreenings[i].AssignedAuditorium, newAuditoriums[i]); //test if instance in memory is changed succesfully
            //test instance in ScreeningDB.json. Can test against instance in memory if previous test is succesful.
            Screening readScreening = JsonHandler.Get<Screening>(testScreenings[i].ID, fileName);
            Assert.AreEqual(readScreening.AssignedAuditorium, testScreenings[i].AssignedAuditorium);
        }
    }

    [TestMethod]
    public void TestAddBundle1()
    {

    }

    [TestMethod]
    public void TestAddBundle2()
    {

    }

    [TestMethod]
    public void TestReserveSeat()
    {

    }

    [TestMethod]
    public void TestCancelSeat()
    {

    }

    [TestMethod]
    public void TestUpdateScreening()
    {

    }

}