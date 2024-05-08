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
