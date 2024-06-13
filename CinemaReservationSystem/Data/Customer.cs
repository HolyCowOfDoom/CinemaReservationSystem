public class Customer : User
{
    public Customer(string name, string birthDate, string email, string password, List<Reservation> reservations = null) 
    : base(name, birthDate, email, password, false, reservations)
    {
    }

    public Customer(string id, string name, string birthDate, string email, string password, List<Reservation> reservations, List<Movie> favorites) 
    : base(id, name, birthDate, email, password, false, reservations, favorites)
    {
    }
}