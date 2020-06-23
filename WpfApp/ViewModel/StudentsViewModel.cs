using DAL.Services;
using Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.View;

namespace WpfApp.ViewModel
{
    public class StudentsViewModel
    {
        private ObservableCollection<Student> _students;

        public StudentsViewModel()
        {
            //DeleteCommand = new CustomCommand(x => Students.Remove(SelectedStudent), x => SelectedStudent != null);
            DeleteCommand = new CustomCommand(x =>
            {
                try
                {
                    Service.Delete(SelectedStudent.Id);
                    Students.Remove(SelectedStudent);
                }
                catch
                {
                    MessageBox.Show("Usuwanie nie powiodło się", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, x => SelectedStudent != null);
        }

        private IStudentService Service { get; } = new DbStudentService();

        //public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student> { new Student { FirstName = "Adam", LastName = "Adamski", BirthDate = new DateTime(1978, 2, 21) }, new Student { FirstName = "Ewa", LastName = "Ewowska", BirthDate = new DateTime(1990, 1, 1), Gender = Gender.Female  } };
        public ObservableCollection<Student> Students => _students ?? (_students = new ObservableCollection<Student>(Service.Read()));

        public Student SelectedStudent { get; set; }

        public ICommand DeleteCommand { get; }

        public ICommand AddCommand => new CustomCommand(x =>
        {
            var student = new Student();
            while (true)
            {
                if (new StudentDialogView(student).ShowDialog() == true)
                {
                    try
                    {
                        student.Id = Service.Create(student);
                        Students.Add(student);
                        return;
                    }
                    catch
                    {
                        MessageBox.Show("Dodawanie nie powiodło się", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                    return;
            }
        });
        public ICommand EditCommand => new CustomCommand(x =>
        {
            var student = (Student)SelectedStudent.Clone();
            if (new StudentDialogView(student).ShowDialog() == true)
            {
                try
                {
                    Service.Update(student.Id, student);
                    Students.Remove(SelectedStudent);
                    Students.Add(student);
                }
                catch
                {
                    MessageBox.Show("Dodawanie nie powiodło się", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }, x => SelectedStudent != null);


        //public ICommand AddCommand => new CustomCommand(x => AddOrEditStudent(new Student()));
        //public ICommand EditCommand => new CustomCommand(x => AddOrEditStudent(SelectedStudent), x => SelectedStudent != null);

        //private void AddOrEditStudent(Student student)
        //{
        //    var studentClone = (Student)student.Clone();
        //    var dialog = new StudentDialogView(studentClone);
        //    if (dialog.ShowDialog() != true)
        //    {
        //        return;
        //    }

        //    if (Students.Contains(student))
        //        Students.Remove(student);
        //    Students.Add(studentClone);
        //}
    }
}
