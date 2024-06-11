public class Admin : User
{
    public Admin(string name, string birthDate, string email, string password, List<Reservation> reservations = null) 
    : base(name, birthDate, email, password, reservations)
    {
        Admin = true;
    }
}