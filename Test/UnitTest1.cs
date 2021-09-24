using System.Threading;
using NUnit.Framework;
using Tracer.TracerResults;
using Tracer.Interfaces;

namespace Test
{
    [TestFixture]
    public class UnitTest1
    {
        private ITracer _tracer;
        
        private void Method_1()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }

        private void Method_2()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            Method_1();
            _tracer.StopTrace();
        }

        [SetUp]
        public void Setup()
        {
            _tracer = new Tracer.Tracers.Tracer();
        }

        [Test]
        public void TraceSingleMethodTimeMore100()
        {
            Method_1();
            Assert.AreEqual(1, (_tracer.Result as TracerResult).Threads.Count);
            Assert.GreaterOrEqual((_tracer.Result as TracerResult).Threads[0].ExecutionTime, 100);
        }
        
        [Test]
        public void TraceTwoMethodsTimeMore200MethodsAmount2()
        {
            Method_1();
            Method_1();
            Assert.AreEqual(1, (_tracer.Result as TracerResult).Threads.Count);
            Assert.AreEqual(2, (_tracer.Result as TracerResult).Threads[0].Methods.Count);
            Assert.GreaterOrEqual((_tracer.Result as TracerResult).Threads[0].ExecutionTime, 200);
        }
        
        [Test]
        public void TraceNestedMethodTimeMore200MethodAmount1()
        {
            Method_2();
            Assert.AreEqual(1, (_tracer.Result as TracerResult).Threads.Count);
            Assert.AreEqual(1, (_tracer.Result as TracerResult).Threads[0].Methods.Count);
            Assert.GreaterOrEqual((_tracer.Result as TracerResult).Threads[0].ExecutionTime, 200);
        }
        
        [Test]
        public void TraceTwoWithNestedTimeMore300MethodAmount2()
        {
            Method_1();
            Method_2();
            Assert.AreEqual(1, (_tracer.Result as TracerResult).Threads.Count);
            Assert.AreEqual(2, (_tracer.Result as TracerResult).Threads[0].Methods.Count);
            Assert.GreaterOrEqual((_tracer.Result as TracerResult).Threads[0].ExecutionTime, 300);
        }
        
        [Test]
        public void TraceTwoThreads()
        {
            var thread1 = new Thread(Method_1);
            var thread2 = new Thread(Method_2);
            thread1.Start();
            thread1.Join();
            thread2.Start();
            thread2.Join();
            
            Assert.AreEqual(2, (_tracer.Result as TracerResult).Threads.Count);
            Assert.AreEqual(1, (_tracer.Result as TracerResult).Threads[0].Methods.Count);
            Assert.AreEqual(1, (_tracer.Result as TracerResult).Threads[1].Methods.Count);
            Assert.GreaterOrEqual((_tracer.Result as TracerResult).Threads[0].ExecutionTime, 100);
            Assert.GreaterOrEqual((_tracer.Result as TracerResult).Threads[1].ExecutionTime, 200);
        }
    }
}