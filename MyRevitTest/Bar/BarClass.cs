using Autodesk.Revit.UI;
using MyRevitTest.ViewFilter;
using MyRevitTest.ViewRename;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyRevitTest
{
    public class BarClass : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location,
                   tabName = "MyTest";
            application.CreateRibbonTab(tabName);
            RibbonPanel testPanel = application.CreateRibbonPanel(tabName, "Тестовая панель");
            PushButtonData renameButtonData = new PushButtonData(nameof(ViewRenameMain), "Переименование видов",
                assemblyLocation, typeof(ViewRenameMain).FullName)
            {
                LongDescription = "Сперва выберите нужные виды или листы для переименования, затем запустите программу",
            };
            testPanel.AddItem(renameButtonData);
            PushButtonData selectButtonData = new PushButtonData(nameof(ViewFilterMain), "Выбор элементв",
                assemblyLocation, typeof(ViewFilterMain).FullName)
            {
                LongDescription = "Откройте вид с элементами и запустите программу",
            };
            testPanel.AddItem(selectButtonData);
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
