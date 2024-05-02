public static class MovieDataController
{
    private static string DBFilePath = "Data/ScreeningDB.json";
    public static void AddScreening(Movie movie, Auditorium assignedAuditorium, DateTime? screeningDateTime)
    {
        Screening newScreening = new Screening(assignedAuditorium, screeningDateTime, movie.ID);
        movie.ScreeningIDs.Add(newScreening.ID);
        UpdateMovie(movie);
    }

    public static void RemoveScreening(Movie movie, string screeningID)
    {
        movie.ScreeningIDs.Remove(screeningID);
        JsonHandler.Remove<Screening>(screeningID, DBFilePath);
        UpdateMovie(movie);
    }

    public static List<Screening> GetAllMovieScreenings(Movie movie)
    {
        List<Screening>? allScreenings = JsonHandler.Read<Screening>(DBFilePath);
        List<Screening> movieScreenings = new List<Screening>();
        if (allScreenings != null)
        {
            foreach (string screeningID in movie.ScreeningIDs)
            {
                foreach (Screening screening in allScreenings)
                {
                    if (screening.ID == screeningID) movieScreenings.Add(screening);
                }
            }
        }
        return movieScreenings;
    }
    
    public static void UpdateMovie(Movie movie) => JsonHandler.Update<Movie>(movie, DBFilePath);

}