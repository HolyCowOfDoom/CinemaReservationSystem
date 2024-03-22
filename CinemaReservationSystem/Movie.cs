using CsvHelper.Configuration.Attributes;

public class Movie : ObjectHasID
{
    public string Title;
    public int AgeRating;
    public List<int> ScreeningIDs;
    public int ID { get; }
    public Movie(string title, int ageRating, List<int>? screenings = null, int? id = null)
    {
        Title = title;
        AgeRating = ageRating;
        ScreeningIDs = screenings == null ? new List<int>() : screenings;

        if (id == null)
        {
            List<Movie> movieList = JsonHandler.Read<Movie>("MovieDB.json");
            int lastID = movieList.Count > 0 ? movieList[movieList.Count -1].ID : 0;
            ID = lastID + 1;
            JsonHandler.Update(this, "MovieDB.json");
        }
        else ID = (int)id;
    }

    public void AddScreening(Auditorium assignedAuditorium, string screeningDateTime)
    {
        Screening newScreening = new Screening(assignedAuditorium, screeningDateTime, this.ID);
        ScreeningIDs.Add(newScreening.ID);
        JsonHandler.Update<Screening>(newScreening, "ScreeningDB.json");
        JsonHandler.Update<Movie>(this, "MovieDB.json");
    }

    public void RemoveScreening(int screeningID)
    {
        JsonHandler.Remove<Screening>(screeningID, "ScreeningDB.json");
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