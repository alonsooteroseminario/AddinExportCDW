using AddinExportCDW.Views;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace AddinExportCDW
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    internal class Information : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            #region Comandos entrada

            //Get application and document objects
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            ComandoEntrada(uiApp, uidoc);

            #endregion Comandos entrada

            InformationForm mainInformationForm = new InformationForm();
            mainInformationForm.ShowDialog();

            return Result.Succeeded;
        }

        private static void ComandoEntrada(UIApplication uiApp, UIDocument uidoc)
        {
            _ = uiApp.ActiveUIDocument.Document;
            _ = uiApp.Application;
            _ = uidoc.ActiveView;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            if (application is null)
            {
                throw new System.ArgumentNullException(nameof(application));
            }

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            if (application is null)
            {
                throw new System.ArgumentNullException(nameof(application));
            }

            return Result.Succeeded;
        }
    }
}