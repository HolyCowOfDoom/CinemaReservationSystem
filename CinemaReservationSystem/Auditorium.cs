public class Auditorium : ObjectHasID
{
    public List<Seat> Seats { get; }
    public string ID { get; }

    public Auditorium(string id, int numberOfSeats)
    {
        ID = id;
        Seats = new List<Seat>();
        InitializeSeats(numberOfSeats);
    }

    private void InitializeSeats(int numberOfSeats)
    {
        // ill give the seats specific colors later, for now its random
        string[] colors = { "Blue", "Yellow", "Red" };
        Random rand = new Random();

        for (int i = 0; i < numberOfSeats; i++)
        {
            string color = colors[rand.Next(0, colors.Length)];
            Seats.Add(new Seat(color));
        }
    }

    public string GetSeatInfo(int seatID)
    {
        foreach (Seat seat in Seats)
        {
            if (seat.ID == seatID)
                return seat.ToString();
        }
        return "Seat not found.";
    }

    public static Auditorium GetAuditorium(int auditoriumNumber)
    {
        switch (auditoriumNumber)
        {
            case 1:
                return new Auditorium("1", 150);
            case 2:
                return new Auditorium("2", 300);
            case 3:
                return new Auditorium("3", 500);
            default:
                throw new ArgumentOutOfRangeException("Invalid auditorium number.");
        }
    }

    public void ReserveSeat(int seatID)
    {
        Seat? seat = Seats.Find(s => s.ID == seatID);
        if (seat != null && !seat.IsReserved)
        {
            seat.ReserveSeat();
            Console.WriteLine($"Seat {seatID} reserved successfully.");
        }
        else
        {
            Console.WriteLine($"Seat {seatID} is either already reserved or does not exist.");
        }
    }

    public void UpdateAuditoriumJson()
    {
        JsonHandler.Update<Auditorium>(this, "AuditoriumDB.json");
    }

    public override string ToString()
    {
        return $"Auditorium ID: {ID}, Number of Seats: {Seats.Count}";
    }
}



