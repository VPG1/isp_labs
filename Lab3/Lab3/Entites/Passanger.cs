namespace Lab3.Entites;

public class Passenger : IComparable
{
    public string Fio { get; }
    public int PassportId { get; }
    public List<Ticket> PassengerTickets { get;}
    
    public event EventHandler<Ticket>? TicketsCollectionChanged;

    public Passenger(string fio, int passportId, List<Ticket> passengerTickets)
    {
        Fio = fio;
        PassportId = passportId;
        PassengerTickets = passengerTickets;
    }
    
    public override string ToString()
    {
        return $"name: {Fio}   passport id: {PassportId}";
    }

    public void AddTicket(Ticket ticket)
    {
        PassengerTickets.Add(ticket);
        
        TicketsCollectionChanged?.Invoke(this, ticket);
    }
    
    public decimal GetTicketsPrice()
    {

        return PassengerTickets.Sum(t => t.Price);
        // decimal commonPrice = 0;
        //
        // foreach (var t in PassengerTickets)
        // {
        //     commonPrice += t.Price;
        // }
        //
        // return commonPrice;
    }

    public int CompareTo(object? obj)
    {
        if (obj is not Passenger passenger) throw new NullReferenceException("null reference exception");

        return PassportId.CompareTo(passenger.PassportId);
    }
    
    public IEnumerable<decimal> GetPaymentForDirections()
    {
        return from el in (from ticket in PassengerTickets group ticket by ticket.DirectionName) 
            select el.Sum(t => t.Price);
    }
}