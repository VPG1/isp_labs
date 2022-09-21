namespace Lab4;

public class FileService : IFileService<Employee>
{
    public IEnumerable<Employee> ReadFile(string fileName)
    {
        var employees = new List<Employee>();
        
        using (var reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
        {
            while (reader.PeekChar() > -1)
            {
                try
                {
                    string name = reader.ReadString();
                    int payment = reader.ReadInt32();
                    bool onSickLeave = reader.ReadBoolean();
                    employees.Add(new Employee(name, payment, onSickLeave));
                }
                catch
                {
                    break;
                }

            }
        }

        return employees;
    }

    public void SaveData(IEnumerable<Employee> data, string fileName)
    {
        using (var binaryWriter = new BinaryWriter(File.Open(fileName, FileMode.OpenOrCreate)))
        {
            foreach (var el in data)
            {
                try
                {
                    binaryWriter.Write(el.Name);
                    binaryWriter.Write(el.Payment);
                    binaryWriter.Write(el.OnSickLeave);
                }
                catch
                {
                    break;
                }

            }
        }
    }
}