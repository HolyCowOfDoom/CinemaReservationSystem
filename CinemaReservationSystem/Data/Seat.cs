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
    
    /*
    this method adds prices without the need of a price parameter
    private double GetPriceFromColor(string color)
    {
        switch (color.ToLower())
        {
            case "red":
                return 20;
            case "yellow":
                return 15;
            case "blue":
                return 10;
            default:
                throw new ArgumentException("Invalid color specified.");
        }
    }
    */

    public void ReserveSeat()
    {
        IsReserved = true;
    }

    public void CancelSeat()
    {
        IsReserved = false;
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

    public double GetDiscountedPrice(User user)
    {
        double finalPrice = Price;
        int userAge = Helper.GetUserAge(user);
        
        // discount for users below the age of 12 and above the age of 60
        if (userAge < 12 || userAge > 60)
        {
            finalPrice *= 0.8; // 20% discount
        }

        return finalPrice;
    }
}