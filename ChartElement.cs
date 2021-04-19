using AddinExportCDW.Views;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddinExportCDW
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    class ChartElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            #region Comandos entrada
            //Get application and document objects
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            Document doc = uiApp.ActiveUIDocument.Document;
            Application app = uiApp.Application;
            // Get Active View
            View activeView = uidoc.ActiveView;
            string ruta = App.ExecutingAssemblyPath;
            #endregion

            #region Macro Code

            #region Collector de Elementos
            // llamada a Class Collectora 

            IList<Element> floors = CollectorElement.Get(doc, activeView, "floors");
            IList<Element> structuralColumns = CollectorElement.Get(doc, activeView, "structuralColumns");
            IList<Element> strFoundation = CollectorElement.Get(doc, activeView, "strFoundation");
            IList<Element> strFramming = CollectorElement.Get(doc, activeView, "strFramming");
            IList<Element> walls = CollectorElement.Get(doc, activeView, "walls");
            #endregion

            List<double> listaN_valor = Core.GetListValores(commandData,
                                                                floors,
                                                                structuralColumns,
                                                                strFoundation,
                                                                strFramming,
                                                                walls);
            List<Dictionary<string, string>> lista_Dictionarios = Core.GetListDictionary(commandData,
                                                    floors,
                                                    structuralColumns,
                                                    strFoundation,
                                                    strFramming,
                                                    walls);
            List<double> lista_desperdicios = Core.GetListDesperdicio(commandData,
                                                    floors,
                                                    structuralColumns,
                                                    strFoundation,
                                                    strFramming,
                                                    walls);
            double desperdicioTotal = Core.GetDesperdicioTotal(commandData,
                                                    floors,
                                                    structuralColumns,
                                                    strFoundation,
                                                    strFramming,
                                                    walls);
            #endregion

            return Result.Succeeded;
        }
        public Result OnStartup(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
