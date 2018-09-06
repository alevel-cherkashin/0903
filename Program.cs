using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _0903
{
    class Program
    {
        static void Main(string[] args)
        {
            Cockroach cockroach1 = new Cockroach(0, "#");
            Cockroach cockroach2 = new Cockroach(1, "@");
            Cockroach cockroach3 = new Cockroach(2, "&");
            Cockroach cockroach4 = new Cockroach(3, "%");

            new Thread(() => { cockroach1.Start(); }).Start();
            new Thread(() => { cockroach2.Start(); }).Start();
            new Thread(() => { cockroach3.Start(); }).Start();
            new Thread(() => { cockroach4.Start(); }).Start();

            Task.WaitAll();

            Console.ReadKey();
        }

        public static void TaskThree()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;
            var task1 = Task.Run(() => 
            {
                Thread.Sleep(1000);
                cts.Cancel(true);
            }, ct);

            var task2 = Task.Run(() =>
            {
                while (!cts.IsCancellationRequested)
                {
                    Console.WriteLine("Hello world!!!");
                }
                Console.WriteLine("Stop");
            });
        }

        public static void TaskOne()
        {
            var TaskReturnsInt = Task.Run(() =>
            {
                var rnd = new Random();
                Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId} is running");
                Thread.Sleep(1000);
                Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId} is finished");
                return rnd.Next(1000);
            });

            var next = TaskReturnsInt.ContinueWith(x =>
          {
              Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId} is running");
              Thread.Sleep(1000);
              Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId} is finished");
              Console.WriteLine(x.Result * 10);
              Console.WriteLine(x.Result);
          });
        }

        public static void TaskTwo()
        {
            var TaskReturnString1 = Task.Run(() =>
            {
                Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId} is running");
                Thread.Sleep(1000);
                Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId} is finished");
                return "GENIA";
            });

            var TaskReturnString2 = TaskReturnString1.ContinueWith(x =>
            {
                Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId} is running");
                Thread.Sleep(1000);
                Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId} is finished");
                return "ILIA";
            });

            var TaskReturnString3 = TaskReturnString2.ContinueWith(x =>
            {
                Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId} is running");
                Thread.Sleep(1000);
                Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId} is finished");
                return "SASHA";
            });

            var TaskReturnString4 = TaskReturnString3.ContinueWith(x =>
            {
                Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId} is running");
                Thread.Sleep(1000);
                Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId} is finished");
                Console.WriteLine($"KOLIA {TaskReturnString1.Result} {TaskReturnString2.Result} {TaskReturnString3.Result}");
            });

        }
    }
}
