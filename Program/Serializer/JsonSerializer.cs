using System.IO;
using Main.Serializer.Interfaces;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Program.Serializer
{
    public class JsonSerializer : ISerializer
    {
        public void Serialize(TextWriter writer, object data)
        {
            writer.WriteLine(
                JsonConvert.SerializeObject(data, Formatting.Indented)
            );
        }
    }
}