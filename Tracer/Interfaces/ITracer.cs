namespace Tracer.Interfaces
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        AbstractTracerResult Result { get; }
    }
}