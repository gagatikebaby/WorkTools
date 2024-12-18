using System.IO;
using Newtonsoft.Json;

namespace WorkToolsSln.Helper
{
    public class FileOperation
    {
        public static T ReadConfig<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath))
                return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static void WriteConfig<T>(string filePath, T configObject) where T : class
        {
            string json = JsonConvert.SerializeObject(configObject, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
