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
using Autodesk.Revit.UI.Selection;

namespace AddinExportCDW
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    class SelectionElement : IExternalCommand
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

			#region Selecton collector
			List<Element> lista_SelectElements = new List<Element>();
			IList<Reference> references = uidoc.Selection.PickObjects(ObjectType.Element, "Seleccionar el Elemento que se quiere analizar");
			foreach (Reference reference in references)
			{
				Element e = doc.GetElement(reference);
				lista_SelectElements.Add(e);
			}
			#endregion

			#region Colectores de Elementos

			List<Element> floors = new List<Element>();
			//Pilar Hormigon
			//FilteredElementCollector DUcoll2 = new FilteredElementCollector(doc, activeView.Id);
			List<Element> structuralColumns = new List<Element>();
			//Cimentaciones
			//FilteredElementCollector DUcoll3 = new FilteredElementCollector(doc, activeView.Id);
			List<Element> strFoundation = new List<Element>();
			// Structural framming
			//FilteredElementCollector DUcoll4 = new FilteredElementCollector(doc, activeView.Id);
			List<Element> strFramming = new List<Element>();
			//  walls
			//FilteredElementCollector DUcoll5 = new FilteredElementCollector(doc, activeView.Id);
			List<Element> walls = new List<Element>();



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


			}

			#endregion

			// 10 elementos
			#region Dictionarios

			List<Dictionary<string, string>> lista_Dictionarios = new List<Dictionary<string, string>>();

			Dictionary<string, string> data_forjado = Dictionary.Get("data_forjado");
			Dictionary<string, string> data_pilar_hormigon = Dictionary.Get("data_pilar_hormigon");
			Dictionary<string, string> data_floors_concreto = Dictionary.Get("data_floors_concreto");
			Dictionary<string, string> data_Cimentaciones = Dictionary.Get("data_Cimentaciones");
			Dictionary<string, string> data_ConcretoDeck = Dictionary.Get("data_ConcretoDeck");
			Dictionary<string, string> data_Droppedbeam = Dictionary.Get("data_Droppedbeam");
			Dictionary<string, string> data_ConcreteSlab = Dictionary.Get(" data_ConcreteSlab");
			Dictionary<string, string> data_Beamembbeded = Dictionary.Get("data_Beamembbeded");
			Dictionary<string, string> data_ConcreteInclinedSlab = Dictionary.Get("data_ConcreteInclinedSlab");
			Dictionary<string, string> data_walls = Dictionary.Get("data_walls");


			#endregion



			#region relacionar el nombre familia con el codigo del material para Filtrarlo

			#region datos iniciales
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

			string nombre_floors_Forjados = "";
			string nombre_structuralColumns_PilarHormigon = "";
			string nombre_floors_Concreto = "";
			string nombre_strfoundation_Cimentaciones = "";
			string nombre_floors_ConcretoDeck = "";
			string nombre_strFramming_Droppedbeam = "";
			string nombre_floors_ConcreteSlab = "";
			string nombre_strFramming_Beamembbededm = "";
			string nombre_floors_ConcreteInclinedSla = "";
			string nombre_walls_Concrete = "";

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
			#endregion

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

			List<string> lista_nombres_elementos = new List<string>();

            // 10 elementos : Aquí se filtran los Elementos con el Código
            if (floors.Count() != 0)
            {
				foreach (Element sc in floors)
				{
					ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
					Parameter pamType = type.LookupParameter("Material estructural");
					if ((pamType.AsValueString() == data_forjado["Código"]))
					{
						Parameter param = sc.LookupParameter("Área");
						areaAcumulada_Forjados = areaAcumulada_Forjados + Math.Round(param.AsDouble() / 10.7639, 4);

						#region codigos de diccionarios

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

						#endregion

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

						#region valorxArea
						double valor2_porArea = double.Parse(valor2) * Math.Round(param.AsDouble() / 10.7639);//07 07 01 - aqueous washing liquids X AREA
						lista2_valor.Add(valor2_porArea);
						double valor3_porArea = double.Parse(valor3) * Math.Round(param.AsDouble() / 10.7639);//"15 01 02 - plastic packaging" X AREA
						lista3_valor.Add(valor3_porArea);
						double valor4_porArea = double.Parse(valor4) * Math.Round(param.AsDouble() / 10.7639);//"15 01 03 - wooden packaging" X AREA
						lista4_valor.Add(valor4_porArea);
						double valor5_porArea = double.Parse(valor5) * Math.Round(param.AsDouble() / 10.7639);//"15 01 04 - metallic packaging" X AREA
						lista5_valor.Add(valor5_porArea);
						double valor6_porArea = double.Parse(valor6) * Math.Round(param.AsDouble() / 10.7639);//"15 01 06 - mixed packaging"]  X AREA
						lista6_valor.Add(valor6_porArea);
						double valor7_porArea = double.Parse(valor7) * Math.Round(param.AsDouble() / 10.7639);//"17 01 01 - concrete"  X AREA
						lista7_valor.Add(valor7_porArea);
						double valor8_porArea = double.Parse(valor8) * Math.Round(param.AsDouble() / 10.7639);//"17 02 01 - wood"  X AREA
						lista8_valor.Add(valor8_porArea);
						double valor9_porArea = double.Parse(valor9) * Math.Round(param.AsDouble() / 10.7639);//"17 02 03 - plastic"  X AREA
						lista9_valor.Add(valor9_porArea);
						double valor10_porArea = double.Parse(valor10) * Math.Round(param.AsDouble() / 10.7639);//"17 04 05 - iron and steel"  X AREA
						lista10_valor.Add(valor10_porArea);
						double valor11_porArea = double.Parse(valor11) * Math.Round(param.AsDouble() / 10.7639);//"17 09 04 - mixed"  X AREA
						lista11_valor.Add(valor11_porArea);
						#endregion

						double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
						valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
						lista_sumaTotal_valor_porArea_Forjado.Add(sumaTotal_valor_porArea);
					}
				}
				lista_Dictionarios.Add(data_forjado);
			}
            if (structuralColumns.Count() != 0)
            {
				foreach (Element sc in structuralColumns)
				{
					if ((sc.LookupParameter("Material estructural").AsValueString() == data_pilar_hormigon["Código"]))
					{
						Parameter param = sc.LookupParameter("Volumen");
						volumenAcumulado_PilarHormigon = volumenAcumulado_PilarHormigon + Math.Round(param.AsDouble() / 35.3147, 4);
						double numero = Math.Round(param.AsDouble() / 35.3147, 4);

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

						double valor2_porVolumen = double.Parse(valor2) * Math.Round(param.AsDouble() / 35.3147, 4);//07 07 01 - aqueous washing liquids X VOLUMEN
						lista2_valor.Add(valor2_porVolumen);
						double valor3_porVolumen = double.Parse(valor3) * Math.Round(param.AsDouble() / 35.3147, 4);//"15 01 02 - plastic packaging" X VOLUMEN
						lista3_valor.Add(valor3_porVolumen);
						double valor4_porVolumen = double.Parse(valor4) * Math.Round(param.AsDouble() / 35.3147, 4);//"15 01 03 - wooden packaging" X VOLUMEN
						lista4_valor.Add(valor4_porVolumen);
						double valor5_porVolumen = double.Parse(valor5) * Math.Round(param.AsDouble() / 35.3147, 4);//"15 01 04 - metallic packaging" X VOLUMEN
						lista5_valor.Add(valor5_porVolumen);
						double valor6_porVolumen = double.Parse(valor6) * Math.Round(param.AsDouble() / 35.3147, 4);//"15 01 06 - mixed packaging"]  X VOLUMEN
						lista6_valor.Add(valor6_porVolumen);
						double valor7_porVolumen = double.Parse(valor7) * Math.Round(param.AsDouble() / 35.3147, 4);//"17 01 01 - concrete"  X VOLUMEN
						lista7_valor.Add(valor7_porVolumen);
						double valor8_porVolumen = double.Parse(valor8) * Math.Round(param.AsDouble() / 35.3147, 4);//"17 02 01 - wood"  X VOLUMEN
						lista8_valor.Add(valor8_porVolumen);
						double valor9_porVolumen = double.Parse(valor9) * Math.Round(param.AsDouble() / 35.3147, 4);//"17 02 03 - plastic"  X VOLUMEN
						lista9_valor.Add(valor9_porVolumen);
						double valor10_porVolumen = double.Parse(valor10) * Math.Round(param.AsDouble() / 35.3147, 4);//"17 04 05 - iron and steel"  X VOLUMEN
						lista10_valor.Add(valor10_porVolumen);
						double valor11_porVolumen = double.Parse(valor11) * Math.Round(param.AsDouble() / 35.3147, 4);//"17 09 04 - mixed"  X VOLUMEN
						lista11_valor.Add(valor11_porVolumen);

						double sumaTotal_valor_porVolumen = valor2_porVolumen + valor3_porVolumen + valor4_porVolumen + valor5_porVolumen + valor6_porVolumen + valor7_porVolumen +
											valor8_porVolumen + valor9_porVolumen + valor10_porVolumen + valor11_porVolumen;
						lista_sumaTotal_valor_porVolumen_PilarConcreto.Add(sumaTotal_valor_porVolumen);
					}
				}
				lista_Dictionarios.Add(data_pilar_hormigon);
			}
            if (floors.Count() != 0)
            {
				foreach (Element sc in floors)
				{
					ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
					Parameter pamType = type.LookupParameter("Material estructural");
					if ((pamType.AsValueString() == data_floors_concreto["Código"]))
					{
						Parameter area = sc.LookupParameter("Área");
						areaAcumulada_Concreto = areaAcumulada_Concreto + Math.Round(area.AsDouble() / 10.7639, 4);

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

						double valor2_porArea = double.Parse(valor2) * Math.Round(area.AsDouble() / 10.7639, 4);//07 07 01 - aqueous washing liquids X AREA
						lista2_valor.Add(valor2_porArea);
						double valor3_porArea = double.Parse(valor3) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 02 - plastic packaging" X AREA
						lista3_valor.Add(valor3_porArea);
						double valor4_porArea = double.Parse(valor4) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 03 - wooden packaging" X AREA
						lista4_valor.Add(valor4_porArea);
						double valor5_porArea = double.Parse(valor5) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 04 - metallic packaging" X AREA
						lista5_valor.Add(valor5_porArea);
						double valor6_porArea = double.Parse(valor6) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 06 - mixed packaging"]  X AREA
						lista6_valor.Add(valor6_porArea);
						double valor7_porArea = double.Parse(valor7) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 01 01 - concrete"  X AREA
						lista7_valor.Add(valor7_porArea);
						double valor8_porArea = double.Parse(valor8) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 02 01 - wood"  X AREA
						lista8_valor.Add(valor8_porArea);
						double valor9_porArea = double.Parse(valor9) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 02 03 - plastic"  X AREA
						lista9_valor.Add(valor9_porArea);
						double valor10_porArea = double.Parse(valor10) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 04 05 - iron and steel"  X AREA
						lista10_valor.Add(valor10_porArea);
						double valor11_porArea = double.Parse(valor11) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 09 04 - mixed"  X AREA
						lista11_valor.Add(valor11_porArea);

						double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
											valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
						lista_sumaTotal_valor_porArea_Concreto.Add(sumaTotal_valor_porArea);
					}
				}
				lista_Dictionarios.Add(data_floors_concreto);
			}
            if (strFoundation.Count() != 0)
            {
				foreach (Element sc in strFoundation)
				{
					ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
					Parameter pamType = type.LookupParameter("Material estructural");
					if ((pamType.AsValueString() == data_Cimentaciones["Código"]))
					{

						Parameter param = sc.LookupParameter("Volumen");
						volumenAcumulado_Cimentaciones = volumenAcumulado_Cimentaciones + Math.Round(param.AsDouble() / 35.3147, 4);

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

						Parameter volumen = sc.LookupParameter("Volumen");

						double valor2_porVolumen = double.Parse(valor2) * Math.Round(volumen.AsDouble() / 35.3147, 4);//07 07 01 - aqueous washing liquids X VOLUMEN
						lista2_valor.Add(valor2_porVolumen);
						double valor3_porVolumen = double.Parse(valor3) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"15 01 02 - plastic packaging" X VOLUMEN
						lista3_valor.Add(valor3_porVolumen);
						double valor4_porVolumen = double.Parse(valor4) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"15 01 03 - wooden packaging" X VOLUMEN
						lista4_valor.Add(valor4_porVolumen);
						double valor5_porVolumen = double.Parse(valor5) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"15 01 04 - metallic packaging" X VOLUMEN
						lista5_valor.Add(valor5_porVolumen);
						double valor6_porVolumen = double.Parse(valor6) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"15 01 06 - mixed packaging"]  X VOLUMEN
						lista6_valor.Add(valor6_porVolumen);
						double valor7_porVolumen = double.Parse(valor7) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 01 01 - concrete"  X VOLUMEN
						lista7_valor.Add(valor7_porVolumen);
						double valor8_porVolumen = double.Parse(valor8) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 02 01 - wood"  X VOLUMEN
						lista8_valor.Add(valor8_porVolumen);
						double valor9_porVolumen = double.Parse(valor9) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 02 03 - plastic"  X VOLUMEN
						lista9_valor.Add(valor9_porVolumen);
						double valor10_porVolumen = double.Parse(valor10) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 04 05 - iron and steel"  X VOLUMEN
						lista10_valor.Add(valor10_porVolumen);
						double valor11_porVolumen = double.Parse(valor11) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 09 04 - mixed"  X VOLUMEN
						lista11_valor.Add(valor11_porVolumen);

						double sumaTotal_valor_porVolumen = valor2_porVolumen + valor3_porVolumen + valor4_porVolumen + valor5_porVolumen + valor6_porVolumen + valor7_porVolumen +
											valor8_porVolumen + valor9_porVolumen + valor10_porVolumen + valor11_porVolumen;
						lista_sumaTotal_valor_porVolumen_Cimentaciones.Add(sumaTotal_valor_porVolumen);
					}
				}
				lista_Dictionarios.Add(data_Cimentaciones);
			}
            if (floors.Count() != 0)
            {
				foreach (Element sc in floors)
				{
					ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
					Parameter pamType = type.LookupParameter("Material estructural");
					if ((pamType.AsValueString() == data_ConcretoDeck["Código"]))
					{

						Parameter param = sc.LookupParameter("Área");
						areaAcumulada_ConcretoDeck = areaAcumulada_ConcretoDeck + Math.Round(param.AsDouble() / 10.7639, 4);

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

						Parameter area = sc.LookupParameter("Área");

						double valor2_porArea = double.Parse(valor2) * Math.Round(area.AsDouble() / 10.7639, 4);//07 07 01 - aqueous washing liquids X AREA
						lista2_valor.Add(valor2_porArea);
						double valor3_porArea = double.Parse(valor3) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 02 - plastic packaging" X AREA
						lista3_valor.Add(valor3_porArea);
						double valor4_porArea = double.Parse(valor4) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 03 - wooden packaging" X AREA
						lista4_valor.Add(valor4_porArea);
						double valor5_porArea = double.Parse(valor5) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 04 - metallic packaging" X AREA
						lista5_valor.Add(valor5_porArea);
						double valor6_porArea = double.Parse(valor6) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 06 - mixed packaging"]  X AREA
						lista6_valor.Add(valor6_porArea);
						double valor7_porArea = double.Parse(valor7) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 01 01 - concrete"  X AREA
						lista7_valor.Add(valor7_porArea);
						double valor8_porArea = double.Parse(valor8) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 02 01 - wood"  X AREA
						lista8_valor.Add(valor8_porArea);
						double valor9_porArea = double.Parse(valor9) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 02 03 - plastic"  X AREA
						lista9_valor.Add(valor9_porArea);
						double valor10_porArea = double.Parse(valor10) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 04 05 - iron and steel"  X AREA
						lista10_valor.Add(valor10_porArea);
						double valor11_porArea = double.Parse(valor11) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 09 04 - mixed"  X AREA
						lista11_valor.Add(valor11_porArea);

						double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
											valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
						lista_sumaTotal_valor_porArea_ConcretoDeck.Add(sumaTotal_valor_porArea);
					}
				}
				lista_Dictionarios.Add(data_ConcretoDeck);
			}
            if (strFramming.Count() != 0)
            {
				foreach (Element sc in strFramming)
				{
					if ((sc.LookupParameter("Material estructural").AsValueString() == data_Droppedbeam["Código"]))
					{

						Parameter param = sc.LookupParameter("Volumen");
						volumenAcumulado_Droppedbeam = volumenAcumulado_Droppedbeam + Math.Round(param.AsDouble() / 35.3147, 4);

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

						Parameter volumen = sc.LookupParameter("Volumen");

						double valor2_porVolumen = double.Parse(valor2) * Math.Round(volumen.AsDouble() / 35.3147, 4);//07 07 01 - aqueous washing liquids X VOLUMEN
						lista2_valor.Add(valor2_porVolumen);
						double valor3_porVolumen = double.Parse(valor3) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"15 01 02 - plastic packaging" X VOLUMEN
						lista3_valor.Add(valor3_porVolumen);
						double valor4_porVolumen = double.Parse(valor4) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"15 01 03 - wooden packaging" X VOLUMEN
						lista4_valor.Add(valor4_porVolumen);
						double valor5_porVolumen = double.Parse(valor5) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"15 01 04 - metallic packaging" X VOLUMEN
						lista5_valor.Add(valor5_porVolumen);
						double valor6_porVolumen = double.Parse(valor6) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"15 01 06 - mixed packaging"]  X VOLUMEN
						lista6_valor.Add(valor6_porVolumen);
						double valor7_porVolumen = double.Parse(valor7) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 01 01 - concrete"  X VOLUMEN
						lista7_valor.Add(valor7_porVolumen);
						double valor8_porVolumen = double.Parse(valor8) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 02 01 - wood"  X VOLUMEN
						lista8_valor.Add(valor8_porVolumen);
						double valor9_porVolumen = double.Parse(valor9) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 02 03 - plastic"  X VOLUMEN
						lista9_valor.Add(valor9_porVolumen);
						double valor10_porVolumen = double.Parse(valor10) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 04 05 - iron and steel"  X VOLUMEN
						lista10_valor.Add(valor10_porVolumen);
						double valor11_porVolumen = double.Parse(valor11) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 09 04 - mixed"  X VOLUMEN
						lista11_valor.Add(valor11_porVolumen);

						double sumaTotal_valor_porVolumen = valor2_porVolumen + valor3_porVolumen + valor4_porVolumen + valor5_porVolumen + valor6_porVolumen + valor7_porVolumen +
											valor8_porVolumen + valor9_porVolumen + valor10_porVolumen + valor11_porVolumen;
						lista_sumaTotal_valor_porArea_Droppedbeam.Add(sumaTotal_valor_porVolumen);
					}
				}
				lista_Dictionarios.Add(data_Droppedbeam);
			}
            if (floors.Count() != 0)
            {
				foreach (Element sc in floors)
				{
					ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
					Parameter pamType = type.LookupParameter("Material estructural");
					if ((pamType.AsValueString() == data_ConcreteSlab["Código"]))
					{

						Parameter param = sc.LookupParameter("Área");
						areaAcumulada_ConcreteSlab = areaAcumulada_ConcreteSlab + Math.Round(param.AsDouble() / 10.7639, 4);

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

						Parameter area = sc.LookupParameter("Área");

						double valor2_porArea = double.Parse(valor2) * Math.Round(area.AsDouble() / 10.7639, 4);//07 07 01 - aqueous washing liquids X AREA
						lista2_valor.Add(valor2_porArea);
						double valor3_porArea = double.Parse(valor3) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 02 - plastic packaging" X AREA
						lista3_valor.Add(valor3_porArea);
						double valor4_porArea = double.Parse(valor4) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 03 - wooden packaging" X AREA
						lista4_valor.Add(valor4_porArea);
						double valor5_porArea = double.Parse(valor5) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 04 - metallic packaging" X AREA
						lista5_valor.Add(valor5_porArea);
						double valor6_porArea = double.Parse(valor6) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 06 - mixed packaging"]  X AREA
						lista6_valor.Add(valor6_porArea);
						double valor7_porArea = double.Parse(valor7) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 01 01 - concrete"  X AREA
						lista7_valor.Add(valor7_porArea);
						double valor8_porArea = double.Parse(valor8) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 02 01 - wood"  X AREA
						lista8_valor.Add(valor8_porArea);
						double valor9_porArea = double.Parse(valor9) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 02 03 - plastic"  X AREA
						lista9_valor.Add(valor9_porArea);
						double valor10_porArea = double.Parse(valor10) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 04 05 - iron and steel"  X AREA
						lista10_valor.Add(valor10_porArea);
						double valor11_porArea = double.Parse(valor11) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 09 04 - mixed"  X AREA
						lista11_valor.Add(valor11_porArea);

						double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
											valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
						lista_sumaTotal_valor_porArea_ConcreteSlab.Add(sumaTotal_valor_porArea);

					}
				}
				lista_Dictionarios.Add(data_ConcreteSlab);
			}
            if (strFramming.Count() != 0)
            {
				foreach (Element sc in strFramming)
				{
					if ((sc.LookupParameter("Material estructural").AsValueString() == data_Beamembbeded["Código"]))
					{

						Parameter param = sc.LookupParameter("Volumen");
						volumenAcumulado_Beamembbededm = volumenAcumulado_Beamembbededm + Math.Round(param.AsDouble() / 35.3147, 4);

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

						Parameter volumen = sc.LookupParameter("Volumen");

						double valor2_porVolumen = double.Parse(valor2) * Math.Round(volumen.AsDouble() / 35.3147, 4);//07 07 01 - aqueous washing liquids X VOLUMEN
						lista2_valor.Add(valor2_porVolumen);
						double valor3_porVolumen = double.Parse(valor3) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"15 01 02 - plastic packaging" X VOLUMEN
						lista3_valor.Add(valor3_porVolumen);
						double valor4_porVolumen = double.Parse(valor4) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"15 01 03 - wooden packaging" X VOLUMEN
						lista4_valor.Add(valor4_porVolumen);
						double valor5_porVolumen = double.Parse(valor5) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"15 01 04 - metallic packaging" X VOLUMEN
						lista5_valor.Add(valor5_porVolumen);
						double valor6_porVolumen = double.Parse(valor6) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"15 01 06 - mixed packaging"]  X VOLUMEN
						lista6_valor.Add(valor6_porVolumen);
						double valor7_porVolumen = double.Parse(valor7) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 01 01 - concrete"  X VOLUMEN
						lista7_valor.Add(valor7_porVolumen);
						double valor8_porVolumen = double.Parse(valor8) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 02 01 - wood"  X VOLUMEN
						lista8_valor.Add(valor8_porVolumen);
						double valor9_porVolumen = double.Parse(valor9) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 02 03 - plastic"  X VOLUMEN
						lista9_valor.Add(valor9_porVolumen);
						double valor10_porVolumen = double.Parse(valor10) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 04 05 - iron and steel"  X VOLUMEN
						lista10_valor.Add(valor10_porVolumen);
						double valor11_porVolumen = double.Parse(valor11) * Math.Round(volumen.AsDouble() / 35.3147, 4);//"17 09 04 - mixed"  X VOLUMEN
						lista11_valor.Add(valor11_porVolumen);

						double sumaTotal_valor_porVolumen = valor2_porVolumen + valor3_porVolumen + valor4_porVolumen + valor5_porVolumen + valor6_porVolumen + valor7_porVolumen +
											valor8_porVolumen + valor9_porVolumen + valor10_porVolumen + valor11_porVolumen;
						lista_sumaTotal_valor_porArea_Beamembbededm.Add(sumaTotal_valor_porVolumen);
					}
				}
				lista_Dictionarios.Add(data_Beamembbeded);
			}
            if (floors.Count() != 0)
            {
				foreach (Element sc in floors)
				{
					ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
					Parameter pamType = type.LookupParameter("Material estructural");
					if ((pamType.AsValueString() == data_ConcreteInclinedSlab["Código"]))
					{

						Parameter param = sc.LookupParameter("Área");
						areaAcumulada_ConcreteInclinedSla = areaAcumulada_ConcreteInclinedSla + Math.Round(param.AsDouble() / 10.7639, 4);

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

						Parameter area = sc.LookupParameter("Área");

						double valor2_porArea = double.Parse(valor2) * Math.Round(area.AsDouble() / 10.7639, 4);//07 07 01 - aqueous washing liquids X AREA
						lista2_valor.Add(valor2_porArea);
						double valor3_porArea = double.Parse(valor3) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 02 - plastic packaging" X AREA
						lista3_valor.Add(valor3_porArea);
						double valor4_porArea = double.Parse(valor4) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 03 - wooden packaging" X AREA
						lista4_valor.Add(valor4_porArea);
						double valor5_porArea = double.Parse(valor5) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 04 - metallic packaging" X AREA
						lista5_valor.Add(valor5_porArea);
						double valor6_porArea = double.Parse(valor6) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 06 - mixed packaging"]  X AREA
						lista6_valor.Add(valor6_porArea);
						double valor7_porArea = double.Parse(valor7) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 01 01 - concrete"  X AREA
						lista7_valor.Add(valor7_porArea);
						double valor8_porArea = double.Parse(valor8) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 02 01 - wood"  X AREA
						lista8_valor.Add(valor8_porArea);
						double valor9_porArea = double.Parse(valor9) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 02 03 - plastic"  X AREA
						lista9_valor.Add(valor9_porArea);
						double valor10_porArea = double.Parse(valor10) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 04 05 - iron and steel"  X AREA
						lista10_valor.Add(valor10_porArea);
						double valor11_porArea = double.Parse(valor11) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 09 04 - mixed"  X AREA
						lista11_valor.Add(valor11_porArea);

						double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
											valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
						lista_sumaTotal_valor_porArea_ConcreteInclinedSlab.Add(sumaTotal_valor_porArea);

					}
				}
				lista_Dictionarios.Add(data_ConcreteInclinedSlab);
			}
            if (walls.Count() != 0)
            {
				foreach (Element sc in walls)
				{
					ElementType type = doc.GetElement(sc.GetTypeId()) as ElementType;
					Parameter pamType = type.LookupParameter("Material estructural");
					string valorSALIDA = pamType.AsValueString();
					if ((pamType.AsValueString() == data_walls["Código"]) || (pamType.AsValueString() == data_Cimentaciones["Código"]) )
					{

						Parameter param = sc.LookupParameter("Volumen");
						volumenAcumulado_walls_Concrete = volumenAcumulado_walls_Concrete + Math.Round(param.AsDouble() / 35.3147, 4);

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

						Parameter area = sc.LookupParameter("Área");

						double valor2_porArea = double.Parse(valor2) * Math.Round(area.AsDouble() / 10.7639, 4);//07 07 01 - aqueous washing liquids X AREA
						lista2_valor.Add(valor2_porArea);
						double valor3_porArea = double.Parse(valor3) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 02 - plastic packaging" X AREA
						lista3_valor.Add(valor3_porArea);
						double valor4_porArea = double.Parse(valor4) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 03 - wooden packaging" X AREA
						lista4_valor.Add(valor4_porArea);
						double valor5_porArea = double.Parse(valor5) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 04 - metallic packaging" X AREA
						lista5_valor.Add(valor5_porArea);
						double valor6_porArea = double.Parse(valor6) * Math.Round(area.AsDouble() / 10.7639, 4);//"15 01 06 - mixed packaging"]  X AREA
						lista6_valor.Add(valor6_porArea);
						double valor7_porArea = double.Parse(valor7) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 01 01 - concrete"  X AREA
						lista7_valor.Add(valor7_porArea);
						double valor8_porArea = double.Parse(valor8) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 02 01 - wood"  X AREA
						lista8_valor.Add(valor8_porArea);
						double valor9_porArea = double.Parse(valor9) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 02 03 - plastic"  X AREA
						lista9_valor.Add(valor9_porArea);
						double valor10_porArea = double.Parse(valor10) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 04 05 - iron and steel"  X AREA
						lista10_valor.Add(valor10_porArea);
						double valor11_porArea = double.Parse(valor11) * Math.Round(area.AsDouble() / 10.7639, 4);//"17 09 04 - mixed"  X AREA
						lista11_valor.Add(valor11_porArea);

						double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
											valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
						lista_sumaTotal_valor_porArea_walls_Concrete.Add(sumaTotal_valor_porArea);
					}
				}
				lista_Dictionarios.Add(data_walls);
			}



            #endregion

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

			#endregion

			if (floors.Count() != 0)// SI Sí existe
            {
				for (int i = 0; i < lista_sumaTotal_valor_porArea_Forjado.Count(); i++)
				{
					desperdicioForjado = desperdicioForjado + lista_sumaTotal_valor_porArea_Forjado[i];// para Forjados
				}
				lista_desperdicios.Add(desperdicioForjado);
			}
            if (structuralColumns.Count() != 0)
            {
				for (int i = 0; i < lista_sumaTotal_valor_porVolumen_PilarConcreto.Count(); i++)
				{
					desperdicioPilarHormigon = desperdicioPilarHormigon + lista_sumaTotal_valor_porVolumen_PilarConcreto[i];// para pilar de hormigon
				}
				lista_desperdicios.Add(desperdicioPilarHormigon);
			}
            if (floors.Count() != 0)
            {
				for (int i = 0; i < lista_sumaTotal_valor_porArea_Concreto.Count(); i++)
				{
					desperdicioConcreto = desperdicioConcreto + lista_sumaTotal_valor_porArea_Concreto[i];// para Concreto
				}
				lista_desperdicios.Add(desperdicioConcreto);
			}
            if (strFoundation.Count() != 0)
            {
				for (int i = 0; i < lista_sumaTotal_valor_porVolumen_Cimentaciones.Count(); i++)
				{
					desperdicioCimentacion = desperdicioCimentacion + lista_sumaTotal_valor_porVolumen_Cimentaciones[i];
				}
				lista_desperdicios.Add(desperdicioCimentacion);
			}
            if (floors.Count() != 0)
            {

				for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcretoDeck.Count(); i++)
				{
					desperdicioConcretoDeck = desperdicioConcretoDeck + lista_sumaTotal_valor_porArea_ConcretoDeck[i];// para Concreto
				}
				lista_desperdicios.Add(desperdicioConcretoDeck);
			}
            if (strFramming.Count() != 0)
            {
				for (int i = 0; i < lista_sumaTotal_valor_porArea_Droppedbeam.Count(); i++)
				{
					desperdicio_Droppedbeam = desperdicio_Droppedbeam + lista_sumaTotal_valor_porArea_Droppedbeam[i];// para Concreto
				}
				lista_desperdicios.Add(desperdicio_Droppedbeam);
			}
            if (floors.Count() != 0)
            {

				for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcreteSlab.Count(); i++)
				{
					desperdicio_ConcreteSlab = desperdicio_ConcreteSlab + lista_sumaTotal_valor_porArea_ConcreteSlab[i];// para Concreto
				}
				lista_desperdicios.Add(desperdicio_ConcreteSlab);
			}
            if (strFramming.Count() != 0)
            {
				for (int i = 0; i < lista_sumaTotal_valor_porArea_Beamembbededm.Count(); i++)
				{
					desperdicio_Beamembbededm = desperdicio_Beamembbededm + lista_sumaTotal_valor_porArea_Beamembbededm[i];// para Concreto
				}
				lista_desperdicios.Add(desperdicio_Beamembbededm);
			}
            if (floors.Count() != 0)
            {
				for (int i = 0; i < lista_sumaTotal_valor_porArea_ConcreteInclinedSlab.Count(); i++)
				{
					desperdicio_ConcreteInclinedSlab = desperdicio_ConcreteInclinedSlab + lista_sumaTotal_valor_porArea_ConcreteInclinedSlab[i];// para Concreto
				}
				lista_desperdicios.Add(desperdicio_ConcreteInclinedSlab);
			}
            if (walls.Count() != 0)
            {
				for (int i = 0; i < lista_sumaTotal_valor_porArea_walls_Concrete.Count(); i++)
				{
					desperdicio_walls_Concrete = desperdicio_walls_Concrete + lista_sumaTotal_valor_porArea_walls_Concrete[i];// para Concreto
				}
				lista_desperdicios.Add(desperdicio_walls_Concrete);
			}


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
			string mensaje = "Código LER     |     RCD (m3) " + Environment.NewLine + Environment.NewLine;
			for (int i = 0; i < lista_Dictionarios.Count(); i++)
            {
				mensaje = mensaje + lista_Dictionarios[i]["Structural element"] + " / " + lista_Dictionarios[i]["Código"] + "    =    " + lista_desperdicios[i].ToString() + Environment.NewLine;

			}
			mensaje = mensaje + Environment.NewLine + Environment.NewLine + "Total    =     " + desperdicioTotal.ToString();


			TaskDialog.Show("CDW Estimación", mensaje);




			#region Obtener Excel
			var bankAccounts = new List<Account>();


			// aqui solo los elementos que se usan . No deberian ser todos.
			for (int i = 0; i < lista_Dictionarios.Count(); i++)
            {
				Account cuenta = new Account
				{
					ID = lista_Dictionarios[i]["Structural element"] + " / " + lista_Dictionarios[i]["Código"],
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
									  ID = "07 07 01 - aqueous washing liquids",
									  Balance = valor2_final//valor_porAres solo hormigon
				                },
					new Account {
									  ID = "15 01 02 - plastic packaging",
									  Balance = valor3_final// valor_porArea solo suelos por defecto
				                },
					new Account {
									ID = "15 01 03 - wooden packaging",
									Balance = valor4_final
					},
					new Account {
									ID = "15 01 04 - metallic packaging",
									Balance = valor5_final
					},
					new Account {
									ID = "15 01 06 - mixed packaging",
									Balance = valor6_final
					},
					new Account {
									ID = "17 01 01 - concrete",
									Balance = valor7_final
					},
					new Account {
									ID = "17 02 01 - wood",
									Balance = valor8_final
					},
					new Account {
									ID = "17 02 03 - plastic",
									Balance = valor9_final
					},
					new Account {
									ID = "17 04 05 - iron and steel",
									Balance = valor10_final
					},
					new Account {
									ID = "17 09 04 - mixed",
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

}
