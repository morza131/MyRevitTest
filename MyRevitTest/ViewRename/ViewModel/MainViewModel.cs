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
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

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
            LoadSettings();
        }        
        public ICommand RenameViewsCommand { get; }
        private void OnRenameViewsCommandExecute(object p)
        {
            RenameModel.Rename(_revitTask, _selectedViews, _commandData,
                PrefixText, StartNumberText, SuffixText);
            SaveSettings();
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
        public string SuffixText
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
                SampleOfName = $"{PrefixText}{startNumber}{SuffixText}";
            }
            else
            {
                SampleOfName = $"{PrefixText}{StartNumberText}{SuffixText}";
            }
        }

        private void SaveSettings()
        {
            var settings = new
            {
                PrefixText = this.PrefixText,
                StartNumberText = this.StartNumberText,
                SuffixText = this.SuffixText
            };
            string json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(SettingsPath, json);
        }

        private void LoadSettings()
        {
            if (File.Exists(SettingsPath))
            {
                string json = File.ReadAllText(SettingsPath);
                var settings = JsonConvert.DeserializeObject<dynamic>(json);
                PrefixText = settings.PrefixText;
                StartNumberText = settings.StartNumberText;
                SuffixText = settings.SuffixText;
            }
        }

        private RevitTask _revitTask;
        private ExternalCommandData _commandData;
        private List<View> _selectedViews;
#if R2022
        private string SettingsPath = @"C:\Program Files\MyTest\R2022\ViewRenameSettings.json";
#elif R2023
        private string SettingsPath = @"C:\Program Files\MyTest\R2023\ViewRenameSettings.json";
#elif R2024
        private string SettingsPath = @"C:\Program Files\MyTest\R2024\ViewRenameSettings.json";
#elif DEBUG
        private string SettingsPath = Assembly.GetExecutingAssembly().Location + "ViewRenameSettings.json";
#endif
    }
}
