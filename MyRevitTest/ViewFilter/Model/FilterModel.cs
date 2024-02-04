using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyRevitTest.ViewFilter.Model
{
    public class FilterModel
    {
        public static async void PickElements(RevitTask revitTask, List<Category> listOfCategories)
        {
            List<ElementId> elementsOfCategories = new List<ElementId>();
            await revitTask.Run(uiApp =>
            {
                UIDocument uidoc = uiApp.ActiveUIDocument;
                Document doc = uidoc.Document;
                View activeView = uidoc.ActiveView;
                foreach (Category category in listOfCategories)
                {
                    BuiltInCategory bic = (BuiltInCategory)category.Id.IntegerValue;
                    ElementCategoryFilter categoryFilter = new ElementCategoryFilter(bic);
                    FilteredElementCollector collector = new FilteredElementCollector(doc, activeView.Id);
                    List<ElementId> elements = collector
                    .WherePasses(categoryFilter)
                    .ToElements()
                    .Select(e=>e.Id)
                    .ToList();
                    elementsOfCategories.AddRange(elements);
                }
                uidoc.Selection.SetElementIds(elementsOfCategories);
            }
            );                
        }
    }
}
