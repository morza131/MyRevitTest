using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRevitTest.ViewRename.Model
{
    internal static class RenameModel
    {        
        public static async void Rename(RevitTask revitTask, List<View> selectedViews, ExternalCommandData commandData,
            string prefixText, string startNumberText, string sufixText)
        {     
            selectedViews.Sort((view1, view2) => NamingUtils.CompareNames(view1.Name, view2.Name));
            await revitTask.Run(uiapp =>
            {
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Document doc = uidoc.Document;
                using (Transaction tx = new Transaction(doc, "Переименование видов"))
                {
                    tx.Start();
                    int startNumber = int.Parse(startNumberText);
                    foreach (View view in selectedViews)
                    {
                        view.Name = prefixText + startNumber + sufixText;
                        startNumber++;
                    }
                    tx.Commit();
                }
            });
        }
    }
}
