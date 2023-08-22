public class JsonParse
{
    public static T FromJson<T>(string json)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
    }

    public static string ToJson(object obj)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
    }
}
