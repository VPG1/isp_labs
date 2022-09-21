using Lab4;

var employees1 = new List<Employee>()
{
    new Employee("aerr", 1500, false),
    new Employee("cvv", 4000, true),
    new Employee("dww", 5000, false),
    new Employee("fsss", 3000, false),
    new Employee("bzzz", 1000, true)
};

var fileService = new FileService();

fileService.SaveData(employees1, "data.bin");

Console.Read();

try
{
    File.Move("data.bin", "employees.bin");
}
catch
{
    File.Delete("employees.bin");
    File.Move("data.bin", "employees.bin");
}


var employees2 = fileService.ReadFile("employees.bin");

employees2 = employees2.OrderBy(x => x, new MyCustomComparer()).ToList();

foreach (var el in employees2)
{
    Console.WriteLine($"{el.Name}\t{el.Payment}");
}
Console.WriteLine();

employees2 = employees2.OrderBy(x => x.Payment);

foreach (var el in employees2)
{
    Console.WriteLine($"{el.Name}\t{el.Payment}");
}