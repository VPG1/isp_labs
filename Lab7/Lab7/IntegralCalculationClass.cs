using System.Diagnostics;

namespace Lab7;

public  class IntegralCalculationClass
{
    private static readonly Mutex Mutex = new Mutex();
    private static readonly Semaphore Semaphore = new Semaphore(2, 5);
    public static readonly List<KeyValuePair<int, int>> CurPercents = new()
    {
        new KeyValuePair<int, int>(),
        new KeyValuePair<int, int>(),
        new KeyValuePair<int, int>(),
        new KeyValuePair<int, int>(),
        new KeyValuePair<int, int>()
    };
    public event EventHandler? Handler;
    public double Calculate(int index) 
    {
        Semaphore.WaitOne();
        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        
        const double delta = 0.00000001;
        double res = 0;
        double cur = 0;
        while (cur <= 1){
            cur += delta;
            res += delta * Math.Sin(cur);
            if ((int)(cur * 100) > (int)((cur - delta) * 100))
            {
                Mutex.WaitOne();
                Handler?.Invoke(this, EventArgs.Empty);
                Mutex.ReleaseMutex();
                CurPercents[index] = new KeyValuePair<int, int>(Thread.CurrentThread.ManagedThreadId, (int)(cur * 100));
            }
        }

        Semaphore.Release();

        return res;
    }
}