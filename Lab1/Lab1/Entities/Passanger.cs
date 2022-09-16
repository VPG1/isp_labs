using Lab1.Collections;

namespace Lab1.Entities;

public class Passenger : IComparable
{
    public string Fio { get; }
    public int PassportId { get; }
    public MyCustomCollection<Tariff> PassengerTickets { get;}
    
    public event EventHandler<Tariff>? TicketsCollectionChanged;

    public Passenger(string fio, int passportId, MyCustomCollection<Tariff> passengerTickets)
    {
        Fio = fio;
        PassportId = passportId;
        PassengerTickets = passengerTickets;
    }
    
    public override string ToString()
    {
        return $"name: {Fio}   passport id: {PassportId}";
    }

    public void AddTicket(Tariff tariff)
    {
        PassengerTickets.Add(tariff);
        
        TicketsCollectionChanged?.Invoke(this, tariff);
    }
    
    public decimal GetTicketsPrice()
    {
        decimal commonPrice = 0;
        
        for (var i = 0; i < PassengerTickets.Count; ++i)
        {
            commonPrice += PassengerTickets[i].Price;
        }

        return commonPrice;
    }

    public int CompareTo(object? obj)
    {
        if (obj is not Passenger passenger) throw new NullReferenceException("null reference exception");

        return PassportId.CompareTo(passenger.PassportId);
    }
}