using AddinExportCDW.Views;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace AddinExportCDW
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    internal class ModelElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            #region Comandos entrada

            //Get application and document objects
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            Document doc = uiApp.ActiveUIDocument.Document;
            View activeView = ComandoEntrada(uiApp, uidoc);

            #endregion Comandos entrada

            #region Macro Code

            #region Collector de Elementos

            // llamada a Class Collectora

            IList<Element> floors = CollectorElement.Get(doc, activeView, "floors");
            IList<Element> structuralColumns = CollectorElement.Get(doc, activeView, "structuralColumns");
            IList<Element> strFoundation = CollectorElement.Get(doc, activeView, "strFoundation");
            IList<Element> strFramming = CollectorElement.Get(doc, activeView, "strFramming");
            IList<Element> walls = CollectorElement.Get(doc, activeView, "walls");
            IList<Element> columns = CollectorElement.Get(doc, activeView, "columns");

            #endregion Collector de Elementos

            #region Crear Tablas Schedules

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            IList<Element> viewSchedulesAllProject = collector.OfClass(typeof(ViewSchedule)).ToElements();
            bool existeSchedule = true;
            foreach (Element viewSche in viewSchedulesAllProject)// Existe?
            {
                if (viewSche.Name.Contains(" CDW ESTIMATION SCHEDULE"))
                {
                    existeSchedule = false;
                    break;
                }
            }

            List<Element> TodoslosElemetosDelModelo = new List<Element>();
            IEnumerable<Element> elementosProyecto = CollectorElement.GetAllModelElements(doc);
            foreach (Element elemento in elementosProyecto)
            {
                TodoslosElemetosDelModelo.Add(elemento);
            }
            IEnumerable<Element> familiasInstanciaProyecto = CollectorElement.GetFamilyInstanceModelElements(doc);
            foreach (Element elemento in familiasInstanciaProyecto)
            {
                TodoslosElemetosDelModelo.Add(elemento);
            }

            List<Element> TodosLosElementosCDW = CollectorElement.FiltrarElementosCDW(commandData, TodoslosElemetosDelModelo);

            if (CreateSchedule.ExistParameters(commandData, Dictionary.Get("data_forjado"), TodosLosElementosCDW))
            {
                if (existeSchedule)
                {
                }
            }
            else
            {
                CreateSchedule.CreateParameters(commandData, Dictionary.Get("data_forjado"));
                if (existeSchedule)
                {
                    CreateSchedule.CreateSchedules(commandData, Dictionary.Get("data_forjado"));
                }
            }

            #endregion Crear Tablas Schedules

            List<double> listaN_valor = Core.GetListValores(commandData,
                                                                floors,
                                                                structuralColumns,
                                                                strFoundation,
                                                                strFramming,
                                                                walls,
                                                                columns);
            List<Dictionary<string, string>> lista_Dictionarios = Core.GetListDictionary(commandData,
                                                    floors,
                                                    structuralColumns,
                                                    strFoundation,
                                                    strFramming,
                                                    walls,
                                                    columns);
            List<double> lista_desperdicios = Core.GetListDesperdicio(commandData,
                                                    floors,
                                                    structuralColumns,
                                                    strFoundation,
                                                    strFramming,
                                                    walls,
                                                    columns);
            double desperdicioTotal = Core.GetDesperdicioTotal(commandData,
                                                    floors,
                                                    structuralColumns,
                                                    strFoundation,
                                                    strFramming,
                                                    walls,
                                                    columns);
            List<List<List<double>>> listaDe_listaN_valorSeparaadaPorDataElemento = Core.GetListValoresSeparaadaPorDataElemento(commandData,
                                                    floors,
                                                    structuralColumns,
                                                    strFoundation,
                                                    strFramming,
                                                    walls,
                                                    columns);

            #region mensaje en Pantalla

            double count = floors.Count()
                        + structuralColumns.Count()
                        + strFoundation.Count()
                        + strFramming.Count()
                        + walls.Count()
                        + columns.Count();

            WindowMensaje MainMensaje = new WindowMensaje(count,
                                                          commandData,
                                                          listaN_valor,
                                                          lista_Dictionarios,
                                                          lista_desperdicios,
                                                          desperdicioTotal,
                                                          listaDe_listaN_valorSeparaadaPorDataElemento);
            MainMensaje.ShowDialog();

            #endregion mensaje en Pantalla

            #endregion Macro Code

            return Result.Succeeded;
        }

        private static View ComandoEntrada(UIApplication uiApp, UIDocument uidoc)
        {
            _ = uiApp.Application;
            View activeView = uidoc.ActiveView;
            return activeView;
        }

        private static void DisplayInExcel(IEnumerable<Account> accounts, IEnumerable<Account> accounts2)
        {
            Excel.Application excelApp = new Excel.Application
            {
                // Make the object visible.
                Visible = true
            };

            // Create a new, empty workbook and add it to the collection returned
            // by property Workbooks. The new workbook becomes the active workbook.
            // Add has an optional parameter for specifying a praticular template.
            // Because no argument is sent in this example, Add creates a new workbook.
            Excel.Workbook xlWorkBook = excelApp.Workbooks.Add();

            // This example uses a single workSheet. The explicit type casting is
            // removed in a later procedure.
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            // Establish column headings in cells A1 and B1.
            workSheet.Cells[1, "A"] = "Elemento";

            workSheet.Cells[1, "B"] = "RCD (m³)";

            var row = 1;
            foreach (var acct in accounts)
            {
                row++;
                workSheet.Cells[row, "A"] = acct.ID;

                workSheet.Cells[row, "B"] = acct.Balance;
            }

            ((Excel.Range)workSheet.Columns[1]).AutoFit();
            ((Excel.Range)workSheet.Columns[2]).AutoFit();

            Excel.Sheets worksheets = xlWorkBook.Worksheets;
            var xlNewSheet = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlNewSheet.Name = "Hoja2";
            xlNewSheet.Cells[1, "A"] = "Código LER";
            xlNewSheet.Cells[1, "B"] = "RCD (m³)";

            var row2 = 1;
            foreach (var acct in accounts2)
            {
                row2++;
                xlNewSheet.Cells[row2, "A"] = acct.ID;
                xlNewSheet.Cells[row2, "B"] = acct.Balance;
            }

            ((Excel.Range)xlNewSheet.Columns[1]).AutoFit();
            ((Excel.Range)xlNewSheet.Columns[2]).AutoFit();
        }

        public Result OnStartup(UIControlledApplication application)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            return Result.Succeeded;
        }
    }
}