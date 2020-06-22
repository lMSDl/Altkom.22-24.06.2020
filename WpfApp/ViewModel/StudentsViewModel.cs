using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModel
{
    public class StudentsViewModel
    {
        public ICollection<Student> Students { get; set; } = new List<Student> { new Student { FirstName = "Adam", LastName = "Adamski" }, new Student { FirstName = "Ewa", LastName = "Ewowska" } };
    }
}
