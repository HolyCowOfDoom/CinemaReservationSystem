public class Admin : User
{
    public Admin(string name, string birthDate, string email, string password, List<Reservation> reservations = null) 
    : base(name, birthDate, email, password, true, reservations)
    {
    }

    public Admin(string id, string name, string birthDate, string email, string password, List<Reservation> reservations, List<Movie> favorites) 
    : base(id, name, birthDate, email, password, true, reservations, favorites)
    {
    }
}