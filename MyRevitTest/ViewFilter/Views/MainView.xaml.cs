using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using MyRevitTest.ViewFilter.ViewModel;
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

namespace MyRevitTest.ViewFilter.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(ExternalCommandData commandData, RevitTask revitTask, List<Category> categoriesAtView)
        {
            InitializeComponent();            
            MainViewModel viewModel = new MainViewModel(commandData, revitTask);
            viewModel.CloseRequest += (s, e) => Close();
            DataContext = viewModel;
            IsCheckedToRevitCategoryConverter categoryConverter = new IsCheckedToRevitCategoryConverter();
            Resources["IsCheckedToRevitCategoryConverter"] = categoryConverter;
            int i = 0;
            foreach (Category category in categoriesAtView)
            {
                
                SelectionGrid.RowDefinitions.Add(new RowDefinition());
                CheckBox checkBox = new CheckBox()
                {   Content = category.Name,
                    Margin = new Thickness(5),                    
                    Name = $"CategoryCheckbox{i + 1}",
                    Tag = category
                };
                                
                System.Windows.Data.Binding bindingSelection = new System.Windows.Data.Binding("ListOfCategories");
                bindingSelection.Source = viewModel;
                bindingSelection.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                bindingSelection.Converter = categoryConverter;
                bindingSelection.ConverterParameter = checkBox.Tag;
                bindingSelection.Mode = BindingMode.TwoWay;
                BindingOperations.SetBinding(checkBox, CheckBox.IsCheckedProperty, bindingSelection);
                SelectionGrid.Children.Add(checkBox);
                int indexOfcheckBox = SelectionGrid.Children.IndexOf(checkBox);
                System.Windows.Controls.Grid.SetRow(SelectionGrid.Children[indexOfcheckBox], i);
                i++;
            }
            SelectionGrid.RowDefinitions.Add(new RowDefinition());
            Button createButton = new Button()
            {
                Margin = new Thickness(5),
                Content = "Выбрать элементы",
            };
            System.Windows.Data.Binding binding = new System.Windows.Data.Binding("SelectElementCommand");
            binding.Source = viewModel;
            BindingOperations.SetBinding(createButton, Button.CommandProperty, binding);
            SelectionGrid.Children.Add(createButton);            
            System.Windows.Controls.Grid.SetRow(createButton, i);
        }
    }
}
