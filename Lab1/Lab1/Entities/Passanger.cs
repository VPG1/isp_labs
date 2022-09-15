using Lab1.Collections;

namespace Lab1.Entities;

public class Passenger : IComparable
{
    public string FIO { get; init; } = "Undefined";
    public int PassportId { get; init; }
    public MyCustomCollection<Tariff> PassengerTariffs { get; init; }
    
    
    public decimal GetTicketsPrice()
    {
        decimal commonPrice = 0;
        
        for (int i = 0; i < PassengerTariffs.Count; ++i)
        {
            commonPrice += PassengerTariffs[i].Price;
        }

        return commonPrice;
    }

    public int CompareTo(object? obj)
    {
        if (obj is not Passenger passenger) throw new NullReferenceException("null reference exception");

        return PassportId.CompareTo(passenger.PassportId);
    }
}