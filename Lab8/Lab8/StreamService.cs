using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Lab8;

public class StreamService<T>
{
    // private static BinaryFormatter _binaryFormatter = new();

    private readonly IProgress<double> _progressBar1 = new MyProgress<double>(percent => Console.Write($"\rWrite to stream: {(int)percent}%"));
    private readonly IProgress<double> _progressBar2 = new MyProgress<double>(percent => Console.Write($"\rCopy to file: {(int)percent}%"));


    public async Task WriteToStreamAsync(Stream stream, IEnumerable<T> data)
    {
        Console.WriteLine($"WriteToStreamAsync thread id: {Thread.CurrentThread.ManagedThreadId}");
        var curCount = 0;
        var dataSize = data.Count();
        
        await stream.WriteAsync(Encoding.ASCII.GetBytes("["));
        foreach (var el in data)
        {
            if (curCount != 0) await stream.WriteAsync(Encoding.ASCII.GetBytes(","));
            await Task.Delay(1);
            curCount++;
            _progressBar1.Report(curCount * 100 / (double)dataSize);
            await stream.WriteAsync(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(el)));
        }
        await stream.WriteAsync(Encoding.ASCII.GetBytes("]"));
        
        Console.WriteLine();


        stream.Position = 0;
        Console.WriteLine($"WriteToStreamAsync thread id: {Thread.CurrentThread.ManagedThreadId}\n");
    }

    public async Task CopyFromStreamAsync(Stream stream, string fileName)
    {
        Console.WriteLine($"CopyFromStreamAsync thread id: {Thread.CurrentThread.ManagedThreadId}");
        
        await using var fs = new FileStream(fileName, FileMode.Create);
        int read;
        var buffer = new byte[100];
        var streamLen = stream.Length;
        var curNumOfBytes = 0;
        while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            curNumOfBytes += read;
            await Task.Delay(1);
            await fs.WriteAsync(buffer, 0, read);
            _progressBar2.Report(curNumOfBytes * 100 / (double)streamLen);
            // _ = await stream.ReadAsync(buffer,  0, buffer.Length);
            // Console.WriteLine(Convert.ToString(buffer));
            // await fs.WriteAsync(buffer);
        }
        
        Console.WriteLine();
        // await stream.CopyToAsync(fs);
        
        Console.WriteLine($"CopyFromStreamAsync thread id: {Thread.CurrentThread.ManagedThreadId}\n");
    }

    public async Task<int> GetStatisticAsync(string filename, Func<T, bool> filter)
    {
        await using var fs = new FileStream(filename, FileMode.Open);
        var objects =  await JsonSerializer.DeserializeAsync<T[]>(fs);
        return objects?.Count(filter) ?? 0;
    }
}