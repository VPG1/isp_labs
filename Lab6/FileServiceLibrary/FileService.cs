using System.Text.Json;
using Lab6;

namespace FileServiceLibrary;

public class FileService : IFileService<Employee>
{
    public IEnumerable<Employee>? ReadFile(string fileName)
    {
        using var fs = new FileStream(fileName, FileMode.OpenOrCreate);
        var employee = JsonSerializer.Deserialize<Employee[]>(fs);
        return employee;
        
    }

    public void SaveData(IEnumerable<Employee> data, string fileName)
    {
        using var fs = new FileStream(fileName, FileMode.OpenOrCreate);
        JsonSerializer.Serialize(fs, data);
    }
}