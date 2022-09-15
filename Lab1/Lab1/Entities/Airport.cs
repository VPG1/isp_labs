using Lab1.Collections;
using Lab1.Interfaces;

namespace Lab1.Entities;

public class Airport
{
    private MyCustomCollection<Tariff> tariffs = new();

    public void AddTariff(string airline, string directionName, decimal price, DateTime dateTimeOfDeparture)
    {
        try
        {
            tariffs.Add(new Tariff
            {
                Airline = airline,
                DirectionName = directionName,
                Price = price,
                DateTimeOfDeparture = dateTimeOfDeparture
            });
        }
        catch(InvalidOperationException)
        {
            throw new ApplicationException("this tariff already exist");
        }
    }
}