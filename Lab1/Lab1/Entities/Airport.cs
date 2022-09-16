using Lab1.Collections;
using Lab1.Interfaces;

namespace Lab1.Entities;

public class Airport
{
    private readonly MyCustomCollection<Tariff> _tariffs = new();
    private readonly MyCustomCollection<Passenger> _passengers = new();
    

    public event EventHandler<Tariff>? TariffsChanged;
    public event EventHandler<Passenger>? PassengersChanged;


    public Tariff AddTariff(string airline, string directionName, decimal price, DateTime dateTimeOfDeparture)
    {
        var tariff = new Tariff
        {
            Airline = airline,
            DirectionName = directionName,
            Price = price,
            DateTimeOfDeparture = dateTimeOfDeparture
        };
        
        try
        {
            _tariffs.Add(tariff);
        }
        catch(InvalidOperationException)
        {
            throw new ApplicationException("this tariff already exist");
        }

        // если хоть кто-то подписан то уведомляем о изменение  
        if(TariffsChanged != null) TariffsChanged(this, tariff);

        return tariff;
    }

    public Passenger RegisterPassenger(string FIO, int passportId, Tariff tariff)
    {
        // проверяем не покупал ли пассажир билеты ранее 
        for (var i = 0; i < _passengers.Count; ++i)
        {
            // если покупал добавим билет в список билетов
            if (_passengers[i].PassportId == passportId)
            {
                try
                {
                    _passengers[i].PassengerTickets.Add(tariff);
                }
                catch (InvalidOperationException)
                {
                    throw new ApplicationException("passenger has already used this tariff");
                }

                return _passengers[i];
            }
        }

        //добавляем пассажира в коллекцию
        
        // init collection
        MyCustomCollection<Tariff> passengerTariffs = new();
        passengerTariffs.Add(tariff);

        // init new passenger
        var passenger = new Passenger
        (
            FIO,
            passportId,
            passengerTariffs
        );

        // add passenger
        _passengers.Add(passenger);
        
        // если хоть кто-то подписан то уведомляем о изменение  
        if (PassengersChanged != null) PassengersChanged(this, passenger);

        return passenger;
    }

    public decimal GetPassengerTicketsPrice(Passenger passenger)
    {
        return passenger.GetTicketsPrice();
    }

    public decimal GetCommonProfit()
    {
        decimal commonProfit = 0;
        for (var i = 0; i < _passengers.Count; ++i)
        {
            commonProfit += _passengers[i].GetTicketsPrice();
        }

        return commonProfit;
    }
    
    
    
}