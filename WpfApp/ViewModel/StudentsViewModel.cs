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
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.View;
using Newtonsoft.Json.Linq;
using Service;
using System.ComponentModel;
using WpfApp.Encryption;

namespace WpfApp.ViewModel
{
    public class StudentsViewModel : INotifyPropertyChanged
    {

        public StudentsViewModel()
        {
            Service.ReadAsync().ContinueWith(x =>
            {
                Students = new ObservableCollection<Student>(x.Result);
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Students)));
            });

            //DeleteCommand = new CustomCommand(x => Students.Remove(SelectedStudent), x => SelectedStudent != null);
            DeleteCommand = new CustomCommand(async x =>
            {
                try
                {
                    await Service.DeleteAsync(SelectedStudent.Id);
                    Students.Remove(SelectedStudent);
                }
                catch
                {
                    MessageBox.Show("Usuwanie nie powiodło się", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, x => SelectedStudent != null);
        }

        private IStudentService Service { get; } = new StudentService();

        //public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student> { new Student { FirstName = "Adam", LastName = "Adamski", BirthDate = new DateTime(1978, 2, 21) }, new Student { FirstName = "Ewa", LastName = "Ewowska", BirthDate = new DateTime(1990, 1, 1), Gender = Gender.Female  } };
        public ObservableCollection<Student> Students { get; set; }

        public Student SelectedStudent { get; set; }

        public ICommand DeleteCommand { get; }

        public ICommand AddCommand => new CustomCommand(async x =>
        {
            var student = new Student();
            while (true)
            {
                if (new StudentDialogView(student).ShowDialog() == true)
                {
                    try
                    {
                        student.Id = await Service.CreateAsync(student);
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
        public ICommand EditCommand => new AsyncCustomCommand(async x =>
        {
            var student = (Student)SelectedStudent.Clone();
            if (new StudentDialogView(student).ShowDialog() == true)
            {
                try
                {
                    var selectedStudent = SelectedStudent;
                    await Service.UpdateAsync(student.Id, student);
                    var studentIndex = Students.Select((collectionStudent, index) => new { collectionStudent, index }).Single(param => param.collectionStudent.Id == selectedStudent.Id).index;
                    Students.RemoveAt(studentIndex);
                    Students.Insert(studentIndex, student);
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
                //PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            var json = JsonConvert.SerializeObject(SelectedStudent, Formatting.Indented, jsonSerializerSettings);

            using (var word = new WordWrapper())
            {
                word.CreateDocument();
                word.AppendTest(json);
                word.SaveAs(dialog.FileName.Replace(".json", ".docx"));
            }

                //var encryptor = new Encryptor("1234");
                //var bytes = encryptor.Encrypt(json, "json");

                var encryptor = new AsymmetricEncryptor();
            var bytes = encryptor.Encrypt(Encoding.Unicode.GetBytes(json), "CN=School");

            Console.WriteLine(json);
            File.WriteAllBytes(dialog.FileName, bytes);

            //using (var writer = new StreamWriter(dialog.OpenFile()))
            //{
            //    writer.Write(json);
            //    writer.Flush();
            //}
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


            var bytes = File.ReadAllBytes(dialog.FileName);

            //var encryptor = new Encryptor("1234");
            //bytes = encryptor.Decrypt(bytes, "json");
            var encryptor = new AsymmetricEncryptor();
            bytes = encryptor.Decrypt(bytes, "CN=School");

            var json = Encoding.Unicode.GetString(bytes);

            SelectedStudent = JsonConvert.DeserializeObject<Student>(json);
            EditCommand.Execute(null);

        });

        public event PropertyChangedEventHandler PropertyChanged;

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
