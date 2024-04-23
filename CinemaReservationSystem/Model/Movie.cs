public class Movie : ObjectHasID
{
    public string Title;
    public int AgeRating;
    public string Description;
    public string Genre;
    public List<string> ScreeningIDs;
    public string ID { get; }
    public Movie(string title, int ageRating, string description, string genre, List<string>? screenings = null, string? id = null)
    {
        Title = title;
        AgeRating = ageRating;
        Description = description;
        Genre = genre;
        ScreeningIDs = screenings == null ? new List<string>() : screenings;

        if (id == null)
        {

            // List<Movie> movieList = JsonHandler.Read<Movie>("MovieDB.json");
            // int lastID = movieList.Count > 0 ? movieList[movieList.Count -1].ID : 0;
            // ID = ++lastID;
            ID = Guid.NewGuid().ToString();
            UpdateMovie();
        }
        else ID = (string)id;
    }

    public void AddScreening(Auditorium assignedAuditorium, DateTime? screeningDateTime)
    {
        Screening newScreening = new Screening(assignedAuditorium, screeningDateTime, this.ID);
        ScreeningIDs.Add(newScreening.ID);
        UpdateMovie();
    }

    public void RemoveScreening(string screeningID)
    {
        ScreeningIDs.Remove(screeningID);
        JsonHandler.Remove<Screening>(screeningID, "Model/ScreeningDB.json");
        UpdateMovie();
    }

    public List<Screening> GetAllMovieScreenings()
    {
        List<Screening>? allScreenings = JsonHandler.Read<Screening>("Model/ScreeningDB.json");
        List<Screening> movieScreenings = new List<Screening>();
        if (allScreenings != null)
        {
            foreach (string screeningID in ScreeningIDs)
            {
                foreach (Screening screening in allScreenings)
                {
                    if (screening.ID == screeningID) movieScreenings.Add(screening);
                }
            }
        }
        return movieScreenings;
    }
    
    public void UpdateMovie() => JsonHandler.Update<Movie>(this, "Model/MovieDB.json");
}