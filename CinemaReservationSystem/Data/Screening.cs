using System.Globalization;

public class Screening : ObjectHasID
{
    public static string DBFilePath = "Data/ScreeningDB.json";
    public Auditorium AssignedAuditorium;
    public DateTime ScreeningDateTime;
    public List<Bundle> Bundles;
    public string MovieID { get; }
    public string ID { get; }

    // only intance this class via the addscreening method in movie (anything else will mess the DB up)
    public Screening(Auditorium assignedAuditorium, DateTime? screeningDateTime, string movieID, string? id = null, string altFilePath = "")
    {

        Directory.CreateDirectory("Data");
        using (StreamWriter w = File.AppendText("Data/ScreeningDB.json")) //create file if it doesn't already exist

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
            ScreeningDataController.UpdateScreening(this, altFilePath);
        }
        else ID = (string)id;
    }
    
   
}