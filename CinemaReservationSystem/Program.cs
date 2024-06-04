using System.Diagnostics;
using System.Formats.Asn1;
using System.Reflection;
using System.Security.AccessControl;

// Following code is to clean up the database upon program startup
Directory.CreateDirectory("Data");
using (StreamWriter w = File.AppendText("Data/ScreeningDB.json")); //create file if it doesn't already exist
using (StreamWriter w = File.AppendText("Data/MovieDB.json"));
using (StreamWriter w = File.AppendText("Data/AuditoriumDDB.json"));
 using (StreamWriter w = File.AppendText("Data/UserDB.csv"));

List<Screening> screenings = JsonHandler.Read<Screening>("Data/ScreeningDB.json");
List<Movie> MovieList = JsonHandler.Read<Movie>("Data/MovieDB.json");
List<Screening> screeningsToRemove = new List<Screening>();
for (int i = 0; i < screenings.Count; i++)
{
    Screening currentScreening = screenings[i];
    if (currentScreening.ScreeningDateTime < DateTime.Now)
    {
        screeningsToRemove.Add(currentScreening);
    }
}
foreach(Screening screening in screeningsToRemove)
{ 
    foreach(Movie movie in MovieList)
    {
        if (movie.ScreeningIDs.Contains(screening.ID)) movie.ScreeningIDs.Remove(screening.ID);
    }
    screenings.Remove(screening);
}
JsonHandler.Write<Screening>(screenings, "Data/ScreeningDB.json");
JsonHandler.Write<Movie>(MovieList, "Data/MovieDB.json");

// Start actual program
Interface.GeneralMenu();

