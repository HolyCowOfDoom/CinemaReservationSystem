//for Reservation converter
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
//

public class Reservation : IEquatable<Reservation>
{
    public List<string> SeatIDs {get; set;}
    public string ScreeningID {get;}
    public int TotalPrice{get;}
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

    //makes object1.Equals(object2) return true if their fields match. default returns false as they ae different obects.
    //https://stackoverflow.com/questions/25461585/operator-overloading-equals
    public static bool operator== (Reservation rsv1, Reservation rsv2)
    {
        return (    rsv1.SeatIDs == rsv2.SeatIDs 
                    && rsv1.ScreeningID == rsv2.ScreeningID 
                    && rsv1.TotalPrice == rsv2.TotalPrice);
    }

    public static bool operator!= (Reservation rsv1, Reservation rsv2)
    {
        return !(   rsv1.SeatIDs == rsv2.SeatIDs 
                    && rsv1.ScreeningID == rsv2.ScreeningID 
                    && rsv1.TotalPrice == rsv2.TotalPrice);
    }
    public bool Equals(Reservation other)
    {   
        bool returnvalue = (SeatIDs.SequenceEqual(other.SeatIDs)
                    && ScreeningID == other.ScreeningID 
                    && TotalPrice == other.TotalPrice);
        return returnvalue;
    }
    //this last like is because the one above can't override Equals due to a signature mismatch due to "User other"
    public override bool Equals(object obj) => obj is Reservation && Equals(obj as Reservation);

}
