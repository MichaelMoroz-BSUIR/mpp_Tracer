using System;
using Main.Serializer.Interfaces;

namespace Program.Serializer
{
    public class XmlSerializer : System.Xml.Serialization.XmlSerializer, ISerializer
    {
        public XmlSerializer(Type t)
            : base(t)
        {
        }
    }
}