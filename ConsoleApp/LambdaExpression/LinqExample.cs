using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.LambdaExpression
{
    public class LinqExample
    {
        int[] numbers = new[] {1, 3, 4, 2, 5, 7, 8, 6, 9, 0 };
        List<string> strings = "wlazł kotek na płotek i mruga".Split(' ').ToList();

        List<Student> students = new List<Student>
        {
            new Student { FirstName = "Adam", LastName = "Adamski", BirthDate = new DateTime(1978, 2, 21) },
            new Student { FirstName = "Ewa", LastName = "Ewowska", BirthDate = new DateTime(1990, 1, 1), Gender = Gender.Female  } ,
            new Student { FirstName = "Adam", LastName = "Ewowska", BirthDate = new DateTime(1978, 2, 21) },
            new Student { FirstName = "Ewa", LastName = "Adamska", BirthDate = new DateTime(1990, 1, 1), Gender = Gender.Female  } ,
            new Student { FirstName = "Piotr", LastName = "Adamski", BirthDate = new DateTime(1978, 2, 21) },
            new Student { FirstName = "Kamila", LastName = "Ewowska", BirthDate = new DateTime(1990, 1, 1), Gender = Gender.Female  } ,
    };

        public void Test()
        {
            var queryresult1 = numbers.Where(x => x <= 4).OrderBy(x => x);
            foreach (var x in queryresult1)
                Console.Write($"{x} ");
            Console.WriteLine();

            var queryresult2 = (from x in numbers
                                where x <= 4
                                select x).OrderByDescending(x => x);
            foreach(var x in queryresult2)
                Console.Write($"{x} ");
            Console.WriteLine();

            var queryResult3 = strings.Where(x => x.Length == 5).Select(x => x.ToUpper());
            queryResult3 = from s in strings
                           where s.Length == 5
                           select s.ToUpper();


            foreach (var x in queryResult3)
                Console.Write($"{x} ");
            Console.WriteLine();

            students.Where(x => x.LastName == "Adamski").Select(x => $"{x.FullName} | {x.Gender}").ToList().ForEach(x => Console.WriteLine(x));
        }
    }
}
