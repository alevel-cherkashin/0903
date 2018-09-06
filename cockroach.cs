using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace _0903
{
    public class Cockroach
    {
        private static int Finished { get; set; }
        private int PositionColl { get; set; }
        private int PositionLine { get; set; }
        private string Name { get; set; }
        private static CancellationTokenSource _cts;
        private static CancellationToken _ct;
        private static object _locker;

        public Cockroach(int Number, string name)
        {
            _cts = new CancellationTokenSource();
            _ct = _cts.Token;
            _locker = new object();
            PositionColl = 0;
            Finished = 10;
            PositionLine = Number;
            Name = name;
        }

        private void Print()
        {
            lock (_locker)
            {
                Clear1();
                Console.SetCursorPosition(PositionColl, PositionLine);
                Console.Write(Name);
            }
        }

        private void Clear1()
        {
            Console.SetCursorPosition(0, PositionLine);
            Console.WriteLine("-----------|");
        }

        public void Start()
        {
            while (!_cts.IsCancellationRequested)
            {
                Random rnd = new Random();

                if (PositionColl == Finished)
                {
                    Console.SetCursorPosition(0, 5);
                    Console.WriteLine($"{Name} finished first!!!");
                    _cts.Cancel();
                }

                Print();

                Thread.Sleep(rnd.Next(1000, 2000));
                PositionColl++;
            }
        }
    }
}
