using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;

namespace WpfApp.ViewModel
{
    public class StudentDialogViewModel
    {
        public Student Student { get; set; }
        public ICommand OkCommand => new CustomCommand(x => ((Window)x).DialogResult = true, x => Student.IsValid);
    }
}
