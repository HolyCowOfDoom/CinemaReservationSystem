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
            double price;

            if ((i >= 1 && i <= 33) || (i >= 36 && i <= 44) || (i >= 49 && i <= 55) || (i >= 62 && i <= 67) ||
                (i >= 74 && i <= 79) || (i >= 86 && i <= 91) || (i >= 98 && i <= 104) || (i >= 109 && i <= 117) ||
                (i >= 120 && i <= 150))
            {

                color = "Blue";
                price = 10;

            }

            else if ((i >= 34 && i <= 35) || (i >= 45 && i <= 48) || (i >= 56 && i <= 57) || (i >= 60 && i <= 61) ||
                    (i >= 68 && i <= 69) || (i >= 72 && i <= 73) || (i >= 80 && i <= 81) || (i >= 84 && i <= 85) ||
                    (i >= 92 && i <= 93) || (i >= 96 && i <= 97) || (i >= 105 && i <= 108) || (i >= 118 && i <= 119))
            {

                color = "Yellow";
                price = 15;

            }

            else
            {

                color = "Red";
                price = 20;
                
            }

            auditorium.Seats.Add(new Seat(color, price));
        }
    }

    private static void InitializeSeatsForAuditorium2(Auditorium auditorium, int numberOfSeats)
    {
        for (int i = 1; i <= numberOfSeats; i++)
        {
            string color;
            double price;

            if ((i >= 1 && i <= 42) || (i >= 49 && i <= 56) || (i >= 63 && i <= 70) || (i >= 79 && i <= 85) ||
                (i >= 96 && i <= 100) || (i >= 113 && i <= 116) || (i >= 131 && i <= 134) || (i >= 149 && i <= 152) ||
                (i >= 167 && i <= 171) || (i >= 184 && i <= 189) || (i >= 202 && i <= 207) || (i >= 218 && i <= 223) ||
                (i >= 234 && i <= 240) || (i >= 249 && i <= 256) || (i >= 265 && i <= 273) || (i >= 280 && i <= 300))
            {

                color = "Blue";
                price = 10;

            }
            else if ((i >= 43 && i <= 48) || (i >= 57 && i <= 62) || (i >= 71 && i <= 78) || (i >= 86 && i <= 89) ||
                     (i >= 92 && i <= 95) || (i >= 101 && i <= 104) || (i >= 109 && i <= 112) || (i >= 117 && i <= 120) ||
                     (i >= 127 && i <= 130) || (i >= 135 && i <= 138) || (i >= 145 && i <= 148) || (i >= 153 && i <= 156) ||
                     (i >= 163 && i <= 166) || (i >= 172 && i <= 174) || (i >= 181 && i <= 183) || (i >= 190 && i <= 193) ||
                     (i >= 198 && i <= 201) || (i >= 208 && i <= 211) || (i >= 214 && i <= 217) || (i >= 224 && i <= 233) ||
                     (i >= 241 && i <= 248) || (i >= 257 && i <= 264) || (i >= 274 && i <= 279))
            {

                color = "Yellow";
                price = 15;

            }
            else
            {

                color = "Red";
                price = 20;

            }

            auditorium.Seats.Add(new Seat(color, price));
        }
    }

    private static void InitializeSeatsForAuditorium3(Auditorium auditorium, int numberOfSeats)
    {
        for (int i = 1; i <= numberOfSeats; i++)
        {
            string color;
            double price;

            if ((i >= 1 && i <= 59) || (i >= 66 && i <= 81) || (i >= 92 && i <= 105) || (i >= 118 && i <= 130) ||
                (i >= 145 && i <= 157) || (i >= 172 && i <= 185) || (i >= 202 && i <= 214) || (i >= 233 && i <= 243) ||
                (i >= 264 && i <= 273) || (i >= 294 && i <= 304) || (i >= 323 && i <= 333) || (i >= 352 && i <= 361) ||
                (i >= 378 && i <= 386) || (i >= 403 && i <= 411) || (i >= 426 && i <= 435) || (i >= 450 && i <= 460) ||
                (i >= 473 && i <= 500))
            {

                color = "Blue";
                price = 10;

            }
            else if ((i >= 60 && i <= 65) || (i >= 82 && i <= 91) || (i >= 106 && i <= 117) || (i >= 131 && i <= 144) ||
                     (i >= 158 && i <= 162) || (i >= 167 && i <= 171) || (i >= 186 && i <= 189) || (i >= 198 && i <= 201) ||
                     (i >= 215 && i <= 219) || (i >= 228 && i <= 232) || (i >= 244 && i <= 249) || (i >= 258 && i <= 263) ||
                     (i >= 274 && i <= 279) || (i >= 288 && i <= 293) || (i >= 305 && i <= 309) || (i >= 318 && i <= 322) ||
                     (i >= 334 && i <= 338) || (i >= 347 && i <= 351) || (i >= 362 && i <= 366) || (i >= 373 && i <= 377) ||
                     (i >= 387 && i <= 392) || (i >= 397 && i <= 402) || (i >= 412 && i <= 425) || (i >= 436 && i <= 449) ||
                     (i >= 461 && i <= 472))
            {

                color = "Yellow";
                price = 15;

            }
            else
            {
                
                color = "Red";
                price = 20;

            }

            auditorium.Seats.Add(new Seat(color, price));
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
        else if(seat.IsReserved)
        {
            Console.WriteLine($"Auditorium.cs: Seat {seatID} is already reserved.");
            return false;
        }
        else
        {
            Console.WriteLine($"Auditorium.cs: Seat {seatID} probably does not exist.");
            return false;
        }
    }

    public static bool CancelSeat(Auditorium auditorium, string seatID)
    {
        Seat? seat = auditorium.Seats.Find(s => s.ID == seatID);
        if (seat != null && seat.IsReserved)
        {
            seat.CancelSeat();
            Console.WriteLine($"Auditorium.cs: Seat {seatID} cancelled successfully.");
            return true;
        }
        else if(!seat.IsReserved)
        {
            Console.WriteLine($"Auditorium.cs: Seat {seatID} is not already reserved.");
            return false;
        }
        else
        {
            Console.WriteLine($"Auditorium.cs: Seat {seatID} probably does not exist.");
            return false;
        }
    }

    //this method is currently not used anywhere. proably because we haven't had a need to change 
    //an auditorium, as they're just a template used for a screening.
    public static void UpdateAuditoriumJson(Auditorium auditorium)
    {
        JsonHandler.Update<Auditorium>(auditorium, DBFilePath);
    }
}