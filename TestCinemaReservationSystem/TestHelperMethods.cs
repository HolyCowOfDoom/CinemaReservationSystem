namespace TestCinemaReservationSystem;
[TestClass]
public class TestHelperMethods
{
    public void CreateTestFile(string fileName)
    {
        Directory.CreateDirectory("TestFiles");
        using var file = File.Create(fileName); //overwrites file with same name, to avoid duplicates
    }

    public List<User> CreateTestUsers(int count, bool addReservations, string fileName) //fileName necessary as constructor adds itself to a file
    {
        List<User> testUsers = new();
        for(int i = 1; i <= count; i++ )
        {
            User newUser = new User($"testUser{i}", $"0{i}-01-2000", $"test.user{i}@gmail.com", $"testPassword{i}", false, null, fileName);
            if (addReservations) newUser.Reservations.Add(new Reservation(new List<string>() {$"{i}"}, $"{i}", i*20));
            testUsers.Add(newUser);
        }
        return testUsers;
    }
    public List<Auditorium> CreateTestAuditoriums(int count) //fileName necessary as constructor adds itself to a file
    {
        List<Seat> testSeats = new() {new Seat("blue", 5), new Seat("blue", 5), new Seat("Red", 10)};
        List<Auditorium> testAuditoriums = new();
        for(int i = 0; i < count; i++)
        {
            testAuditoriums.Add(new($"testauditorium{i}", testSeats));
        }
        return testAuditoriums;
    }

    public List<Screening> CreateTestScreenings(int count, string fileName)
    {
        List<Auditorium> testAuditoriums = CreateTestAuditoriums(count);
        DateTime dateTime = new DateTime(2000, 1, 1, 0, 0, 0);
        List<Screening> testScreenings = new();
        for(int i = 0; i < count; i++)
        {
            testScreenings.Add(new Screening(testAuditoriums[i], dateTime, $"movie{i}", $"screening{i}", fileName));
        }
        return testScreenings;
    }
}