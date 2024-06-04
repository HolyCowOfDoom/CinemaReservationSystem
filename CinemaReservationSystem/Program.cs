using System.Diagnostics;
using System.Formats.Asn1;
using System.Reflection;
using System.Security.AccessControl;

// Following code is to clean up the database upon program startup
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

