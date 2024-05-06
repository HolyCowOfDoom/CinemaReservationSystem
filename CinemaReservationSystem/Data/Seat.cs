public class Seat : ObjectHasID
{
    public string Color {get;}
    public string ID {get;}
    public bool IsReserved {get; private set;}

    public double Price { get; }
    private static int lastID = 0;

    public Seat(string color, double price, string? id = null, bool isReserved = false)
    {
        Color = color;
        Price = price;
        ID = id != null ? id : Convert.ToString(++lastID);
        IsReserved = isReserved;
    }

    public void ReserveSeat()
    {
        IsReserved = true;
    }

    // public bool IsSeatReserved() //property should suffice
    // {
    //     return IsReserved;
    // }

    public override string ToString()
    {
        return $"Seat ID: {ID}, Color: {Color}, Reserved: {IsReserved}";
    }

    public double GetPrice()
    {
        return Price;
    }
}