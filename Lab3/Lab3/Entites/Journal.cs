namespace Lab3.Entites;

public class Journal
{
    public readonly List<string> _tariffsLogs = new();
    public readonly List<string> _passengersLogs = new();

    public bool WriteToConsole { get; set; } = false;

    public void TariffsChanged(object sender, Ticket ticket)
    {
        _tariffsLogs.Add($"{ticket}");
        if (WriteToConsole)
        {
            Console.WriteLine($"Added new tariff {ticket}");
        }
    }

    public void PassengersChanged(object sender, Passenger passenger)
    {
        _passengersLogs.Add($"{passenger}");
        if (WriteToConsole)
        {
            Console.WriteLine($"Added new passenger {passenger}");
        }    
    }
} 