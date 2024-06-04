using System.Globalization;
public static class ScreeningDataController
{
    public static string DBFilePath = "Data/ScreeningDB.json";
    //NOTE: altFilePath is checked on emptiness in UpdateScreening() only, 

     // Adjust the datetime based on a datetime string with the format : dd-MM-yyyy HH:mm
    public static void AdjustDateTime(Screening screening, string dateTime, string altFilePath = "")
    {
        try
        {
            screening.ScreeningDateTime = DateTime.ParseExact(dateTime, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            UpdateScreening(screening, altFilePath);
        }
        catch (FormatException)
        {
            Console.WriteLine($"Format error with {dateTime}. Please use [dd-MM-yyyy HH:mm]");
        }
        catch
        {
            Console.WriteLine($"caught other unexpected error with {dateTime} ");
        }
    }
    public static void AdjustTime(Screening screening, string time, string altFilePath = "")
    // Adjust screening time based on a datetime string format : HH:mmWS    public static void AdjustTime(Screening screening, string time)
    {
        try
        {
            TimeSpan newTime = TimeSpan.Parse(time);
            screening.ScreeningDateTime = screening.ScreeningDateTime.Date + newTime;
            UpdateScreening(screening, altFilePath);
        }
        catch (FormatException)
        {
            Console.WriteLine("Format error. Please use [HH:mm]");
        }
    }

    // Adjust date of screening based on datetime string with format : dd-MM-yyyy
    public static void AdjustDate(Screening screening, string date, string altFilePath = "")
    {
        try
        {
            screening.ScreeningDateTime = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture) + screening.ScreeningDateTime.TimeOfDay;
            UpdateScreening(screening, altFilePath);
        }
        catch (FormatException)
        {
            Console.WriteLine("Format error. Please use [dd-MM-yyyy]");
        }
    }

    // change screening auditorium (resets seat reservations)
    public static void AdjustAuditorium(Screening screening, Auditorium newAuditorium, string altFilePath = "")
    {
        screening.AssignedAuditorium = newAuditorium;
        UpdateScreening(screening, altFilePath);
    }

    // adds bundles based on bundle fields
    public static void AddBundle(Screening screening, string bundleCode, string bundleDescription, int price, string altFilePath = "")
    {
        screening.Bundles.Add(new Bundle(bundleCode, bundleDescription, price));
        UpdateScreening(screening, altFilePath);
    }

    // adds given bundle object to the bundle list
    public static void AddBundle(Screening screening, Bundle bundle, string altFilePath = "")
    {
        screening.Bundles.Add(bundle);
        UpdateScreening(screening, altFilePath);
    }

    public static bool ReserveSeat(Screening screening, string seatID, string altFilePath = "")
    {
        bool succesfullReserve = AuditoriumDataController.ReserveSeat(screening.AssignedAuditorium,seatID);
        if (succesfullReserve) {
            UpdateScreening(screening, altFilePath);
            Console.WriteLine($"Screening.cs: Seat {seatID} reserved successfully and ScreeningDB updated.");
            return true;
        }
        else{
            Console.WriteLine($"Screening.cs: Seat {seatID} is either already reserved or does not exist.");
            return false;
        }

    }

    public static bool CancelSeat(Screening screening, string seatID, string altFilePath = "")
    {
        bool succesfullCancel = AuditoriumDataController.CancelSeat(screening.AssignedAuditorium,seatID);
        if (succesfullCancel) {
            UpdateScreening(screening, altFilePath);
            Console.WriteLine($"Screening.cs: Seat {seatID} cancelled successfully and ScreeningDB updated.");
            return true;
        }
        else{
            Console.WriteLine($"Screening.cs: Seat {seatID} is either already cancelled or does not exist.");
            return false;
        }

    }

    // updates this.screening to the database
    public static void UpdateScreening(Screening screening, string altFilePath = "") 
    {
        if(altFilePath == "") JsonHandler.Update<Screening>(screening, DBFilePath);
        else JsonHandler.Update<Screening>(screening, altFilePath);
    }
}