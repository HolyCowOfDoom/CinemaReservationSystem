public class Movie
{
    public readonly int ID;
    public string Title;
    public int AgeRating;
    public List<int> ScreeningIDs = new List<int>;
    public Movie(string title, int ageRating, List<int> screenings = null)
    {
        ID = (JsonHandler.Read<Movie>("MovieDB.json")).Count + 1;
        Title = title;
        AgeRating = ageRating;
        if (screenings != null) Screenings = screenings;
    }

    public void AddScreening(Auditorium assignedAuditorium, string screeningDateTime)
    {
        Screenings newScreening = new Screening(assignedAuditorium, screeningDateTime, this.ID);
        Screenings.Add(newScreening.ID);
        JsonHandler.Update<Screening>(newScreening, "ScreeningDB.json");
        JsonHandler.Update<Movie>(this, "MovieDB.json");
    }

    public void RemoveScreening(int screeningID)
    {
        screeningToRemove = GetScreeningByID(screeningID);
        JsonHandler.Remove<Screening>(screeningToRemove);
    }

    public List<Screening> GetAllMovieScreenings()
    {
        List<Screening> allScreenings = JsonHandler.Read<Screening>("ScreeningDB.json");
        List<Screening> movieScreenings = new List<Screening>();
        foreach (int screeningID in ScreeningIDs)
        {
            foreach (Screening screening in allScreenings)
            {
                if (screening.ID == screeningID) movieScreenings.Add(screening);
            }
        }
        return movieScreenings;
    }

    public static Screening? GetScreeningByID(int screeningID)
    {
        List<Screening> allScreenings = JsonHandler.Read<Screening>("ScreeningDB.json");
        foreach (Screening screening in allScreenings)
        {
            if (screening.ID == screeningID) return screening;
        }
        return null;
    }


}