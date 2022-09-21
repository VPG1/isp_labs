namespace Lab4;

public class Employee
{
    public string Name { get; set; }
    public int Payment { get; set; }
    public bool OnSickLeave { get; set; }

    public Employee(string name, int payment, bool onSickLeave)
    {
        Name = name;
        Payment = payment;
        OnSickLeave = onSickLeave;
    }

}