using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Delegates
{
    public class EventExample
    {
        public delegate void OddNumberDelegate();
        public event OddNumberDelegate OddNumberEvent;

        public void Add(int a, int b)
        {
            var result = a + b;
            Console.WriteLine(result);
            if (result % 2 != 0)
                OddNumberEvent?.Invoke();
        }

        private int _counter = 0;
        void CountOddNumbers()
        {
            _counter++;
        }

        public void Test()
        {
            OddNumberEvent += delegate () { OddNumberEvent = null; };
            OddNumberEvent += delegate () { Console.WriteLine("** Odd number detected **"); };
            OddNumberEvent += CountOddNumbers;

            for(var i = 0; i < 3; i++)
                for (var ii = 0; ii < 3; ii++)
                {
                    Add(i, ii);
                }
            Console.WriteLine("Number of odd numbers: " + _counter);
        }

    }
}
