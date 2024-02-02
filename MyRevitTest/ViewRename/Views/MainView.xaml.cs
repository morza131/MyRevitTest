using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using MyRevitTest.ViewRename.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyRevitTest.ViewRename.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(ExternalCommandData commandData, List<View> selectedViews, RevitTask revitTask)
        {
            InitializeComponent();
           
            MainViewModel viewModel = new MainViewModel(commandData, selectedViews, revitTask);
            viewModel.CloseRequest += (s, e) => Close();
            DataContext = viewModel;
            
        }
    }
}
