using Lab6;

namespace FileServiceLibrary;

public class FileService : IFileService<Employee>
{
    public IEnumerable<Employee> ReadFile(string fileName)
    {
        throw new NotImplementedException();
    }

    public void SaveData(IEnumerable<Employee> data, string fileName)
    {
        throw new NotImplementedException();
    }
}