// See https://aka.ms/new-console-template for more information

using Lab3.Entites;

var airport = new Airport();
var journal = new Journal();
journal.WriteToConsole = true;

airport.PassengersChanged += journal.PassengersChanged;
airport.TariffsChanged += journal.TariffsChanged;


var tariff1 = airport.AddTariff("adce1234","Belavia", "Moscow", 120, new DateTime(2022,9, 20, 22, 50, 0));
var tariff2 = airport.AddTariff("adce1244","Aerfloat", "Moscow", 155, new DateTime(2022, 9, 20, 19, 0, 0));
var tariff3 = airport.AddTariff("adce1254","Emirates", "New York City", 1600, new DateTime(2022, 9, 19, 0, 0, 0));
var tariff4 = airport.AddTariff("adce1334","Austrian Airlines", "Belgia", 670, new DateTime(2022, 9, 26, 12, 20, 0));

// Console.WriteLine($"Common profit: {airport.GetCommonProfit()}");
//
// Console.WriteLine("...");

var passenger1 = airport.RegisterPassenger("qwererqw", 1234, tariff1); 
passenger1.TicketsCollectionChanged += (sender, tariff) =>
{
    if (sender == null) return;
    var passenger = (Passenger)sender;
    Console.WriteLine($"Passenger with passport id: {passenger.PassportId} buy ticket: {tariff}");
};


var passenger2 = airport.RegisterPassenger("qwererqw", 1234, tariff2);
var passenger3 = airport.RegisterPassenger("fwefw", 4323, tariff4);