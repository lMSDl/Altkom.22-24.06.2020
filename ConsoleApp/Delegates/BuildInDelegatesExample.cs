using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Delegates
{
    public class BuildInDelegatesExample
    {
        public event EventHandler<OddNumberEventArgs> OddNumberEvent;

        public void Add(int a, int b)
        {
            var result = a + b;
            Console.WriteLine(result);
            if (result % 2 != 0)
                OddNumberEvent?.Invoke(this, new OddNumberEventArgs { Result = result});
        }

        public bool Substract(int a, int b)
        {
            var result = a - b;
            Console.WriteLine(result);
            return result % 2 != 0;
        }

        private int _counter = 0;
        void CountOddNumbers()
        {
            _counter++;
        }

        public void Test()
        {
            OddNumberEvent += BuildInDelegatesExample_OddNumberEvent;
            OddNumberEvent += BuildInDelegatesExample_OddNumberEvent1;

            var add = new Action<int, int>(Add);
            var substract = new Func<int, int, bool>(Substract);
            NewMethod(add, substract);
            Console.WriteLine("Number of odd numbers: " + _counter);
        }

        private void NewMethod(Action<int, int> add, Func<int, int, bool> substract)
        {
            for (var i = 0; i < 3; i++)
                for (var ii = 0; ii < 3; ii++)
                {
                    add(i, ii);
                    if (substract(i, ii))
                        OddNumberEvent?.Invoke(this, new OddNumberEventArgs());
                }
        }

        private void BuildInDelegatesExample_OddNumberEvent1(object sender, OddNumberEventArgs e)
        {
            Console.WriteLine("** Odd number detected **: " + e.Result);
        }

        private void BuildInDelegatesExample_OddNumberEvent(object sender, OddNumberEventArgs e)
        {
            CountOddNumbers();
        }

        public class OddNumberEventArgs : EventArgs
        {
            public int? Result { get; set; }
        }
    }
}
