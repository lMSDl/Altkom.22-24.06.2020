using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Delegates
{
    public class DelegateExample
    {
        public delegate void NoParametersNoReturnTypeDelegate();
        public delegate void ParametersNoReturnTypeDelegate(string @string);
        public delegate bool ParametersDelegate(int a, int b);

        public void Fun1()
        {
            Console.WriteLine(nameof(Fun1));
        }

        public void Fun2(string someString)
        {
            Console.WriteLine($"{nameof(Fun2)}: {someString}");
        }

        public bool Fun3(int x, int y)
        {
            Console.WriteLine($"{nameof(Fun3)}: {x} {y}");
            return x == y;
        }

        ParametersDelegate Delegate3 { get; set; }

        public void Test()
        {
            var delegate1 = new NoParametersNoReturnTypeDelegate(Fun1);

            //delegate1();
            //if (delegate1 != null)
            //    delegate1.Invoke();
            delegate1?.Invoke();

            ParametersNoReturnTypeDelegate delegate2 = null;
            delegate2 += Fun2;
            delegate2?.Invoke("some string");

            Delegate3 += Fun3;

            for(var i = 0; i < 3; i++)
                if(Delegate3(1, i))
                    Console.WriteLine("==");
        }
    }
}
