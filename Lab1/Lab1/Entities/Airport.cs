using Lab1.Collections;
using Lab1.Interfaces;

namespace Lab1.Entities;

public class Airport
{
    private MyCustomCollection<Tariff> tariffs = new();
    private MyCustomCollection<Passenger> passengers = new();

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
            tariffs.Add(tariff);
        }
        catch(InvalidOperationException)
        {
            throw new ApplicationException("this tariff already exist");
        }

        return tariff;
    }

    public Passenger RegisterPassenger(string FIO, int passportId, Tariff tariff)
    {
        for (var i = 0; i < passengers.Count; ++i)
        {
            if (passengers[i].PassportId == passportId)
            {
                try
                {
                    passengers[i].PassengerTariffs?.Add(tariff);
                }
                catch (InvalidOperationException)
                {
                    throw new ApplicationException("passenger has already used this tariff");
                }

                return passengers[i];
            }
        }

        // init collection
        MyCustomCollection<Tariff> passengerTariffs = new();
        passengerTariffs.Add(tariff);

        // init new passenger
        var passenger = new Passenger
        {
            FIO = FIO,
            PassportId = passportId,
            PassengerTariffs = passengerTariffs
        };

        // add passenger
        passengers.Add(passenger);

        return passenger;
    }

    public decimal GetPassengerTicketsPrice(Passenger passenger)
    {
        return passenger.GetTicketsPrice();
    }

    public decimal GetCommonProfit()
    {
        decimal commonProfit = 0;
        for (int i = 0; i < passengers.Count; ++i)
        {
            commonProfit += passengers[i].GetTicketsPrice();
        }

        return commonProfit;
    }
    
    
    
}