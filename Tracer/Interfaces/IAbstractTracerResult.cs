using System;
using System.Xml.Serialization;
using Tracer.TracerResults;

namespace Tracer.Interfaces
{
    [Serializable]
    [XmlInclude(typeof(MethodTracerResult))]
    [XmlInclude(typeof(ThreadTracerResult))]
    [XmlInclude(typeof(TracerResult))]
    public abstract class AbstractTracerResult
    {
    }
}