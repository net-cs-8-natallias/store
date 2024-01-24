using Newtonsoft.Json;

namespace Basket.Host.Services;

public class JsonSerializer
{
    public string Serialize<T>(T data)
    {
        return JsonConvert.SerializeObject(data);
    }

    public T Deserialize<T>(string value)
    {
        return JsonConvert.DeserializeObject<T>(value)!;
    }
}