using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tracer.TracerResults;
using Tracer.Interfaces;

namespace Tracer.Tracers
{
    internal class MethodTracer : ITracer
    {
        private readonly MethodTracerResult _tracerResult;
        private readonly int _skip;

        public MethodTracer(int skip = 3)
        {
            ExecutorStopwatch = new Stopwatch();
            InnerTracers = new List<MethodTracer>();
            this._skip = skip;
            _tracerResult = new MethodTracerResult();
        }

        public bool Active { get; private set; }

        private Stopwatch ExecutorStopwatch { get; }

        private List<MethodTracer> InnerTracers { get; }

        public AbstractTracerResult Result
        {
            get
            {
                _tracerResult.Methods = InnerTracers.Select(method => method.Result).Cast<MethodTracerResult>().ToList();
                return _tracerResult;
            }
        }

        public void StartTrace()
        {
            if (!Active)
            {
                Active = true;

                var method = new StackFrame(_skip).GetMethod();
                if (method is not null)
                {
                    _tracerResult.MethodName = method.Name;
                    if (method.DeclaringType != null) _tracerResult.ClassName = method.DeclaringType.Name;
                }

                ExecutorStopwatch.Start();
            }
            else
            {
                if (InnerTracers.Any() && InnerTracers.Last().Active)
                {
                    InnerTracers.Last().StartTrace();
                }
                else
                {
                    var tracer = new MethodTracer(_skip + 1);
                    InnerTracers.Add(tracer);
                    tracer.StartTrace();
                }
            }
        }

        public void StopTrace()
        {
            if (InnerTracers.Any() && InnerTracers.Last().Active)
            {
                InnerTracers.Last().StopTrace();
            }
            else
            {
                ExecutorStopwatch.Stop();
                Active = false;
                _tracerResult.ExecutionTime = ExecutorStopwatch.ElapsedMilliseconds;
            }
        }
    }
}