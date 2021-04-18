using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using forms = System.Windows.Forms;

namespace AddinExportCDW
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    class App : IExternalApplication
    {
        public static string ExecutingAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;


        public Result OnStartup(UIControlledApplication application)
        {
            //Aqui va los botones
            // Todo el código para crear los botones en la Ribbon
            string tabName = "CDW Estimación";
            application.CreateRibbonTab(tabName);

            // Crear Panel 1
            RibbonPanel panel1 = application.CreateRibbonPanel(tabName, "Calcular");
            RibbonPanel panel2 = application.CreateRibbonPanel(tabName, "Seleccionar");
            //RibbonPanel panel3 = application.CreateRibbonPanel(tabName, "Chart");

            // Agregar un botón a Panel 1
            PushButton button = panel1.AddItem(new PushButtonData("button", "Calcular", ExecutingAssemblyPath, "AddinExportCDW.ModelElement")) as PushButton;
            PushButton button2 = panel2.AddItem(new PushButtonData("button2", "Seleccionar", ExecutingAssemblyPath, "AddinExportCDW.SelectionElement")) as PushButton;
            //PushButton button3 = panel3.AddItem(new PushButtonData("button3", "Chart", ExecutingAssemblyPath, "AddinExportCDW.ChartElement")) as PushButton;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
