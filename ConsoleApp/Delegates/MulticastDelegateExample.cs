using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Delegates
{
    public class MulticastDelegateExample
    {
        public delegate void ShowMessage(string s);

        public void Message1(string msg)
        {
            Console.WriteLine($"1st message: {msg}");
        }

        public void Message2(string msg)
        {
            Console.WriteLine($"2st message: {msg}");
        }

        public void Message3(string msg)
        {
            Console.WriteLine($"3st message: {msg}");
        }

        public void Test()
        {
            ShowMessage message = null;
            message += Message1;
            message += Message3;
            message += Message2;

            message("Hello!");
            message("Hi!");

            message -= Message3;
            message("Message3 removed");

        }
    }
}
