using System;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

public static class Util
{
    public static Dictionary<string, string> returnJsonFile(string filePath)
    {
        Dictionary<string, string> deserializedDic;
          using (StreamReader r = new StreamReader(filePath))
        {
            string json = r.ReadToEnd();
            deserializedDic = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        return deserializedDic;
    }

}