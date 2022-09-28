namespace Lab8;

public class MyProgress<T> : IProgress<T>
{
    private readonly Action<T> _action;

    public MyProgress(Action<T> action)
    {
        _action = action;
    }

    public void Report(T value)
    {
        _action.Invoke(value);
    }
}