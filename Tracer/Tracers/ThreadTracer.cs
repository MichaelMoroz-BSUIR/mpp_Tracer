using System.Collections.Generic;
using System.Linq;
using Tracer.TracerResults;
using Tracer.Interfaces;

namespace Tracer.Tracers
{
    internal class ThreadTracer : ITracer
    {
        private readonly ThreadTracerResult _tracerResult;

        public ThreadTracer(int threadId)
        {
            InnerTracers = new List<MethodTracer>();
            Active = false;
            _tracerResult = new ThreadTracerResult {Id = threadId};
        }

        private List<MethodTracer> InnerTracers { get; }

        private bool Active { get; set; }

        public AbstractTracerResult Result
        {
            get
            {
                _tracerResult.ExecutionTime = InnerTracers.Select(method => method.Result as MethodTracerResult).Sum(result => result.ExecutionTime);
                _tracerResult.Methods = InnerTracers.Select(method => method.Result as MethodTracerResult).ToList();
                return _tracerResult;
            }
        }

        public void StartTrace()
        {
            if (!Active)
            {
                Active = true;
                var methodTracer = new MethodTracer();
                InnerTracers.Add(methodTracer);
                methodTracer.StartTrace();
            }
            else
            {
                InnerTracers.Last().StartTrace();
            }
        }

        public void StopTrace()
        {
            InnerTracers.Last().StopTrace();
            Active = InnerTracers.Last().Active;
        }
    }
}