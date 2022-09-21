namespace Lab3.Entites;

public class Airport
{
    private readonly Dictionary<string, Ticket> _tariffIdToTicket = new();
    private readonly List<Passenger> _passengers = new();
    

    public event EventHandler<Ticket>? TariffsChanged;
    public event EventHandler<Passenger>? PassengersChanged;


    public Ticket AddTariff(string tariffId, string airline, string directionName, decimal price, DateTime dateTimeOfDeparture)
    {
        var ticket = new Ticket
        {
            Airline = airline,
            DirectionName = directionName,
            Price = price,
            DateTimeOfDeparture = dateTimeOfDeparture
        };

        var addingSuccessfully = _tariffIdToTicket.TryAdd(tariffId, ticket);

        if (!addingSuccessfully)
        {
            throw new ApplicationException("this tariff already exist");
        }

        // если хоть кто-то подписан то уведомляем о изменение  
        if(TariffsChanged != null) TariffsChanged(this, ticket);

        return ticket;
    }

    public Passenger RegisterPassenger(string fio, int passportId, Ticket ticket)
    {
        // проверяем не покупал ли пассажир билеты ранее 
        foreach (var p in _passengers.Where(p => p.PassportId == passportId))
        {
            p.AddTicket(ticket);

            return p;
        }

        //добавляем пассажира в коллекцию
        
        // init collection
        List<Ticket> passengerTickets = new();
        passengerTickets.Add(ticket);

        // init new passenger
        var passenger = new Passenger
        (
            fio,
            passportId,
            passengerTickets
        );

        // add passenger
        _passengers.Add(passenger);
        
        // если хоть кто-то подписан то уведомляем о изменение  
        PassengersChanged?.Invoke(this, passenger);

        return passenger;
    }

    public static decimal GetPassengerTicketsPrice(Passenger passenger)
    {
        return passenger.GetTicketsPrice();
    }

    public decimal GetCommonProfit()
    {
        // var a = from p in _passengers 
        return _passengers.Sum(t => t.GetTicketsPrice());
    }
}