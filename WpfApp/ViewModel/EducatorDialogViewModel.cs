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
    public class EducatorDialogViewModel
    {
        public Educator Educator { get; set; }
        public ICommand OkCommand => new CustomCommand(x => ((Window)x).DialogResult = true);
    }
}
