using Lab5.Domain;
using MySerializer = MySerializer.MySerializer;

var factories = new List<Factory>()
{
    new Factory("fwefew", new PartsWarehouse(12, 42, 43)),
    new Factory("fweoifjoiwej", new PartsWarehouse(53, 43, 12)),
    new Factory("xojgvois", new PartsWarehouse(43, 34, 34)),
    new Factory("xopijboirjf", new PartsWarehouse(100, 60, 43)),
    new Factory("pohjtr", new PartsWarehouse(12, 12, 12))
};


var serializer = new global::MySerializer.MySerializer();

serializer.SerializeByJson(factories, "Factories.json");
var factories1 = serializer.DeSerializeByJson("Factories.json");
PrintFactory(factories1);

serializer.SerializeByXml(factories, "Factories.xml");
var factories2 = serializer.DeSerializeByXml("Factories.xml");
PrintFactory(factories2);

serializer.SerializeByLinq(factories, "FactoriesLinq.xml");
var factories3 = serializer.DeSerializeByLinq("FactoriesLinq.xml");
PrintFactory(factories3);



void PrintFactory(IEnumerable<Factory>? fac)
{
    foreach (var factory in fac)
    {
        
        Console.WriteLine($"Name: {factory.Name}," +
                          $" Parts warehouse: l = {factory.PartsWarehouse.Length}, w = {factory.PartsWarehouse.Width}, h = {factory.PartsWarehouse.Height}");
    }
    Console.WriteLine();
}



