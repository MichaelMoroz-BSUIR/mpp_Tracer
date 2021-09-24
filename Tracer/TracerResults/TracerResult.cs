using System.Collections.Generic;
using System.Xml.Serialization;
using Tracer.Interfaces;

namespace Tracer.TracerResults
{
    public class TracerResult : AbstractTracerResult
    {
        [XmlIgnore]
        public IReadOnlyList<ThreadTracerResult> Threads { get; internal set; }
    }
}