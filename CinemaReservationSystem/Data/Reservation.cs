//for Reservation converter
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
//

public class Reservation : IEquatable<Reservation>
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

//should look like:         //requires custom delimiters for each nesting, which is really dumb
//",Password, Reservations"
//"password123, {seatIDs:[1,2,3;4];screeningID:1;totalPrice:30}&{seatIDs..."
//alternatively I could make the headers: Reservation:SeatIDs, Reservation:ScreeningID, Reservation:TotalPrice"
//but I'm not sure how to map that
public class ReservationConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if(text == "" || text is null) return new List<Reservation>();
        List<string> reservationsStr = text.Split("&").ToList();
        List<Reservation> reservations = new();
        foreach(string reservationStr in reservationsStr)
        {
            string reservationsData = reservationStr.Replace("{", "").Replace("}", "");
            List<string>stringParts = reservationsData.Split("; ").ToList();
            List<string> seatIds = stringParts[0].Replace("SeatIDs: ", "").Replace("[", "").Replace("]", "").Split(",").ToList();
            string screeningID = stringParts[1].Replace("ScreeningID: ", "");
            int totalPrice = Convert.ToInt32(stringParts[2].Replace("TotalPrice: ", ""));
            // List<string> seatIds = row.GetField(1).Split(",").ToList();
            // string seatIdsStr = row.GetField(1).Replace("{", "");
            // string screeningID = row.GetField( 2 );
            // int totalPrice = Convert.ToInt32(row.GetField( 2 ));
        
            Reservation reservation = new(seatIds, screeningID, totalPrice);
            reservations.Add(reservation);
        }
        return reservations;
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        string finalString = "";
        if(value is null) Console.WriteLine("value was null");
        else if(value is not List<Reservation>) Console.WriteLine("this shouldn't happen");
        else if(value is List<Reservation>){
            List<Reservation> reservations = value as List<Reservation>;
            if (reservations.Count == 0) return finalString;
            else{
                foreach(Reservation reservation in reservations)
                {
                    string newValue = "{";
                    string seatIDs = "SeatIDs: [" + string.Join( ",", reservation.SeatIDs) + "]; ";
                    string screeningID = "ScreeningID: " + reservation.ScreeningID + "; ";
                    string totalPrice = "TotalPrice: " + reservation.TotalPrice.ToString();
                    newValue += seatIDs + screeningID + totalPrice + "}";
                    finalString += newValue + "&";
                }
                finalString = finalString.Remove(finalString.Length -1);
            }
        }
        return base.ConvertToString(finalString, row, memberMapData);
    }
}