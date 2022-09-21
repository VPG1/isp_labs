namespace Lab4;

public class MyCustomComparer : IComparer<Employee>
{
    public int Compare(Employee? x, Employee? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        
        return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
    }
}