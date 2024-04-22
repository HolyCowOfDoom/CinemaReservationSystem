//using System.ComponentModel;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

public sealed class UserClassMap : ClassMap<User>
{
    public UserClassMap()
    {
        Map(x => x.ID).Name("id");
        Map(x => x.Name).Name("name");
        Map(x => x.BirthDate).Name("birthDate");
        Map(x => x.Email).Name("email");
        Map(x => x.Password).Name("password");
        Map(x => x.Admin).Name("admin");
        Map(x => x.Reservations).Name("reservations").TypeConverter<ReservationConverter>();
    }
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
