using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using MyRevitTest.ViewFilter.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRevitTest.ViewFilter
{
    [Transaction(TransactionMode.Manual)]
    public class ViewFilterMain : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication application = commandData.Application;
            RevitTask revitTask = new RevitTask();
            List<Category> categoriesAtView = GetCategoriesAtView(application);
            MainView window = new MainView(commandData, revitTask, categoriesAtView);
            window.ShowDialog();
            return Result.Succeeded;
        }

        private List<Category> GetCategoriesAtView(UIApplication application)
        {
            UIDocument uidoc = application.ActiveUIDocument;
            Document doc = uidoc.Document;
            View activeView = doc.ActiveView;
            List<Category> categoriesAtView = new FilteredElementCollector(doc, activeView.Id)                
                .ToElements()
                .Select(e => e.Category)
                .Where(c => c != null)
                .GroupBy(c => c.Id)
                .Select(g => g.First())
                .ToList();
            return categoriesAtView;
        }
    }
}
