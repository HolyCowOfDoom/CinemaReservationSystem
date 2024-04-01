using System.Collections;

public class Seat : ObjectHasID
{
    public string Color { get; }
    public string ID { get; }
    public bool IsReserved { get; private set; }

    private static int lastID = 0;

    public Seat(string color)
    {
        Color = color;
        int intID = Convert.ToInt32(ID);
        intID = ++lastID;
        ID = Convert.ToString(intID);
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