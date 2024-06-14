public class Auditorium : ObjectHasID
{
    public List<Seat> Seats{get;}
    public string ID{get;}

    public Auditorium(string id, List<Seat>? seats = null)
    {
        //Directory.CreateDirectory("Data");
       // using (StreamWriter w = File.AppendText("Data/AuditoriumDB.json")); //create file if it doesn't already exist
        ID = id;
        if (seats != null)
        {
            Seats = seats;
        }
        else
        {
            Seats = new List<Seat>();
            AuditoriumDataController.InitializeSeats(this);
        }
    }

    public override string ToString()
    {
        return $"Auditorium ID: {ID}, Number of Seats: {Seats.Count}";
    }

    public Seat? GetSeat(string id)
    {
        foreach (Seat seat in Seats)
        {
            if (seat.ID == id)
            {
                return seat;
            }
        }
        return null;
    }
}



