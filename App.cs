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
            //Aqui va los botones
            // Todo el código para crear los botones en la Ribbon
            string tabName = "CDW Estimación";
            application.CreateRibbonTab(tabName);
            // Crear Panel 1
            RibbonPanel panel1 = application.CreateRibbonPanel(tabName, "Calcular");
            RibbonPanel panel2 = application.CreateRibbonPanel(tabName, "Seleccionar");
            // Agregar un botón a Panel 1
            PushButton button = panel1.AddItem(new PushButtonData("button", "Todo el Proyecto", ExecutingAssemblyPath, "AddinExportCDW.ModelElement")) as PushButton;
            PushButton button2 = panel2.AddItem(new PushButtonData("button2", "Seleccionar elementos", ExecutingAssemblyPath, "AddinExportCDW.SelectionElement")) as PushButton;
            // Agregar Imagen al botón
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