using Autodesk.Revit.UI;
using System;
using System.Windows.Media.Imaging;

namespace AddinExportCDW
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    internal class App : IExternalApplication
    {
        public static string ExecutingAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

        public Result OnStartup(UIControlledApplication application)
        {
            string tabName = "CDW Estimation";
            application.CreateRibbonTab(tabName);
            RibbonPanel panel1 = application.CreateRibbonPanel(tabName, "Calculate");
            RibbonPanel panel2 = application.CreateRibbonPanel(tabName, "Select");
            PushButton button = panel1.AddItem(new PushButtonData("button", "TOTAL WASTE", ExecutingAssemblyPath, "AddinExportCDW.ModelElement")) as PushButton;
            PushButton button2 = panel2.AddItem(new PushButtonData("button2", "WASTE by ELEMENT", ExecutingAssemblyPath, "AddinExportCDW.SelectionElement")) as PushButton;
            button.LargeImage = new BitmapImage(new Uri("pack://application:,,,/AddinExportCDW;component/Resources/pictograma_total_waste_opt.png"));
            button.ToolTip = "Calcula la estimación CDW para todos los elementos del documento activo.";
            button2.LargeImage = new BitmapImage(new Uri("pack://application:,,,/AddinExportCDW;component/Resources/pictograma_waste_by_element_opt.png"));
            button2.ToolTip = "Calcula la estimación CDW solamente para los elementos que sean seleccionados directamente del documento activo.";

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}