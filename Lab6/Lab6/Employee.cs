namespace Lab6;

public class Employee
{
    public string Name { get; init; }
    public int Payment { get; init; }
    public bool OnSickLeave { get; init; }

    public Employee(){}

    public Employee(string name, int payment, bool onSickLeave)
    {
        Name = name;
        Payment = payment;
        OnSickLeave = onSickLeave;
    }

}