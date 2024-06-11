public class Customer : User
{
    public Customer(string name, string birthDate, string email, string password, List<Reservation> reservations = null) 
    : base(name, birthDate, email, password, reservations)
    {
        Admin = false;
    }
}