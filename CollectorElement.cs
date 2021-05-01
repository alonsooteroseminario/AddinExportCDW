using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;

namespace AddinExportCDW
{
    public static class CollectorElement
    {
        #region Get all model elements

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

        #endregion Get all model elements

        public static IList<Element> Get(Document doc, View activeView, string categoryElement)
        {
            #region Colectores de Elementos

            ElementClassFilter elemFilter_floor = new ElementClassFilter(typeof(Floor));
            ElementClassFilter elemFilter_familyInstance = new ElementClassFilter(typeof(FamilyInstance));
            ElementClassFilter elemFilter_walls = new ElementClassFilter(typeof(Wall));
            //Forjado y Concreto
            ElementCategoryFilter Categoryfilter_floors = new ElementCategoryFilter(BuiltInCategory.OST_Floors);
            //Pilar Hormigon
            ElementCategoryFilter Categoryfilter_strColumns = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            //Cimentaciones
            ElementCategoryFilter Categoryfilter_strFoundation = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFoundation);
            // Structural framing
            ElementCategoryFilter Categoryfilter_strFramming = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            // walls
            ElementCategoryFilter Categoryfilter_walls = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            //Pilar
            ElementCategoryFilter Categoryfilter_Columns = new ElementCategoryFilter(BuiltInCategory.OST_Columns);

            LogicalAndFilter DUInstancesFilter_floors = new LogicalAndFilter(elemFilter_floor, Categoryfilter_floors);
            LogicalAndFilter DUInstancesFilter_strColumns = new LogicalAndFilter(elemFilter_familyInstance, Categoryfilter_strColumns);
            LogicalAndFilter DUInstancesFilter_strFoundation = new LogicalAndFilter(elemFilter_floor, Categoryfilter_strFoundation);
            LogicalAndFilter DUInstancesFilter_strFramming = new LogicalAndFilter(elemFilter_familyInstance, Categoryfilter_strFramming);
            LogicalAndFilter DUInstancesFilter_walls = new LogicalAndFilter(elemFilter_walls, Categoryfilter_walls);
            LogicalAndFilter DUInstancesFilter_Columns = new LogicalAndFilter(elemFilter_familyInstance, Categoryfilter_Columns);

            //Forjado y Concreto
            FilteredElementCollector DUcoll = new FilteredElementCollector(doc, activeView.Id);
            IList<Element> floors = DUcoll.WherePasses(DUInstancesFilter_floors).ToElements();
            //Pilar Hormigon
            FilteredElementCollector DUcoll2 = new FilteredElementCollector(doc, activeView.Id);
            IList<Element> structuralColumns = DUcoll2.WherePasses(DUInstancesFilter_strColumns).ToElements();
            //Cimentaciones
            FilteredElementCollector DUcoll3 = new FilteredElementCollector(doc, activeView.Id);
            IList<Element> strFoundation = DUcoll3.WherePasses(DUInstancesFilter_strFoundation).ToElements();
            // Structural framming
            FilteredElementCollector DUcoll4 = new FilteredElementCollector(doc, activeView.Id);
            IList<Element> strFramming = DUcoll4.WherePasses(DUInstancesFilter_strFramming).ToElements();
            //  walls
            FilteredElementCollector DUcoll5 = new FilteredElementCollector(doc, activeView.Id);
            IList<Element> walls = DUcoll5.WherePasses(DUInstancesFilter_walls).ToElements();
            //Pilar
            FilteredElementCollector DUcoll6 = new FilteredElementCollector(doc, activeView.Id);
            IList<Element> columns = DUcoll6.WherePasses(DUInstancesFilter_Columns).ToElements();

            #endregion Colectores de Elementos

            if (categoryElement == "floors")
            {
                return floors;
            }
            if (categoryElement == "structuralColumns")
            {
                return structuralColumns;
            }
            if (categoryElement == "strFoundation")
            {
                return strFoundation;
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
            Document doc = uiApp.ActiveUIDocument.Document;
            Application app = uiApp.Application;
            // Get Active View
            View activeView = uidoc.ActiveView;
            string ruta = App.ExecutingAssemblyPath;

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
    }
}