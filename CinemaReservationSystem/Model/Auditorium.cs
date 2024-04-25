
public class Auditorium : ObjectHasID
{
    public List<Seat> Seats { get; }
    public string ID { get; }

    public Auditorium(string id, List<Seat>? seats = null)
    {
        ID = id;
        if (seats != null)
        {
            Seats = seats;
        }
        else
        {
            Seats = new List<Seat>();
            InitializeSeats();
        }
    }

    private void InitializeSeats()
    {
        switch (this.ID)
        {
            case "1":
                InitializeSeatsForAuditorium1(150);
                break;
            case "2":
                InitializeSeatsForAuditorium2(300);
                break;
            case "3":
                InitializeSeatsForAuditorium3(500);
                break;
            default:
                throw new ArgumentException($"Invalid auditorium ID. Current ID: {ID}");
        }
    }

    private void InitializeSeatsForAuditorium1(int numberOfSeats)
    {
        for (int i = 1; i <= numberOfSeats; i++)
        {
            switch (i)
            {
                case int n when (n >= 1 && n <= 33) || (n >= 36 && n <= 44) || (n >= 49 && n <= 55) || (n >= 62 && n <= 67) ||
                                (n >= 74 && n <= 79) || (n >= 84 && n <= 89) || (n >= 96 && n <= 102) || (n >= 107 && n <= 115) ||
                                (n >= 117 && n <= 150):
                    Seats.Add(new Seat("Blue"));
                    break;
                case int n when (n >= 34 && n <= 35) || (n >= 45 && n <= 48) || (n >= 56 && n <= 57) || (n >= 60 && n <= 61) ||
                                (n >= 68 && n <= 69) || (n >= 72 && n <= 73) || (n >= 80 && n <= 81) || (n >= 83 && n <= 84) ||
                                (n >= 90 && n <= 91) || (n >= 94 && n <= 95) || (n >= 103 && n <= 106) || (n >= 116 && n <= 117):
                    Seats.Add(new Seat("Yellow"));
                    break;
                case int n when (n >= 58 && n <= 59) || (n >= 70 && n <= 71) || (n >= 82 && n <= 83) || (n >= 92 && n <= 93):
                    Seats.Add(new Seat("Red"));
                    break;
            }
        }
    }

    private void InitializeSeatsForAuditorium2(int numberOfSeats)
    {
        for (int i = 1; i <= numberOfSeats; i++)
        {
            switch (i)
            {
                case int n when (n >= 1 && n <= 21) || (n >= 28 && n <= 36) || (n >= 45 && n <= 52) || (n >= 61 && n <= 67) ||
                                (n >= 78 && n <= 83) || (n >= 94 && n <= 99) || (n >= 112 && n <= 117) || (n >= 130 && n <= 134) ||
                                (n >= 149 && n <= 152) || (n >= 167 && n <= 170) || (n >= 185 && n <= 188) || (n >= 201 && n <= 205) ||
                                (n >= 216 && n <= 222) || (n >= 231 && n <= 238) || (n >= 245 && n <= 252) || (n >= 259 && n <= 300):
                    Seats.Add(new Seat("Blue"));
                    break;
                case int n when (n >= 22 && n <= 27) || (n >= 37 && n <= 44) || (n >= 53 && n <= 60) || (n >= 69 && n <= 77) ||
                                (n >= 84 && n <= 87) || (n >= 90 && n <= 93) || (n >= 100 && n <= 103) || (n >= 108 && n <= 111) ||
                                (n >= 118 && n <= 120) || (n >= 127 && n <= 129) || (n >= 135 && n <= 138) || (n >= 145 && n <= 148) ||
                                (n >= 153 && n <= 156) || (n >= 163 && n <= 166) || (n >= 171 && n <= 174) || (n >= 181 && n <= 184) ||
                                (n >= 189 && n <= 192) || (n >= 197 && n <= 200) || (n >= 206 && n <= 209) || (n >= 212 && n <= 215) ||
                                (n >= 223 && n <= 230) || (n >= 239 && n <= 244) || (n >= 253 && n <= 258):
                    Seats.Add(new Seat("Yellow"));
                    break;
                case int n when (n >= 88 && n <= 89) || (n >= 104 && n <= 107) || (n >= 121 && n <= 126) || (n >= 139 && n <= 144) ||
                                (n >= 157 && n <= 162) || (n >= 175 && n <= 180) || (n >= 193 && n <= 196) || (n >= 210 && n <= 211):
                    Seats.Add(new Seat("Red"));
                    break;
            }
        }
    }

    private void InitializeSeatsForAuditorium3(int numberOfSeats)
    {
        for (int i = 1; i <= numberOfSeats; i++)
        {
            if ((i >= 1 && i <= 28) || (i >= 41 && i <= 51) || (i >= 66 && i <= 75) || (i >= 90 && i <= 98) ||
                (i >= 116 && i <= 124) || (i >= 141 && i <= 150) || (i >= 169 && i <= 179) || (i >= 198 && i <= 209) ||
                (i >= 231 && i <= 240) || (i >= 261 && i <= 271) || (i >= 290 && i <= 302) || (i >= 319 && i <= 332) ||
                (i >= 347 && i <= 359) || (i >= 374 && i <= 386) || (i >= 399 && i <= 412) || (i >= 423 && i <= 438) ||
                (i >= 446 && i <= 500))
            {
                Seats.Add(new Seat("Blue"));
            }
            else if ((i >= 29 && i <= 40) || (i >= 52 && i <= 65) || (i >= 76 && i <= 89) || (i >= 99 && i <= 105) ||
                    (i >= 110 && i <= 115) || (i >= 125 && i <= 129) || (i >= 136 && i <= 140) || (i >= 151 && i <= 155) ||
                    (i >= 164 && i <= 168) || (i >= 180 && i <= 184) || (i >= 193 && i <= 197) || (i >= 210 && i <= 215) ||
                    (i >= 225 && i <= 230) || (i >= 241 && i <= 246) || (i >= 255 && i <= 260) || (i >= 272 && i <= 276) ||
                    (i >= 285 && i <= 289) || (i >= 303 && i <= 306) || (i >= 315 && i <= 318) || (i >= 333 && i <= 337) ||
                    (i >= 342 && i <= 346) || (i >= 360 && i <= 373) || (i >= 387 && i <= 398) || (i >= 413 && i <= 422) ||
                    (i >= 439 && i <= 445))
            {
                Seats.Add(new Seat("Yellow"));
            }
            else if ((i >= 106 && i <= 109) || (i >= 130 && i <= 135) || (i >= 156 && i <= 163) || (i >= 185 && i <= 192) ||
                    (i >= 216 && i <= 224) || (i >= 247 && i <= 254) || (i >= 277 && i <= 284) || (i >= 307 && i <= 314) ||
                    (i >= 338 && i <= 341))
            {
                Seats.Add(new Seat("Red"));
            }
        }
    }

    public string GetSeatInfo(string seatID)
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
                return new Auditorium("1");
            case 2:
                return new Auditorium("2");
            case 3:
                return new Auditorium("3");
            default:
                throw new ArgumentOutOfRangeException("Invalid auditorium number.");
        }
    }

    public bool ReserveSeat(string seatID)
    {
        Seat? seat = Seats.Find(s => s.ID == seatID);
        if (seat != null && !seat.IsReserved)
        {
            seat.ReserveSeat();
            Console.WriteLine($"Auditorium.cs: Seat {seatID} reserved successfully.");
            return true;
        }
        else
        {
            Console.WriteLine($"Auditorium.cs: Seat {seatID} is either already reserved or does not exist.");
            return false;
        }
    }

    public void UpdateAuditoriumJson()
    {
        JsonHandler.Update<Auditorium>(this, "Model/AuditoriumDB.json");
    }

    public override string ToString()
    {
        return $"Auditorium ID: {ID}, Number of Seats: {Seats.Count}";
    }
}



