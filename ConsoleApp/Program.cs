using ConsoleApp.Delegates;
using ConsoleApp.Indexers;
using ConsoleApp.LambdaExpression;
using ConsoleApp.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new LinqExample().Test();
            //Abc();

            Console.ReadKey();
        }


        static void Abc()
        {
            Nullable<int> a = null;
            int? b = 5;
            int c;

            if (a - b == 0)
                c = (a + b) ?? 0;
            else
            {
                var result = a - b;
                if (result.HasValue) //if(result != null)
                    c = result.Value;
                else
                    c = 0;
            }

            c = (a - b == 0 ? a + b : a - b) ?? 0;
        }
    }
}
