using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace MyRevitTest
{
    internal class IsCheckedToRevitCategoryConverter : IValueConverter
    {       
        private List<Category> listOfCategories = new List<Category>();
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {                                  
            Category category = parameter as Category;
            if (listOfCategories.Contains(category))
            {                
                return true;
            }
            else
            {                
                return false;
            }            
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? isChecked = value as bool?;
            Category category = parameter as Category;
            if (isChecked == true)
            {
                listOfCategories.Add(category);                
            }
            else
            {
                listOfCategories.Remove(category);                
            }
            return listOfCategories;
        }
    }
}
