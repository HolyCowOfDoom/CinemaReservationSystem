public class Reservation
{
    public List<string> SeatIDs;
    public readonly string ScreeningID;
    public readonly int TotalPrice;
    public Reservation(List<string> seatIDs, string screeningID, int totalPrice)
    {
        SeatIDs = seatIDs;
        ScreeningID = screeningID;
        TotalPrice = totalPrice;
    }
    public Reservation(string seatID, string screeningID, int totalPrice)
    {
        SeatIDs = [seatID];
        ScreeningID = screeningID;
        TotalPrice = totalPrice;
    }
}