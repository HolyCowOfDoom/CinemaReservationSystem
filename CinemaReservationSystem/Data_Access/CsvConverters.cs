//for Reservation converter
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;




//Reservation

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


//Movie
public class MovieConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if(text == "" || text is null) return new List<Movie>();
        List<string> moviesStr = text.Split("&").ToList();
        List<Movie> movies = new();
        foreach(string movieStr in moviesStr)
        {
            //all the Replace methods just cut out declarations like "Title: " and seperators :;{}[] to get the actual data
            string moviesData = movieStr.Replace("{", "").Replace("}", "");
            List<string>stringParts = moviesData.Split("; ").ToList();
            string Title = stringParts[0].Replace("Title: ", "");
            string ID = stringParts[1].Replace("ID: ", "");
            int AgeRating = Convert.ToInt32(stringParts[2].Replace("Rating: ", ""));
            string Description = stringParts[3].Replace("Description: ", "");
            string Genre = stringParts[4].Replace("Genre: ", "");
            List<string> ScreeningIDs = stringParts[5].Replace("ScreeningIDs: ", "").Replace("[", "").Replace("]", "").Split(",").ToList();

            // List<string> seatIds = row.GetField(1).Split(",").ToList();
            // string seatIdsStr = row.GetField(1).Replace("{", "");
            // string screeningID = row.GetField( 2 );
            // int totalPrice = Convert.ToInt32(row.GetField( 2 ));
        
            Movie movie = new(Title, AgeRating, Description, Genre, ScreeningIDs, ID);
            movies.Add(movie);
        }
        return movies;
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        string finalString = "";
        if(value is null) Console.WriteLine("value was null");
        else if(value is not List<Movie>) Console.WriteLine("this shouldn't happen");
        else if(value is List<Movie>){
            List<Movie> movies = value as List<Movie>;
            if (movies.Count == 0) return finalString;
            else{
                foreach(Movie movie in movies)
                {
                    //surround each movie data with {}
                    string newValue = "{";
                    string Title = "Title: " + movie.Title + "; ";
                    string ID = "ID: " + movie.ID + "; ";
                    string AgeRating = "Rating: " + movie.AgeRating + "; ";
                    string Description = "Description: " + movie.Description + "; ";
                    string Genre = "Genre: " + movie.Genre + "; ";
                    string ScreeningIDs = "ScreeningIDs: [" + string.Join( ",", movie.ScreeningIDs) + "]; ";
                    newValue += Title + ID + AgeRating + Description + Genre + ScreeningIDs + "}";
                    finalString += newValue + "&";
                }
                finalString = finalString.Remove(finalString.Length -1); //remove last "&"
            }
        }
        return base.ConvertToString(finalString, row, memberMapData);
    }
}