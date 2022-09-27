using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab8;

public class StreamService<T>
{
    
    public async Task WriteToStreamAsync(Stream stream, IEnumerable<T> data)
    {
        await JsonSerializer.SerializeAsync(stream, data);
        stream.Position = 0;
    }

    public async Task CopyFromStreamAsync(Stream stream, string fileName)
    {
        await using var fs = new FileStream(fileName, FileMode.Create);
        await stream.CopyToAsync(fs);
    }

    public async Task<int> GetStatisticAsync(string filename, Func<T, bool> filter)
    {
        await using var fs = new FileStream(filename, FileMode.Open);
        var objects = await JsonSerializer.DeserializeAsync<T[]>(fs);
        
        var res = 0;
        if (objects != null) res = objects.Count(filter);

        return res;
    }
}