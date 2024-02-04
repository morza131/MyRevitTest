using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using MyRevitTest.ViewFilter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyRevitTest.ViewFilter.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel(ExternalCommandData commandData, RevitTask revitTask)
        {
            _revitTask = revitTask;
            SelectElementCommand = new RelayCommand(OnSelectElementExecute);
        }

        public ICommand SelectElementCommand { get; }
        private void OnSelectElementExecute(object p)
        {
            FilterModel.PickElements(_revitTask, ListOfCategories);
            RaiseCloseRequest();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event EventHandler CloseRequest;
        public void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

        private List<Category> listOfCategories;
        public List<Category> ListOfCategories
        {
            get
            {
                return listOfCategories; 
            }
            set
            {
                listOfCategories = value;
                OnPropertyChanged(); 
            }
        }
        private RevitTask _revitTask;
    }
}
