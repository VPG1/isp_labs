// See https://aka.ms/new-console-template for more information

using Lab1.Collections;
using Lab1.Entities;
using Lab1.Exceptions;

var airport = new Airport();
var journal = new Journal();
journal.WriteToConsole = true;

airport.PassengersChanged += journal.PassengersChanged;
airport.TariffsChanged += journal.TariffsChanged;


var tariff1 = airport.AddTariff("Belavia", "Moscow", 120, new DateTime(2022,9, 20, 22, 50, 0));
var tariff2 = airport.AddTariff("Aerfloat", "Moscow", 155, new DateTime(2022, 9, 20, 19, 0, 0));
var tariff3 = airport.AddTariff("Emirates", "New York City", 1600, new DateTime(2022, 9, 19, 0, 0, 0));
var tariff4 = airport.AddTariff("Austrian Airlines", "Belgia", 670, new DateTime(2022, 9, 26, 12, 20, 0));

// Console.WriteLine($"Common profit: {airport.GetCommonProfit()}");
//
// Console.WriteLine("...");

var passenger1 = airport.RegisterPassenger("qwererqw", 1234, tariff1); 
passenger1.TicketsCollectionChanged += (sender, tariff) =>
{
    if (sender == null) return;
    Passenger passenger = (Passenger)sender;
    Console.WriteLine($"Passenger with passport id: {passenger.PassportId} buy ticket: {tariff}");
};


var passenger2 = airport.RegisterPassenger("qwererqw", 1234, tariff2);
var passenger3 = airport.RegisterPassenger("fwefw", 4323, tariff4);

Console.WriteLine();


MyCustomCollection<int> list = new();

list.Add(12);
list.Add(13);
list.Add(14);
list.Add(15);
list.Add(16);


foreach (var el in list)
{
    Console.Write($"{el} ");
}
Console.WriteLine();

list.Remove(16);
list.Remove(13);
list.Remove(12);
list.Remove(14);
list.Remove(15);

try
{
    list.Remove(0);
}
catch(RemoveException)
{
    Console.WriteLine("Remove exception here");
}

//
// list.Add(51);
// list.Add(13);
// list.Add(14);
// list.Add(15);
// list.Add(16);

//
//
// list.Print();
//
//
// Console.WriteLine();
//
//
// Console.WriteLine(list.Current());
//
// list.Next();
//
// Console.WriteLine(list.Current());
//
// list.Remove(13);
//
// Console.WriteLine(list.Current());
//
// list.Remove(51);
//
//
// Console.WriteLine(list.Current());
// Console.WriteLine();
//
// list.Print();
//
// Console.WriteLine();
//
// list.Remove(14);
// list.Remove(15);
// list.Remove(16);
//
//
// Console.WriteLine(list.Current());
