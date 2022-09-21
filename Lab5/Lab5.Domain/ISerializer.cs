namespace Lab5.Domain;

public interface ISerializer
{
    IEnumerable<Factory> DeSerializeByLinq(string fileName);
    IEnumerable<Factory>? DeSerializeByXml(string fileName);
    IEnumerable<Factory>? DeSerializeByJson(string fileName);

    void SerializeByLinq(IEnumerable<Factory> factories, string fileName);
    void SerializeByXml(IEnumerable<Factory> factories, string fileName);
    void SerializeByJson(IEnumerable<Factory> factories, string fileName);

}