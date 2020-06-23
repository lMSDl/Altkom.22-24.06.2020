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
    public class Student : DataErrorInfo, ICloneable
    {
        public Student()
        {
        }

        public Student(string firstName, string lastName) 
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string FullName => $"{LastName} {FirstName}";

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
