using System.Globalization;

public class Screening : ObjectHasID
{
    public Auditorium AssignedAuditorium;
    public DateTime ScreeningDateTime;
    public List<Bundle> Bundles;
    public string MovieID { get; }
    public string ID { get; }

    // only intance this class via the addscreening method in movie (anything else will mess the DB up)
    public Screening(Auditorium assignedAuditorium, DateTime? screeningDateTime, string movieID, string? id = null)
    {
        AssignedAuditorium = assignedAuditorium;
        ScreeningDateTime = (DateTime)(screeningDateTime ?? new DateTime(2000, 1, 1, 0, 0, 0));
        Bundles = new List<Bundle>();
        MovieID = movieID;

        if (id == null)
        {
            // List<Screening> screeningList = JsonHandler.Read<Screening>("ScreeningDB.json");
            // int lastID = screeningList.Count > 0 ? screeningList[screeningList.Count -1 ].ID : 0;
            // ID = lastID + 1;
            ID = Guid.NewGuid().ToString();
            UpdateScreening();
        }
        else ID = (string)id;
    }
    
    // Adjust the datetime based on a datetime string with the format : dd-MM-yyyy HH:mm
    public void AdjustDateTime(string dateTime)
    {
        try
        {
            ScreeningDateTime = DateTime.ParseExact(dateTime, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            UpdateScreening();
        }
        catch (FormatException)
        {
            Console.WriteLine("Format error. Please use [dd-MM-yyyy HH:mm]");
        }
    }

    // Adjust screening time based on a datetime string format : HH:mm
    public void AdjustTime(string time)
    {
        try
        {
            TimeSpan newTime = TimeSpan.Parse(time);
            ScreeningDateTime = ScreeningDateTime.Date + newTime;
            UpdateScreening();
        }
        catch (FormatException)
        {
            Console.WriteLine("Format error. Please use [HH:mm]");
        }
    }

    // Adjust date of screening based on datetime string with format : dd-MM-yyyy
    public void AdjustDate(string date)
    {
        try
        {
            ScreeningDateTime = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture) + ScreeningDateTime.TimeOfDay;
            UpdateScreening();
        }
        catch (FormatException)
        {
            Console.WriteLine("Format error. Please use [dd-MM-yyyy]");
        }
    }

    // change screening auditorium (resets seat reservations)
    public void AdjustAuditorium(Auditorium newAuditorium)
    {
        AssignedAuditorium = newAuditorium;
        UpdateScreening();
    }

    // adds bundles based on bundle fields
    public void AddBundle(string bundleCode, string bundleDescription, int price)
    {
        Bundles.Add(new Bundle(bundleCode, bundleDescription, price));
        UpdateScreening();
    }

    // adds given bundle object to the bundle list
    public void AddBundle(Bundle bundle)
    {
        Bundles.Add(bundle);
        UpdateScreening();
    }

    // updates this.screening to the database
    public void UpdateScreening() => JsonHandler.Update<Screening>(this, "ScreeningDB.json");
}