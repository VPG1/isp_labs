namespace Lab8;

public class MyProgress<T> : IProgress<T>
{
    public event Action<T>? ProgressChanged;

    public MyProgress(Action<T> handler)
    {
        ProgressChanged += handler;
    }

    public void Report(T value)
    {
        ProgressChanged?.Invoke(value);
    }
}