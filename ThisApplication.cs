using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using forms = System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using AddinExportCDW;

namespace AddinExportCDW
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    class ThisApplication : IExternalCommand
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


			List<Dictionary<string, string>> lista_Dictionarios = new List<Dictionary<string, string>>();

			// 10 elementos
			#region Dictionarios
			Dictionary<string, string> data_forjado = new Dictionary<string, string>(){
				{"Structural element", "Forjado - 30 cm"},
				{"Código", "05WCH80110N"},
				{"07 07 01 - aqueous washing liquids", "0,000008"},
				{"15 01 02 - plastic packaging", "0,000554"},//
				{"15 01 03 - wooden packaging", "0,004619"},//
				{"15 01 04 - metallic packaging", "0,000468"},
				{"15 01 06 - mixed packaging", "0,000056"},
				{"17 01 01 - concrete", "0,004070"},
				{"17 02 01 - wood", "0,001020"},//
				{"17 02 03 - plastic", "0,004385"},//
				{"17 04 05 - iron and steel", "0,000055"},
				{"17 09 04 - mixed", "0,000095"},
			};
			lista_Dictionarios.Add(data_forjado);
			Dictionary<string, string> data_pilar_hormigon = new Dictionary<string, string>(){
				{"Structural element", "300 x 300 mm"},
				{"Código", "05HRP80020"},
				{"07 07 01 - aqueous washing liquids", "0,000344"},
				{"15 01 02 - plastic packaging", "0"},//
				{"15 01 03 - wooden packaging", "0"},//
				{"15 01 04 - metallic packaging", "0,019147"},
				{"15 01 06 - mixed packaging", "0,000191"},
				{"17 01 01 - concrete", "0,022000"},
				{"17 02 01 - wood", "0"},//
				{"17 02 03 - plastic", "0"},//
				{"17 04 05 - iron and steel", "0,000990"},
				{"17 09 04 - mixed", "0,000233"},
			};
			lista_Dictionarios.Add(data_pilar_hormigon);
			Dictionary<string, string> data_floors_concreto = new Dictionary<string, string>(){
                {"Structural element", "Concrete ground slab"},
                {"Código", "03HRL80090"},//
                {"07 07 01 - aqueous washing liquids", "0"},
                {"15 01 02 - plastic packaging", "0"},
				{"15 01 03 - wooden packaging", "0"},
				{"15 01 04 - metallic packaging", "0"},
                {"15 01 06 - mixed packaging", "0"},
                {"17 01 01 - concrete", "0,022"},//
                {"17 02 01 - wood", "0"},
				{"17 02 03 - plastic", "0"},
				{"17 04 05 - iron and steel", "0,000050"},//
                {"17 09 04 - mixed", "0,000221"},//
            };
			lista_Dictionarios.Add(data_floors_concreto);
			Dictionary<string, string> data_Cimentaciones = new Dictionary<string, string>(){
				{"Structural element", "Losa de cimentación 650 mm"},
				{"Código", "03HRM80080"},//
                {"07 07 01 - aqueous washing liquids", "0,000017"},//
				{"15 01 02 - plastic packaging", "0"},
				{"15 01 03 - wooden packaging", "0"},
				{"15 01 04 - metallic packaging", "0,001018"},//
				{"15 01 06 - mixed packaging", "0,000010"},//
				{"17 01 01 - concrete", "0,022000"},//
                {"17 02 01 - wood", "0,003944"},//
				{"17 02 03 - plastic", "0"},
				{"17 04 05 - iron and steel", "0,000193"},//
                {"17 09 04 - mixed", "0,000262"},//
            };
			lista_Dictionarios.Add(data_Cimentaciones);
			Dictionary<string, string> data_ConcretoDeck = new Dictionary<string, string>(){
				{"Structural element", "Concrete deck "},//
				{"Código", "05HRL80020"},//
                {"07 07 01 - aqueous washing liquids", "0,000049"},//
				{"15 01 02 - plastic packaging", "0"},
				{"15 01 03 - wooden packaging", "0"},
				{"15 01 04 - metallic packaging", "0,002867"},//
				{"15 01 06 - mixed packaging", "0,000029"},//
				{"17 01 01 - concrete", "0,022000"},//
                {"17 02 01 - wood", "0,008330"},//
				{"17 02 03 - plastic", "0"},
				{"17 04 05 - iron and steel", "0,000378"},//
                {"17 09 04 - mixed", "0,000308"},//
            };
			lista_Dictionarios.Add(data_ConcretoDeck);
			Dictionary<string, string> data_Droppedbeam = new Dictionary<string, string>(){
				{"Structural element", "Dropped beam"},//
				{"Código", "05HRJ80110"},//
                {"07 07 01 - aqueous washing liquids", "0,000041"},//
				{"15 01 02 - plastic packaging", "0"},
				{"15 01 03 - wooden packaging", "0"},
				{"15 01 04 - metallic packaging", "0,003045"},//
				{"15 01 06 - mixed packaging", "0,000030"},//
				{"17 01 01 - concrete", "0,022000"},//
                {"17 02 01 - wood", "0,008893"},//
				{"17 02 03 - plastic", "0"},
				{"17 04 05 - iron and steel", "0,000078"},//
                {"17 09 04 - mixed", "0,000310"},//
            };
			lista_Dictionarios.Add(data_Droppedbeam);
			Dictionary<string, string> data_ConcreteSlab = new Dictionary<string, string>(){
				{"Structural element", "Concrete waffle slab"},//
				{"Código", "05HRJ80110"},
                {"07 07 01 - aqueous washing liquids", "0,000009"},//
				{"15 01 02 - plastic packaging", "0,000609"},//
				{"15 01 03 - wooden packaging", "0,005081"},//
				{"15 01 04 - metallic packaging", "0,000515"},//
				{"15 01 06 - mixed packaging", "0,000062"},//
				{"17 01 01 - concrete", "0,004477"},//
                {"17 02 01 - wood", "0,001122"},//
				{"17 02 03 - plastic", "0,004824"},//
				{"17 04 05 - iron and steel", "0,000061"},//
                {"17 09 04 - mixed", "0,000105"},//
            };
			lista_Dictionarios.Add(data_ConcreteSlab);
			Dictionary<string, string> data_Beamembbeded = new Dictionary<string, string>(){
				{"Structural element", "Beam embbeded floor"},//
				{"Código", "05HRJ80020"},//
				{"07 07 01 - aqueous washing liquids", "0,000039"},//
				{"15 01 02 - plastic packaging", "0"},
				{"15 01 03 - wooden packaging", "0"},
				{"15 01 04 - metallic packaging", "0,002907"},//
				{"15 01 06 - mixed packaging", "0,000029"},//
				{"17 01 01 - concrete", "0,022000"},//
                {"17 02 01 - wood", "0,008488"},//
				{"17 02 03 - plastic", "0"},
				{"17 04 05 - iron and steel", "0,000078"},//
                {"17 09 04 - mixed", "0,000306"},//
            };
			lista_Dictionarios.Add(data_Beamembbeded);
			Dictionary<string, string> data_ConcreteInclinedSlab = new Dictionary<string, string>(){
				{"Structural element", "Concrete inclined slab"},//
				{"Código", "05HRL80080"},//
				{"07 07 01 - aqueous washing liquids", "0,000066"},//
				{"15 01 02 - plastic packaging", "0"},
				{"15 01 03 - wooden packaging", "0"},
				{"15 01 04 - metallic packaging", "0,000001"},//
				{"15 01 06 - mixed packaging", "0,000000"},//
				{"17 01 01 - concrete", "0,022000"},//
                {"17 02 01 - wood", "0,011390"},//
				{"17 02 03 - plastic", "0"},
				{"17 04 05 - iron and steel", "0,000403"},//
                {"17 09 04 - mixed", "0,000339"},//
            };
			lista_Dictionarios.Add(data_ConcreteInclinedSlab);
			Dictionary<string, string> data_walls = new Dictionary<string, string>(){
				{"Structural element", "Concrete wall"},//
				{"Código", "05HRM80050"},//
				{"07 07 01 - aqueous washing liquids", "0,000344"},//
				{"15 01 02 - plastic packaging", "0"},
				{"15 01 03 - wooden packaging", "0"},
				{"15 01 04 - metallic packaging", "0,019147"},//
				{"15 01 06 - mixed packaging", "0,000191"},//
				{"17 01 01 - concrete", "0,022000"},//
                {"17 02 01 - wood", "0"},
				{"17 02 03 - plastic", "0"},
				{"17 04 05 - iron and steel", "0,000146"},//
                {"17 09 04 - mixed", "0,000225"},//
            };
			lista_Dictionarios.Add(data_walls);


			#endregion

			#region relacionar el nombre familia con el codigo del material para Filtrarlo

			// 10 elementos : Listas Filtradas
			List<Element> floors_Forjados = new List<Element>();
			List<Element> structuralColumns_PilarHormigon = new List<Element>();
			List<Element> floors_Concreto = new List<Element>();
			List<Element> strfoundation_Cimentaciones = new List<Element>();
			List<Element> floors_ConcretoDeck = new List<Element>();
			List<Element> strFramming_Droppedbeam = new List<Element>();
			List<Element> floors_ConcreteSlab = new List<Element>();
			List<Element> strFramming_Beamembbededm = new List<Element>();
			List<Element> floors_ConcreteInclinedSlab = new List<Element>();
			List<Element> walls_Concrete = new List<Element>();

			// 10 elementos : Aquí se filtran los Elementos con el Código
			foreach (Element sc in floors)
			{
				ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
				Parameter pamType = type.LookupParameter("Material estructural");
				if ( /*(f.Name.ToString() == data_forjado["Structural element"]) ||*/
					(pamType.AsValueString() == data_forjado["Código"]) )
				{
					floors_Forjados.Add(sc);
				}
			}
			foreach (Element sc in structuralColumns)
			{
				if ( /*(sc.Name.ToString() == data_pilar_hormigon["Structural element"]) ||*/
					(sc.LookupParameter("Material estructural").AsValueString() == data_pilar_hormigon["Código"]) )
				{
					structuralColumns_PilarHormigon.Add(sc);
				}
			}
			foreach (Element sc in floors)
			{
				ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
				Parameter pamType = type.LookupParameter("Material estructural");
				if ( /*(f.Name.ToString() == data_forjado["Structural element"]) ||*/
					(pamType.AsValueString() == data_floors_concreto["Código"]))
				{
					floors_Concreto.Add(sc);
				}
			}
            foreach (Element sc in strFoundation)
            {
				ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
				Parameter pamType = type.LookupParameter("Material estructural");
				if ( /*(f.Name.ToString() == data_forjado["Structural element"]) ||*/
					(pamType.AsValueString() == data_Cimentaciones["Código"]))
				{
					strfoundation_Cimentaciones.Add(sc);
				}
			}
            foreach (Element sc in floors)
            {
				ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
				Parameter pamType = type.LookupParameter("Material estructural");
				if ( /*(f.Name.ToString() == data_forjado["Structural element"]) ||*/
					(pamType.AsValueString() == data_ConcretoDeck["Código"]))
				{
					floors_ConcretoDeck.Add(sc);
				}
			}
            foreach (Element sc in strFramming)
            {
				if ( /*(sc.Name.ToString() == data_pilar_hormigon["Structural element"]) ||*/
					(sc.LookupParameter("Material estructural").AsValueString() == data_Droppedbeam["Código"]))
				{
					strFramming_Droppedbeam.Add(sc);
				}
			}
			foreach (Element sc in floors)
			{
				ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
				Parameter pamType = type.LookupParameter("Material estructural");
				if ( /*(f.Name.ToString() == data_forjado["Structural element"]) ||*/
					(pamType.AsValueString() == data_ConcreteSlab["Código"]))
				{
					floors_ConcreteSlab.Add(sc);
				}
			}
			foreach (Element sc in strFramming)
			{
				if ( /*(sc.Name.ToString() == data_pilar_hormigon["Structural element"]) ||*/
					(sc.LookupParameter("Material estructural").AsValueString() == data_Beamembbeded["Código"]))
				{
					strFramming_Beamembbededm.Add(sc);
				}
			}
			foreach (Element sc in floors)
			{
				ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
				Parameter pamType = type.LookupParameter("Material estructural");
				if ( /*(f.Name.ToString() == data_forjado["Structural element"]) ||*/
					(pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
				{
					floors_ConcreteInclinedSlab.Add(sc);
				}
			}
			foreach (Element sc in walls)
			{
				ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
				Parameter pamType = type.LookupParameter("Material estructural");
				if ( /*(f.Name.ToString() == data_forjado["Structural element"]) ||*/
					(pamType.AsValueString() == data_walls["Código"]))
				{
					walls_Concrete.Add(sc);
				}
			}

			#region Obtener Materiales (no se usa)

			//FilteredElementCollector collector = new FilteredElementCollector(doc).OfClass(typeof(Material));
			//IEnumerable<Material> materialsEnum = collector.ToElements().Cast<Material>();

			//// 			TaskDialog.Show("materiales", materialsEnum.Count().ToString());

			//var materialReturn1 = from materialElement in materialsEnum where materialElement.Name == codigo1 select materialElement;
			//var materialReturn2 = from materialElement in materialsEnum where materialElement.Name == codigo2 select materialElement;

			//			TaskDialog.Show("material", materialReturn1.First().Name.ToString() + Environment.NewLine +
			//			               				materialReturn2.First().Name.ToString());

			// Obtener el material de los elementos deberian ser 2 materiales

			#endregion

			#endregion

			#region Obtener Areas y Volumenes

			// 10 elementos : Area acumulada por cada Elemento con Código
			double areaAcumulada_Forjados = 0;
			double volumenAcumulado_PilarHormigon = 0;
			double areaAcumulada_Concreto = 0;
			double volumenAcumulado_Cimentaciones = 0;
			double areaAcumulada_ConcretoDeck = 0;
			double volumenAcumulado_Droppedbeam = 0;
			double areaAcumulada_ConcreteSlab = 0;
			double volumenAcumulado_Beamembbededm = 0;
			double areaAcumulada_ConcreteInclinedSla = 0;
			double volumenAcumulado_walls_Concrete = 0;


			foreach (Element f in floors_Forjados)
			{
				Parameter param = f.LookupParameter("Área");
				areaAcumulada_Forjados = areaAcumulada_Forjados + Math.Round(param.AsDouble() / 10.7639);
			}
			foreach (Element sc in structuralColumns_PilarHormigon)
			{
				Parameter param = sc.LookupParameter("Volumen");
				volumenAcumulado_PilarHormigon = volumenAcumulado_PilarHormigon + Math.Round(param.AsDouble() / 35.3147);
			}
            foreach (Element f in floors_Concreto)
            {
				Parameter param = f.LookupParameter("Área");
				areaAcumulada_Concreto = areaAcumulada_Concreto + Math.Round(param.AsDouble() / 10.7639);
			}
            foreach (Element f in strfoundation_Cimentaciones)
            {
				Parameter param = f.LookupParameter("Volumen");
				volumenAcumulado_Cimentaciones = volumenAcumulado_Cimentaciones + Math.Round(param.AsDouble() / 35.3147);
			}
			foreach (Element f in floors_ConcretoDeck)
			{
				Parameter param = f.LookupParameter("Área");
				areaAcumulada_ConcretoDeck = areaAcumulada_ConcretoDeck + Math.Round(param.AsDouble() / 10.7639);
			}
            foreach (Element f in strFramming_Droppedbeam)
            {
				Parameter param = f.LookupParameter("Volumen");
				volumenAcumulado_Droppedbeam = volumenAcumulado_Droppedbeam + Math.Round(param.AsDouble() / 35.3147);
			}
			foreach (Element f in floors_ConcreteSlab)
			{
				Parameter param = f.LookupParameter("Área");
				areaAcumulada_ConcreteSlab = areaAcumulada_ConcreteSlab + Math.Round(param.AsDouble() / 10.7639);
			}
			foreach (Element f in strFramming_Beamembbededm)
			{
				Parameter param = f.LookupParameter("Volumen");
				volumenAcumulado_Beamembbededm = volumenAcumulado_Beamembbededm + Math.Round(param.AsDouble() / 35.3147);
			}
			foreach (Element f in floors_ConcreteInclinedSlab)
			{
				Parameter param = f.LookupParameter("Área");
				areaAcumulada_ConcreteInclinedSla = areaAcumulada_ConcreteInclinedSla + Math.Round(param.AsDouble() / 10.7639);
			}
			foreach (Element f in walls_Concrete)
			{
				Parameter param = f.LookupParameter("Volumen");
				volumenAcumulado_walls_Concrete = volumenAcumulado_walls_Concrete + Math.Round(param.AsDouble() / 35.3147);
			}

			#endregion
			// 10 elementos
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


			#region keys
			string key0 = "";
			string key1 = "";
			string key2 = "";
			string key3 = "";
			string key4 = "";
			string key5 = "";
			string key6 = "";
			string key7 = "";
			string key8 = "";
			string key9 = "";
			string key10 = "";
			string key11 = "";
			#endregion

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
			#endregion

			// 10 elementos
			for (int i = 0; i < floors_Forjados.Count(); i++)
            {
				string valor0 = data_forjado["Structural element"];
				string valor1 = data_forjado["Código"];
				string valor2 = data_forjado["07 07 01 - aqueous washing liquids"];//
				string valor3 = data_forjado["15 01 02 - plastic packaging"];
				string valor4 = data_forjado["15 01 03 - wooden packaging"];
				string valor5 = data_forjado["15 01 04 - metallic packaging"];//
				string valor6 = data_forjado["15 01 06 - mixed packaging"];//
				string valor7 = data_forjado["17 01 01 - concrete"];//
				string valor8 = data_forjado["17 02 01 - wood"];
				string valor9 = data_forjado["17 02 03 - plastic"];
				string valor10 = data_forjado["17 04 05 - iron and steel"];//
				string valor11 = data_forjado["17 09 04 - mixed"];//

                #region keys
                key0 = data_forjado.FirstOrDefault(x => x.Value == valor0).Key;//"Structural element"
				key1 = data_forjado.FirstOrDefault(x => x.Value == valor1).Key;//"Código"
				key2 = data_forjado.FirstOrDefault(x => x.Value == valor2).Key;//07 07 01 - aqueous washing liquids
				key3 = data_forjado.FirstOrDefault(x => x.Value == valor3).Key;//"15 01 02 - plastic packaging
				key4 = data_forjado.FirstOrDefault(x => x.Value == valor4).Key;//"15 01 03 - wooden packaging"
				key5 = data_forjado.FirstOrDefault(x => x.Value == valor5).Key;//"15 01 04 - metallic packaging"
				key6 = data_forjado.FirstOrDefault(x => x.Value == valor6).Key;//"15 01 06 - mixed packaging"
				key7 = data_forjado.FirstOrDefault(x => x.Value == valor7).Key;//"17 01 01 - concrete"
				key8 = data_forjado.FirstOrDefault(x => x.Value == valor8).Key;//"17 02 01 - wood"
				key9 = data_forjado.FirstOrDefault(x => x.Value == valor9).Key;//"17 02 03 - plastic"
				key10 = data_forjado.FirstOrDefault(x => x.Value == valor10).Key;//"17 04 05 - iron and steel"
				key11 = data_forjado.FirstOrDefault(x => x.Value == valor11).Key;//"17 09 04 - mixed"
				#endregion

				Element f = floors_Forjados[i];
				Parameter area = f.LookupParameter("Área");

				double valor2_porArea = double.Parse(valor2) * Math.Round(area.AsDouble() / 10.7639);//07 07 01 - aqueous washing liquids X AREA
				lista2_valor.Add(valor2_porArea);
				double valor3_porArea = double.Parse(valor3) * Math.Round(area.AsDouble() / 10.7639);//"15 01 02 - plastic packaging" X AREA
				lista3_valor.Add(valor3_porArea);
				double valor4_porArea = double.Parse(valor4) * Math.Round(area.AsDouble() / 10.7639);//"15 01 03 - wooden packaging" X AREA
				lista4_valor.Add(valor4_porArea);
				double valor5_porArea = double.Parse(valor5) * Math.Round(area.AsDouble() / 10.7639);//"15 01 04 - metallic packaging" X AREA
				lista5_valor.Add(valor5_porArea);
				double valor6_porArea = double.Parse(valor6) * Math.Round(area.AsDouble() / 10.7639);//"15 01 06 - mixed packaging"]  X AREA
				lista6_valor.Add(valor6_porArea);
				double valor7_porArea = double.Parse(valor7) * Math.Round(area.AsDouble() / 10.7639);//"17 01 01 - concrete"  X AREA
				lista7_valor.Add(valor7_porArea);
				double valor8_porArea = double.Parse(valor8) * Math.Round(area.AsDouble() / 10.7639);//"17 02 01 - wood"  X AREA
				lista8_valor.Add(valor8_porArea);
				double valor9_porArea = double.Parse(valor9) * Math.Round(area.AsDouble() / 10.7639);//"17 02 03 - plastic"  X AREA
				lista9_valor.Add(valor9_porArea);
				double valor10_porArea = double.Parse(valor10) * Math.Round(area.AsDouble() / 10.7639);//"17 04 05 - iron and steel"  X AREA
				lista10_valor.Add(valor10_porArea);
				double valor11_porArea = double.Parse(valor11) * Math.Round(area.AsDouble() / 10.7639);//"17 09 04 - mixed"  X AREA
				lista11_valor.Add(valor11_porArea);

                double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
                                    valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
				lista_sumaTotal_valor_porArea_Forjado.Add(sumaTotal_valor_porArea);


            }
			for (int i = 0; i < structuralColumns_PilarHormigon.Count(); i++)
			{
				string valor0 = data_pilar_hormigon["Structural element"];
                string valor1 = data_pilar_hormigon["Código"];
                string valor2 = data_pilar_hormigon["07 07 01 - aqueous washing liquids"];
				string valor3 = data_pilar_hormigon["15 01 02 - plastic packaging"];
				string valor4 = data_pilar_hormigon["15 01 03 - wooden packaging"];
				string valor5 = data_pilar_hormigon["15 01 04 - metallic packaging"];
				string valor6 = data_pilar_hormigon["15 01 06 - mixed packaging"];
				string valor7 = data_pilar_hormigon["17 01 01 - concrete"];
				string valor8 = data_pilar_hormigon["17 02 01 - wood"];
				string valor9 = data_pilar_hormigon["17 02 03 - plastic"];
				string valor10 = data_pilar_hormigon["17 04 05 - iron and steel"];
				string valor11 = data_pilar_hormigon["17 09 04 - mixed"];

				Element p = structuralColumns_PilarHormigon[i];
				Parameter volumen = p.LookupParameter("Volumen");

				double valor2_porVolumen = double.Parse(valor2) * Math.Round(volumen.AsDouble() / 35.3147);//07 07 01 - aqueous washing liquids X VOLUMEN
				lista2_valor.Add(valor2_porVolumen);
				double valor3_porVolumen = double.Parse(valor3) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 02 - plastic packaging" X VOLUMEN
				lista3_valor.Add(valor3_porVolumen);
				double valor4_porVolumen = double.Parse(valor4) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 03 - wooden packaging" X VOLUMEN
				lista4_valor.Add(valor4_porVolumen);
				double valor5_porVolumen = double.Parse(valor5) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 04 - metallic packaging" X VOLUMEN
				lista5_valor.Add(valor5_porVolumen);
				double valor6_porVolumen = double.Parse(valor6) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 06 - mixed packaging"]  X VOLUMEN
				lista6_valor.Add(valor6_porVolumen);
				double valor7_porVolumen = double.Parse(valor7) * Math.Round(volumen.AsDouble() / 35.3147);//"17 01 01 - concrete"  X VOLUMEN
				lista7_valor.Add(valor7_porVolumen);
				double valor8_porVolumen = double.Parse(valor8) * Math.Round(volumen.AsDouble() / 35.3147);//"17 02 01 - wood"  X VOLUMEN
				lista8_valor.Add(valor8_porVolumen);
				double valor9_porVolumen = double.Parse(valor9) * Math.Round(volumen.AsDouble() / 35.3147);//"17 02 03 - plastic"  X VOLUMEN
				lista9_valor.Add(valor9_porVolumen);
				double valor10_porVolumen = double.Parse(valor10) * Math.Round(volumen.AsDouble() / 35.3147);//"17 04 05 - iron and steel"  X VOLUMEN
				lista10_valor.Add(valor10_porVolumen);
				double valor11_porVolumen = double.Parse(valor11) * Math.Round(volumen.AsDouble() / 35.3147);//"17 09 04 - mixed"  X VOLUMEN
				lista11_valor.Add(valor11_porVolumen);

                double sumaTotal_valor_porVolumen = valor2_porVolumen + valor3_porVolumen + valor4_porVolumen + valor5_porVolumen + valor6_porVolumen + valor7_porVolumen +
									valor8_porVolumen + valor9_porVolumen + valor10_porVolumen + valor11_porVolumen;
				lista_sumaTotal_valor_porVolumen_PilarConcreto.Add(sumaTotal_valor_porVolumen);

            }
			for (int i = 0; i < floors_Concreto.Count(); i++)
			{
				string valor0 = data_floors_concreto["Structural element"];
				string valor1 = data_floors_concreto["Código"];
				string valor2 = data_floors_concreto["07 07 01 - aqueous washing liquids"];//
				string valor3 = data_floors_concreto["15 01 02 - plastic packaging"];
				string valor4 = data_floors_concreto["15 01 03 - wooden packaging"];
				string valor5 = data_floors_concreto["15 01 04 - metallic packaging"];//
				string valor6 = data_floors_concreto["15 01 06 - mixed packaging"];//
				string valor7 = data_floors_concreto["17 01 01 - concrete"];//
				string valor8 = data_floors_concreto["17 02 01 - wood"];
				string valor9 = data_floors_concreto["17 02 03 - plastic"];
				string valor10 = data_floors_concreto["17 04 05 - iron and steel"];//
				string valor11 = data_floors_concreto["17 09 04 - mixed"];//

				Element f = floors_Concreto[i];
				Parameter area = f.LookupParameter("Área");

				double valor2_porArea = double.Parse(valor2) * Math.Round(area.AsDouble() / 10.7639);//07 07 01 - aqueous washing liquids X AREA
				lista2_valor.Add(valor2_porArea);
				double valor3_porArea = double.Parse(valor3) * Math.Round(area.AsDouble() / 10.7639);//"15 01 02 - plastic packaging" X AREA
				lista3_valor.Add(valor3_porArea);
				double valor4_porArea = double.Parse(valor4) * Math.Round(area.AsDouble() / 10.7639);//"15 01 03 - wooden packaging" X AREA
				lista4_valor.Add(valor4_porArea);
				double valor5_porArea = double.Parse(valor5) * Math.Round(area.AsDouble() / 10.7639);//"15 01 04 - metallic packaging" X AREA
				lista5_valor.Add(valor5_porArea);
				double valor6_porArea = double.Parse(valor6) * Math.Round(area.AsDouble() / 10.7639);//"15 01 06 - mixed packaging"]  X AREA
				lista6_valor.Add(valor6_porArea);
				double valor7_porArea = double.Parse(valor7) * Math.Round(area.AsDouble() / 10.7639);//"17 01 01 - concrete"  X AREA
				lista7_valor.Add(valor7_porArea);
				double valor8_porArea = double.Parse(valor8) * Math.Round(area.AsDouble() / 10.7639);//"17 02 01 - wood"  X AREA
				lista8_valor.Add(valor8_porArea);
				double valor9_porArea = double.Parse(valor9) * Math.Round(area.AsDouble() / 10.7639);//"17 02 03 - plastic"  X AREA
				lista9_valor.Add(valor9_porArea);
				double valor10_porArea = double.Parse(valor10) * Math.Round(area.AsDouble() / 10.7639);//"17 04 05 - iron and steel"  X AREA
				lista10_valor.Add(valor10_porArea);
				double valor11_porArea = double.Parse(valor11) * Math.Round(area.AsDouble() / 10.7639);//"17 09 04 - mixed"  X AREA
				lista11_valor.Add(valor11_porArea);

				double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
									valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
				lista_sumaTotal_valor_porArea_Concreto.Add(sumaTotal_valor_porArea);


			}
            for (int i = 0; i < strfoundation_Cimentaciones.Count(); i++)
            {
				string valor0 = data_Cimentaciones["Structural element"];
				string valor1 = data_Cimentaciones["Código"];
				string valor2 = data_Cimentaciones["07 07 01 - aqueous washing liquids"];
				string valor3 = data_Cimentaciones["15 01 02 - plastic packaging"];
				string valor4 = data_Cimentaciones["15 01 03 - wooden packaging"];
				string valor5 = data_Cimentaciones["15 01 04 - metallic packaging"];
				string valor6 = data_Cimentaciones["15 01 06 - mixed packaging"];
				string valor7 = data_Cimentaciones["17 01 01 - concrete"];
				string valor8 = data_Cimentaciones["17 02 01 - wood"];
				string valor9 = data_Cimentaciones["17 02 03 - plastic"];
				string valor10 = data_Cimentaciones["17 04 05 - iron and steel"];
				string valor11 = data_Cimentaciones["17 09 04 - mixed"];

				Element p = strfoundation_Cimentaciones[i];
				Parameter volumen = p.LookupParameter("Volumen");

				double valor2_porVolumen = double.Parse(valor2) * Math.Round(volumen.AsDouble() / 35.3147);//07 07 01 - aqueous washing liquids X VOLUMEN
				lista2_valor.Add(valor2_porVolumen);
				double valor3_porVolumen = double.Parse(valor3) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 02 - plastic packaging" X VOLUMEN
				lista3_valor.Add(valor3_porVolumen);
				double valor4_porVolumen = double.Parse(valor4) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 03 - wooden packaging" X VOLUMEN
				lista4_valor.Add(valor4_porVolumen);
				double valor5_porVolumen = double.Parse(valor5) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 04 - metallic packaging" X VOLUMEN
				lista5_valor.Add(valor5_porVolumen);
				double valor6_porVolumen = double.Parse(valor6) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 06 - mixed packaging"]  X VOLUMEN
				lista6_valor.Add(valor6_porVolumen);
				double valor7_porVolumen = double.Parse(valor7) * Math.Round(volumen.AsDouble() / 35.3147);//"17 01 01 - concrete"  X VOLUMEN
				lista7_valor.Add(valor7_porVolumen);
				double valor8_porVolumen = double.Parse(valor8) * Math.Round(volumen.AsDouble() / 35.3147);//"17 02 01 - wood"  X VOLUMEN
				lista8_valor.Add(valor8_porVolumen);
				double valor9_porVolumen = double.Parse(valor9) * Math.Round(volumen.AsDouble() / 35.3147);//"17 02 03 - plastic"  X VOLUMEN
				lista9_valor.Add(valor9_porVolumen);
				double valor10_porVolumen = double.Parse(valor10) * Math.Round(volumen.AsDouble() / 35.3147);//"17 04 05 - iron and steel"  X VOLUMEN
				lista10_valor.Add(valor10_porVolumen);
				double valor11_porVolumen = double.Parse(valor11) * Math.Round(volumen.AsDouble() / 35.3147);//"17 09 04 - mixed"  X VOLUMEN
				lista11_valor.Add(valor11_porVolumen);

				double sumaTotal_valor_porVolumen = valor2_porVolumen + valor3_porVolumen + valor4_porVolumen + valor5_porVolumen + valor6_porVolumen + valor7_porVolumen +
									valor8_porVolumen + valor9_porVolumen + valor10_porVolumen + valor11_porVolumen;
				lista_sumaTotal_valor_porVolumen_Cimentaciones.Add(sumaTotal_valor_porVolumen);

			}
			for (int i = 0; i < floors_ConcretoDeck.Count(); i++)
			{
				string valor0 = data_ConcretoDeck["Structural element"];
				string valor1 = data_ConcretoDeck["Código"];
				string valor2 = data_ConcretoDeck["07 07 01 - aqueous washing liquids"];//
				string valor3 = data_ConcretoDeck["15 01 02 - plastic packaging"];
				string valor4 = data_ConcretoDeck["15 01 03 - wooden packaging"];
				string valor5 = data_ConcretoDeck["15 01 04 - metallic packaging"];//
				string valor6 = data_ConcretoDeck["15 01 06 - mixed packaging"];//
				string valor7 = data_ConcretoDeck["17 01 01 - concrete"];//
				string valor8 = data_ConcretoDeck["17 02 01 - wood"];
				string valor9 = data_ConcretoDeck["17 02 03 - plastic"];
				string valor10 = data_ConcretoDeck["17 04 05 - iron and steel"];//
				string valor11 = data_ConcretoDeck["17 09 04 - mixed"];//

				Element f = floors_ConcretoDeck[i];
				Parameter area = f.LookupParameter("Área");

				double valor2_porArea = double.Parse(valor2) * Math.Round(area.AsDouble() / 10.7639);//07 07 01 - aqueous washing liquids X AREA
				lista2_valor.Add(valor2_porArea);
				double valor3_porArea = double.Parse(valor3) * Math.Round(area.AsDouble() / 10.7639);//"15 01 02 - plastic packaging" X AREA
				lista3_valor.Add(valor3_porArea);
				double valor4_porArea = double.Parse(valor4) * Math.Round(area.AsDouble() / 10.7639);//"15 01 03 - wooden packaging" X AREA
				lista4_valor.Add(valor4_porArea);
				double valor5_porArea = double.Parse(valor5) * Math.Round(area.AsDouble() / 10.7639);//"15 01 04 - metallic packaging" X AREA
				lista5_valor.Add(valor5_porArea);
				double valor6_porArea = double.Parse(valor6) * Math.Round(area.AsDouble() / 10.7639);//"15 01 06 - mixed packaging"]  X AREA
				lista6_valor.Add(valor6_porArea);
				double valor7_porArea = double.Parse(valor7) * Math.Round(area.AsDouble() / 10.7639);//"17 01 01 - concrete"  X AREA
				lista7_valor.Add(valor7_porArea);
				double valor8_porArea = double.Parse(valor8) * Math.Round(area.AsDouble() / 10.7639);//"17 02 01 - wood"  X AREA
				lista8_valor.Add(valor8_porArea);
				double valor9_porArea = double.Parse(valor9) * Math.Round(area.AsDouble() / 10.7639);//"17 02 03 - plastic"  X AREA
				lista9_valor.Add(valor9_porArea);
				double valor10_porArea = double.Parse(valor10) * Math.Round(area.AsDouble() / 10.7639);//"17 04 05 - iron and steel"  X AREA
				lista10_valor.Add(valor10_porArea);
				double valor11_porArea = double.Parse(valor11) * Math.Round(area.AsDouble() / 10.7639);//"17 09 04 - mixed"  X AREA
				lista11_valor.Add(valor11_porArea);

				double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
									valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
				lista_sumaTotal_valor_porArea_ConcretoDeck.Add(sumaTotal_valor_porArea);


			}
            for (int i = 0; i < strFramming_Droppedbeam.Count(); i++)
            {
				string valor0 = data_Droppedbeam["Structural element"];
				string valor1 = data_Droppedbeam["Código"];
				string valor2 = data_Droppedbeam["07 07 01 - aqueous washing liquids"];
				string valor3 = data_Droppedbeam["15 01 02 - plastic packaging"];
				string valor4 = data_Droppedbeam["15 01 03 - wooden packaging"];
				string valor5 = data_Droppedbeam["15 01 04 - metallic packaging"];
				string valor6 = data_Droppedbeam["15 01 06 - mixed packaging"];
				string valor7 = data_Droppedbeam["17 01 01 - concrete"];
				string valor8 = data_Droppedbeam["17 02 01 - wood"];
				string valor9 = data_Droppedbeam["17 02 03 - plastic"];
				string valor10 = data_Droppedbeam["17 04 05 - iron and steel"];
				string valor11 = data_Droppedbeam["17 09 04 - mixed"];

				Element p = strFramming_Droppedbeam[i];
				Parameter volumen = p.LookupParameter("Volumen");

				double valor2_porVolumen = double.Parse(valor2) * Math.Round(volumen.AsDouble() / 35.3147);//07 07 01 - aqueous washing liquids X VOLUMEN
				lista2_valor.Add(valor2_porVolumen);
				double valor3_porVolumen = double.Parse(valor3) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 02 - plastic packaging" X VOLUMEN
				lista3_valor.Add(valor3_porVolumen);
				double valor4_porVolumen = double.Parse(valor4) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 03 - wooden packaging" X VOLUMEN
				lista4_valor.Add(valor4_porVolumen);
				double valor5_porVolumen = double.Parse(valor5) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 04 - metallic packaging" X VOLUMEN
				lista5_valor.Add(valor5_porVolumen);
				double valor6_porVolumen = double.Parse(valor6) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 06 - mixed packaging"]  X VOLUMEN
				lista6_valor.Add(valor6_porVolumen);
				double valor7_porVolumen = double.Parse(valor7) * Math.Round(volumen.AsDouble() / 35.3147);//"17 01 01 - concrete"  X VOLUMEN
				lista7_valor.Add(valor7_porVolumen);
				double valor8_porVolumen = double.Parse(valor8) * Math.Round(volumen.AsDouble() / 35.3147);//"17 02 01 - wood"  X VOLUMEN
				lista8_valor.Add(valor8_porVolumen);
				double valor9_porVolumen = double.Parse(valor9) * Math.Round(volumen.AsDouble() / 35.3147);//"17 02 03 - plastic"  X VOLUMEN
				lista9_valor.Add(valor9_porVolumen);
				double valor10_porVolumen = double.Parse(valor10) * Math.Round(volumen.AsDouble() / 35.3147);//"17 04 05 - iron and steel"  X VOLUMEN
				lista10_valor.Add(valor10_porVolumen);
				double valor11_porVolumen = double.Parse(valor11) * Math.Round(volumen.AsDouble() / 35.3147);//"17 09 04 - mixed"  X VOLUMEN
				lista11_valor.Add(valor11_porVolumen);

				double sumaTotal_valor_porVolumen = valor2_porVolumen + valor3_porVolumen + valor4_porVolumen + valor5_porVolumen + valor6_porVolumen + valor7_porVolumen +
									valor8_porVolumen + valor9_porVolumen + valor10_porVolumen + valor11_porVolumen;
				lista_sumaTotal_valor_porArea_Droppedbeam.Add(sumaTotal_valor_porVolumen);
			}
			for (int i = 0; i < floors_ConcreteSlab.Count(); i++)
			{
				string valor0 = data_ConcreteSlab["Structural element"];
				string valor1 = data_ConcreteSlab["Código"];
				string valor2 = data_ConcreteSlab["07 07 01 - aqueous washing liquids"];//
				string valor3 = data_ConcreteSlab["15 01 02 - plastic packaging"];
				string valor4 = data_ConcreteSlab["15 01 03 - wooden packaging"];
				string valor5 = data_ConcreteSlab["15 01 04 - metallic packaging"];//
				string valor6 = data_ConcreteSlab["15 01 06 - mixed packaging"];//
				string valor7 = data_ConcreteSlab["17 01 01 - concrete"];//
				string valor8 = data_ConcreteSlab["17 02 01 - wood"];
				string valor9 = data_ConcreteSlab["17 02 03 - plastic"];
				string valor10 = data_ConcreteSlab["17 04 05 - iron and steel"];//
				string valor11 = data_ConcreteSlab["17 09 04 - mixed"];//

				Element f = floors_ConcreteSlab[i];
				Parameter area = f.LookupParameter("Área");

				double valor2_porArea = double.Parse(valor2) * Math.Round(area.AsDouble() / 10.7639);//07 07 01 - aqueous washing liquids X AREA
				lista2_valor.Add(valor2_porArea);
				double valor3_porArea = double.Parse(valor3) * Math.Round(area.AsDouble() / 10.7639);//"15 01 02 - plastic packaging" X AREA
				lista3_valor.Add(valor3_porArea);
				double valor4_porArea = double.Parse(valor4) * Math.Round(area.AsDouble() / 10.7639);//"15 01 03 - wooden packaging" X AREA
				lista4_valor.Add(valor4_porArea);
				double valor5_porArea = double.Parse(valor5) * Math.Round(area.AsDouble() / 10.7639);//"15 01 04 - metallic packaging" X AREA
				lista5_valor.Add(valor5_porArea);
				double valor6_porArea = double.Parse(valor6) * Math.Round(area.AsDouble() / 10.7639);//"15 01 06 - mixed packaging"]  X AREA
				lista6_valor.Add(valor6_porArea);
				double valor7_porArea = double.Parse(valor7) * Math.Round(area.AsDouble() / 10.7639);//"17 01 01 - concrete"  X AREA
				lista7_valor.Add(valor7_porArea);
				double valor8_porArea = double.Parse(valor8) * Math.Round(area.AsDouble() / 10.7639);//"17 02 01 - wood"  X AREA
				lista8_valor.Add(valor8_porArea);
				double valor9_porArea = double.Parse(valor9) * Math.Round(area.AsDouble() / 10.7639);//"17 02 03 - plastic"  X AREA
				lista9_valor.Add(valor9_porArea);
				double valor10_porArea = double.Parse(valor10) * Math.Round(area.AsDouble() / 10.7639);//"17 04 05 - iron and steel"  X AREA
				lista10_valor.Add(valor10_porArea);
				double valor11_porArea = double.Parse(valor11) * Math.Round(area.AsDouble() / 10.7639);//"17 09 04 - mixed"  X AREA
				lista11_valor.Add(valor11_porArea);

				double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
									valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
				lista_sumaTotal_valor_porArea_ConcreteSlab.Add(sumaTotal_valor_porArea);


			}
			for (int i = 0; i < strFramming_Beamembbededm.Count(); i++)
			{
				string valor0 = data_Beamembbeded["Structural element"];
				string valor1 = data_Beamembbeded["Código"];
				string valor2 = data_Beamembbeded["07 07 01 - aqueous washing liquids"];
				string valor3 = data_Beamembbeded["15 01 02 - plastic packaging"];
				string valor4 = data_Beamembbeded["15 01 03 - wooden packaging"];
				string valor5 = data_Beamembbeded["15 01 04 - metallic packaging"];
				string valor6 = data_Beamembbeded["15 01 06 - mixed packaging"];
				string valor7 = data_Beamembbeded["17 01 01 - concrete"];
				string valor8 = data_Beamembbeded["17 02 01 - wood"];
				string valor9 = data_Beamembbeded["17 02 03 - plastic"];
				string valor10 = data_Beamembbeded["17 04 05 - iron and steel"];
				string valor11 = data_Beamembbeded["17 09 04 - mixed"];

				Element p = strFramming_Beamembbededm[i];
				Parameter volumen = p.LookupParameter("Volumen");

				double valor2_porVolumen = double.Parse(valor2) * Math.Round(volumen.AsDouble() / 35.3147);//07 07 01 - aqueous washing liquids X VOLUMEN
				lista2_valor.Add(valor2_porVolumen);
				double valor3_porVolumen = double.Parse(valor3) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 02 - plastic packaging" X VOLUMEN
				lista3_valor.Add(valor3_porVolumen);
				double valor4_porVolumen = double.Parse(valor4) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 03 - wooden packaging" X VOLUMEN
				lista4_valor.Add(valor4_porVolumen);
				double valor5_porVolumen = double.Parse(valor5) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 04 - metallic packaging" X VOLUMEN
				lista5_valor.Add(valor5_porVolumen);
				double valor6_porVolumen = double.Parse(valor6) * Math.Round(volumen.AsDouble() / 35.3147);//"15 01 06 - mixed packaging"]  X VOLUMEN
				lista6_valor.Add(valor6_porVolumen);
				double valor7_porVolumen = double.Parse(valor7) * Math.Round(volumen.AsDouble() / 35.3147);//"17 01 01 - concrete"  X VOLUMEN
				lista7_valor.Add(valor7_porVolumen);
				double valor8_porVolumen = double.Parse(valor8) * Math.Round(volumen.AsDouble() / 35.3147);//"17 02 01 - wood"  X VOLUMEN
				lista8_valor.Add(valor8_porVolumen);
				double valor9_porVolumen = double.Parse(valor9) * Math.Round(volumen.AsDouble() / 35.3147);//"17 02 03 - plastic"  X VOLUMEN
				lista9_valor.Add(valor9_porVolumen);
				double valor10_porVolumen = double.Parse(valor10) * Math.Round(volumen.AsDouble() / 35.3147);//"17 04 05 - iron and steel"  X VOLUMEN
				lista10_valor.Add(valor10_porVolumen);
				double valor11_porVolumen = double.Parse(valor11) * Math.Round(volumen.AsDouble() / 35.3147);//"17 09 04 - mixed"  X VOLUMEN
				lista11_valor.Add(valor11_porVolumen);

				double sumaTotal_valor_porVolumen = valor2_porVolumen + valor3_porVolumen + valor4_porVolumen + valor5_porVolumen + valor6_porVolumen + valor7_porVolumen +
									valor8_porVolumen + valor9_porVolumen + valor10_porVolumen + valor11_porVolumen;
				lista_sumaTotal_valor_porArea_Beamembbededm.Add(sumaTotal_valor_porVolumen);
			}
			for (int i = 0; i < floors_ConcreteInclinedSlab.Count(); i++)
			{
				string valor0 = data_ConcreteInclinedSlab["Structural element"];
				string valor1 = data_ConcreteInclinedSlab["Código"];
				string valor2 = data_ConcreteInclinedSlab["07 07 01 - aqueous washing liquids"];//
				string valor3 = data_ConcreteInclinedSlab["15 01 02 - plastic packaging"];
				string valor4 = data_ConcreteInclinedSlab["15 01 03 - wooden packaging"];
				string valor5 = data_ConcreteInclinedSlab["15 01 04 - metallic packaging"];//
				string valor6 = data_ConcreteInclinedSlab["15 01 06 - mixed packaging"];//
				string valor7 = data_ConcreteInclinedSlab["17 01 01 - concrete"];//
				string valor8 = data_ConcreteInclinedSlab["17 02 01 - wood"];
				string valor9 = data_ConcreteInclinedSlab["17 02 03 - plastic"];
				string valor10 = data_ConcreteInclinedSlab["17 04 05 - iron and steel"];//
				string valor11 = data_ConcreteInclinedSlab["17 09 04 - mixed"];//

				Element f = floors_ConcreteInclinedSlab[i];
				Parameter area = f.LookupParameter("Área");

				double valor2_porArea = double.Parse(valor2) * Math.Round(area.AsDouble() / 10.7639);//07 07 01 - aqueous washing liquids X AREA
				lista2_valor.Add(valor2_porArea);
				double valor3_porArea = double.Parse(valor3) * Math.Round(area.AsDouble() / 10.7639);//"15 01 02 - plastic packaging" X AREA
				lista3_valor.Add(valor3_porArea);
				double valor4_porArea = double.Parse(valor4) * Math.Round(area.AsDouble() / 10.7639);//"15 01 03 - wooden packaging" X AREA
				lista4_valor.Add(valor4_porArea);
				double valor5_porArea = double.Parse(valor5) * Math.Round(area.AsDouble() / 10.7639);//"15 01 04 - metallic packaging" X AREA
				lista5_valor.Add(valor5_porArea);
				double valor6_porArea = double.Parse(valor6) * Math.Round(area.AsDouble() / 10.7639);//"15 01 06 - mixed packaging"]  X AREA
				lista6_valor.Add(valor6_porArea);
				double valor7_porArea = double.Parse(valor7) * Math.Round(area.AsDouble() / 10.7639);//"17 01 01 - concrete"  X AREA
				lista7_valor.Add(valor7_porArea);
				double valor8_porArea = double.Parse(valor8) * Math.Round(area.AsDouble() / 10.7639);//"17 02 01 - wood"  X AREA
				lista8_valor.Add(valor8_porArea);
				double valor9_porArea = double.Parse(valor9) * Math.Round(area.AsDouble() / 10.7639);//"17 02 03 - plastic"  X AREA
				lista9_valor.Add(valor9_porArea);
				double valor10_porArea = double.Parse(valor10) * Math.Round(area.AsDouble() / 10.7639);//"17 04 05 - iron and steel"  X AREA
				lista10_valor.Add(valor10_porArea);
				double valor11_porArea = double.Parse(valor11) * Math.Round(area.AsDouble() / 10.7639);//"17 09 04 - mixed"  X AREA
				lista11_valor.Add(valor11_porArea);

				double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
									valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
				lista_sumaTotal_valor_porArea_ConcreteInclinedSlab.Add(sumaTotal_valor_porArea);


			}
			for (int i = 0; i < walls_Concrete.Count(); i++)
			{
				string valor0 = data_walls["Structural element"];
				string valor1 = data_walls["Código"];
				string valor2 = data_walls["07 07 01 - aqueous washing liquids"];//
				string valor3 = data_walls["15 01 02 - plastic packaging"];
				string valor4 = data_walls["15 01 03 - wooden packaging"];
				string valor5 = data_walls["15 01 04 - metallic packaging"];//
				string valor6 = data_walls["15 01 06 - mixed packaging"];//
				string valor7 = data_walls["17 01 01 - concrete"];//
				string valor8 = data_walls["17 02 01 - wood"];
				string valor9 = data_walls["17 02 03 - plastic"];
				string valor10 = data_walls["17 04 05 - iron and steel"];//
				string valor11 = data_walls["17 09 04 - mixed"];//

				Element f = walls_Concrete[i];
				Parameter area = f.LookupParameter("Área");

				double valor2_porArea = double.Parse(valor2) * Math.Round(area.AsDouble() / 10.7639);//07 07 01 - aqueous washing liquids X AREA
				lista2_valor.Add(valor2_porArea);
				double valor3_porArea = double.Parse(valor3) * Math.Round(area.AsDouble() / 10.7639);//"15 01 02 - plastic packaging" X AREA
				lista3_valor.Add(valor3_porArea);
				double valor4_porArea = double.Parse(valor4) * Math.Round(area.AsDouble() / 10.7639);//"15 01 03 - wooden packaging" X AREA
				lista4_valor.Add(valor4_porArea);
				double valor5_porArea = double.Parse(valor5) * Math.Round(area.AsDouble() / 10.7639);//"15 01 04 - metallic packaging" X AREA
				lista5_valor.Add(valor5_porArea);
				double valor6_porArea = double.Parse(valor6) * Math.Round(area.AsDouble() / 10.7639);//"15 01 06 - mixed packaging"]  X AREA
				lista6_valor.Add(valor6_porArea);
				double valor7_porArea = double.Parse(valor7) * Math.Round(area.AsDouble() / 10.7639);//"17 01 01 - concrete"  X AREA
				lista7_valor.Add(valor7_porArea);
				double valor8_porArea = double.Parse(valor8) * Math.Round(area.AsDouble() / 10.7639);//"17 02 01 - wood"  X AREA
				lista8_valor.Add(valor8_porArea);
				double valor9_porArea = double.Parse(valor9) * Math.Round(area.AsDouble() / 10.7639);//"17 02 03 - plastic"  X AREA
				lista9_valor.Add(valor9_porArea);
				double valor10_porArea = double.Parse(valor10) * Math.Round(area.AsDouble() / 10.7639);//"17 04 05 - iron and steel"  X AREA
				lista10_valor.Add(valor10_porArea);
				double valor11_porArea = double.Parse(valor11) * Math.Round(area.AsDouble() / 10.7639);//"17 09 04 - mixed"  X AREA
				lista11_valor.Add(valor11_porArea);

				double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
									valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
				lista_sumaTotal_valor_porArea_walls_Concrete.Add(sumaTotal_valor_porArea);


			}

			List<double> lista_desperdicios = new List<double>();
			// 10 elementos
			double desperdicioForjado = 0;
            for (int i = 0; i < lista_sumaTotal_valor_porArea_Forjado.Count(); i++)
            {
				desperdicioForjado = desperdicioForjado + lista_sumaTotal_valor_porArea_Forjado[i];// para Forjados
            }
			lista_desperdicios.Add(desperdicioForjado);
			double desperdicioPilarHormigon = 0;
            for (int i = 0; i < lista_sumaTotal_valor_porVolumen_PilarConcreto.Count(); i++)
            {
				desperdicioPilarHormigon = desperdicioPilarHormigon + lista_sumaTotal_valor_porVolumen_PilarConcreto[i];// para pilar de hormigon
            }
			lista_desperdicios.Add(desperdicioPilarHormigon);
			double desperdicioConcreto = 0;
			for (int i = 0; i < lista_sumaTotal_valor_porArea_Concreto.Count(); i++)
			{
				desperdicioConcreto = desperdicioConcreto + lista_sumaTotal_valor_porArea_Concreto[i];// para Concreto
			}
			lista_desperdicios.Add(desperdicioConcreto);
			double desperdicioCimentacion = 0;
            for (int i = 0; i < lista_sumaTotal_valor_porVolumen_Cimentaciones.Count(); i++)
            {
				desperdicioCimentacion = desperdicioCimentacion + lista_sumaTotal_valor_porVolumen_Cimentaciones[i];
			}
			lista_desperdicios.Add(desperdicioCimentacion);
			double desperdicioConcretoDeck = 0;
			for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcretoDeck.Count(); i++)
			{
				desperdicioConcretoDeck = desperdicioConcretoDeck + lista_sumaTotal_valor_porArea_ConcretoDeck[i];// para Concreto
			}
			lista_desperdicios.Add(desperdicioConcretoDeck);
			double desperdicio_Droppedbeam = 0;
			for (int i = 0; i < lista_sumaTotal_valor_porArea_Droppedbeam.Count(); i++)
			{
				desperdicio_Droppedbeam = desperdicio_Droppedbeam + lista_sumaTotal_valor_porArea_Droppedbeam[i];// para Concreto
			}
			lista_desperdicios.Add(desperdicio_Droppedbeam);
			double desperdicio_ConcreteSlab = 0;
			for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcreteSlab.Count(); i++)
			{
				desperdicio_ConcreteSlab = desperdicio_ConcreteSlab + lista_sumaTotal_valor_porArea_ConcreteSlab[i];// para Concreto
			}
			lista_desperdicios.Add(desperdicio_ConcreteSlab);
			double desperdicio_Beamembbededm = 0;
			for (int i = 0; i < lista_sumaTotal_valor_porArea_Beamembbededm.Count(); i++)
			{
				desperdicio_Beamembbededm = desperdicio_Beamembbededm + lista_sumaTotal_valor_porArea_Beamembbededm[i];// para Concreto
			}
			lista_desperdicios.Add(desperdicio_Beamembbededm);
			double desperdicio_ConcreteInclinedSlab = 0;
			for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcreteInclinedSlab.Count(); i++)
			{
				desperdicio_ConcreteInclinedSlab = desperdicio_ConcreteInclinedSlab + lista_sumaTotal_valor_porArea_ConcreteInclinedSlab[i];// para Concreto
			}
			lista_desperdicios.Add(desperdicio_ConcreteInclinedSlab);
			double desperdicio_walls_Concrete = 0;
			for (int i = 0; i < lista_sumaTotal_valor_porArea_walls_Concrete.Count(); i++)
			{
				desperdicio_walls_Concrete = desperdicio_walls_Concrete + lista_sumaTotal_valor_porArea_walls_Concrete[i];// para Concreto
			}
			lista_desperdicios.Add(desperdicio_walls_Concrete);

			#region Desperdicio Total
			// Desperdico total
			double desperdicioTotal = 0;
            for (int i = 0; i < lista_desperdicios.Count(); i++)
            {
				desperdicioTotal = desperdicioTotal + lista_desperdicios[i];
            }
			#endregion

			#region Suma de todos los valores
			//Suma de todos los Valores
			double valor2_final = 0; //07 07 01 - aqueous washing liquids ACUMULADO
			for (int i = 0; i < lista2_valor.Count(); i++)
            {
				valor2_final = valor2_final + lista2_valor[i];
            }
			double valor3_final = 0; //"15 01 02 - plastic packaging" ACUMULADO
			for (int i = 0; i < lista3_valor.Count(); i++)
			{
				valor3_final = valor3_final + lista3_valor[i];
			}
			double valor4_final = 0;//"15 01 03 - wooden packaging" ACUMULADO
			for (int i = 0; i < lista4_valor.Count(); i++)
			{
				valor4_final = valor4_final + lista4_valor[i];
			}
			double valor5_final = 0;//"15 01 04 - metallic packaging" ACUMULADO
			for (int i = 0; i < lista5_valor.Count(); i++)
			{
				valor5_final = valor5_final + lista5_valor[i];
			}
			double valor6_final = 0;//"15 01 06 - mixed packaging"] ACUMULADO
			for (int i = 0; i < lista6_valor.Count(); i++)
			{
				valor6_final = valor6_final + lista6_valor[i];
			}
			double valor7_final = 0;//"17 01 01 - concrete" ACUMULADO
			for (int i = 0; i < lista7_valor.Count(); i++)
			{
				valor7_final = valor7_final + lista7_valor[i];
			}
			double valor8_final = 0;//"17 02 01 - wood" ACUMULADO
			for (int i = 0; i < lista8_valor.Count(); i++)
			{
				valor8_final = valor8_final + lista8_valor[i];
			}
			double valor9_final = 0; //"17 02 03 - plastic" ACUMULADO
			for (int i = 0; i < lista9_valor.Count(); i++)
			{
				valor9_final = valor9_final + lista9_valor[i];
			}
			double valor10_final = 0;//"17 04 05 - iron and steel" ACUMULADO
			for (int i = 0; i < lista10_valor.Count(); i++)
			{
				valor10_final = valor10_final + lista10_valor[i];
			}
			double valor11_final = 0;//"17 09 04 - mixed" ACUMULADO
			for (int i = 0; i < lista11_valor.Count(); i++)
			{
				valor11_final = valor11_final + lista11_valor[i];
			}
			#endregion

			#region Obtener Excel
			var bankAccounts = new List<Account>();

			for (int i = 0; i < lista_Dictionarios.Count(); i++)
            {
				Account cuenta = new Account
				{
					ID = lista_Dictionarios[i]["Structural element"],
					Balance = lista_desperdicios[i]

				};
				bankAccounts.Add(cuenta);
            }
			Account nuevo1 = new Account
			{
				ID = "",
				Balance = 0
			};
			Account nuevo2 = new Account
			{
                ID = "Total",
                Balance = desperdicioTotal
            };
			bankAccounts.Add(nuevo1);
			bankAccounts.Add(nuevo2);

			var bankAccounts2 = new List<Account> {
					new Account {
									  ID = key2,
									  Balance = valor2_final//valor_porAres solo hormigon
				                },
					new Account {
									  ID = key3,
									  Balance = valor3_final// valor_porArea solo suelos por defecto
				                },
					new Account {
									ID = key4,
									Balance = valor4_final
					},
					new Account {
									ID = key5,
									Balance = valor5_final
					},
					new Account {
									ID = key6,
									Balance = valor6_final
					},
					new Account {
									ID = key7,
									Balance = valor7_final
					},
					new Account {
									ID = key8,
									Balance = valor8_final
					},
					new Account {
									ID = key9,
									Balance = valor9_final
					},
					new Account {
									ID = key10,
									Balance = valor10_final
					},
					new Account {
									ID = key11,
									Balance = valor11_final
					},
					new Account {
									ID = "",
									Balance = 0
					},
					new Account {
									ID = "Total",
									Balance =  desperdicioTotal
					}
				};

			DisplayInExcel(bankAccounts, bankAccounts2);
			#endregion

			// se crean key schedule con mismas tablas de excel

			// se abre ventana con chart

			#endregion

			return Result.Succeeded;
        }

		static void DisplayInExcel(IEnumerable<Account> accounts, IEnumerable<Account> accounts2)
		{
			Excel.Application excelApp = new Excel.Application();

			// Make the object visible.
			excelApp.Visible = true;

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

		#region Get all model elements
		/// <summary>
		/// Return all model elements, cf.
		/// http://forums.autodesk.com/t5/revit-api/traverse-all-model-elements-in-a-project-top-down-approach/m-p/5815247
		/// </summary>
		IEnumerable<Element> GetAllModelElements(
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

		IList<Element> GetFamilyInstanceModelElements(
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

		public Result OnStartup(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {

            return Result.Succeeded;
        }
    }

    internal class Account
    {
		public string ID { get; set; }
		public double Balance { get; set; }
	}
}
