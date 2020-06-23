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
    public class EducatorsViewModel
    {
        public EducatorsViewModel()
        {
            DeleteCommand = new CustomCommand(x => Educators.Remove(SelectedEducator), x => SelectedEducator != null);
        }

        public ObservableCollection<Educator> Educators { get; set; } = new ObservableCollection<Educator> { new Educator { FirstName = "Adam", LastName = "Adamski", BirthDate = new DateTime(1978, 2, 21) }, new Educator { FirstName = "Ewa", LastName = "Ewowska", BirthDate = new DateTime(1990, 1, 1), Gender = Gender.Female  } };
        public Educator SelectedEducator { get; set; }

        public ICommand DeleteCommand { get; }
        public ICommand AddCommand => new CustomCommand(x => AddOrEditEducator(new Educator()));
        public ICommand EditCommand => new CustomCommand(x => AddOrEditEducator(SelectedEducator), x => SelectedEducator != null);

        private void AddOrEditEducator(Educator educator)
        {
            var educatorClone = (Educator)educator.Clone();
            var dialog = new EducatorDialogView(educatorClone);
            if(dialog.ShowDialog() != true)
            {
                return;
            }

            if (Educators.Contains(educator))
                Educators.Remove(educator);
            Educators.Add(educatorClone);
        }
    }
}
