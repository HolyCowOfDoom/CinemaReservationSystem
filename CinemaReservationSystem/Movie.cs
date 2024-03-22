using CsvHelper.Configuration.Attributes;

public class Movie : ObjectHasID
{
    public int ID { get;}
    public string Title;
    public int AgeRating;
    public List<int> ScreeningIDs = new List<int>();
    public Movie(string title, int ageRating, List<int>? screenings = null)
    {
        List<Screening>? allMovies = JsonHandler.Read<Screening>("MovieDB.json");
        ID = allMovies != null ? allMovies.Count + 1 : 1;
        Title = title;
        AgeRating = ageRating;
        if (screenings != null) ScreeningIDs = screenings;
    }

    public void AddScreening(Auditorium assignedAuditorium, string screeningDateTime)
    {
        Screening newScreening = new Screening(assignedAuditorium, screeningDateTime, this.ID);
        ScreeningIDs.Add(newScreening.ID);
        bool updateSucces = JsonHandler.Update<Screening>(newScreening, "ScreeningDB.json");
        updateSucces = JsonHandler.Update<Movie>(this, "MovieDB.json");
    }

    public void RemoveScreening(int screeningID)
    {
        bool removalSucces = JsonHandler.Remove<Screening>(screeningID, "ScreeningDB.json");
    }

    public List<Screening> GetAllMovieScreenings()
    {
        List<Screening>? allScreenings = JsonHandler.Read<Screening>("ScreeningDB.json");
        List<Screening> movieScreenings = new List<Screening>();
        if (allScreenings != null)
        {
            foreach (int screeningID in ScreeningIDs)
            {
                foreach (Screening screening in allScreenings)
                {
                    if (screening.ID == screeningID) movieScreenings.Add(screening);
                }
            }
        }
        return movieScreenings;
    }
}