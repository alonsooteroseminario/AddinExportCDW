using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;

namespace AddinExportCDW
{
    public static class CollectorElement
    {
        /// <summary>
        /// Return all model elements, cf.
        /// http://forums.autodesk.com/t5/revit-api/traverse-all-model-elements-in-a-project-top-down-approach/m-p/5815247
        /// </summary>
        public static IEnumerable<Element> GetAllModelElements(
          Document doc)
        {
            Options opt = new Options();

            return new FilteredElementCollector(doc)
              .WhereElementIsNotElementType()
              .WhereElementIsViewIndependent()
              .Where<Element>(e
               => null != e.Category
               && null != e.get_Geometry(opt));
        }

        public static IList<Element> GetFamilyInstanceModelElements(
          Document doc)
        {
            ElementClassFilter familyInstanceFilter
              = new ElementClassFilter(
                typeof(FamilyInstance));

            FilteredElementCollector familyInstanceCollector
              = new FilteredElementCollector(doc);

            IList<Element> elementsCollection
              = familyInstanceCollector.WherePasses(
                familyInstanceFilter).ToElements();

            IList<Element> modelElements
              = new List<Element>();

            foreach (Element e in elementsCollection)
            {
                if ((null != e.Category)
                && (null != e.LevelId)
                && (null != e.get_Geometry(new Options()))
                )
                {
                    modelElements.Add(e);
                }
            }
            return modelElements;
        }

        public static IList<Element> Get(Document doc, string categoryElement)
        {
            #region Colectores de Elementos

            ElementClassFilter elemFilter_floor = new ElementClassFilter(typeof(Floor));
            ElementClassFilter elemFilter_familyInstance = new ElementClassFilter(typeof(FamilyInstance));
            ElementClassFilter elemFilter_walls = new ElementClassFilter(typeof(Wall));
            //ElementClassFilter elemFilter_foundations = new ElementClassFilter(typeof(WallFoundation));

            ElementCategoryFilter Categoryfilter_floors = new ElementCategoryFilter(BuiltInCategory.OST_Floors);
            ElementCategoryFilter Categoryfilter_strColumns = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            ElementCategoryFilter Categoryfilter_strFoundation = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFoundation);
            ElementCategoryFilter Categoryfilter_strFramming = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            ElementCategoryFilter Categoryfilter_walls = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            ElementCategoryFilter Categoryfilter_Columns = new ElementCategoryFilter(BuiltInCategory.OST_Columns);

            LogicalAndFilter DUInstancesFilter_floors = new LogicalAndFilter(elemFilter_floor, Categoryfilter_floors);
            LogicalAndFilter DUInstancesFilter_strColumns = new LogicalAndFilter(elemFilter_familyInstance, Categoryfilter_strColumns);
            LogicalAndFilter DUInstancesFilter_strFoundation = new LogicalAndFilter(elemFilter_floor, Categoryfilter_strFoundation);
            LogicalAndFilter DUInstancesFilter_strFoundation_FamilyInstance = new LogicalAndFilter(elemFilter_familyInstance, Categoryfilter_strFoundation);
            LogicalAndFilter DUInstancesFilter_strFramming = new LogicalAndFilter(elemFilter_familyInstance, Categoryfilter_strFramming);
            LogicalAndFilter DUInstancesFilter_walls = new LogicalAndFilter(elemFilter_walls, Categoryfilter_walls);
            LogicalAndFilter DUInstancesFilter_Columns = new LogicalAndFilter(elemFilter_familyInstance, Categoryfilter_Columns);

            FilteredElementCollector DUcoll = new FilteredElementCollector(doc);
            IList<Element> floors = DUcoll.WherePasses(DUInstancesFilter_floors).ToElements();
            FilteredElementCollector DUcoll2 = new FilteredElementCollector(doc);
            IList<Element> strColumns = DUcoll2.WherePasses(DUInstancesFilter_strColumns).ToElements();
            FilteredElementCollector DUcoll3 = new FilteredElementCollector(doc);
            IList<Element> strFoundation = DUcoll3.WherePasses(DUInstancesFilter_strFoundation).ToElements();
            FilteredElementCollector DUcoll3_FamilyInstance = new FilteredElementCollector(doc);
            IList<Element> strFoundation_FamilyInstance = DUcoll3_FamilyInstance.WherePasses(DUInstancesFilter_strFoundation_FamilyInstance).ToElements();
            FilteredElementCollector DUcoll4 = new FilteredElementCollector(doc);
            IList<Element> strFramming = DUcoll4.WherePasses(DUInstancesFilter_strFramming).ToElements();
            FilteredElementCollector DUcoll5 = new FilteredElementCollector(doc);
            IList<Element> walls = DUcoll5.WherePasses(DUInstancesFilter_walls).ToElements();
            FilteredElementCollector DUcoll6 = new FilteredElementCollector(doc);
            IList<Element> columns = DUcoll6.WherePasses(DUInstancesFilter_Columns).ToElements();

            #endregion Colectores de Elementos

            //List<Element> floors = new List<Element>();
            //List<Element> strFoundation = new List<Element>();
            //List<Element> strColumns = new List<Element>();
            //List<Element> strFramming = new List<Element>();
            //List<Element> walls = new List<Element>();
            //List<Element> columns = new List<Element>();

            if (categoryElement == "floors")
            {
                return floors;
            }
            if (categoryElement == "structuralColumns")
            {
                return strColumns;
            }
            if (categoryElement == "strFoundation")
            {
                return strFoundation;
            }
            if (categoryElement == "strFoundation_FamilyInstance")
            {
                return strFoundation_FamilyInstance;
            }
            if (categoryElement == "strFramming")
            {
                return strFramming;
            }
            if (categoryElement == "walls")
            {
                return walls;
            }
            if (categoryElement == "columns")
            {
                return columns;
            }
            return null;
        }

        public static List<Element> FiltrarElementosCDW(ExternalCommandData commandData,
                                    IList<Element> listaElementos)
        {
            #region Comandos entrada

            //Get application and document objects
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            ComandoEntrada(uiApp, uidoc);

            #endregion Comandos entrada

            List<Element> salida = new List<Element>();

            foreach (Element sc in listaElementos)
            {
                Category category = sc.Category;
                BuiltInCategory builtCategory = (BuiltInCategory)category.Id.IntegerValue;

                if (builtCategory == BuiltInCategory.OST_Floors)
                {
                    salida.Add(sc);
                }
                if (builtCategory == BuiltInCategory.OST_Walls)
                {
                    salida.Add(sc);
                }
                if (builtCategory == BuiltInCategory.OST_StructuralColumns)
                {
                    salida.Add(sc);
                }
                if (builtCategory == BuiltInCategory.OST_StructuralFraming)
                {
                    salida.Add(sc);
                }
                if (builtCategory == BuiltInCategory.OST_StructuralFoundation)
                {
                    salida.Add(sc);
                }
                if (builtCategory == BuiltInCategory.OST_Columns)
                {
                    salida.Add(sc);
                }
            }
            return salida;
        }

        private static void ComandoEntrada(UIApplication uiApp, UIDocument uidoc)
        {
            _ = uiApp.ActiveUIDocument.Document;
            _ = uiApp.Application;
            _ = uidoc.ActiveView;
        }
    }
}