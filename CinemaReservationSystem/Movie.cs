public class Movie
{
    public readonly int ID;
    public string Title;
    public int AgeRating;
    public List<int> ScreeningIDs;
    public Movie(string title, int ageRating)
    {
        List<Movie> allMovies = JsonHandler.Read<Movie>("MovieDB.json");
        MovieID = allMovies.Count + 1;
        Title = title;
        AgeRating = ageRating;
        Screenings = new List<Screening>();
    }

    public bool AddScreening(Auditorium assignedAuditorium, DateTime screeningDateTime)
    {
        Screenings newScreening = new Screening(assignedAuditorium, screeningDateTime, this.ID);
        Screenings.Add(newScreening);
        JsonHandler.Update<Screening>(newScreening, "ScreeningDB.json");
    }
}