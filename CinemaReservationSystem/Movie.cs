public class Movie : ObjectHasID
{
    public string Title;
    public int AgeRating;
    public string Description;
    public List<int> ScreeningIDs;
    public int ID { get; }
    public Movie(string title, int ageRating, string description, List<int>? screenings = null, int? id = null)
    {
        Title = title;
        AgeRating = ageRating;
        Description = description;
        ScreeningIDs = screenings == null ? new List<int>() : screenings;

        if (id == null)
        {
            List<Movie> movieList = JsonHandler.Read<Movie>("MovieDB.json");
            int lastID = movieList.Count > 0 ? movieList[movieList.Count -1].ID : 0;
            ID = ++lastID;
            UpdateMovie();
        }
        else ID = (int)id;
    }

    public void AddScreening(Auditorium assignedAuditorium, DateTime? screeningDateTime)
    {
        Screening newScreening = new Screening(assignedAuditorium, screeningDateTime, this.ID);
        ScreeningIDs.Add(newScreening.ID);
        UpdateMovie();
    }

    public void RemoveScreening(int screeningID)
    {
        ScreeningIDs.Remove(screeningID);
        JsonHandler.Remove<Screening>(screeningID, "ScreeningDB.json");
        UpdateMovie();
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
    
    public void UpdateMovie() => JsonHandler.Update<Movie>(this, "MovieDB.json");
}