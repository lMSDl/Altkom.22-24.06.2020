using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{ 
    public class Educator : Person
    {
        public Educator()
        {

        }

        public Educator(string firstName, string lastName) : base(firstName, lastName)
        {
        }

        public Specialization Specialization { get; set; }
    }
}
