using Lab1.Collections;

namespace Lab1.Entities;

public class Journal
{
    private readonly MyCustomCollection<Tariff> _tariffs = new();
    private readonly MyCustomCollection<Passenger> _passengers = new();

    public bool WriteToConsole { get; set; } = true;

    public void TariffsChanged(object sender, Tariff tariff)
    {
        _tariffs.Add(tariff);
        if (WriteToConsole)
        {
            Console.WriteLine($"Added new tariff {tariff}");
        }
    }

    public void PassengersChanged(object sender, Passenger passenger)
    {
        _passengers.Add(passenger);
        if (WriteToConsole)
        {
            Console.WriteLine($"Added new passenger {passenger}");
        }    
    }
} 