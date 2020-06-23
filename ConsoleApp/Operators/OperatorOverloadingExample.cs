using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Operators
{
    public class OperatorOverloadingExample
    {
        class Box
        {
            public float Lenght { get; set; }
            public float Height { get; set; }
            public float Breadth { get; set; }

            public float Volume => Lenght * Height * Breadth;

            public override bool Equals(object obj)
            {
                return obj is Box box &&
                       Lenght == box.Lenght &&
                       Height == box.Height &&
                       Breadth == box.Breadth;
            }

            public override int GetHashCode()
            {
                var hashCode = 1383944048;
                hashCode = hashCode * -1521134295 + Lenght.GetHashCode();
                hashCode = hashCode * -1521134295 + Height.GetHashCode();
                hashCode = hashCode * -1521134295 + Breadth.GetHashCode();
                return hashCode;
            }

            public static Box operator +(Box a, Box b)
            {
                return new Box
                {
                    Lenght = a.Lenght + b.Lenght,
                    Height = a.Height + b.Height,
                    Breadth = a.Breadth + b.Breadth
                };
            }

            public static bool operator ==(Box a, Box b) {
                return a.Equals(b);
            }

            public static bool operator !=(Box a, Box b)
            {
                return !(a == b);
            }
        }

        public void Test()
        {
            var box1 = new Box() { Lenght = 1, Height = 2, Breadth = 3 };
            var box2 = new Box() { Lenght = 5, Height = 10, Breadth = 15 };

            var box3 = box1 + box2;
            var box4 = box3 + box3;
            var box5 = box1 + box2;

            Console.WriteLine(box3 == box5);
            Console.WriteLine(box3.Equals(box5));
        }

    }
}
