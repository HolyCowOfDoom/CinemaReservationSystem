//for Reservation converter
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

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
            MovieDataController.UpdateMovie(this);
        }
        else ID = (string)id;
    }



}

