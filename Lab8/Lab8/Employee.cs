using System.Security.Principal;

namespace Lab8;

public class Employee
{
    public int Id { get; };
    public string Name { get; }
    public int Age { get; }
    
    public Employee(int id, string name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }
}