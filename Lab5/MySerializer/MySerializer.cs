using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using Lab5.Domain;

namespace MySerializer;

public class MySerializer : ISerializer
{
    private readonly XmlSerializer _formatter = new(typeof(Factory[]));
    
    public IEnumerable<Factory> DeSerializeByLinq(string fileName)
    {
        var xmlDoc = XDocument.Load(fileName);

        return from factory in xmlDoc.Element("Factories")?.Elements()
            select new Factory(
                factory.Value,
                new PartsWarehouse(
                    Convert.ToDecimal(factory.Element("PartsWarehouse").Element("Length").Value),
                    Convert.ToDecimal(factory.Element("PartsWarehouse").Element("Width").Value), 
                    Convert.ToDecimal(factory.Element("PartsWarehouse").Element("Height").Value)
                )
            );
    }

    public IEnumerable<Factory>? DeSerializeByXml(string fileName)
    {
        using var fs = new FileStream(fileName, FileMode.OpenOrCreate);
        var factories = _formatter.Deserialize(fs) as IEnumerable<Factory>;
        return factories;
    }

    public IEnumerable<Factory>? DeSerializeByJson(string fileName)
    {
        using var fs = new FileStream(fileName, FileMode.OpenOrCreate);
        var factories = JsonSerializer.Deserialize<Factory[]>(fs);
        return factories;
    }

    public void SerializeByLinq(IEnumerable<Factory> factories, string fileName)
    {
        var xmlDoc = new XDocument(
            new XElement("Factories", from factory in factories
                select new XElement("Factory",
                    new XElement("Name", factory.Name),
                    new XElement("PartsWarehouse",
                        new XElement("Length",factory.PartsWarehouse.Length),
                        new XElement("Width", factory.PartsWarehouse.Width),
                        new XElement("Height", factory.PartsWarehouse.Height)
                        )
                    )
                )
            );
        xmlDoc.Save(fileName);
    }

    public void SerializeByXml(IEnumerable<Factory> factories, string fileName)
    {
        using var fs = new FileStream(fileName, FileMode.OpenOrCreate);
        _formatter.Serialize(fs, factories.ToArray());
    }

    public void SerializeByJson(IEnumerable<Factory> factories, string fileName)
    {
        using var fs = new FileStream(fileName, FileMode.OpenOrCreate);
        JsonSerializer.Serialize<Factory[]>(fs, factories.ToArray());
    }
}