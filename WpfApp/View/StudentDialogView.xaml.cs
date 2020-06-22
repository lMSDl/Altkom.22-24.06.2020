using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.ViewModel;

namespace WpfApp.View
{
    /// <summary>
    /// Interaction logic for StudentDialogView.xaml
    /// </summary>
    public partial class StudentDialogView : Window
    {
        public StudentDialogView(Student student)
        {
            InitializeComponent();
            DataContext = new StudentDialogViewModel() { Student = student };
        }
    }
}
