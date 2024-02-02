using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using MyRevitTest.ViewRename.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRevitTest.ViewRename
{

    [Transaction(TransactionMode.Manual)]
    internal class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication application = commandData.Application;
            UIDocument uidoc = application.ActiveUIDocument;
            Document doc = uidoc.Document;
            RevitTask revitTask = new RevitTask();
            ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();
            List<View> selectedViews = new List<View>();
            foreach (ElementId id in selectedIds)
            {
                Element elem = doc.GetElement(id);
                if (elem is View view)
                {
                    selectedViews.Add(view);
                }
            }
            if (selectedViews == null)
            {
                TaskDialog.Show("Ошибка", "Сперва выберите виды для переименования в диспетчере " +
                    "проекта, затем запустите программу");
                return Result.Cancelled;
            }
            MainView window = new MainView(commandData, selectedViews, revitTask);
            window.ShowDialog();
            return Result.Succeeded;
        }
    }

}
