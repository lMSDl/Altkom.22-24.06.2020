using DAL.Services;
using Microsoft.Win32;
using Models;
using Newtonsoft.Json;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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


        public ICommand ExportCommand => new CustomCommand(x =>
        {
            var dialog = new SaveFileDialog()
            {
                Filter = "Json|*.json|Wszystkie pliki|*.*",
                FileName = SelectedStudent.FullName,
                InitialDirectory = "C:\\Users\\Student\\Desktop"
            };

            if (dialog.ShowDialog() != true)
                return;
            
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            var json = JsonConvert.SerializeObject(SelectedStudent, Formatting.Indented, jsonSerializerSettings);
            Console.WriteLine(json);

            using (var writer = new StreamWriter(dialog.OpenFile()))
            {
                writer.Write(json);
                writer.Flush();
            }
            //File.WriteAllText(dialog.FileName, json);


        }, x => SelectedStudent != null);

        public ICommand ImportCommand => new CustomCommand(x =>
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Json|*.json",
                InitialDirectory = "C:\\Users\\Student\\Desktop"
            };

            if (dialog.ShowDialog() != true)
                return;


            var json = File.ReadAllText(dialog.FileName);

            SelectedStudent = JsonConvert.DeserializeObject<Student>(json);
            EditCommand.Execute(null);

        });

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
