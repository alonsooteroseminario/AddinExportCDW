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
            RibbonPanel panel1 = application.CreateRibbonPanel(tabName, "Information");
            RibbonPanel panel2 = application.CreateRibbonPanel(tabName, "Calculate");
            RibbonPanel panel3 = application.CreateRibbonPanel(tabName, "Select");

            PushButton button = panel1.AddItem(new PushButtonData("button", "Information", ExecutingAssemblyPath, "AddinExportCDW.Information")) as PushButton;
            PushButton button2 = panel2.AddItem(new PushButtonData("button2", "Total Waste", ExecutingAssemblyPath, "AddinExportCDW.ModelElement")) as PushButton;
            PushButton button3 = panel3.AddItem(new PushButtonData("button3", "Waste by Element", ExecutingAssemblyPath, "AddinExportCDW.SelectionElement")) as PushButton;

            button.LargeImage = new BitmapImage(new Uri("pack://application:,,,/AddinExportCDW;component/Resources/pictograma_información.png"));
            button.LongDescription = "Application designed for the quantification of construction waste in the design phase. The types and quantities of CDW are estimated and managed according to EU guidelines, identified and quantified waste is classified according to the European List of Wastes (LoW).";

            button2.LargeImage = new BitmapImage(new Uri("pack://application:,,,/AddinExportCDW;component/Resources/pictograma_residuos.png"));
            button2.ToolTip = "Waste quantification of the project building elements.";

            button3.LargeImage = new BitmapImage(new Uri("pack://application:,,,/AddinExportCDW;component/Resources/pictograma_residuos.png"));
            button3.ToolTip = "Waste quantification of the building element selected directly with the mouse.";

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}