using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{ 
    public class Educator : ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string FullName => $"{LastName} {FirstName}";

        public Specialization Specialization { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
