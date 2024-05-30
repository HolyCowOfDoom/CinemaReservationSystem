namespace TestCinemaReservationSystem;

[TestClass]
public class TestReservation : TestHelperMethods
{
    [TestMethod]
    public void TestConstructor()
    {
        List<string> seatIDs = new() {"1", "2", "3"};
        Reservation reservation = new(seatIDs, "1", 20);
        Assert.AreEqual(reservation.SeatIDs, seatIDs);
        Assert.AreEqual(reservation.ScreeningID, "1");
        Assert.AreEqual(reservation.TotalPrice, 20);
    }
}