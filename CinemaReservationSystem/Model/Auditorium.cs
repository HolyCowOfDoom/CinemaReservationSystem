
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
            switch (i)
            {
                case int n when (n >= 1 && n <= 28) || (n >= 41 && n <= 51) || (n >= 66 && n <= 75) || (n >= 90 && n <= 98) ||
                                (n >= 116 && n <= 124) || (n >= 141 && n <= 150) || (n >= 169 && n <= 179) || (n >= 198 && n <= 209) ||
                                (n >= 231 && n <= 240) || (n >= 261 && n <= 271) || (n >= 290 && n <= 302) || (n >= 319 && n <= 332) ||
                                (n >= 347 && n <= 359) || (n >= 374 && n <= 386) || (n >= 399 && n <= 412) || (n >= 423 && n <= 438) ||
                                (n >= 446 && n <= 500):
                    Seats.Add(new Seat("Blue"));
                    break;
                case int n when (n >= 29 && n <= 40) || (n >= 52 && n <= 65) || (n >= 76 && n <= 89) || (n >= 99 && n <= 105) ||
                                (n >= 110 && n <= 115) || (n >= 125 && n <= 129) || (n >= 136 && n <= 140) || (n >= 151 && n <= 155) ||
                                (n >= 164 && n <= 168) || (n >= 180 && n <= 184) || (n >= 193 && n <= 197) || (n >= 210 && n <= 215) ||
                                (n >= 225 && n <= 230) || (n >= 241 && n <= 246) || (n >= 255 && n <= 260) || (n >= 272 && n <= 276) ||
                                (n >= 285 && n <= 289) || (n >= 303 && n <= 306) || (n >= 315 && n <= 318) || (n >= 333 && n <= 337) ||
                                (n >= 342 && n <= 346) || (n >= 360 && n <= 373) || (n >= 387 && n <= 398) || (n >= 413 && n <= 422) ||
                                (n >= 439 && n <= 445):
                    Seats.Add(new Seat("Yellow"));
                    break;
                case int n when (n >= 106 && n <= 109) || (n >= 130 && n <= 135) || (n >= 156 && n <= 163) || (n >= 185 && n <= 192) ||
                                (n >= 216 && n <= 224) || (n >= 247 && n <= 254) || (n >= 277 && n <= 284) || (n >= 307 && n <= 314) ||
                                (n >= 338 && n <= 341):
                    Seats.Add(new Seat("Red"));
                    break;
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



