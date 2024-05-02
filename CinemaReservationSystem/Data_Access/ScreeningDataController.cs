using System.Globalization;
public static class ScreeningDataController
{
    public static string DBFilePath = "Data/ScreeningDB.json";
     // Adjust the datetime based on a datetime string with the format : dd-MM-yyyy HH:mm
    public static void AdjustDateTime(Screening screening, string dateTime)
    {
        try
        {
            screening.ScreeningDateTime = DateTime.ParseExact(dateTime, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            UpdateScreening(screening);
        }
        catch (FormatException)
        {
            Console.WriteLine("Format error. Please use [dd-MM-yyyy HH:mm]");
        }
    }

    // Adjust screening time based on a datetime string format : HH:mm
    public static void AdjustTime(Screening screening, string time)
    {
        try
        {
            TimeSpan newTime = TimeSpan.Parse(time);
            screening.ScreeningDateTime = screening.ScreeningDateTime.Date + newTime;
            UpdateScreening(screening);
        }
        catch (FormatException)
        {
            Console.WriteLine("Format error. Please use [HH:mm]");
        }
    }

    // Adjust date of screening based on datetime string with format : dd-MM-yyyy
    public static void AdjustDate(Screening screening, string date)
    {
        try
        {
            screening.ScreeningDateTime = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture) + screening.ScreeningDateTime.TimeOfDay;
            UpdateScreening(screening);
        }
        catch (FormatException)
        {
            Console.WriteLine("Format error. Please use [dd-MM-yyyy]");
        }
    }

    // change screening auditorium (resets seat reservations)
    public static void AdjustAuditorium(Screening screening, Auditorium newAuditorium)
    {
        screening.AssignedAuditorium = newAuditorium;
        UpdateScreening(screening);
    }

    // adds bundles based on bundle fields
    public static void AddBundle(Screening screening, string bundleCode, string bundleDescription, int price)
    {
        screening.Bundles.Add(new Bundle(bundleCode, bundleDescription, price));
        UpdateScreening(screening);
    }

    // adds given bundle object to the bundle list
    public static void AddBundle(Screening screening, Bundle bundle)
    {
        screening.Bundles.Add(bundle);
        UpdateScreening(screening);
    }

    public static bool ReserveSeat(Screening screening, string seatID)
    {
        bool succesfullReserve = AuditoriumDataController.ReserveSeat(screening.AssignedAuditorium,seatID);
        if (succesfullReserve) {
            UpdateScreening(screening);
            Console.WriteLine($"Screening.cs: Seat {seatID} reserved successfully and ScreeningDB updated.");
            return true;
        }
        else{
            Console.WriteLine($"Screening.cs: Seat {seatID} is either already reserved or does not exist.");
            return false;
        }

    }

    // updates this.screening to the database
    public static void UpdateScreening(Screening screening) => JsonHandler.Update<Screening>(screening, DBFilePath);
}