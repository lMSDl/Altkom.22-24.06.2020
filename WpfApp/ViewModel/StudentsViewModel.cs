using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.View;

namespace WpfApp.ViewModel
{
    public class StudentsViewModel
    {
        public StudentsViewModel()
        {
            DeleteCommand = new CustomCommand(x => Students.Remove(SelectedStudent), x => SelectedStudent != null);
        }

        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student> { new Student { FirstName = "Adam", LastName = "Adamski", BirthDate = new DateTime(1978, 2, 21) }, new Student { FirstName = "Ewa", LastName = "Ewowska", BirthDate = new DateTime(1990, 1, 1), Gender = Gender.Female  } };
        public Student SelectedStudent { get; set; }

        public ICommand DeleteCommand { get; }
        public ICommand AddCommand => new CustomCommand(x => AddOrEditStudent(new Student()));
        public ICommand EditCommand => new CustomCommand(x => AddOrEditStudent(SelectedStudent), x => SelectedStudent != null);

        private void AddOrEditStudent(Student student)
        {
            var studentClone = (Student)student.Clone();
            var dialog = new StudentDialogView(studentClone);
            if(dialog.ShowDialog() != true)
            {
                return;
            }

            if (Students.Contains(student))
                Students.Remove(student);
            Students.Add(studentClone);
        }
    }
}
