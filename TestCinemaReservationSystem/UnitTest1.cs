using NUnit.Framework;
using System;

[TestFixture]
public class MovieReservationTests
{
    [Test]
    public void TestAuditoriumInitialization()
    {
        // Arrange
        Auditorium auditorium1 = AuditoriumDataController.GetAuditorium(1);
        Auditorium auditorium2 = AuditoriumDataController.GetAuditorium(2);
        Auditorium auditorium3 = AuditoriumDataController.GetAuditorium(3);

        // Assert
        Assert.IsNotNull(auditorium1);
        Assert.IsNotNull(auditorium2);
        Assert.IsNotNull(auditorium3);
    }

    [Test]
    public void TestSeatReservation()
    {
        // Arrange
        Auditorium auditorium = AuditoriumDataController.GetAuditorium(1);
        Seat seatToReserve = auditorium.Seats[0];

        // Act
        bool reservationResult = AuditoriumDataController.ReserveSeat(auditorium, seatToReserve.ID);

        // Assert
        Assert.IsTrue(reservationResult);
        Assert.IsTrue(seatToReserve.IsReserved);
    }

    [Test]
    public void TestSeatReservationFailureWhenAlreadyReserved()
    {
        // Arrange
        Auditorium auditorium = AuditoriumDataController.GetAuditorium(1);
        Seat seatToReserve = auditorium.Seats[0];
        seatToReserve.ReserveSeat(); // Reserve the seat

        // Act
        bool reservationResult = AuditoriumDataController.ReserveSeat(auditorium, seatToReserve.ID);

        // Assert
        Assert.IsFalse(reservationResult);
        Assert.IsTrue(seatToReserve.IsReserved);
    }

    [Test]
    public void TestGetSeatInfo()
    {
        // Arrange
        Auditorium auditorium = AuditoriumDataController.GetAuditorium(1);
        Seat seatToQuery = auditorium.Seats[0];
        string expectedInfo = $"Seat ID: {seatToQuery.ID}, Color: {seatToQuery.Color}, Reserved: {seatToQuery.IsReserved}";

        // Act
        string seatInfo = AuditoriumDataController.GetSeatInfo(auditorium, seatToQuery.ID);

        // Assert
        Assert.AreEqual(expectedInfo, seatInfo);
    }

    // More tests...

    [Test]
    public void TestAuditorium3Initialization()
    {
        // Arrange
        Auditorium auditorium = AuditoriumDataController.GetAuditorium(3);

        // Assert
        Assert.AreEqual(500, auditorium.Seats.Count);
    }

}

[Test]
public void TestReserveSeatMultipleSeats()
{
    // Arrange
    Auditorium auditorium = AuditoriumDataController.GetAuditorium(1);
    Seat seat1 = auditorium.Seats[0];
    Seat seat2 = auditorium.Seats[1];

    // Act
    bool reservationResult1 = AuditoriumDataController.ReserveSeat(auditorium, seat1.ID);
    bool reservationResult2 = AuditoriumDataController.ReserveSeat(auditorium, seat2.ID);

    // Assert
    Assert.IsTrue(reservationResult1);
    Assert.IsTrue(reservationResult2);
    Assert.IsTrue(seat1.IsReserved);
    Assert.IsTrue(seat2.IsReserved);
}

[Test]
public void TestReserveSeatPrice()
{
    // Arrange
    Auditorium auditorium = AuditoriumDataController.GetAuditorium(1);
    Seat blueSeat = auditorium.Seats[0]; // Blue seat
    Seat yellowSeat = auditorium.Seats[34]; // Yellow seat
    Seat redSeat = auditorium.Seats[58]; // Red seat

    // Act
    AuditoriumDataController.ReserveSeat(auditorium, blueSeat.ID);
    AuditoriumDataController.ReserveSeat(auditorium, yellowSeat.ID);
    AuditoriumDataController.ReserveSeat(auditorium, redSeat.ID);

    // Assert
    Assert.AreEqual(10, blueSeat.GetPrice());
    Assert.AreEqual(15, yellowSeat.GetPrice());
    Assert.AreEqual(20, redSeat.GetPrice());
}

[Test]
public void TestGetAuditoriumInvalidNumber()
{
    // Arrange & Act
    TestDelegate testDelegate = () => AuditoriumDataController.GetAuditorium(4);

    // Assert
    Assert.Throws<ArgumentOutOfRangeException>(testDelegate);
}

[Test]
public void TestReserveSeatNonExistingAuditorium()
{
    // Arrange
    Auditorium auditorium = new Auditorium("4"); // Non-existing auditorium

    // Act
    bool reservationResult = AuditoriumDataController.ReserveSeat(auditorium, "123");

    // Assert
    Assert.IsFalse(reservationResult);
}

[Test]
public void TestReserveSeatNonExistingSeatID()
{
    // Arrange
    Auditorium auditorium = AuditoriumDataController.GetAuditorium(1);

    // Act
    bool reservationResult = AuditoriumDataController.ReserveSeat(auditorium, "999");

    // Assert
    Assert.IsFalse(reservationResult);
}

[Test]
public void TestGetSeatInfoReservedSeat()
{
    // Arrange
    Auditorium auditorium = AuditoriumDataController.GetAuditorium(1);
    Seat seat = auditorium.Seats[0];
    seat.ReserveSeat();

    // Act
    string seatInfo = AuditoriumDataController.GetSeatInfo(auditorium, seat.ID);

    // Assert
    Assert.AreEqual($"Seat ID: {seat.ID}, Color: {seat.Color}, Reserved: {true}", seatInfo);
}

[Test]
public void TestGetSeatInfoUnreservedSeat()
{
    // Arrange
    Auditorium auditorium = AuditoriumDataController.GetAuditorium(1);
    Seat seat = auditorium.Seats[0];

    // Act
    string seatInfo = AuditoriumDataController.GetSeatInfo(auditorium, seat.ID);

    // Assert
    Assert.AreEqual($"Seat ID: {seat.ID}, Color: {seat.Color}, Reserved: {false}", seatInfo);
}

[Test]
public void TestGetSeatInfoNonExistingSeat()
{
    // Arrange
    Auditorium auditorium = AuditoriumDataController.GetAuditorium(1);

    // Act
    string seatInfo = AuditoriumDataController.GetSeatInfo(auditorium, "999");

    // Assert
    Assert.AreEqual("Seat not found.", seatInfo);
}

[Test]
public void TestInitializeSeatsAuditorium1()
{
    // Arrange
    Auditorium auditorium = new Auditorium("1");

    // Act
    AuditoriumDataController.InitializeSeats(auditorium);

    // Assert
    Assert.AreEqual(150, auditorium.Seats.Count);
    foreach (var seat in auditorium.Seats)
    {
        if (seat.Color == "Blue")
        {
            Assert.IsTrue(seat.ID.StartsWith("1") || seat.ID.StartsWith("2"));
            Assert.IsTrue(seat.Price == 10);
        }
        else if (seat.Color == "Yellow")
        {
            Assert.IsTrue(seat.ID.StartsWith("3") || seat.ID.StartsWith("4"));
            Assert.IsTrue(seat.Price == 15);
        }
        else if (seat.Color == "Red")
        {
            Assert.IsTrue(seat.ID.StartsWith("5") || seat.ID.StartsWith("6"));
            Assert.IsTrue(seat.Price == 20);
        }
    }
}

[Test]
public void TestInitializeSeatsAuditorium2()
{
    // Arrange
    Auditorium auditorium = new Auditorium("2");

    // Act
    AuditoriumDataController.InitializeSeats(auditorium);

    // Assert
    Assert.AreEqual(300, auditorium.Seats.Count);
    // Add assertions similar to InitializeSeatsAuditorium1 for auditorium 2
}

