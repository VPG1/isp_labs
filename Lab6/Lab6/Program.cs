using System.Reflection;
using System.Text.Json;
using Lab6;

var employees1 = new List<Employee>()
{
    new Employee("aerr", 1500, false),
    new Employee("cvv", 4000, true),
    new Employee("dww", 5000, false),
    new Employee("fsss", 3000, false),
    new Employee("bzzz", 1000, true)
};

// const string dllPath = "../../../../FileServiceLibrary/bin/Debug/net6.0/FileServiceLibrary.dll";
const string dllPath = "/home/kirill/IspLabs/isp_labs/Lab6/FileServiceLibrary/bin/Debug/net6.0/FileServiceLibrary.dll";

// подгружаем библиотеку
var assembly = Assembly.LoadFile(dllPath);

var type = assembly.GetType("FileServiceLibrary.FileService");

if (type == null) return;
var fileService = Activator.CreateInstance(type);


var saveData = type.GetMethod("SaveData");
saveData?.Invoke(fileService, new object[] { employees1, "data.json" });

var readFile = type.GetMethod("ReadFile");

if (readFile?.Invoke(fileService, new object[] { "data.json" }) is IEnumerable<Employee> employees)
    foreach (var employee in employees)
    {
        Console.WriteLine($"{employee.Name} {employee.Payment} {employee.OnSickLeave}");
    }

