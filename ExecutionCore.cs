using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;

namespace AddinExportCDW
{
    public static class Core
    {
        //entrega lista de listas con los valores de las listas2,3,4,5,6,7,8,9,10,11
        public static List<List<double>> GetListValoresByName(ExternalCommandData commandData,
                        IList<Element> floors,
                        IList<Element> structuralColumns,
                        IList<Element> strFoundation,
                        IList<Element> strFramming,
                        IList<Element> walls,
                        IList<Element> columns)
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

            #endregion Comandos entrada

            #region Dictionarios

            Dictionary<string, string> data_forjado = Dictionary.Get("data_forjado");
            Dictionary<string, string> data_pilar_hormigon = Dictionary.Get("data_pilar_hormigon");
            Dictionary<string, string> data_floors_concreto = Dictionary.Get("data_floors_concreto");
            Dictionary<string, string> data_Cimentaciones = Dictionary.Get("data_Cimentaciones");
            Dictionary<string, string> data_ConcretoDeck = Dictionary.Get("data_ConcretoDeck");
            Dictionary<string, string> data_Droppedbeam = Dictionary.Get("data_Droppedbeam");
            Dictionary<string, string> data_ConcreteSlab = Dictionary.Get("data_ConcreteSlab");
            Dictionary<string, string> data_Beamembbeded = Dictionary.Get("data_Beamembbeded");
            Dictionary<string, string> data_ConcreteInclinedSlab = Dictionary.Get("data_ConcreteInclinedSlab");
            Dictionary<string, string> data_walls = Dictionary.Get("data_walls");
            Dictionary<string, string> data_SteelColumns = Dictionary.Get("data_SteelColumns");

            #endregion Dictionarios

            #region listas de valores

            // listas de valores
            List<double> lista2_valor = new List<double>();
            List<double> lista3_valor = new List<double>();
            List<double> lista4_valor = new List<double>();
            List<double> lista5_valor = new List<double>();
            List<double> lista6_valor = new List<double>();
            List<double> lista7_valor = new List<double>();
            List<double> lista8_valor = new List<double>();
            List<double> lista9_valor = new List<double>();
            List<double> lista10_valor = new List<double>();
            List<double> lista11_valor = new List<double>();

            #endregion listas de valores

            // 10 elementos : Aquí se filtran los Elementos con el Código
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");

                    if ((pamType.AsValueString() == data_forjado["Código"]))
                    {
                        Dictionary<string, string> data = data_forjado;
                        SetValueToParameter.SetArea(data, sc, doc);
                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (structuralColumns.Count() != 0)
            {
                foreach (Element sc in structuralColumns)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_pilar_hormigon["Código"]))
                    {
                        Dictionary<string, string> data = data_pilar_hormigon;
                        SetValueToParameter.SetVolume(data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        Dictionary<string, string> data = data_floors_concreto;
                        SetValueToParameter.SetArea(data, sc, doc);
                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (strFoundation.Count() != 0)
            {
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_Cimentaciones;
                        SetValueToParameter.SetVolume(data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcretoDeck["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcretoDeck;
                        SetValueToParameter.SetArea(data, sc, doc);
                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_Droppedbeam["Código"]))
                    {
                        Dictionary<string, string> data = data_Droppedbeam;
                        SetValueToParameter.SetVolume(data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcreteSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteSlab;
                        SetValueToParameter.SetArea(data, sc, doc);
                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_Beamembbeded["Código"]))
                    {
                        Dictionary<string, string> data = data_Beamembbeded;
                        SetValueToParameter.SetVolume(data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteInclinedSlab;
                        SetValueToParameter.SetArea(data, sc, doc);
                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (walls.Count() != 0)
            {
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");

                    if ((pamType.AsValueString() == data_walls["Código"]) || (pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_walls;
                        SetValueToParameter.SetVolume(data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (columns.Count() != 0)
            {
                foreach (Element sc in columns)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material");
                    Dictionary<string, string> data = data_SteelColumns;
                    if ((pamType.AsValueString() == data["Código"]))
                    {
                        SetValueToParameter.SetVolume_SteelColumnSpecialCommand(data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "11"));
                    }
                }
            }

            List<List<double>> salida = new List<List<double>>();

            salida.Add(lista2_valor);
            salida.Add(lista3_valor);
            salida.Add(lista4_valor);
            salida.Add(lista5_valor);
            salida.Add(lista6_valor);
            salida.Add(lista7_valor);
            salida.Add(lista8_valor);
            salida.Add(lista9_valor);
            salida.Add(lista10_valor);
            salida.Add(lista11_valor);

            return salida;
        }

        //entrega una lista con todo los valores sumados de las listas2,3,4,5,6,7,8,9,10,11
        public static List<double> GetListValores(ExternalCommandData commandData,
                                IList<Element> floors,
                                IList<Element> structuralColumns,
                                IList<Element> strFoundation,
                                IList<Element> strFramming,
                                IList<Element> walls,
                                IList<Element> columns)
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

            #endregion Comandos entrada

            #region Dictionarios

            List<Dictionary<string, string>> lista_Dictionarios = new List<Dictionary<string, string>>();

            Dictionary<string, string> data_forjado = Dictionary.Get("data_forjado");
            Dictionary<string, string> data_pilar_hormigon = Dictionary.Get("data_pilar_hormigon");
            Dictionary<string, string> data_floors_concreto = Dictionary.Get("data_floors_concreto");
            Dictionary<string, string> data_Cimentaciones = Dictionary.Get("data_Cimentaciones");
            Dictionary<string, string> data_ConcretoDeck = Dictionary.Get("data_ConcretoDeck");
            Dictionary<string, string> data_Droppedbeam = Dictionary.Get("data_Droppedbeam");
            Dictionary<string, string> data_ConcreteSlab = Dictionary.Get("data_ConcreteSlab");
            Dictionary<string, string> data_Beamembbeded = Dictionary.Get("data_Beamembbeded");
            Dictionary<string, string> data_ConcreteInclinedSlab = Dictionary.Get("data_ConcreteInclinedSlab");
            Dictionary<string, string> data_walls = Dictionary.Get("data_walls");
            Dictionary<string, string> data_SteelColumns = Dictionary.Get("data_SteelColumns");

            #endregion Dictionarios

            #region listas de valores

            // listas de valores
            List<double> lista2_valor = new List<double>();
            List<double> lista3_valor = new List<double>();
            List<double> lista4_valor = new List<double>();
            List<double> lista5_valor = new List<double>();
            List<double> lista6_valor = new List<double>();
            List<double> lista7_valor = new List<double>();
            List<double> lista8_valor = new List<double>();
            List<double> lista9_valor = new List<double>();
            List<double> lista10_valor = new List<double>();
            List<double> lista11_valor = new List<double>();

            #endregion listas de valores

            // 10 elementos : Aquí se filtran los Elementos con el Código
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");

                    if ((pamType.AsValueString() == data_forjado["Código"]))
                    {
                        Dictionary<string, string> data = data_forjado;
                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (structuralColumns.Count() != 0)
            {
                foreach (Element sc in structuralColumns)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_pilar_hormigon["Código"]))
                    {
                        Dictionary<string, string> data = data_pilar_hormigon;

                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        Dictionary<string, string> data = data_floors_concreto;

                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (strFoundation.Count() != 0)
            {
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_Cimentaciones;

                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcretoDeck["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcretoDeck;

                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_Droppedbeam["Código"]))
                    {
                        Dictionary<string, string> data = data_Droppedbeam;

                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcreteSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteSlab;

                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_Beamembbeded["Código"]))
                    {
                        Dictionary<string, string> data = data_Beamembbeded;

                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteInclinedSlab;

                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (walls.Count() != 0)
            {
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_walls["Código"]) || (pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_walls;

                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
            }
            if (columns.Count() != 0)
            {
                foreach (Element sc in columns)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material");
                    Dictionary<string, string> data = data_SteelColumns;
                    if ((pamType.AsValueString() == data["Código"]))
                    {
                        lista2_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "11"));
                    }
                }
            }

            List<double> salida = new List<double>();

            #region Suma de todos los valores de las listas

            //Suma de todos los Valores
            double valor2_final = 0; //07 07 01 - aqueous washing liquids ACUMULADO
            for (int i = 0; i < lista2_valor.Count(); i++)
            {
                valor2_final = valor2_final + lista2_valor[i];
            }
            salida.Add(valor2_final);
            double valor3_final = 0; //"15 01 02 - plastic packaging" ACUMULADO
            for (int i = 0; i < lista3_valor.Count(); i++)
            {
                valor3_final = valor3_final + lista3_valor[i];
            }
            salida.Add(valor3_final);
            double valor4_final = 0;//"15 01 03 - wooden packaging" ACUMULADO
            for (int i = 0; i < lista4_valor.Count(); i++)
            {
                valor4_final = valor4_final + lista4_valor[i];
            }
            salida.Add(valor4_final);
            double valor5_final = 0;//"15 01 04 - metallic packaging" ACUMULADO
            for (int i = 0; i < lista5_valor.Count(); i++)
            {
                valor5_final = valor5_final + lista5_valor[i];
            }
            salida.Add(valor5_final);
            double valor6_final = 0;//"15 01 06 - mixed packaging"] ACUMULADO
            for (int i = 0; i < lista6_valor.Count(); i++)
            {
                valor6_final = valor6_final + lista6_valor[i];
            }
            salida.Add(valor6_final);
            double valor7_final = 0;//"17 01 01 - concrete" ACUMULADO
            for (int i = 0; i < lista7_valor.Count(); i++)
            {
                valor7_final = valor7_final + lista7_valor[i];
            }
            salida.Add(valor7_final);
            double valor8_final = 0;//"17 02 01 - wood" ACUMULADO
            for (int i = 0; i < lista8_valor.Count(); i++)
            {
                valor8_final = valor8_final + lista8_valor[i];
            }
            salida.Add(valor8_final);
            double valor9_final = 0; //"17 02 03 - plastic" ACUMULADO
            for (int i = 0; i < lista9_valor.Count(); i++)
            {
                valor9_final = valor9_final + lista9_valor[i];
            }
            salida.Add(valor9_final);
            double valor10_final = 0;//"17 04 05 - iron and steel" ACUMULADO
            for (int i = 0; i < lista10_valor.Count(); i++)
            {
                valor10_final = valor10_final + lista10_valor[i];
            }
            salida.Add(valor10_final);
            double valor11_final = 0;//"17 09 04 - mixed" ACUMULADO
            for (int i = 0; i < lista11_valor.Count(); i++)
            {
                valor11_final = valor11_final + lista11_valor[i];
            }
            salida.Add(valor11_final);

            #endregion Suma de todos los valores de las listas

            return salida;
        }

        //entrega lista de los diccionarios que se usan en la ejecución
        public static List<Dictionary<string, string>> GetListDictionary(ExternalCommandData commandData,
                                IList<Element> floors,
                                IList<Element> structuralColumns,
                                IList<Element> strFoundation,
                                IList<Element> strFramming,
                                IList<Element> walls,
                                IList<Element> columns)
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

            #endregion Comandos entrada

            #region Dictionarios

            List<Dictionary<string, string>> lista_Dictionarios = new List<Dictionary<string, string>>();

            Dictionary<string, string> data_forjado = Dictionary.Get("data_forjado");
            Dictionary<string, string> data_pilar_hormigon = Dictionary.Get("data_pilar_hormigon");
            Dictionary<string, string> data_floors_concreto = Dictionary.Get("data_floors_concreto");
            Dictionary<string, string> data_Cimentaciones = Dictionary.Get("data_Cimentaciones");
            Dictionary<string, string> data_ConcretoDeck = Dictionary.Get("data_ConcretoDeck");
            Dictionary<string, string> data_Droppedbeam = Dictionary.Get("data_Droppedbeam");
            Dictionary<string, string> data_ConcreteSlab = Dictionary.Get("data_ConcreteSlab");
            Dictionary<string, string> data_Beamembbeded = Dictionary.Get("data_Beamembbeded");
            Dictionary<string, string> data_ConcreteInclinedSlab = Dictionary.Get("data_ConcreteInclinedSlab");
            Dictionary<string, string> data_walls = Dictionary.Get("data_walls");
            Dictionary<string, string> data_SteelColumns = Dictionary.Get("data_SteelColumns");

            #endregion Dictionarios

            // 10 elementos : Aquí se filtran los Elementos con el Código
            if (floors.Count() != 0)
            {
                lista_Dictionarios.Add(data_forjado);
            }
            if (structuralColumns.Count() != 0)
            {
                lista_Dictionarios.Add(data_pilar_hormigon);
            }
            if (floors.Count() != 0)
            {
                lista_Dictionarios.Add(data_floors_concreto);
            }
            if (strFoundation.Count() != 0)
            {
                lista_Dictionarios.Add(data_Cimentaciones);
            }
            if (floors.Count() != 0)
            {
                lista_Dictionarios.Add(data_ConcretoDeck);
            }
            if (strFramming.Count() != 0)
            {
                lista_Dictionarios.Add(data_Droppedbeam);
            }
            if (floors.Count() != 0)
            {
                lista_Dictionarios.Add(data_ConcreteSlab);
            }
            if (strFramming.Count() != 0)
            {
                lista_Dictionarios.Add(data_Beamembbeded);
            }
            if (floors.Count() != 0)
            {
                lista_Dictionarios.Add(data_ConcreteInclinedSlab);
            }
            if (walls.Count() != 0)
            {
                lista_Dictionarios.Add(data_walls);
            }
            if (columns.Count() != 0)
            {
                lista_Dictionarios.Add(data_SteelColumns);
            }

            return lista_Dictionarios;
        }

        //entrega la lista de despecidios de los elementos presentes separados por categoria
        public static List<double> GetListDesperdicio(ExternalCommandData commandData,
                                IList<Element> floors,
                                IList<Element> structuralColumns,
                                IList<Element> strFoundation,
                                IList<Element> strFramming,
                                IList<Element> walls,
                                IList<Element> columns)
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

            #endregion Comandos entrada

            #region Dictionarios

            List<Dictionary<string, string>> lista_Dictionarios = new List<Dictionary<string, string>>();

            Dictionary<string, string> data_forjado = Dictionary.Get("data_forjado");
            Dictionary<string, string> data_pilar_hormigon = Dictionary.Get("data_pilar_hormigon");
            Dictionary<string, string> data_floors_concreto = Dictionary.Get("data_floors_concreto");
            Dictionary<string, string> data_Cimentaciones = Dictionary.Get("data_Cimentaciones");
            Dictionary<string, string> data_ConcretoDeck = Dictionary.Get("data_ConcretoDeck");
            Dictionary<string, string> data_Droppedbeam = Dictionary.Get("data_Droppedbeam");
            Dictionary<string, string> data_ConcreteSlab = Dictionary.Get("data_ConcreteSlab");
            Dictionary<string, string> data_Beamembbeded = Dictionary.Get("data_Beamembbeded");
            Dictionary<string, string> data_ConcreteInclinedSlab = Dictionary.Get("data_ConcreteInclinedSlab");
            Dictionary<string, string> data_walls = Dictionary.Get("data_walls");
            Dictionary<string, string> data_SteelColumns = Dictionary.Get("data_SteelColumns");

            #endregion Dictionarios

            #region datos iniciales

            List<double> lista_sumaTotal_valor_porArea_Forjado = new List<double>();
            List<double> lista_sumaTotal_valor_porVolumen_PilarConcreto = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Concreto = new List<double>();
            List<double> lista_sumaTotal_valor_porVolumen_Cimentaciones = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_ConcretoDeck = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Droppedbeam = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_ConcreteSlab = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Beamembbededm = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_ConcreteInclinedSlab = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_walls_Concrete = new List<double>();
            List<double> lista_sumaTotal_valor_porVolumen_data_SteelColumns = new List<double>();

            #endregion datos iniciales

            #region desperdicios

            List<double> lista_desperdicios = new List<double>();
            // 10 elementos
            double desperdicioForjado = 0;
            double desperdicioPilarHormigon = 0;
            double desperdicioConcreto = 0;
            double desperdicioCimentacion = 0;
            double desperdicioConcretoDeck = 0;
            double desperdicio_Droppedbeam = 0;
            double desperdicio_ConcreteSlab = 0;
            double desperdicio_Beamembbededm = 0;
            double desperdicio_ConcreteInclinedSlab = 0;
            double desperdicio_walls_Concrete = 0;
            double desperdicio_SteelColumns = 0;

            #endregion desperdicios

            // 10 elementos : Aquí se filtran los Elementos con el Código
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");

                    if ((pamType.AsValueString() == data_forjado["Código"]))
                    {
                        Dictionary<string, string> data = data_forjado;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_Forjado.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Forjado.Count(); i++)
                {
                    desperdicioForjado = desperdicioForjado + lista_sumaTotal_valor_porArea_Forjado[i];// para Forjados
                }
                lista_desperdicios.Add(desperdicioForjado);
            }
            if (structuralColumns.Count() != 0)
            {
                foreach (Element sc in structuralColumns)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_pilar_hormigon["Código"]))
                    {
                        Dictionary<string, string> data = data_pilar_hormigon;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
                        lista_sumaTotal_valor_porVolumen_PilarConcreto.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porVolumen_PilarConcreto.Count(); i++)
                {
                    desperdicioPilarHormigon = desperdicioPilarHormigon + lista_sumaTotal_valor_porVolumen_PilarConcreto[i];// para pilar de hormigon
                }
                lista_desperdicios.Add(desperdicioPilarHormigon);
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        Dictionary<string, string> data = data_floors_concreto;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_Concreto.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Concreto.Count(); i++)
                {
                    desperdicioConcreto = desperdicioConcreto + lista_sumaTotal_valor_porArea_Concreto[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicioConcreto);
            }
            if (strFoundation.Count() != 0)
            {
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_Cimentaciones;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
                        lista_sumaTotal_valor_porVolumen_Cimentaciones.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porVolumen_Cimentaciones.Count(); i++)
                {
                    desperdicioCimentacion = desperdicioCimentacion + lista_sumaTotal_valor_porVolumen_Cimentaciones[i];
                }
                lista_desperdicios.Add(desperdicioCimentacion);
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcretoDeck["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcretoDeck;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_ConcretoDeck.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcretoDeck.Count(); i++)
                {
                    desperdicioConcretoDeck = desperdicioConcretoDeck + lista_sumaTotal_valor_porArea_ConcretoDeck[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicioConcretoDeck);
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_Droppedbeam["Código"]))
                    {
                        Dictionary<string, string> data = data_Droppedbeam;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
                        lista_sumaTotal_valor_porArea_Droppedbeam.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Droppedbeam.Count(); i++)
                {
                    desperdicio_Droppedbeam = desperdicio_Droppedbeam + lista_sumaTotal_valor_porArea_Droppedbeam[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_Droppedbeam);
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcreteSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteSlab;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_ConcreteSlab.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcreteSlab.Count(); i++)
                {
                    desperdicio_ConcreteSlab = desperdicio_ConcreteSlab + lista_sumaTotal_valor_porArea_ConcreteSlab[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_ConcreteSlab);
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_Beamembbeded["Código"]))
                    {
                        Dictionary<string, string> data = data_Beamembbeded;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
                        lista_sumaTotal_valor_porArea_Beamembbededm.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Beamembbededm.Count(); i++)
                {
                    desperdicio_Beamembbededm = desperdicio_Beamembbededm + lista_sumaTotal_valor_porArea_Beamembbededm[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_Beamembbededm);
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteInclinedSlab;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_ConcreteInclinedSlab.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcreteInclinedSlab.Count(); i++)
                {
                    desperdicio_ConcreteInclinedSlab = desperdicio_ConcreteInclinedSlab + lista_sumaTotal_valor_porArea_ConcreteInclinedSlab[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_ConcreteInclinedSlab);
            }
            if (walls.Count() != 0)
            {
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_walls["Código"]) || (pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_walls;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
                        lista_sumaTotal_valor_porArea_walls_Concrete.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_walls_Concrete.Count(); i++)
                {
                    desperdicio_walls_Concrete = desperdicio_walls_Concrete + lista_sumaTotal_valor_porArea_walls_Concrete[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_walls_Concrete);
            }
            if (columns.Count() != 0)
            {
                foreach (Element sc in columns)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material");
                    Dictionary<string, string> data = data_SteelColumns;
                    if ((pamType.AsValueString() == data["Código"]))
                    {
                        double sumaTotal_valor_porVolumen = CalcVolume.Get_SteelColumnSpecialCommand(data, sc);
                        lista_sumaTotal_valor_porVolumen_data_SteelColumns.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porVolumen_data_SteelColumns.Count(); i++)
                {
                    desperdicio_SteelColumns = desperdicio_SteelColumns + lista_sumaTotal_valor_porVolumen_data_SteelColumns[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_SteelColumns);
            }

            return lista_desperdicios;
        }

        //entrega el despecidio total de todos sumados
        public static double GetDesperdicioTotal(ExternalCommandData commandData,
                        IList<Element> floors,
                        IList<Element> structuralColumns,
                        IList<Element> strFoundation,
                        IList<Element> strFramming,
                        IList<Element> walls,
                        IList<Element> columns)
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

            #endregion Comandos entrada

            #region Dictionarios

            Dictionary<string, string> data_forjado = Dictionary.Get("data_forjado");
            Dictionary<string, string> data_pilar_hormigon = Dictionary.Get("data_pilar_hormigon");
            Dictionary<string, string> data_floors_concreto = Dictionary.Get("data_floors_concreto");
            Dictionary<string, string> data_Cimentaciones = Dictionary.Get("data_Cimentaciones");
            Dictionary<string, string> data_ConcretoDeck = Dictionary.Get("data_ConcretoDeck");
            Dictionary<string, string> data_Droppedbeam = Dictionary.Get("data_Droppedbeam");
            Dictionary<string, string> data_ConcreteSlab = Dictionary.Get("data_ConcreteSlab");
            Dictionary<string, string> data_Beamembbeded = Dictionary.Get("data_Beamembbeded");
            Dictionary<string, string> data_ConcreteInclinedSlab = Dictionary.Get("data_ConcreteInclinedSlab");
            Dictionary<string, string> data_walls = Dictionary.Get("data_walls");
            Dictionary<string, string> data_SteelColumns = Dictionary.Get("data_SteelColumns");

            #endregion Dictionarios

            #region datos iniciales

            List<double> lista_sumaTotal_valor_porArea_Forjado = new List<double>();
            List<double> lista_sumaTotal_valor_porVolumen_PilarConcreto = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Concreto = new List<double>();
            List<double> lista_sumaTotal_valor_porVolumen_Cimentaciones = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_ConcretoDeck = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Droppedbeam = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_ConcreteSlab = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Beamembbededm = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_ConcreteInclinedSlab = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_walls_Concrete = new List<double>();
            List<double> lista_sumaTotal_valor_porVolumen_data_SteelColumns = new List<double>();

            #endregion datos iniciales

            #region desperdicios

            List<double> lista_desperdicios = new List<double>();
            // 10 elementos
            double desperdicioForjado = 0;
            double desperdicioPilarHormigon = 0;
            double desperdicioConcreto = 0;
            double desperdicioCimentacion = 0;
            double desperdicioConcretoDeck = 0;
            double desperdicio_Droppedbeam = 0;
            double desperdicio_ConcreteSlab = 0;
            double desperdicio_Beamembbededm = 0;
            double desperdicio_ConcreteInclinedSlab = 0;
            double desperdicio_walls_Concrete = 0;
            double desperdicio_SteelColumns = 0;

            #endregion desperdicios

            // 10 elementos : Aquí se filtran los Elementos con el Código
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");

                    if ((pamType.AsValueString() == data_forjado["Código"]))
                    {
                        Dictionary<string, string> data = data_forjado;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_Forjado.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Forjado.Count(); i++)
                {
                    desperdicioForjado = desperdicioForjado + lista_sumaTotal_valor_porArea_Forjado[i];// para Forjados
                }
                lista_desperdicios.Add(desperdicioForjado);
            }
            if (structuralColumns.Count() != 0)
            {
                foreach (Element sc in structuralColumns)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_pilar_hormigon["Código"]))
                    {
                        Dictionary<string, string> data = data_pilar_hormigon;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
                        lista_sumaTotal_valor_porVolumen_PilarConcreto.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porVolumen_PilarConcreto.Count(); i++)
                {
                    desperdicioPilarHormigon = desperdicioPilarHormigon + lista_sumaTotal_valor_porVolumen_PilarConcreto[i];// para pilar de hormigon
                }
                lista_desperdicios.Add(desperdicioPilarHormigon);
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        Dictionary<string, string> data = data_floors_concreto;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_Concreto.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Concreto.Count(); i++)
                {
                    desperdicioConcreto = desperdicioConcreto + lista_sumaTotal_valor_porArea_Concreto[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicioConcreto);
            }
            if (strFoundation.Count() != 0)
            {
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_Cimentaciones;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
                        lista_sumaTotal_valor_porVolumen_Cimentaciones.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porVolumen_Cimentaciones.Count(); i++)
                {
                    desperdicioCimentacion = desperdicioCimentacion + lista_sumaTotal_valor_porVolumen_Cimentaciones[i];
                }
                lista_desperdicios.Add(desperdicioCimentacion);
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcretoDeck["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcretoDeck;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_ConcretoDeck.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcretoDeck.Count(); i++)
                {
                    desperdicioConcretoDeck = desperdicioConcretoDeck + lista_sumaTotal_valor_porArea_ConcretoDeck[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicioConcretoDeck);
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_Droppedbeam["Código"]))
                    {
                        Dictionary<string, string> data = data_Droppedbeam;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
                        lista_sumaTotal_valor_porArea_Droppedbeam.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Droppedbeam.Count(); i++)
                {
                    desperdicio_Droppedbeam = desperdicio_Droppedbeam + lista_sumaTotal_valor_porArea_Droppedbeam[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_Droppedbeam);
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcreteSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteSlab;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_ConcreteSlab.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcreteSlab.Count(); i++)
                {
                    desperdicio_ConcreteSlab = desperdicio_ConcreteSlab + lista_sumaTotal_valor_porArea_ConcreteSlab[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_ConcreteSlab);
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_Beamembbeded["Código"]))
                    {
                        Dictionary<string, string> data = data_Beamembbeded;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
                        lista_sumaTotal_valor_porArea_Beamembbededm.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Beamembbededm.Count(); i++)
                {
                    desperdicio_Beamembbededm = desperdicio_Beamembbededm + lista_sumaTotal_valor_porArea_Beamembbededm[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_Beamembbededm);
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteInclinedSlab;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_ConcreteInclinedSlab.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcreteInclinedSlab.Count(); i++)
                {
                    desperdicio_ConcreteInclinedSlab = desperdicio_ConcreteInclinedSlab + lista_sumaTotal_valor_porArea_ConcreteInclinedSlab[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_ConcreteInclinedSlab);
            }
            if (walls.Count() != 0)
            {
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_walls["Código"]) || (pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_walls;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
                        lista_sumaTotal_valor_porArea_walls_Concrete.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_walls_Concrete.Count(); i++)
                {
                    desperdicio_walls_Concrete = desperdicio_walls_Concrete + lista_sumaTotal_valor_porArea_walls_Concrete[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_walls_Concrete);
            }
            if (columns.Count() != 0)
            {
                foreach (Element sc in columns)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material");
                   
                    if ((pamType.AsValueString() == data_SteelColumns["Código"]))
                    {
                        Dictionary<string, string> data = data_SteelColumns;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get_SteelColumnSpecialCommand(data, sc);
                        lista_sumaTotal_valor_porVolumen_data_SteelColumns.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porVolumen_data_SteelColumns.Count(); i++)
                {
                    desperdicio_SteelColumns = desperdicio_SteelColumns + lista_sumaTotal_valor_porVolumen_data_SteelColumns[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_SteelColumns);
            }

            #region Desperdicio Total

            // Desperdicio total
            double desperdicioTotal = 0;
            for (int i = 0; i < lista_desperdicios.Count(); i++)
            {
                desperdicioTotal = desperdicioTotal + lista_desperdicios[i];
            }

            #endregion Desperdicio Total

            return desperdicioTotal;
        }

        //entrega lista de listas de listas separadas por categoria
        public static List<List<List<double>>> GetListValoresSeparaadaPorDataElemento(ExternalCommandData commandData,
                        IList<Element> floors,
                        IList<Element> structuralColumns,
                        IList<Element> strFoundation,
                        IList<Element> strFramming,
                        IList<Element> walls,
                        IList<Element> columns)
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

            #endregion Comandos entrada

            #region Dictionarios

            List<Dictionary<string, string>> lista_Dictionarios = new List<Dictionary<string, string>>();

            Dictionary<string, string> data_forjado = Dictionary.Get("data_forjado");
            Dictionary<string, string> data_pilar_hormigon = Dictionary.Get("data_pilar_hormigon");
            Dictionary<string, string> data_floors_concreto = Dictionary.Get("data_floors_concreto");
            Dictionary<string, string> data_Cimentaciones = Dictionary.Get("data_Cimentaciones");
            Dictionary<string, string> data_ConcretoDeck = Dictionary.Get("data_ConcretoDeck");
            Dictionary<string, string> data_Droppedbeam = Dictionary.Get("data_Droppedbeam");
            Dictionary<string, string> data_ConcreteSlab = Dictionary.Get("data_ConcreteSlab");
            Dictionary<string, string> data_Beamembbeded = Dictionary.Get("data_Beamembbeded");
            Dictionary<string, string> data_ConcreteInclinedSlab = Dictionary.Get("data_ConcreteInclinedSlab");
            Dictionary<string, string> data_walls = Dictionary.Get("data_walls");
            Dictionary<string, string> data_SteelColumns = Dictionary.Get("data_SteelColumns");

            #endregion Dictionarios

            List<List<List<double>>> salida = new List<List<List<double>>>();

            List<List<double>> salida_data_forjado = new List<List<double>>();
            List<List<double>> salida_data_pilar_hormigon = new List<List<double>>();
            List<List<double>> salida_data_floors_concreto = new List<List<double>>();
            List<List<double>> salida_data_Cimentaciones = new List<List<double>>();
            List<List<double>> salida_data_ConcretoDeck = new List<List<double>>();
            List<List<double>> salida_data_Droppedbeam = new List<List<double>>();
            List<List<double>> salida_data_ConcreteSlab = new List<List<double>>();
            List<List<double>> salida_data_Beamembbeded = new List<List<double>>();
            List<List<double>> salida_data_ConcreteInclinedSlab = new List<List<double>>();
            List<List<double>> salida_data_walls = new List<List<double>>();
            List<List<double>> salida_data_SteelColumns = new List<List<double>>();

            // 10 elementos : Aquí se filtran los Elementos con el Código
            if (floors.Count() != 0)
            {
                #region listas de valores

                // listas de valores
                List<double> lista2_valor = new List<double>();
                List<double> lista3_valor = new List<double>();
                List<double> lista4_valor = new List<double>();
                List<double> lista5_valor = new List<double>();
                List<double> lista6_valor = new List<double>();
                List<double> lista7_valor = new List<double>();
                List<double> lista8_valor = new List<double>();
                List<double> lista9_valor = new List<double>();
                List<double> lista10_valor = new List<double>();
                List<double> lista11_valor = new List<double>();

                #endregion listas de valores

                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");

                    if ((pamType.AsValueString() == data_forjado["Código"]))
                    {
                        Dictionary<string, string> data = data_forjado;
                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
                salida_data_forjado.Add(lista2_valor);//1
                salida_data_forjado.Add(lista3_valor);//2
                salida_data_forjado.Add(lista4_valor);//3
                salida_data_forjado.Add(lista5_valor);//4
                salida_data_forjado.Add(lista6_valor);//5
                salida_data_forjado.Add(lista7_valor);//6
                salida_data_forjado.Add(lista8_valor);//7
                salida_data_forjado.Add(lista9_valor);//8
                salida_data_forjado.Add(lista10_valor);//9
                salida_data_forjado.Add(lista11_valor);//10

                salida.Add(salida_data_forjado);
            }//data_forjado
            if (structuralColumns.Count() != 0)
            {
                #region listas de valores

                // listas de valores
                List<double> lista2_valor = new List<double>();
                List<double> lista3_valor = new List<double>();
                List<double> lista4_valor = new List<double>();
                List<double> lista5_valor = new List<double>();
                List<double> lista6_valor = new List<double>();
                List<double> lista7_valor = new List<double>();
                List<double> lista8_valor = new List<double>();
                List<double> lista9_valor = new List<double>();
                List<double> lista10_valor = new List<double>();
                List<double> lista11_valor = new List<double>();

                #endregion listas de valores

                foreach (Element sc in structuralColumns)//todos los elementos del modelo entero
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_pilar_hormigon["Código"]))
                    {
                        Dictionary<string, string> data = data_pilar_hormigon;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
                salida_data_pilar_hormigon.Add(lista2_valor);
                salida_data_pilar_hormigon.Add(lista3_valor);
                salida_data_pilar_hormigon.Add(lista4_valor);
                salida_data_pilar_hormigon.Add(lista5_valor);
                salida_data_pilar_hormigon.Add(lista6_valor);
                salida_data_pilar_hormigon.Add(lista7_valor);
                salida_data_pilar_hormigon.Add(lista8_valor);
                salida_data_pilar_hormigon.Add(lista9_valor);
                salida_data_pilar_hormigon.Add(lista10_valor);
                salida_data_pilar_hormigon.Add(lista11_valor);

                salida.Add(salida_data_pilar_hormigon);
            }
            if (floors.Count() != 0)
            {
                #region listas de valores

                // listas de valores
                List<double> lista2_valor = new List<double>();
                List<double> lista3_valor = new List<double>();
                List<double> lista4_valor = new List<double>();
                List<double> lista5_valor = new List<double>();
                List<double> lista6_valor = new List<double>();
                List<double> lista7_valor = new List<double>();
                List<double> lista8_valor = new List<double>();
                List<double> lista9_valor = new List<double>();
                List<double> lista10_valor = new List<double>();
                List<double> lista11_valor = new List<double>();

                #endregion listas de valores

                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        Dictionary<string, string> data = data_floors_concreto;
                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
                salida_data_floors_concreto.Add(lista2_valor);
                salida_data_floors_concreto.Add(lista3_valor);
                salida_data_floors_concreto.Add(lista4_valor);
                salida_data_floors_concreto.Add(lista5_valor);
                salida_data_floors_concreto.Add(lista6_valor);
                salida_data_floors_concreto.Add(lista7_valor);
                salida_data_floors_concreto.Add(lista8_valor);
                salida_data_floors_concreto.Add(lista9_valor);
                salida_data_floors_concreto.Add(lista10_valor);
                salida_data_floors_concreto.Add(lista11_valor);

                salida.Add(salida_data_floors_concreto);
            }
            if (strFoundation.Count() != 0)
            {
                #region listas de valores

                // listas de valores
                List<double> lista2_valor = new List<double>();
                List<double> lista3_valor = new List<double>();
                List<double> lista4_valor = new List<double>();
                List<double> lista5_valor = new List<double>();
                List<double> lista6_valor = new List<double>();
                List<double> lista7_valor = new List<double>();
                List<double> lista8_valor = new List<double>();
                List<double> lista9_valor = new List<double>();
                List<double> lista10_valor = new List<double>();
                List<double> lista11_valor = new List<double>();

                #endregion listas de valores

                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_Cimentaciones;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
                salida_data_Cimentaciones.Add(lista2_valor);
                salida_data_Cimentaciones.Add(lista3_valor);
                salida_data_Cimentaciones.Add(lista4_valor);
                salida_data_Cimentaciones.Add(lista5_valor);
                salida_data_Cimentaciones.Add(lista6_valor);
                salida_data_Cimentaciones.Add(lista7_valor);
                salida_data_Cimentaciones.Add(lista8_valor);
                salida_data_Cimentaciones.Add(lista9_valor);
                salida_data_Cimentaciones.Add(lista10_valor);
                salida_data_Cimentaciones.Add(lista11_valor);

                salida.Add(salida_data_Cimentaciones);
            }
            if (floors.Count() != 0)
            {
                #region listas de valores

                // listas de valores
                List<double> lista2_valor = new List<double>();
                List<double> lista3_valor = new List<double>();
                List<double> lista4_valor = new List<double>();
                List<double> lista5_valor = new List<double>();
                List<double> lista6_valor = new List<double>();
                List<double> lista7_valor = new List<double>();
                List<double> lista8_valor = new List<double>();
                List<double> lista9_valor = new List<double>();
                List<double> lista10_valor = new List<double>();
                List<double> lista11_valor = new List<double>();

                #endregion listas de valores

                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcretoDeck["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcretoDeck;
                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
                salida_data_ConcretoDeck.Add(lista2_valor);
                salida_data_ConcretoDeck.Add(lista3_valor);
                salida_data_ConcretoDeck.Add(lista4_valor);
                salida_data_ConcretoDeck.Add(lista5_valor);
                salida_data_ConcretoDeck.Add(lista6_valor);
                salida_data_ConcretoDeck.Add(lista7_valor);
                salida_data_ConcretoDeck.Add(lista8_valor);
                salida_data_ConcretoDeck.Add(lista9_valor);
                salida_data_ConcretoDeck.Add(lista10_valor);
                salida_data_ConcretoDeck.Add(lista11_valor);

                salida.Add(salida_data_ConcretoDeck);
            }
            if (strFramming.Count() != 0)
            {
                #region listas de valores

                // listas de valores
                List<double> lista2_valor = new List<double>();
                List<double> lista3_valor = new List<double>();
                List<double> lista4_valor = new List<double>();
                List<double> lista5_valor = new List<double>();
                List<double> lista6_valor = new List<double>();
                List<double> lista7_valor = new List<double>();
                List<double> lista8_valor = new List<double>();
                List<double> lista9_valor = new List<double>();
                List<double> lista10_valor = new List<double>();
                List<double> lista11_valor = new List<double>();

                #endregion listas de valores

                foreach (Element sc in strFramming)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_Droppedbeam["Código"]))
                    {
                        Dictionary<string, string> data = data_Droppedbeam;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
                salida_data_Droppedbeam.Add(lista2_valor);
                salida_data_Droppedbeam.Add(lista3_valor);
                salida_data_Droppedbeam.Add(lista4_valor);
                salida_data_Droppedbeam.Add(lista5_valor);
                salida_data_Droppedbeam.Add(lista6_valor);
                salida_data_Droppedbeam.Add(lista7_valor);
                salida_data_Droppedbeam.Add(lista8_valor);
                salida_data_Droppedbeam.Add(lista9_valor);
                salida_data_Droppedbeam.Add(lista10_valor);
                salida_data_Droppedbeam.Add(lista11_valor);

                salida.Add(salida_data_Droppedbeam);
            }
            if (floors.Count() != 0)
            {
                #region listas de valores

                // listas de valores
                List<double> lista2_valor = new List<double>();
                List<double> lista3_valor = new List<double>();
                List<double> lista4_valor = new List<double>();
                List<double> lista5_valor = new List<double>();
                List<double> lista6_valor = new List<double>();
                List<double> lista7_valor = new List<double>();
                List<double> lista8_valor = new List<double>();
                List<double> lista9_valor = new List<double>();
                List<double> lista10_valor = new List<double>();
                List<double> lista11_valor = new List<double>();

                #endregion listas de valores

                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcreteSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteSlab;
                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
                salida_data_ConcreteSlab.Add(lista2_valor);
                salida_data_ConcreteSlab.Add(lista3_valor);
                salida_data_ConcreteSlab.Add(lista4_valor);
                salida_data_ConcreteSlab.Add(lista5_valor);
                salida_data_ConcreteSlab.Add(lista6_valor);
                salida_data_ConcreteSlab.Add(lista7_valor);
                salida_data_ConcreteSlab.Add(lista8_valor);
                salida_data_ConcreteSlab.Add(lista9_valor);
                salida_data_ConcreteSlab.Add(lista10_valor);
                salida_data_ConcreteSlab.Add(lista11_valor);

                salida.Add(salida_data_ConcreteSlab);
            }
            if (strFramming.Count() != 0)
            {
                #region listas de valores

                // listas de valores
                List<double> lista2_valor = new List<double>();
                List<double> lista3_valor = new List<double>();
                List<double> lista4_valor = new List<double>();
                List<double> lista5_valor = new List<double>();
                List<double> lista6_valor = new List<double>();
                List<double> lista7_valor = new List<double>();
                List<double> lista8_valor = new List<double>();
                List<double> lista9_valor = new List<double>();
                List<double> lista10_valor = new List<double>();
                List<double> lista11_valor = new List<double>();

                #endregion listas de valores

                foreach (Element sc in strFramming)
                {
                    if ((sc.LookupParameter("Material estructural").AsValueString() == data_Beamembbeded["Código"]))
                    {
                        Dictionary<string, string> data = data_Beamembbeded;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
                salida_data_Beamembbeded.Add(lista2_valor);
                salida_data_Beamembbeded.Add(lista3_valor);
                salida_data_Beamembbeded.Add(lista4_valor);
                salida_data_Beamembbeded.Add(lista5_valor);
                salida_data_Beamembbeded.Add(lista6_valor);
                salida_data_Beamembbeded.Add(lista7_valor);
                salida_data_Beamembbeded.Add(lista8_valor);
                salida_data_Beamembbeded.Add(lista9_valor);
                salida_data_Beamembbeded.Add(lista10_valor);
                salida_data_Beamembbeded.Add(lista11_valor);

                salida.Add(salida_data_Beamembbeded);
            }
            if (floors.Count() != 0)
            {
                #region listas de valores

                // listas de valores
                List<double> lista2_valor = new List<double>();
                List<double> lista3_valor = new List<double>();
                List<double> lista4_valor = new List<double>();
                List<double> lista5_valor = new List<double>();
                List<double> lista6_valor = new List<double>();
                List<double> lista7_valor = new List<double>();
                List<double> lista8_valor = new List<double>();
                List<double> lista9_valor = new List<double>();
                List<double> lista10_valor = new List<double>();
                List<double> lista11_valor = new List<double>();

                #endregion listas de valores

                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if ((pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteInclinedSlab;
                        lista2_valor.Add(CalcArea.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcArea.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcArea.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcArea.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcArea.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcArea.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcArea.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcArea.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcArea.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcArea.GetByValueOfKey(data, sc, "11"));
                    }
                }
                salida_data_ConcreteInclinedSlab.Add(lista2_valor);
                salida_data_ConcreteInclinedSlab.Add(lista3_valor);
                salida_data_ConcreteInclinedSlab.Add(lista4_valor);
                salida_data_ConcreteInclinedSlab.Add(lista5_valor);
                salida_data_ConcreteInclinedSlab.Add(lista6_valor);
                salida_data_ConcreteInclinedSlab.Add(lista7_valor);
                salida_data_ConcreteInclinedSlab.Add(lista8_valor);
                salida_data_ConcreteInclinedSlab.Add(lista9_valor);
                salida_data_ConcreteInclinedSlab.Add(lista10_valor);
                salida_data_ConcreteInclinedSlab.Add(lista11_valor);

                salida.Add(salida_data_ConcreteInclinedSlab);
            }
            if (walls.Count() != 0)
            {
                #region listas de valores

                // listas de valores
                List<double> lista2_valor = new List<double>();
                List<double> lista3_valor = new List<double>();
                List<double> lista4_valor = new List<double>();
                List<double> lista5_valor = new List<double>();
                List<double> lista6_valor = new List<double>();
                List<double> lista7_valor = new List<double>();
                List<double> lista8_valor = new List<double>();
                List<double> lista9_valor = new List<double>();
                List<double> lista10_valor = new List<double>();
                List<double> lista11_valor = new List<double>();

                #endregion listas de valores

                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    string valorSALIDA = pamType.AsValueString();
                    if ((pamType.AsValueString() == data_walls["Código"]) || (pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_walls;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(data, sc, "11"));
                    }
                }
                salida_data_walls.Add(lista2_valor);
                salida_data_walls.Add(lista3_valor);
                salida_data_walls.Add(lista4_valor);
                salida_data_walls.Add(lista5_valor);
                salida_data_walls.Add(lista6_valor);
                salida_data_walls.Add(lista7_valor);
                salida_data_walls.Add(lista8_valor);
                salida_data_walls.Add(lista9_valor);
                salida_data_walls.Add(lista10_valor);
                salida_data_walls.Add(lista11_valor);

                salida.Add(salida_data_walls);
            }
            if (columns.Count() != 0)
            {
                #region listas de valores

                // listas de valores
                List<double> lista2_valor = new List<double>();
                List<double> lista3_valor = new List<double>();
                List<double> lista4_valor = new List<double>();
                List<double> lista5_valor = new List<double>();
                List<double> lista6_valor = new List<double>();
                List<double> lista7_valor = new List<double>();
                List<double> lista8_valor = new List<double>();
                List<double> lista9_valor = new List<double>();
                List<double> lista10_valor = new List<double>();
                List<double> lista11_valor = new List<double>();

                #endregion listas de valores

                foreach (Element sc in columns)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material");
                    Dictionary<string, string> data = data_SteelColumns;
                    if ((pamType.AsValueString() == data["Código"]))
                    {
                        lista2_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(data, sc, "11"));
                    }
                }
                salida_data_SteelColumns.Add(lista2_valor);
                salida_data_SteelColumns.Add(lista3_valor);
                salida_data_SteelColumns.Add(lista4_valor);
                salida_data_SteelColumns.Add(lista5_valor);
                salida_data_SteelColumns.Add(lista6_valor);
                salida_data_SteelColumns.Add(lista7_valor);
                salida_data_SteelColumns.Add(lista8_valor);
                salida_data_SteelColumns.Add(lista9_valor);
                salida_data_SteelColumns.Add(lista10_valor);
                salida_data_SteelColumns.Add(lista11_valor);

                salida.Add(salida_data_SteelColumns);
            }

            return salida;//salida_data_forjado, salida_pilar_hormigon, .. etc
                          //en cada uno de las salidas de arriba van los 10 parametros valores 2,3,4,5,6,7,8,9,10,11
        }
    }
}