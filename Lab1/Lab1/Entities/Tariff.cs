namespace Lab1.Entities;

public class Tariff : IComparable
{
    public string Airline { get; init; } = "Undefined";
    public string DirectionName { get; init; } = "Undefined";
    public decimal Price { get; init; }
    public DateTime DateTimeOfDeparture { get; init; }
    
    
    public override string ToString()
    {
        return $"airline: {Airline}   direction name: {DirectionName}   price: {Price}   date and time of departure: {DateTimeOfDeparture}";
    }


    public int CompareTo(object? obj)
    {
        if (obj is not Tariff tariff) throw new NullReferenceException("null reference exception");
        
        // сравниваем по авиакомпании
        var airlineCompareResult = string.Compare(Airline, tariff.Airline, StringComparison.Ordinal);
        // сравниваем по направлению
        var directionCompareResult = string.Compare(DirectionName, tariff.DirectionName, StringComparison.Ordinal);
        // // сравниваем по цене
        // var priceCompareResult = Price.CompareTo(tariff.Price);
        // сравниваем по времени вылета
        var departureCompareResult = DateTimeOfDeparture.CompareTo(tariff.DateTimeOfDeparture);

        
        // если что-либо кроме ценны не совпадавет таррифы не равны 
        if (airlineCompareResult != 0)
        {
            return airlineCompareResult;
        }
        if (directionCompareResult != 0)
        {
            return directionCompareResult;
        }
        if (departureCompareResult != 0)
        {
            return departureCompareResult;
        }

        return 0;
    }
}