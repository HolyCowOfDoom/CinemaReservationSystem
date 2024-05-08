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
        return $"Seat ID: {ID},Price: {Price}, Color: {Color}, Reserved: {IsReserved}";
    }

    public double GetPrice()
    {
        return Price;
    }

    public double GetPrice(User user)
    {
        double price = Price;
        
        int userAge = Helper.GetUserAge(user);
        if (userAge < 12 || userAge > 60)
        {
            // 20% discount for users below age 12 or above age 60
            price *= 0.8;
        }

        return price;
    }
}