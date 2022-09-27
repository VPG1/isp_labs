using System.Collections.Concurrent;
using Lab8;

var rand = new Random();

var streamService = new StreamService<Employee>();

using var ms = new MemoryStream();


const int numOfEmployees = 1000;
var employees = new Employee[numOfEmployees];
for (var i = 0; i < numOfEmployees; ++i)
{
    employees[i] = new Employee(i + 1, GetRandomString(), GetRandomInt(18, 70));
}



Console.WriteLine($"Thread id: {Thread.CurrentThread.ManagedThreadId}");
Console.WriteLine("Work has begun...\n");

await streamService.WriteToStreamAsync(ms, employees);
Thread.Sleep(200); 
await streamService.CopyFromStreamAsync(ms, "file.txt");

// await task1;
// await task2;

Console.WriteLine(await streamService.GetStatisticAsync("file.txt", employee => employee.Age >= 35));



int GetRandomInt(int l, int r)
{
    if (rand == null) throw new Exception("rand");
    return rand.Next(l, r + 1);
}

string GetRandomString()
{
    var res = "";
    var len = GetRandomInt(2, 10);

    res += Convert.ToString((char)GetRandomInt('A', 'Z'));
    for (var i = 0; i < len - 1; ++i)
    {
        res += Convert.ToString((char)GetRandomInt('a', 'z'));
    }
    return res;
}


