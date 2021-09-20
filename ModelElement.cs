using AddinExportCDW.Views;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddinExportCDW
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    internal class ModelElement : IExternalCommand
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

                #endregion Comandos entrada

                #region Macro Code

                #region Collector de Elementos

                // llamada a Class Collectora

                IList<Element> floors = CollectorElement.Get(doc, "floors");
                IList<Element> structuralColumns = CollectorElement.Get(doc, "structuralColumns");
                IList<Element> strFoundation = CollectorElement.Get(doc, "strFoundation");
                IList<Element> strFramming = CollectorElement.Get(doc, "strFramming");
                IList<Element> walls = CollectorElement.Get(doc, "walls");
                IList<Element> columns = CollectorElement.Get(doc, "columns");

                #endregion Collector de Elementos

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
            catch (Exception e)
            {
                StepLog.Write(commandData, e.Message.ToString());
                TaskDialog.Show("InformationForm", "To run this command you need to go back to a 3D Model View, it cannot be executed within the active Schedule View. ");
                return Result.Cancelled;
            }
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