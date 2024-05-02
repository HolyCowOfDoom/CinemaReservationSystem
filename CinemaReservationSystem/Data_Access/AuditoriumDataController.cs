public static class AuditoriumDataController
{
    private static string DBFilePath = "Data/AuditoriumDB.json";
    public static void InitializeSeats(Auditorium auditorium)
    {
        switch (auditorium.ID)
        {
            case "1":
                InitializeSeatsForAuditorium1(auditorium, 150);
                break;
            case "2":
                InitializeSeatsForAuditorium2(auditorium, 300);
                break;
            case "3":
                InitializeSeatsForAuditorium3(auditorium, 500);
                break;
            default:
                throw new ArgumentException($"Invalid auditorium ID. Current ID: {auditorium.ID}");
        }
    }

    private static void InitializeSeatsForAuditorium1(Auditorium auditorium, int numberOfSeats)
    {
        for (int i = 1; i <= numberOfSeats; i++)
        {
            string color;
            if ((i >= 1 && i <= 33) || (i >= 36 && i <= 44) || (i >= 49 && i <= 55) || (i >= 62 && i <= 67) ||
                (i >= 74 && i <= 79) || (i >= 86 && i <= 91) || (i >= 98 && i <= 104) || (i >= 109 && i <= 117) ||
                (i >= 120 && i <= 150))
            {
                color = "Blue";
            }
            else if ((i >= 34 && i <= 35) || (i >= 45 && i <= 48) || (i >= 56 && i <= 57) || (i >= 60 && i <= 61) ||
                     (i >= 68 && i <= 69) || (i >= 72 && i <= 73) || (i >= 80 && i <= 81) || (i >= 84 && i <= 85) ||
                     (i >= 92 && i <= 93) || (i >= 96 && i <= 97) || (i >= 105 && i <= 108) || (i >= 118 && i <= 119))
            {
                color = "Yellow";
            }
            else
            {
                color = "Red";
            }

            auditorium.Seats.Add(new Seat(color));
        }
    }

    private static void InitializeSeatsForAuditorium2(Auditorium auditorium, int numberOfSeats)
    {
        for (int i = 1; i <= numberOfSeats; i++)
        {
            string color;
            if ((i >= 1 && i <= 42) || (i >= 49 && i <= 56) || (i >= 63 && i <= 70) || (i >= 79 && i <= 85) ||
                (i >= 96 && i <= 100) || (i >= 113 && i <= 116) || (i >= 131 && i <= 134) || (i >= 149 && i <= 152) ||
                (i >= 167 && i <= 171) || (i >= 184 && i <= 189) || (i >= 202 && i <= 207) || (i >= 218 && i <= 223) ||
                (i >= 234 && i <= 240) || (i >= 249 && i <= 256) || (i >= 265 && i <= 273) || (i >= 280 && i <= 300))
            {
                color = "Blue";
            }
            else if ((i >= 43 && i <= 48) || (i >= 57 && i <= 62) || (i >= 71 && i <= 78) || (i >= 86 && i <= 89) ||
                     (i >= 92 && i <= 95) || (i >= 101 && i <= 104) || (i >= 109 && i <= 112) || (i >= 117 && i <= 120) ||
                     (i >= 127 && i <= 130) || (i >= 135 && i <= 138) || (i >= 145 && i <= 148) || (i >= 153 && i <= 156) ||
                     (i >= 163 && i <= 166) || (i >= 172 && i <= 174) || (i >= 181 && i <= 183) || (i >= 190 && i <= 193) ||
                     (i >= 198 && i <= 201) || (i >= 208 && i <= 211) || (i >= 214 && i <= 217) || (i >= 224 && i <= 233) ||
                     (i >= 241 && i <= 248) || (i >= 257 && i <= 264) || (i >= 274 && i <= 279))
            {
                color = "Yellow";
            }
            else
            {
                color = "Red";
            }

            auditorium.Seats.Add(new Seat(color));
        }
    }

    private static void InitializeSeatsForAuditorium3(Auditorium auditorium, int numberOfSeats)
    {
        for (int i = 1; i <= numberOfSeats; i++)
        {
            if ((i >= 1 && i <= 28) || (i >= 41 && i <= 51) || (i >= 66 && i <= 75) || (i >= 90 && i <= 98) ||
                (i >= 116 && i <= 124) || (i >= 141 && i <= 150) || (i >= 169 && i <= 179) || (i >= 198 && i <= 209) ||
                (i >= 231 && i <= 240) || (i >= 261 && i <= 271) || (i >= 290 && i <= 302) || (i >= 319 && i <= 332) ||
                (i >= 347 && i <= 359) || (i >= 374 && i <= 386) || (i >= 399 && i <= 412) || (i >= 423 && i <= 438) ||
                (i >= 446 && i <= 500))
            {
                auditorium.Seats.Add(new Seat("Blue"));
            }
            else if ((i >= 29 && i <= 40) || (i >= 52 && i <= 65) || (i >= 76 && i <= 89) || (i >= 99 && i <= 105) ||
                    (i >= 110 && i <= 115) || (i >= 125 && i <= 129) || (i >= 136 && i <= 140) || (i >= 151 && i <= 155) ||
                    (i >= 164 && i <= 168) || (i >= 180 && i <= 184) || (i >= 193 && i <= 197) || (i >= 210 && i <= 215) ||
                    (i >= 225 && i <= 230) || (i >= 241 && i <= 246) || (i >= 255 && i <= 260) || (i >= 272 && i <= 276) ||
                    (i >= 285 && i <= 289) || (i >= 303 && i <= 306) || (i >= 315 && i <= 318) || (i >= 333 && i <= 337) ||
                    (i >= 342 && i <= 346) || (i >= 360 && i <= 373) || (i >= 387 && i <= 398) || (i >= 413 && i <= 422) ||
                    (i >= 439 && i <= 445))
            {
                auditorium.Seats.Add(new Seat("Yellow"));
            }
            else if ((i >= 106 && i <= 109) || (i >= 130 && i <= 135) || (i >= 156 && i <= 163) || (i >= 185 && i <= 192) ||
                    (i >= 216 && i <= 224) || (i >= 247 && i <= 254) || (i >= 277 && i <= 284) || (i >= 307 && i <= 314) ||
                    (i >= 338 && i <= 341))
            {
                auditorium.Seats.Add(new Seat("Red"));
            }
        }
    }

    public static string GetSeatInfo(Auditorium auditorium, string seatID)
    {
        foreach (Seat seat in auditorium.Seats)
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

    public static bool ReserveSeat(Auditorium auditorium, string seatID)
    {
        Seat? seat = auditorium.Seats.Find(s => s.ID == seatID);
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

    public static void UpdateAuditoriumJson(Auditorium auditorium)
    {
        JsonHandler.Update<Auditorium>(auditorium, DBFilePath);
    }
}