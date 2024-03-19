public class Screening
{
    public Auditorium AssignedAuditorium;
    public DateTime ScreeningDateTime;
    public Screening(Auditorium assignedAuditorium, DateTime screeningDateTime)
    {
        AssignedAuditorium = assignedAuditorium;
        ScreeningDateTime = screeningDateTime;
    }

    public bool AdjustDateTime(DateTime dateTime)
    {
        // Code to adjust date and time
        return true;
    }

    public bool AdjustDateTime(DateTime time)
    {
        //code to adjust solly time
        return true;
    }
    public bool AdjustDateTime(DateTime date)
    {
        //code to adjust solly date
        return true;
    }
}