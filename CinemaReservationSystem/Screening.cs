public class Screening
{
    public readonly int ID;
    public Auditorium AssignedAuditorium;
    public DateTime ScreeningDateTime;
    public readonly int MovieID;
    public Screening(Auditorium assignedAuditorium, string screeningDateTime, int movieID)
    {
        List<Screening> allScreenings = JsonHandler.Read<Screening>("ScreeningDB.json");
        ID = allScreenings.Count + 1;
        AssignedAuditorium = assignedAuditorium;
        ScreeningDateTime = DateTime.ParseExact(timeStampString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
        MovieID = movieID;
    }

    public bool AdjustDateTime(string dateTime)
    {
        try
        {
            ScreeningDateTime = DateTime.ParseExact(timeStampString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            bool result = JsonHandler.Update(this, "ScreeningsDB.Json");
            return result;
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Format error. Please use [dd-MM-yyyy HH:mm]");
            return false;
        }
    }

    public bool AdjustTime(string time)
    {
        try
        {
            TimeSpan newTime = TimeSpan.Parse(time);
            ScreeningDateTime = ScreeningDateTime.Date + newTime;
            bool result = JsonHandler.Update(this, "ScreeningsDB.Json");
            return result;
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Format error. Please use [HH:mm]");
            return false;
        }
    }

    public bool AdjustDate(string date)
    {
        try
        {
            ScreeningDateTime = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture) + ScreeningDateTime.TimeOfDay;
            bool result = JsonHandler.Update(this, "ScreeningsDB.Json");
            return result;
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Format error. Please use [dd-MM-yyyy]");
            return false;
        }
    }

    public bool AdjustAuditorium(Auditorium newAuditorium)
    {
        AssignedAuditorium = newAuditorium;
        bool result = JsonHandler.Update(this, "ScreeningsDB.Json");
        return result;
    }
}