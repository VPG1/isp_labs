using System.Text.Json.Serialization;

namespace Lab5.Domain;

public class Factory
{
    public string Name { get; set; }
    public PartsWarehouse PartsWarehouse { get; set; }


    public Factory() {}
    public Factory(string name, PartsWarehouse partsWarehouse)
    {
        Name = name;
        PartsWarehouse = partsWarehouse;
    }
}