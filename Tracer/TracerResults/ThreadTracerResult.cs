using System.Collections.Generic;
using Tracer.Interfaces;

namespace Tracer.TracerResults
{
    public class ThreadTracerResult : AbstractTracerResult
    {
        public int Id { get; internal init; }
        
        public long ExecutionTime { get; internal set; }
        
        public IReadOnlyList<MethodTracerResult> Methods { get; internal set; }

        public override string ToString()
        {
            return $"{{id: {Id}, time: {ExecutionTime}, methods: [{string.Join(", ", Methods)}]}}";
        }
    }
}