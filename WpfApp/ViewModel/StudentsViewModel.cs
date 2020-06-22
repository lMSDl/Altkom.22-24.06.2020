using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Commands;

namespace WpfApp.ViewModel
{
    public class StudentsViewModel
    {
        public StudentsViewModel()
        {
            DeleteCommand = new CustomCommand(x => Students.Remove(SelectedStudent), x => Students.Contains(SelectedStudent));
        }

        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student> { new Student { FirstName = "Adam", LastName = "Adamski" }, new Student { FirstName = "Ewa", LastName = "Ewowska" } };
        public Student SelectedStudent { get; set; }

        public ICommand DeleteCommand { get; }
    }
}
