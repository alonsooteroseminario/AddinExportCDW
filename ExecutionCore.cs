using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;

namespace AddinExportCDW
{
    public static class Core
    {
        private static void ComandosEntrada(UIApplication uiApp, UIDocument uidoc)
        {
            _ = uiApp.Application;
            _ = uidoc.ActiveView;
        }

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
            ComandosEntrada(uiApp, uidoc);

            #endregion Comandos entrada

            #region Dictionarios

            Dictionary<string, string> data_forjado = Dictionary.Get("data_forjado");
            Dictionary<string, string> data_pilar_hormigon = Dictionary.Get("data_pilar_hormigon");
            Dictionary<string, string> data_floors_concreto = Dictionary.Get("data_floors_concreto");
            Dictionary<string, string> data_Cimentaciones = Dictionary.Get("data_Cimentaciones");
            Dictionary<string, string> data_ConcretoDeck = Dictionary.Get("data_ConcretoDeck");
            Dictionary<string, string> data_Droppedbeam = Dictionary.Get("data_Droppedbeam");
            Dictionary<string, string> data_Beamembbeded = Dictionary.Get("data_Beamembbeded");
            Dictionary<string, string> data_ConcreteInclinedSlab = Dictionary.Get("data_ConcreteInclinedSlab");
            Dictionary<string, string> data_walls = Dictionary.Get("data_walls");
            Dictionary<string, string> data_SteelColumns = Dictionary.Get("data_SteelColumns");
            Dictionary<string, string> data_forjado35 = Dictionary.Get("data_forjado35");

            Dictionary<string, string> data_foundation = Dictionary.Get("data_foundation");
            Dictionary<string, string> data_CollaboratingSheetMetal = Dictionary.Get("data_CollaboratingSheetMetal");
            Dictionary<string, string> data_Steelbeam = Dictionary.Get("data_Steelbeam");

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

            StepLog.Write(commandData, "GetListValores Start");

            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
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
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_pilar_hormigon["Código"]))
                    {
                        Dictionary<string, string> data = data_pilar_hormigon;
                        SetValueToParameter.SetVolume(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
            }
            if ((floors.Count() != 0) || (strFoundation.Count() != 0))//m3 volumen
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        Dictionary<string, string> data = data_floors_concreto;

                        SetValueToParameter.SetVolume(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        Dictionary<string, string> data = data_floors_concreto;

                        SetValueToParameter.SetVolume(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
            }
            if ((strFoundation.Count() != 0) || (walls.Count() != 0))
            {
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_Cimentaciones;
                        SetValueToParameter.SetVolume(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_Cimentaciones;
                        SetValueToParameter.SetVolume(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
            }
            if (floors.Count() != 0)//m3 volumen
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcretoDeck["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcretoDeck;

                        SetValueToParameter.SetVolume(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Droppedbeam["Código"]))
                    {
                        Dictionary<string, string> data = data_Droppedbeam;
                        SetValueToParameter.SetVolume(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Beamembbeded["Código"]))
                    {
                        Dictionary<string, string> data = data_Beamembbeded;
                        SetValueToParameter.SetVolume(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
            }
            if (floors.Count() != 0)//m3 volumen
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteInclinedSlab;
                        SetValueToParameter.SetVolume(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
            }
            if (walls.Count() != 0)
            {
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_walls["Código"]))
                    {
                        Dictionary<string, string> data = data_walls;
                        SetValueToParameter.SetVolume(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
            }
            if (columns.Count() != 0)
            {
                foreach (Element sc in columns)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Material");
                    }
                    if ((pamType.AsValueString() == data_SteelColumns["Código"]))
                    {
                        Dictionary<string, string> data = data_SteelColumns;
                        SetValueToParameter.SetVolume_SteelColumnSpecialCommand(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "11"));
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_forjado35["Código"]))
                    {
                        Dictionary<string, string> data = data_forjado35;
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
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_foundation["Código"]))
                    {
                        Dictionary<string, string> data = data_foundation;
                        SetValueToParameter.SetVolume(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_CollaboratingSheetMetal["Código"]))
                    {
                        Dictionary<string, string> data = data_CollaboratingSheetMetal;
                        SetValueToParameter.SetVolume(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Steelbeam["Código"]))
                    {
                        Dictionary<string, string> data = data_Steelbeam;
                        SetValueToParameter.SetVolume(commandData, data, sc, doc);
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
            }

            List<double> salida = new List<double>();

            #region Suma de todos los valores de las listas

            //Suma de todos los Valores
            double valor2_final = 0; //07 07 01 - aqueous washing liquids ACUMULADO
            for (int i = 0; i < lista2_valor.Count(); i++)
            {
                valor2_final += lista2_valor[i];
            }
            salida.Add(valor2_final);
            double valor3_final = 0; //"15 01 02 - plastic packaging" ACUMULADO
            for (int i = 0; i < lista3_valor.Count(); i++)
            {
                valor3_final += lista3_valor[i];
            }
            salida.Add(valor3_final);
            double valor4_final = 0;//"15 01 03 - wooden packaging" ACUMULADO
            for (int i = 0; i < lista4_valor.Count(); i++)
            {
                valor4_final += lista4_valor[i];
            }
            salida.Add(valor4_final);
            double valor5_final = 0;//"15 01 04 - metallic packaging" ACUMULADO
            for (int i = 0; i < lista5_valor.Count(); i++)
            {
                valor5_final += lista5_valor[i];
            }
            salida.Add(valor5_final);
            double valor6_final = 0;//"15 01 06 - mixed packaging"] ACUMULADO
            for (int i = 0; i < lista6_valor.Count(); i++)
            {
                valor6_final += lista6_valor[i];
            }
            salida.Add(valor6_final);
            double valor7_final = 0;//"17 01 01 - concrete" ACUMULADO
            for (int i = 0; i < lista7_valor.Count(); i++)
            {
                valor7_final += lista7_valor[i];
            }
            salida.Add(valor7_final);
            double valor8_final = 0;//"17 02 01 - wood" ACUMULADO
            for (int i = 0; i < lista8_valor.Count(); i++)
            {
                valor8_final += lista8_valor[i];
            }
            salida.Add(valor8_final);
            double valor9_final = 0; //"17 02 03 - plastic" ACUMULADO
            for (int i = 0; i < lista9_valor.Count(); i++)
            {
                valor9_final += lista9_valor[i];
            }
            salida.Add(valor9_final);
            double valor10_final = 0;//"17 04 05 - iron and steel" ACUMULADO
            for (int i = 0; i < lista10_valor.Count(); i++)
            {
                valor10_final += lista10_valor[i];
            }
            salida.Add(valor10_final);
            double valor11_final = 0;//"17 09 04 - mixed" ACUMULADO
            for (int i = 0; i < lista11_valor.Count(); i++)
            {
                valor11_final += lista11_valor[i];
            }
            salida.Add(valor11_final);

            #endregion Suma de todos los valores de las listas

            StepLog.Write(commandData, "GetListValores Finish");

            return salida;
        }

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
            ComandosEntrada(uiApp, uidoc);

            #endregion Comandos entrada

            #region Dictionarios

            List<Dictionary<string, string>> lista_Dictionarios = new List<Dictionary<string, string>>();

            Dictionary<string, string> data_forjado = Dictionary.Get("data_forjado");
            Dictionary<string, string> data_pilar_hormigon = Dictionary.Get("data_pilar_hormigon");
            Dictionary<string, string> data_floors_concreto = Dictionary.Get("data_floors_concreto");
            Dictionary<string, string> data_Cimentaciones = Dictionary.Get("data_Cimentaciones");
            Dictionary<string, string> data_ConcretoDeck = Dictionary.Get("data_ConcretoDeck");
            Dictionary<string, string> data_Droppedbeam = Dictionary.Get("data_Droppedbeam");
            Dictionary<string, string> data_Beamembbeded = Dictionary.Get("data_Beamembbeded");
            Dictionary<string, string> data_ConcreteInclinedSlab = Dictionary.Get("data_ConcreteInclinedSlab");
            Dictionary<string, string> data_walls = Dictionary.Get("data_walls");
            Dictionary<string, string> data_SteelColumns = Dictionary.Get("data_SteelColumns");
            Dictionary<string, string> data_forjado35 = Dictionary.Get("data_forjado35");

            Dictionary<string, string> data_foundation = Dictionary.Get("data_foundation");
            Dictionary<string, string> data_CollaboratingSheetMetal = Dictionary.Get("data_CollaboratingSheetMetal");
            Dictionary<string, string> data_Steelbeam = Dictionary.Get("data_Steelbeam");

            #endregion Dictionarios

            StepLog.Write(commandData, "GetListDictionary Start");

            // 10 elementos : Aquí se filtran los Elementos con el Código
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_forjado["Código"]))
                    {
                        lista_Dictionarios.Add(data_forjado);
                        break;
                    }
                }
            }
            if (structuralColumns.Count() != 0)
            {
                foreach (Element sc in structuralColumns)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_pilar_hormigon["Código"]))
                    {
                        lista_Dictionarios.Add(data_pilar_hormigon);
                        break;
                    }
                }
            }
            if ((floors.Count() != 0) || (strFoundation.Count() != 0))
            {
                bool pasoPorAqui = false;
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        lista_Dictionarios.Add(data_floors_concreto);
                        pasoPorAqui = true;
                        break;
                    }
                }
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        if (!pasoPorAqui)
                        {
                            lista_Dictionarios.Add(data_floors_concreto);
                            break;
                        }
                    }
                }
            }
            if ((strFoundation.Count() != 0) || (walls.Count() != 0))
            {
                bool pasoPorAqui = false;
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        lista_Dictionarios.Add(data_Cimentaciones);
                        pasoPorAqui = true;
                        break;
                    }
                }
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        if (!pasoPorAqui)
                        {
                            lista_Dictionarios.Add(data_Cimentaciones);
                            break;
                        }
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcretoDeck["Código"]))
                    {
                        lista_Dictionarios.Add(data_ConcretoDeck);
                        break;
                    }
                }
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Droppedbeam["Código"]))
                    {
                        lista_Dictionarios.Add(data_Droppedbeam);
                        break;
                    }
                }
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Beamembbeded["Código"]))
                    {
                        lista_Dictionarios.Add(data_Beamembbeded);
                        break;
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
                    {
                        lista_Dictionarios.Add(data_ConcreteInclinedSlab);
                        break;
                    }
                }
            }
            if (walls.Count() != 0)
            {
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_walls["Código"]))
                    {
                        lista_Dictionarios.Add(data_walls);
                        break;
                    }
                }
            }
            if (columns.Count() != 0)
            {
                foreach (Element sc in columns)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Material");
                    }
                    Dictionary<string, string> data = data_SteelColumns;
                    if ((pamType.AsValueString() == data["Código"]))
                    {
                        lista_Dictionarios.Add(data_SteelColumns);
                        break;
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_forjado35["Código"]))
                    {
                        lista_Dictionarios.Add(data_forjado35);
                        break;
                    }
                }
            }

            if (strFoundation.Count() != 0)
            {
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_foundation["Código"]))
                    {
                        lista_Dictionarios.Add(data_foundation);
                        break;
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_CollaboratingSheetMetal["Código"]))
                    {
                        lista_Dictionarios.Add(data_CollaboratingSheetMetal);
                        break;
                    }
                }
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Steelbeam["Código"]))
                    {
                        lista_Dictionarios.Add(data_Steelbeam);
                        break;
                    }
                }
            }

            StepLog.Write(commandData, "GetListDictionary Finish");

            return lista_Dictionarios;
        }

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
            ComandosEntrada(uiApp, uidoc);

            #endregion Comandos entrada

            #region Dictionarios

            Dictionary<string, string> data_forjado = Dictionary.Get("data_forjado");
            Dictionary<string, string> data_pilar_hormigon = Dictionary.Get("data_pilar_hormigon");
            Dictionary<string, string> data_floors_concreto = Dictionary.Get("data_floors_concreto");
            Dictionary<string, string> data_Cimentaciones = Dictionary.Get("data_Cimentaciones");
            Dictionary<string, string> data_ConcretoDeck = Dictionary.Get("data_ConcretoDeck");
            Dictionary<string, string> data_Droppedbeam = Dictionary.Get("data_Droppedbeam");
            Dictionary<string, string> data_Beamembbeded = Dictionary.Get("data_Beamembbeded");
            Dictionary<string, string> data_ConcreteInclinedSlab = Dictionary.Get("data_ConcreteInclinedSlab");
            Dictionary<string, string> data_walls = Dictionary.Get("data_walls");
            Dictionary<string, string> data_SteelColumns = Dictionary.Get("data_SteelColumns");
            Dictionary<string, string> data_forjado35 = Dictionary.Get("data_forjado35");

            Dictionary<string, string> data_foundation = Dictionary.Get("data_foundation");
            Dictionary<string, string> data_CollaboratingSheetMetal = Dictionary.Get("data_CollaboratingSheetMetal");
            Dictionary<string, string> data_Steelbeam = Dictionary.Get("data_Steelbeam");

            #endregion Dictionarios

            #region datos iniciales

            List<double> lista_sumaTotal_valor_porArea_Forjado = new List<double>();
            List<double> lista_sumaTotal_valor_porVolumen_PilarConcreto = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Concreto = new List<double>();
            List<double> lista_sumaTotal_valor_porVolumen_Cimentaciones = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_ConcretoDeck = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Droppedbeam = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Beamembbededm = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_ConcreteInclinedSlab = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_walls_Concrete = new List<double>();
            List<double> lista_sumaTotal_valor_porVolumen_data_SteelColumns = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_forjado35 = new List<double>();

            List<double> lista_sumaTotal_valor_porArea_foundation = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_CollaboratingSheetMetal = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Steelbeam = new List<double>();

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
            double desperdicio_Beamembbededm = 0;
            double desperdicio_ConcreteInclinedSlab = 0;
            double desperdicio_walls_Concrete = 0;
            double desperdicio_SteelColumns = 0;
            double desperdicio_forjado35 = 0;

            double desperdicio_foundation = 0;
            double desperdicio_CollaboratingSheetMetal = 0;
            double desperdicio_Steelbeam = 0;

            #endregion desperdicios

            StepLog.Write(commandData, "GetListDesperdicio Start");

            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_forjado["Código"]))
                    {
                        Dictionary<string, string> data = data_forjado;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_Forjado.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Forjado.Count(); i++)
                {
                    desperdicioForjado += lista_sumaTotal_valor_porArea_Forjado[i];// para Forjados
                }
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_forjado["Código"]))
                    {
                        lista_desperdicios.Add(desperdicioForjado);
                        break;
                    }
                }
            }
            if (structuralColumns.Count() != 0)
            {
                foreach (Element sc in structuralColumns)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_pilar_hormigon["Código"]))
                    {
                        Dictionary<string, string> data = data_pilar_hormigon;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porVolumen_PilarConcreto.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porVolumen_PilarConcreto.Count(); i++)
                {
                    desperdicioPilarHormigon += lista_sumaTotal_valor_porVolumen_PilarConcreto[i];
                }
                foreach (Element sc in structuralColumns)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_pilar_hormigon["Código"]))
                    {
                        lista_desperdicios.Add(desperdicioPilarHormigon);
                        break;
                    }
                }
            }
            if ((floors.Count() != 0) || (strFoundation.Count() != 0))//m3 volumen
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        Dictionary<string, string> data = data_floors_concreto;
                        double sumaTotal_valor_porArea = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_Concreto.Add(sumaTotal_valor_porArea);
                    }
                }
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        Dictionary<string, string> data = data_floors_concreto;
                        double sumaTotal_valor_porArea = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_Concreto.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Concreto.Count(); i++)
                {
                    desperdicioConcreto += lista_sumaTotal_valor_porArea_Concreto[i];
                }
                bool pasoPorAqui = false;
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        lista_desperdicios.Add(desperdicioConcreto);
                        pasoPorAqui = true;
                        break;
                    }
                }
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        if (!pasoPorAqui)
                        {
                            lista_desperdicios.Add(desperdicioConcreto);
                            break;
                        }
                    }
                }
            }
            if ((strFoundation.Count() != 0) || (walls.Count() != 0))
            {
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_Cimentaciones;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porVolumen_Cimentaciones.Add(sumaTotal_valor_porVolumen);
                    }
                }
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_Cimentaciones;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porVolumen_Cimentaciones.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porVolumen_Cimentaciones.Count(); i++)
                {
                    desperdicioCimentacion += lista_sumaTotal_valor_porVolumen_Cimentaciones[i];
                }
                bool pasoPorAqui = false;
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        lista_desperdicios.Add(desperdicioCimentacion);
                        pasoPorAqui = true;
                        break;
                    }
                }
                if (!pasoPorAqui)
                {
                    foreach (Element sc in walls)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        Parameter pamType = type.LookupParameter("Material estructural");
                        if (pamType == null)
                        {
                            pamType = type.LookupParameter("Structural Material");
                        }
                        if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                        {
                            lista_desperdicios.Add(desperdicioCimentacion);
                            break;
                        }
                    }
                }
            }
            if (floors.Count() != 0)//m3 volumen
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcretoDeck["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcretoDeck;
                        double sumaTotal_valor_porArea = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_ConcretoDeck.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcretoDeck.Count(); i++)
                {
                    desperdicioConcretoDeck += lista_sumaTotal_valor_porArea_ConcretoDeck[i];
                }
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcretoDeck["Código"]))
                    {
                        lista_desperdicios.Add(desperdicioConcretoDeck);
                        break;
                    }
                }
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Droppedbeam["Código"]))
                    {
                        Dictionary<string, string> data = data_Droppedbeam;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_Droppedbeam.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Droppedbeam.Count(); i++)
                {
                    desperdicio_Droppedbeam += lista_sumaTotal_valor_porArea_Droppedbeam[i];
                }
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Droppedbeam["Código"]))
                    {
                        lista_desperdicios.Add(desperdicio_Droppedbeam);
                        break;
                    }
                }
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Beamembbeded["Código"]))
                    {
                        Dictionary<string, string> data = data_Beamembbeded;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_Beamembbededm.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Beamembbededm.Count(); i++)
                {
                    desperdicio_Beamembbededm += lista_sumaTotal_valor_porArea_Beamembbededm[i];
                }
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Beamembbeded["Código"]))
                    {
                        lista_desperdicios.Add(desperdicio_Beamembbededm);
                        break;
                    }
                }
            }
            if (floors.Count() != 0)//m3 volumen
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteInclinedSlab;
                        double sumaTotal_valor_porArea = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_ConcreteInclinedSlab.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcreteInclinedSlab.Count(); i++)
                {
                    desperdicio_ConcreteInclinedSlab += lista_sumaTotal_valor_porArea_ConcreteInclinedSlab[i];// para Concreto
                }
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
                    {
                        lista_desperdicios.Add(desperdicio_ConcreteInclinedSlab);
                        break;
                    }
                }
            }
            if (walls.Count() != 0)
            {
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_walls["Código"]))
                    {
                        Dictionary<string, string> data = data_walls;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_walls_Concrete.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_walls_Concrete.Count(); i++)
                {
                    desperdicio_walls_Concrete += lista_sumaTotal_valor_porArea_walls_Concrete[i];// para Concreto
                }
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_walls["Código"]))
                    {
                        lista_desperdicios.Add(desperdicio_walls_Concrete);
                        break;
                    }
                }
            }
            if (columns.Count() != 0)
            {
                foreach (Element sc in columns)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Material");
                    }
                    Dictionary<string, string> data = data_SteelColumns;
                    if ((pamType.AsValueString() == data["Código"]))
                    {
                        double sumaTotal_valor_porVolumen = CalcVolume.Get_SteelColumnSpecialCommand(commandData, data, sc);
                        lista_sumaTotal_valor_porVolumen_data_SteelColumns.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porVolumen_data_SteelColumns.Count(); i++)
                {
                    desperdicio_SteelColumns += lista_sumaTotal_valor_porVolumen_data_SteelColumns[i];// para Concreto
                }
                foreach (Element sc in columns)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Material");
                    }
                    Dictionary<string, string> data = data_SteelColumns;
                    if ((pamType.AsValueString() == data["Código"]))
                    {
                        lista_desperdicios.Add(desperdicio_SteelColumns);
                        break;
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_forjado35["Código"]))
                    {
                        Dictionary<string, string> data = data_forjado35;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_forjado35.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_forjado35.Count(); i++)
                {
                    desperdicio_forjado35 += lista_sumaTotal_valor_porArea_forjado35[i];// para Forjados
                }
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_forjado35["Código"]))
                    {
                        lista_desperdicios.Add(desperdicio_forjado35);
                        break;
                    }
                }
            }

            if (strFoundation.Count() != 0)
            {
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_foundation["Código"]))
                    {
                        Dictionary<string, string> data = data_foundation;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_foundation.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_foundation.Count(); i++)
                {
                    desperdicio_foundation += lista_sumaTotal_valor_porArea_foundation[i];
                }
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_foundation["Código"]))
                    {
                        lista_desperdicios.Add(desperdicio_foundation);
                        break;
                    }
                }
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_CollaboratingSheetMetal["Código"]))
                    {
                        Dictionary<string, string> data = data_CollaboratingSheetMetal;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_CollaboratingSheetMetal.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_CollaboratingSheetMetal.Count(); i++)
                {
                    desperdicio_CollaboratingSheetMetal += lista_sumaTotal_valor_porArea_CollaboratingSheetMetal[i];// para Forjados
                }
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_CollaboratingSheetMetal["Código"]))
                    {
                        lista_desperdicios.Add(desperdicio_CollaboratingSheetMetal);
                        break;
                    }
                }
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Steelbeam["Código"]))
                    {
                        Dictionary<string, string> data = data_Steelbeam;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_Steelbeam.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Steelbeam.Count(); i++)
                {
                    desperdicio_Steelbeam += lista_sumaTotal_valor_porArea_Steelbeam[i];
                }
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Steelbeam["Código"]))
                    {
                        lista_desperdicios.Add(desperdicio_Steelbeam);
                        break;
                    }
                }
            }

            StepLog.Write(commandData, "GetListDesperdicio Finish");

            return lista_desperdicios;
        }

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
            ComandosEntrada(uiApp, uidoc);

            #endregion Comandos entrada

            #region Dictionarios

            Dictionary<string, string> data_forjado = Dictionary.Get("data_forjado");
            Dictionary<string, string> data_pilar_hormigon = Dictionary.Get("data_pilar_hormigon");
            Dictionary<string, string> data_floors_concreto = Dictionary.Get("data_floors_concreto");
            Dictionary<string, string> data_Cimentaciones = Dictionary.Get("data_Cimentaciones");
            Dictionary<string, string> data_ConcretoDeck = Dictionary.Get("data_ConcretoDeck");
            Dictionary<string, string> data_Droppedbeam = Dictionary.Get("data_Droppedbeam");
            Dictionary<string, string> data_Beamembbeded = Dictionary.Get("data_Beamembbeded");
            Dictionary<string, string> data_ConcreteInclinedSlab = Dictionary.Get("data_ConcreteInclinedSlab");
            Dictionary<string, string> data_walls = Dictionary.Get("data_walls");
            Dictionary<string, string> data_SteelColumns = Dictionary.Get("data_SteelColumns");
            Dictionary<string, string> data_forjado35 = Dictionary.Get("data_forjado35");

            Dictionary<string, string> data_foundation = Dictionary.Get("data_foundation");
            Dictionary<string, string> data_CollaboratingSheetMetal = Dictionary.Get("data_CollaboratingSheetMetal");
            Dictionary<string, string> data_Steelbeam = Dictionary.Get("data_Steelbeam");

            #endregion Dictionarios

            #region datos iniciales

            List<double> lista_sumaTotal_valor_porArea_Forjado = new List<double>();
            List<double> lista_sumaTotal_valor_porVolumen_PilarConcreto = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Concreto = new List<double>();
            List<double> lista_sumaTotal_valor_porVolumen_Cimentaciones = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_ConcretoDeck = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Droppedbeam = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Beamembbededm = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_ConcreteInclinedSlab = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_walls_Concrete = new List<double>();
            List<double> lista_sumaTotal_valor_porVolumen_data_SteelColumns = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_forjado35 = new List<double>();

            List<double> lista_sumaTotal_valor_porArea_foundation = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_CollaboratingSheetMetal = new List<double>();
            List<double> lista_sumaTotal_valor_porArea_Steelbeam = new List<double>();

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
            double desperdicio_Beamembbededm = 0;
            double desperdicio_ConcreteInclinedSlab = 0;
            double desperdicio_walls_Concrete = 0;
            double desperdicio_SteelColumns = 0;
            double desperdicio_forjado35 = 0;

            double desperdicio_foundation = 0;
            double desperdicio_CollaboratingSheetMetal = 0;
            double desperdicio_Steelbeam = 0;

            #endregion desperdicios

            StepLog.Write(commandData, "GetDesperdicioTotal Start");

            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_forjado["Código"]))
                    {
                        Dictionary<string, string> data = data_forjado;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_Forjado.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Forjado.Count(); i++)
                {
                    desperdicioForjado += lista_sumaTotal_valor_porArea_Forjado[i];// para Forjados
                }
                lista_desperdicios.Add(desperdicioForjado);
            }
            if (structuralColumns.Count() != 0)
            {
                foreach (Element sc in structuralColumns)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_pilar_hormigon["Código"]))
                    {
                        Dictionary<string, string> data = data_pilar_hormigon;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porVolumen_PilarConcreto.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porVolumen_PilarConcreto.Count(); i++)
                {
                    desperdicioPilarHormigon += lista_sumaTotal_valor_porVolumen_PilarConcreto[i];// para pilar de hormigon
                }
                lista_desperdicios.Add(desperdicioPilarHormigon);
            }
            if ((floors.Count() != 0) || (strFoundation.Count() != 0))//m3 volumen
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        Dictionary<string, string> data = data_floors_concreto;
                        double sumaTotal_valor_porArea = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_Concreto.Add(sumaTotal_valor_porArea);
                    }
                }
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        Dictionary<string, string> data = data_floors_concreto;
                        double sumaTotal_valor_porArea = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_Concreto.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Concreto.Count(); i++)
                {
                    desperdicioConcreto += lista_sumaTotal_valor_porArea_Concreto[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicioConcreto);
            }
            if ((strFoundation.Count() != 0) || (walls.Count() != 0))
            {
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_Cimentaciones;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porVolumen_Cimentaciones.Add(sumaTotal_valor_porVolumen);
                    }
                }
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_Cimentaciones;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porVolumen_Cimentaciones.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porVolumen_Cimentaciones.Count(); i++)
                {
                    desperdicioCimentacion += lista_sumaTotal_valor_porVolumen_Cimentaciones[i];
                }
                lista_desperdicios.Add(desperdicioCimentacion);
            }
            if (floors.Count() != 0)//m3 volumen
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcretoDeck["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcretoDeck;
                        double sumaTotal_valor_porArea = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_ConcretoDeck.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcretoDeck.Count(); i++)
                {
                    desperdicioConcretoDeck += lista_sumaTotal_valor_porArea_ConcretoDeck[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicioConcretoDeck);
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Droppedbeam["Código"]))
                    {
                        Dictionary<string, string> data = data_Droppedbeam;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_Droppedbeam.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Droppedbeam.Count(); i++)
                {
                    desperdicio_Droppedbeam += lista_sumaTotal_valor_porArea_Droppedbeam[i];
                }
                lista_desperdicios.Add(desperdicio_Droppedbeam);
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Beamembbeded["Código"]))
                    {
                        Dictionary<string, string> data = data_Beamembbeded;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_Beamembbededm.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Beamembbededm.Count(); i++)
                {
                    desperdicio_Beamembbededm += lista_sumaTotal_valor_porArea_Beamembbededm[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_Beamembbededm);
            }
            if (floors.Count() != 0)//m3 volumen
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteInclinedSlab;
                        double sumaTotal_valor_porArea = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_ConcreteInclinedSlab.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcreteInclinedSlab.Count(); i++)
                {
                    desperdicio_ConcreteInclinedSlab += lista_sumaTotal_valor_porArea_ConcreteInclinedSlab[i];
                }
                lista_desperdicios.Add(desperdicio_ConcreteInclinedSlab);
            }
            if (walls.Count() != 0)
            {
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_walls["Código"]))
                    {
                        Dictionary<string, string> data = data_walls;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_walls_Concrete.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_walls_Concrete.Count(); i++)
                {
                    desperdicio_walls_Concrete += lista_sumaTotal_valor_porArea_walls_Concrete[i];
                }
                lista_desperdicios.Add(desperdicio_walls_Concrete);
            }
            if (columns.Count() != 0)
            {
                foreach (Element sc in columns)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Material");
                    }
                    if ((pamType.AsValueString() == data_SteelColumns["Código"]))
                    {
                        Dictionary<string, string> data = data_SteelColumns;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get_SteelColumnSpecialCommand(commandData, data, sc);
                        lista_sumaTotal_valor_porVolumen_data_SteelColumns.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porVolumen_data_SteelColumns.Count(); i++)
                {
                    desperdicio_SteelColumns += lista_sumaTotal_valor_porVolumen_data_SteelColumns[i];
                }
                lista_desperdicios.Add(desperdicio_SteelColumns);
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_forjado35["Código"]))
                    {
                        Dictionary<string, string> data = data_forjado35;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_forjado35.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_forjado35.Count(); i++)
                {
                    desperdicio_forjado35 += lista_sumaTotal_valor_porArea_forjado35[i];// para Forjados
                }
                lista_desperdicios.Add(desperdicio_forjado35);
            }

            if (strFoundation.Count() != 0)
            {
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_foundation["Código"]))
                    {
                        Dictionary<string, string> data = data_foundation;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_foundation.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_foundation.Count(); i++)
                {
                    desperdicio_foundation += lista_sumaTotal_valor_porArea_foundation[i];
                }
                lista_desperdicios.Add(desperdicio_foundation);
            }
            if (floors.Count() != 0)
            {
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_CollaboratingSheetMetal["Código"]))
                    {
                        Dictionary<string, string> data = data_CollaboratingSheetMetal;
                        double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
                        lista_sumaTotal_valor_porArea_CollaboratingSheetMetal.Add(sumaTotal_valor_porArea);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_CollaboratingSheetMetal.Count(); i++)
                {
                    desperdicio_CollaboratingSheetMetal += lista_sumaTotal_valor_porArea_CollaboratingSheetMetal[i];// para Forjados
                }
                lista_desperdicios.Add(desperdicio_CollaboratingSheetMetal);
            }
            if (strFramming.Count() != 0)
            {
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Steelbeam["Código"]))
                    {
                        Dictionary<string, string> data = data_Steelbeam;
                        double sumaTotal_valor_porVolumen = CalcVolume.Get(commandData, data, sc);
                        lista_sumaTotal_valor_porArea_Steelbeam.Add(sumaTotal_valor_porVolumen);
                    }
                }
                for (int i = 0; i < lista_sumaTotal_valor_porArea_Steelbeam.Count(); i++)
                {
                    desperdicio_Steelbeam += lista_sumaTotal_valor_porArea_Steelbeam[i];// para Concreto
                }
                lista_desperdicios.Add(desperdicio_Steelbeam);
            }

            #region Desperdicio Total

            // Desperdicio total
            double desperdicioTotal = 0;
            for (int i = 0; i < lista_desperdicios.Count(); i++)
            {
                desperdicioTotal += lista_desperdicios[i];
            }

            #endregion Desperdicio Total

            StepLog.Write(commandData, "GetDesperdicioTotal Finish");

            return desperdicioTotal;
        }

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
            ComandosEntrada(uiApp, uidoc);

            #endregion Comandos entrada

            #region Dictionarios

            Dictionary<string, string> data_forjado = Dictionary.Get("data_forjado");
            Dictionary<string, string> data_pilar_hormigon = Dictionary.Get("data_pilar_hormigon");
            Dictionary<string, string> data_floors_concreto = Dictionary.Get("data_floors_concreto");
            Dictionary<string, string> data_Cimentaciones = Dictionary.Get("data_Cimentaciones");
            Dictionary<string, string> data_ConcretoDeck = Dictionary.Get("data_ConcretoDeck");
            Dictionary<string, string> data_Droppedbeam = Dictionary.Get("data_Droppedbeam");
            Dictionary<string, string> data_Beamembbeded = Dictionary.Get("data_Beamembbeded");
            Dictionary<string, string> data_ConcreteInclinedSlab = Dictionary.Get("data_ConcreteInclinedSlab");
            Dictionary<string, string> data_walls = Dictionary.Get("data_walls");
            Dictionary<string, string> data_SteelColumns = Dictionary.Get("data_SteelColumns");
            Dictionary<string, string> data_forjado35 = Dictionary.Get("data_forjado35");

            Dictionary<string, string> data_foundation = Dictionary.Get("data_foundation");
            Dictionary<string, string> data_CollaboratingSheetMetal = Dictionary.Get("data_CollaboratingSheetMetal");
            Dictionary<string, string> data_Steelbeam = Dictionary.Get("data_Steelbeam");

            #endregion Dictionarios

            StepLog.Write(commandData, "GetListValoresSeparaadaPorDataElemento Start");

            List<List<List<double>>> salida = new List<List<List<double>>>();

            List<List<double>> salida_data_forjado = new List<List<double>>();
            List<List<double>> salida_data_pilar_hormigon = new List<List<double>>();
            List<List<double>> salida_data_floors_concreto = new List<List<double>>();
            List<List<double>> salida_data_Cimentaciones = new List<List<double>>();
            List<List<double>> salida_data_ConcretoDeck = new List<List<double>>();
            List<List<double>> salida_data_Droppedbeam = new List<List<double>>();
            List<List<double>> salida_data_Beamembbeded = new List<List<double>>();
            List<List<double>> salida_data_ConcreteInclinedSlab = new List<List<double>>();
            List<List<double>> salida_data_walls = new List<List<double>>();
            List<List<double>> salida_data_SteelColumns = new List<List<double>>();
            List<List<double>> salida_data_forjado35 = new List<List<double>>();

            List<List<double>> salida_data_foundation = new List<List<double>>();
            List<List<double>> salida_data_CollaboratingSheetMetal = new List<List<double>>();
            List<List<double>> salida_data_Steelbeam = new List<List<double>>();

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
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
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
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_forjado["Código"]))
                    {
                        salida.Add(salida_data_forjado);
                        break;
                    }
                }
            }
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
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_pilar_hormigon["Código"]))
                    {
                        Dictionary<string, string> data = data_pilar_hormigon;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
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

                foreach (Element sc in structuralColumns)//todos los elementos del modelo entero
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_pilar_hormigon["Código"]))
                    {
                        salida.Add(salida_data_pilar_hormigon);
                        break;
                    }
                }
            }
            if ((floors.Count() != 0) || (strFoundation.Count() != 0))//m3 volumen
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
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        Dictionary<string, string> data = data_floors_concreto;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        Dictionary<string, string> data = data_floors_concreto;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
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
                bool pasoPorAqui = false;
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        salida.Add(salida_data_floors_concreto);
                        pasoPorAqui = true;
                        break;
                    }
                }
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_floors_concreto["Código"]))
                    {
                        if (!pasoPorAqui)
                        {
                            salida.Add(salida_data_floors_concreto);
                            break;
                        }
                    }
                }
            }
            if ((strFoundation.Count() != 0) || (walls.Count() != 0))
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
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_Cimentaciones;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        Dictionary<string, string> data = data_Cimentaciones;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
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
                bool pasoPorAqui = false;
                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                    {
                        salida.Add(salida_data_Cimentaciones);
                        pasoPorAqui = true;
                        break;
                    }
                }
                if (!pasoPorAqui)
                {
                    foreach (Element sc in walls)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        Parameter pamType = type.LookupParameter("Material estructural");
                        if (pamType == null)
                        {
                            pamType = type.LookupParameter("Structural Material");
                        }
                        if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
                        {
                            salida.Add(salida_data_Cimentaciones);
                            break;
                        }
                    }
                }
            }
            if (floors.Count() != 0)//m3 volumen
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
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcretoDeck["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcretoDeck;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
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
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcretoDeck["Código"]))
                    {
                        salida.Add(salida_data_ConcretoDeck);
                        break;
                    }
                }
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
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Droppedbeam["Código"]))
                    {
                        Dictionary<string, string> data = data_Droppedbeam;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
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
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Droppedbeam["Código"]))
                    {
                        salida.Add(salida_data_Droppedbeam);
                        break;
                    }
                }
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
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Beamembbeded["Código"]))
                    {
                        Dictionary<string, string> data = data_Beamembbeded;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
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
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Beamembbeded["Código"]))
                    {
                        salida.Add(salida_data_Beamembbeded);
                        break;
                    }
                }
            }
            if (floors.Count() != 0)//m3 volumen
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
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
                    {
                        Dictionary<string, string> data = data_ConcreteInclinedSlab;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
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
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
                    {
                        salida.Add(salida_data_ConcreteInclinedSlab);
                        break;
                    }
                }
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
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_walls["Código"]))
                    {
                        Dictionary<string, string> data = data_walls;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
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
                foreach (Element sc in walls)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_walls["Código"]))
                    {
                        salida.Add(salida_data_walls);
                        break;
                    }
                }
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
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Material");
                    }
                    Dictionary<string, string> data = data_SteelColumns;
                    if ((pamType.AsValueString() == data["Código"]))
                    {
                        lista2_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, "11"));
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
                foreach (Element sc in columns)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Material");
                    }
                    Dictionary<string, string> data = data_SteelColumns;
                    if ((pamType.AsValueString() == data["Código"]))
                    {
                        salida.Add(salida_data_SteelColumns);
                        break;
                    }
                }
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
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_forjado35["Código"]))
                    {
                        Dictionary<string, string> data = data_forjado35;
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
                salida_data_forjado35.Add(lista2_valor);//1
                salida_data_forjado35.Add(lista3_valor);//2
                salida_data_forjado35.Add(lista4_valor);//3
                salida_data_forjado35.Add(lista5_valor);//4
                salida_data_forjado35.Add(lista6_valor);//5
                salida_data_forjado35.Add(lista7_valor);//6
                salida_data_forjado35.Add(lista8_valor);//7
                salida_data_forjado35.Add(lista9_valor);//8
                salida_data_forjado35.Add(lista10_valor);//9
                salida_data_forjado35.Add(lista11_valor);//10
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_forjado35["Código"]))
                    {
                        salida.Add(salida_data_forjado35);
                        break;
                    }
                }
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
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_foundation["Código"]))
                    {
                        Dictionary<string, string> data = data_foundation;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }

                salida_data_foundation.Add(lista2_valor);
                salida_data_foundation.Add(lista3_valor);
                salida_data_foundation.Add(lista4_valor);
                salida_data_foundation.Add(lista5_valor);
                salida_data_foundation.Add(lista6_valor);
                salida_data_foundation.Add(lista7_valor);
                salida_data_foundation.Add(lista8_valor);
                salida_data_foundation.Add(lista9_valor);
                salida_data_foundation.Add(lista10_valor);
                salida_data_foundation.Add(lista11_valor);

                foreach (Element sc in strFoundation)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_foundation["Código"]))
                    {
                        salida.Add(salida_data_foundation);
                        break;
                    }
                }
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
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_CollaboratingSheetMetal["Código"]))
                    {
                        Dictionary<string, string> data = data_CollaboratingSheetMetal;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
                salida_data_CollaboratingSheetMetal.Add(lista2_valor);
                salida_data_CollaboratingSheetMetal.Add(lista3_valor);
                salida_data_CollaboratingSheetMetal.Add(lista4_valor);
                salida_data_CollaboratingSheetMetal.Add(lista5_valor);
                salida_data_CollaboratingSheetMetal.Add(lista6_valor);
                salida_data_CollaboratingSheetMetal.Add(lista7_valor);
                salida_data_CollaboratingSheetMetal.Add(lista8_valor);
                salida_data_CollaboratingSheetMetal.Add(lista9_valor);
                salida_data_CollaboratingSheetMetal.Add(lista10_valor);
                salida_data_CollaboratingSheetMetal.Add(lista11_valor);
                foreach (Element sc in floors)
                {
                    ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                    Parameter pamType = type.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_CollaboratingSheetMetal["Código"]))
                    {
                        salida.Add(salida_data_CollaboratingSheetMetal);
                        break;
                    }
                }
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
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Steelbeam["Código"]))
                    {
                        Dictionary<string, string> data = data_Steelbeam;
                        lista2_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "2"));
                        lista3_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "3"));
                        lista4_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "4"));
                        lista5_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "5"));
                        lista6_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "6"));
                        lista7_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "7"));
                        lista8_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "8"));
                        lista9_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "9"));
                        lista10_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "10"));
                        lista11_valor.Add(CalcVolume.GetByValueOfKey(commandData, data, sc, "11"));
                    }
                }
                salida_data_Steelbeam.Add(lista2_valor);
                salida_data_Steelbeam.Add(lista3_valor);
                salida_data_Steelbeam.Add(lista4_valor);
                salida_data_Steelbeam.Add(lista5_valor);
                salida_data_Steelbeam.Add(lista6_valor);
                salida_data_Steelbeam.Add(lista7_valor);
                salida_data_Steelbeam.Add(lista8_valor);
                salida_data_Steelbeam.Add(lista9_valor);
                salida_data_Steelbeam.Add(lista10_valor);
                salida_data_Steelbeam.Add(lista11_valor);
                foreach (Element sc in strFramming)
                {
                    Parameter pamType = sc.LookupParameter("Material estructural");
                    if (pamType == null)
                    {
                        pamType = sc.LookupParameter("Structural Material");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Material estructural");
                    }
                    if (pamType == null)
                    {
                        ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
                        pamType = type.LookupParameter("Structural Material");
                    }
                    if ((pamType.AsValueString() == data_Steelbeam["Código"]))
                    {
                        salida.Add(salida_data_Steelbeam);
                        break;
                    }
                }
            }

            StepLog.Write(commandData, "GetListValoresSeparaadaPorDataElemento Finish");

            return salida;
        }
    }
}