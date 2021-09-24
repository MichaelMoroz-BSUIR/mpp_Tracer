using System.IO;
using System.Threading;
using Tracer.Interfaces;
using Main.Serializer;
using Main.Serializer.Interfaces;
using Program.Serializer;

namespace Main
{
    class Program
    {
        private class Test
        {
            private readonly ITracer _tracer;

            public Test(ITracer tracer)
            {
                this._tracer = tracer;
            }

            public void Method1()
            {
                _tracer.StartTrace();
                Thread.Sleep(100);
                _tracer.StopTrace();
            }

            public void Method2()
            {
                _tracer.StartTrace();
                Thread.Sleep(100);
                Method1();
                _tracer.StopTrace();
            }

            public void Method3()
            {
                _tracer.StartTrace();
                Method1();
                Method2();
                _tracer.StopTrace();
            }
        }

        private static void Main()
        {
            var tracer = new Tracer.Tracers.Tracer();
            var test = new Test(tracer);

            var t1 = new Thread(() =>
            {
                test.Method1();
                test.Method2();
                test.Method3();
            });
            t1.Start();

            var t2 = new Thread(() =>
            {
                test.Method1();
                test.Method2();
                test.Method3();
            });
            t2.Start();

            t1.Join();
            t2.Join();

            ISerializer serializer = new JsonSerializer();
            using var file = new FileStream("C:/Users/misha/Desktop/result.json", FileMode.Create);
            using var writer = new StreamWriter(file);
            serializer.Serialize(writer, tracer.Result);
        }
    }
}