using System.IO;

namespace Main.Serializer.Interfaces
{
    public interface ISerializer
    {
        void Serialize(TextWriter stream, object data);
    }
}