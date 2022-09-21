using System.ComponentModel.DataAnnotations;
using Lab7;


var results = new List<double>(){0, 0, 0, 0, 0};
var threads = new List<Thread>(); 

for (var i = 0; i < 5; ++i)
{
    
    var i1 = i;
    threads.Add(new Thread(() =>
    {
        var icClass = new IntegralCalculationClass();
        IntegralCalculationClass.CurPercents[i1] = new KeyValuePair<int, int>(Thread.CurrentThread.ManagedThreadId, 0);
        icClass.Handler += (_, _) =>
        {
            Console.Clear();
            Console.WriteLine(StatusBarView(IntegralCalculationClass.CurPercents[0]));
            Console.WriteLine(StatusBarView(IntegralCalculationClass.CurPercents[1]));
            Console.WriteLine(StatusBarView(IntegralCalculationClass.CurPercents[2]));
            Console.WriteLine(StatusBarView(IntegralCalculationClass.CurPercents[3]));
            Console.WriteLine(StatusBarView(IntegralCalculationClass.CurPercents[4]));
        };
        results[i1] = icClass.Calculate(i1);
    }));
    threads[i].Start(); 
}

foreach (var thread in threads)
{
    thread.Join();
}

// foreach (var el in results)
// {
//     Console.WriteLine(el);
// }

string StatusBarView(KeyValuePair<int, int> threadIdToPercent)
{

    string statusBarString = $"Поток {threadIdToPercent.Key} [";
    
    if (threadIdToPercent.Value != 100)
    {
        int i;
        for (i = 0; i < threadIdToPercent.Value / 5; ++i)
        {
            statusBarString += "=";
        }

        statusBarString += ">";
        ++i;

        for (; i < 20; ++i)
        {
            statusBarString += " ";
        }
    }
    else
    {
        for (int i = 0; i < 20; ++i)
        {
            statusBarString += "=";
        }
    }

    statusBarString += "]";

    statusBarString += $"{threadIdToPercent.Value}%";

    return statusBarString;
}


