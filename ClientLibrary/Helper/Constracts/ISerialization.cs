namespace ClientLibrary.Helper.Constracts
{
    public interface ISerialization
    {
        string? SerializeModelObject<T>(T modelObject) where T : class;
        T? DeserializeJsonString<T>(string jsonString) where T : class;
        List<T>? DeserializeJsonStringList<T>(string jsonString) where T : class;
    }
}
