using Models.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Attributes;
using System.ComponentModel;
using Newtonsoft.Json;

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

        //[JsonIgnore]
        public string SomeProperty { get; set; }

        public bool ShouldSerializeSomeProperty()
        {
            return Gender == Gender.Female;
        }

        public Student Mentor { get; set; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj) == false)
            {
                if (obj is Student student)
                    if (student.Id == Id)
                        return true;
            }
            return false;
        }
    }
}
