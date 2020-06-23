using Models.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Attributes;
using System.ComponentModel;

namespace Models
{
    [Validator(typeof(StudentValidator))]
    public class Student : Person
    {
        public Student()
        {

        }

        public Student(string firstName, string lastName) : base(firstName, lastName)
        {
        }

        public string SomeProperty { get; set; }
    }
}
