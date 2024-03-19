public class Seat
{
    public string Color { get; }
    public int ID { get; }
    public bool IsReserved { get; private set; }

    private static int lastID = 0;

    public Seat(string color)
    {
        Color = color;
        ID = ++lastID;
        IsReserved = false;
    }

    public void ReserveSeat()
    {
        IsReserved = true;
    }

    public bool IsSeatReserved()
    {
        return IsReserved;
    }

    public override string ToString()
    {
        return $"Seat ID: {ID}, Color: {Color}, Reserved: {IsReserved}";
    }
}