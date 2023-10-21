using System.Text.Json;

namespace Name_Here
{
    /// <summary>
    ///    insures consitenet json serialization
    /// </summary>
    public static class JsonHelper
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true // TODO: set to false for production 
        };
        public static string Serialize(this object source)
        {
            return System.Text.Json.JsonSerializer.Serialize(source, _options);
        }

        public static T Deserialize<T>(this string source)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(source, _options);
        }
    }
}
