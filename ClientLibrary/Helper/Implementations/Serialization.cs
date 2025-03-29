using ClientLibrary.Helper.Constracts;
using System.Text.Json;

namespace ClientLibrary.Helper.Implementations;

public class Serialization : ISerialization
{
    public string? SerializeModelObject<T>(T modelObject) where T : class
    {
        return JsonSerializer.Serialize(modelObject);
    }

    public T? DeserializeJsonString<T>(string jsonString) where T : class
    {
        return JsonSerializer.Deserialize<T>(jsonString);
    }
    
    public List<T>? DeserializeJsonStringList<T>(string jsonString)where T : class
    {
        return JsonSerializer.Deserialize<List<T>>(jsonString);
    }

}
