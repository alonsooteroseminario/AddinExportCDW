using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddinExportCDW
{
    public static class CollectorElement
    {

        #region Get all model elements
        /// <summary>
        /// Return all model elements, cf.
        /// http://forums.autodesk.com/t5/revit-api/traverse-all-model-elements-in-a-project-top-down-approach/m-p/5815247
        /// </summary>
        static IEnumerable<Element> GetAllModelElements(
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

        static IList<Element> GetFamilyInstanceModelElements(
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
		#endregion // Get all model elements

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

			LogicalAndFilter DUInstancesFilter_floors = new LogicalAndFilter(elemFilter_floor, Categoryfilter_floors);
			LogicalAndFilter DUInstancesFilter_strColumns = new LogicalAndFilter(elemFilter_familyInstance, Categoryfilter_strColumns);
			LogicalAndFilter DUInstancesFilter_strFoundation = new LogicalAndFilter(elemFilter_floor, Categoryfilter_strFoundation);
			LogicalAndFilter DUInstancesFilter_strFramming = new LogicalAndFilter(elemFilter_familyInstance, Categoryfilter_strFramming);
			LogicalAndFilter DUInstancesFilter_walls = new LogicalAndFilter(elemFilter_walls, Categoryfilter_walls);

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


			// Todos los Elementos del Modelo
			IEnumerable<Element> elementosProyecto = GetAllModelElements(doc);
			// Todos las Familias de instacia del proyecto
			IEnumerable<Element> familiasInstanciaProyecto = GetFamilyInstanceModelElements(doc);

			#endregion

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

			return null;
		}


	}
}
