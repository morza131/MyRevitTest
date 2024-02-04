using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using MyRevitTest.ViewRename.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyRevitTest.ViewRename.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel(ExternalCommandData commandData, List<View> selectedViews, RevitTask revitTask)
        {
            _revitTask = revitTask;
            _commandData = commandData;
            _selectedViews = selectedViews;
            RenameViewsCommand = new RelayCommand(OnRenameViewsCommandExecute, CanRenameViewsCommandExecuted);
        }        
        public ICommand RenameViewsCommand { get; }
        private void OnRenameViewsCommandExecute(object p)
        {
            RenameModel.Rename(_revitTask, _selectedViews, _commandData,
                PrefixText, StartNumberText, SufixText);
            RaiseCloseRequest();
            TaskDialog.Show("Готово", "Виды успешно переименованы");
        }
        private bool CanRenameViewsCommandExecuted(object p)
        {
            if (!int.TryParse(startNumberText, out _))
            {
                return false;
            }
            return true;
        }

        private string prefixText;
        public string PrefixText
        {
            get => prefixText;
            set
            {
                prefixText = value;
                OnPropertyChanged();
                UpdateCombinedProperty();
            }
        }
        private string startNumberText;
        public string StartNumberText
        {
            get => startNumberText;
            set
            {
                startNumberText = value;
                OnPropertyChanged();
                UpdateCombinedProperty();
            }
        }
        private string sufixText;
        public string SufixText
        {
            get => sufixText;
            set
            {
                sufixText = value;
                OnPropertyChanged();
                UpdateCombinedProperty();
            }
        }
        private string sampleOfName;
        public string SampleOfName
        {
            get => sampleOfName;
            set
            {
                sampleOfName = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public event EventHandler CloseRequest;
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateCombinedProperty()
        {
            int startNumber;
            if (int.TryParse(StartNumberText, out startNumber))
            {
                SampleOfName = $"{PrefixText}{startNumber}{SufixText}";
            }
            else
            {
                SampleOfName = $"{PrefixText}{StartNumberText}{SufixText}";
            }
        }

        private RevitTask _revitTask;
        private ExternalCommandData _commandData;
        private List<View> _selectedViews;
    }
}
