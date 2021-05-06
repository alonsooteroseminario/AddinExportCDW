﻿using AddinExportCDW.Views;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace AddinExportCDW
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    internal class SelectionElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                #region Comandos entrada

                //Get application and document objects
                UIApplication uiApp = commandData.Application;
                UIDocument uidoc = uiApp.ActiveUIDocument;
                Document doc = uiApp.ActiveUIDocument.Document;
                ComandoEntrada(uiApp, uidoc);

                #endregion Comandos entrada

                #region Macro Code

                #region Selecton collector

                List<Element> lista_SelectElements = new List<Element>();
                try
                {
                    IList<Reference> references = uidoc.Selection.PickObjects(ObjectType.Element, "Select the Element you want to analyze");
                    foreach (Reference reference in references)
                    {
                        Element e = doc.GetElement(reference);
                        lista_SelectElements.Add(e);
                    }
                }
                catch
                {
                    return Result.Cancelled;
                }

                #endregion Selecton collector

                #region Colectores de Elementos

                List<Element> floors = new List<Element>();
                List<Element> structuralColumns = new List<Element>();
                List<Element> strFoundation = new List<Element>();
                List<Element> strFramming = new List<Element>();
                List<Element> walls = new List<Element>();
                List<Element> columns = new List<Element>();

                foreach (Element sc in lista_SelectElements)
                {
                    Category category = sc.Category;
                    BuiltInCategory builtCategory = (BuiltInCategory)category.Id.IntegerValue;

                    if (builtCategory == BuiltInCategory.OST_Floors)
                    {
                        floors.Add(sc);
                    }
                    if (builtCategory == BuiltInCategory.OST_Walls)
                    {
                        walls.Add(sc);
                    }
                    if (builtCategory == BuiltInCategory.OST_StructuralColumns)
                    {
                        structuralColumns.Add(sc);
                    }
                    if (builtCategory == BuiltInCategory.OST_StructuralFraming)
                    {
                        strFramming.Add(sc);
                    }
                    if (builtCategory == BuiltInCategory.OST_StructuralFoundation)
                    {
                        strFoundation.Add(sc);
                    }
                    if (builtCategory == BuiltInCategory.OST_Columns)
                    {
                        columns.Add(sc);
                    }
                }

                #endregion Colectores de Elementos

                #region Crear Tablas Schedules

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

                CreateSchedule.CreateParameters(commandData, Dictionary.Get("data_forjado"), TodosLosElementosCDW);
                CreateSchedule.CreateSchedules(commandData, Dictionary.Get("data_forjado"));

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

                double count = lista_SelectElements.Count();

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
            catch (Exception e)
            {
                StepLog.Write(commandData, e.Message.ToString());
                throw;
            }
        }

        private static void ComandoEntrada(UIApplication uiApp, UIDocument uidoc)
        {
            _ = uiApp.Application;
            _ = uidoc.ActiveView;
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