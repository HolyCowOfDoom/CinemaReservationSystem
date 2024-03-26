using System.Globalization;

public class Screening : ObjectHasID
{
    public Auditorium AssignedAuditorium;
    public DateTime ScreeningDateTime;
    public List<Bundle> Bundles;
    public int MovieID { get; }
    public int ID { get; }
    public Screening(Auditorium assignedAuditorium, DateTime? screeningDateTime, int movieID, int? id = null)
    {
        AssignedAuditorium = assignedAuditorium;
        ScreeningDateTime = (DateTime)(screeningDateTime ?? new DateTime(2000, 1, 1, 0, 0, 0));
        Bundles = new List<Bundle>();
        MovieID = movieID;

        if (id == null)
        {
            List<Screening> screeningList = JsonHandler.Read<Screening>("ScreeningDB.json");
            int lastID = screeningList.Count > 0 ? screeningList[screeningList.Count -1 ].ID : 0;
            ID = lastID + 1;
            JsonHandler.Update(this, "ScreeningDB.json");
        }
        else ID = (int)id;
    }
    
    public void AdjustDateTime(string dateTime)
    {
        try
        {
            ScreeningDateTime = DateTime.ParseExact(dateTime, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            JsonHandler.Update(this, "ScreeningDB.Json");
        }
        catch (FormatException)
        {
            Console.WriteLine("Format error. Please use [dd-MM-yyyy HH:mm]");
        }
    }

    public void AdjustTime(string time)
    {
        try
        {
            TimeSpan newTime = TimeSpan.Parse(time);
            ScreeningDateTime = ScreeningDateTime.Date + newTime;
            JsonHandler.Update(this, "ScreeningDB.Json");
        }
        catch (FormatException)
        {
            Console.WriteLine("Format error. Please use [HH:mm]");
        }
    }

    public void AdjustDate(string date)
    {
        try
        {
            ScreeningDateTime = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture) + ScreeningDateTime.TimeOfDay;
            JsonHandler.Update(this, "ScreeningDB.Json");
        }
        catch (FormatException)
        {
            Console.WriteLine("Format error. Please use [dd-MM-yyyy]");
        }
    }

    public void AdjustAuditorium(Auditorium newAuditorium)
    {
        AssignedAuditorium = newAuditorium;
        JsonHandler.Update(this, "ScreeningDB.Json");
    }

    public void AddBundle(string bundleCode, string bundleDescription, int price)
    {
        Bundles.Add(new Bundle(bundleCode, bundleDescription, price));
    }

    public void AddBundle(Bundle bundle) => Bundles.Add(bundle);
}