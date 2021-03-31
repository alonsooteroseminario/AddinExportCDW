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
            //Get application and document objects
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            Document doc = uiApp.ActiveUIDocument.Document;
            Application app = uiApp.Application;

            // Get Active View
            View activeView = uidoc.ActiveView;

            string ruta = App.ExecutingAssemblyPath;

			#region Macro Code

			#region Colectores de Elementos

			ElementClassFilter elemFilter = new ElementClassFilter(typeof(Floor));
			ElementClassFilter elemFilter2 = new ElementClassFilter(typeof(FamilyInstance));

			// Create a category filter for Ducts
			ElementCategoryFilter DUCategoryfilter = new ElementCategoryFilter(BuiltInCategory.OST_Floors);
			ElementCategoryFilter DUCategoryfilter2 = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);

			// Create a logic And filter for all MechanicalEquipment Family
			LogicalAndFilter DUInstancesFilter = new LogicalAndFilter(elemFilter, DUCategoryfilter);
			LogicalAndFilter DUInstancesFilter2 = new LogicalAndFilter(elemFilter2, DUCategoryfilter2);

			// Apply the filter to the elements in the active document
			FilteredElementCollector DUcoll = new FilteredElementCollector(doc, activeView.Id);
			IList<Element> floors = DUcoll.WherePasses(DUInstancesFilter).ToElements();

			FilteredElementCollector DUcoll2 = new FilteredElementCollector(doc, activeView.Id);
			IList<Element> structuralColumns = DUcoll2.WherePasses(DUInstancesFilter2).ToElements();

			#endregion

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


			#endregion

			#region relacionar el nombre familia con el codigo del material

			#region Separacion de Elementos Forjado - 30 cm y Columns 300 x 300 mm


			List<Element> floors_Forjados = new List<Element>();
			List<Element> structuralColumns_PilarHormigon = new List<Element>();

			foreach (Element f in floors)
			{
				ElementType type = doc.GetElement(f.GetTypeId()) as ElementType;
				Parameter pamType = type.LookupParameter("Material estructural");
				if ( (f.Name.ToString() == data_forjado["Structural element"]) ||
					(pamType.AsValueString() == data_forjado["Código"]) )
				{
					floors_Forjados.Add(f);
				}
				//if (pamType.AsValueString() == data_forjado["Código"])
				//{
				//	codigo1 = pamType.AsValueString();
				//}
			}

			foreach (Element sc in structuralColumns)
			{
				if ( (sc.Name.ToString() == data_pilar_hormigon["Structural element"]) ||
					(sc.LookupParameter("Material estructural").AsValueString() == data_pilar_hormigon["Código"]) )
				{
					structuralColumns_PilarHormigon.Add(sc);
				}
				//if (sc.LookupParameter("Material estructural").AsValueString() == data_pilar_hormigon["Código"])
				//{
				//	codigo2 = sc.LookupParameter("Material estructural").AsValueString();
				//}
			}
			
			#endregion

			#region Obtener Materiales

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

			List<string> lista_Areas = new List<string>();
			List<string> lista_Volumenes = new List<string>();

			double areaAcumulada = 0;
			double volumenAcumulado = 0;

			foreach (Element f in floors_Forjados)
			{
				//AQUI CAMBIAR IDIOMA A ESPAÑOL "Area" por "Área"
				Parameter param = f.LookupParameter("Área");
				lista_Areas.Add(param.AsValueString());
				//				TaskDialog.Show("msg", param.AsValueString());
				areaAcumulada = areaAcumulada + param.AsDouble();
				//				TaskDialog.Show("area", (Math.Round(areaAcumulada/10.7639)).ToString());
			}

			foreach (Element sc in structuralColumns_PilarHormigon)
			{
				//AQUI CAMBIAR IDIOMA A ESPAÑOL "Volume" por "Volumen"
				Parameter param = sc.LookupParameter("Volumen");
				lista_Volumenes.Add(param.AsValueString());
				//				TaskDialog.Show("msg", param.AsValueString());
				volumenAcumulado = volumenAcumulado + param.AsDouble();
				//				TaskDialog.Show("volumen", (Math.Round(volumenAcumulado/35.3147)).ToString());
			}

			//			TaskDialog.Show("areas y volumenes", areaAcumulada.ToString() + Environment.NewLine + volumenAcumulado.ToString());
			//			
			#region tasdialog

			//string mensaje2 = "";
			//for (int i = 0; i < lista_Areas.Count(); i++)
			//{
			//	mensaje2 = mensaje2 + lista_Areas[i] + Environment.NewLine;
			//}
			//for (int i = 0; i < lista_Volumenes.Count(); i++)
			//{
			//	mensaje2 = mensaje2 + Environment.NewLine + lista_Volumenes[i] + Environment.NewLine;
			//}
			//			TaskDialog.Show("Mensaje de Prueba 2", mensaje2);
			#endregion

			#endregion

			List<double> lista_sumaTotal_valor_porArea = new List<double>();
			List<double> lista_sumaTotal_valor_porVolumen = new List<double>();

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

				key0 = data_forjado.FirstOrDefault(x => x.Value == valor0).Key; //07 07 01 - aqueous washing liquids
				key1 = data_forjado.FirstOrDefault(x => x.Value == valor1).Key;
				key2 = data_forjado.FirstOrDefault(x => x.Value == valor2).Key;
				key3 = data_forjado.FirstOrDefault(x => x.Value == valor3).Key;
				key4 = data_forjado.FirstOrDefault(x => x.Value == valor4).Key;
				key5 = data_forjado.FirstOrDefault(x => x.Value == valor5).Key;
				key6 = data_forjado.FirstOrDefault(x => x.Value == valor6).Key;
				key7 = data_forjado.FirstOrDefault(x => x.Value == valor7).Key;
				key8 = data_forjado.FirstOrDefault(x => x.Value == valor8).Key;
				key9 = data_forjado.FirstOrDefault(x => x.Value == valor9).Key;
				key10 = data_forjado.FirstOrDefault(x => x.Value == valor10).Key;
				key11 = data_forjado.FirstOrDefault(x => x.Value == valor11).Key;

				Element f = floors_Forjados[i];
				Parameter area = f.LookupParameter("Área");

				double valor2_porArea = double.Parse(valor2) * Math.Round(area.AsDouble() / 10.7639);
				lista2_valor.Add(valor2_porArea);

				double valor3_porArea = double.Parse(valor3) * Math.Round(area.AsDouble() / 10.7639);
				lista3_valor.Add(valor3_porArea);

				double valor4_porArea = double.Parse(valor4) * Math.Round(area.AsDouble() / 10.7639);
				lista4_valor.Add(valor4_porArea);

				double valor5_porArea = double.Parse(valor5) * Math.Round(area.AsDouble() / 10.7639);
				lista5_valor.Add(valor5_porArea);

				double valor6_porArea = double.Parse(valor6) * Math.Round(area.AsDouble() / 10.7639);
				lista6_valor.Add(valor6_porArea);

				double valor7_porArea = double.Parse(valor7) * Math.Round(area.AsDouble() / 10.7639);
				lista7_valor.Add(valor7_porArea);

				double valor8_porArea = double.Parse(valor8) * Math.Round(area.AsDouble() / 10.7639);
				lista8_valor.Add(valor8_porArea);

				double valor9_porArea = double.Parse(valor9) * Math.Round(area.AsDouble() / 10.7639);
				lista9_valor.Add(valor9_porArea);

				double valor10_porArea = double.Parse(valor10) * Math.Round(area.AsDouble() / 10.7639);
				lista10_valor.Add(valor10_porArea);

				double valor11_porArea = double.Parse(valor11) * Math.Round(area.AsDouble() / 10.7639);
				lista11_valor.Add(valor11_porArea);

				double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
									valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
				lista_sumaTotal_valor_porArea.Add(sumaTotal_valor_porArea);


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

				double valor2_porArea = double.Parse(valor2) * Math.Round(volumen.AsDouble() / 35.3147);
				lista2_valor.Add(valor2_porArea);

				double valor3_porArea = double.Parse(valor3) * Math.Round(volumen.AsDouble() / 35.3147);
				lista3_valor.Add(valor3_porArea);

				double valor4_porArea = double.Parse(valor4) * Math.Round(volumen.AsDouble() / 35.3147);
				lista4_valor.Add(valor4_porArea);

				double valor5_porArea = double.Parse(valor5) * Math.Round(volumen.AsDouble() / 35.3147);
				lista5_valor.Add(valor5_porArea);

				double valor6_porArea = double.Parse(valor6) * Math.Round(volumen.AsDouble() / 35.3147);
				lista6_valor.Add(valor6_porArea);

				double valor7_porArea = double.Parse(valor7) * Math.Round(volumen.AsDouble() / 35.3147);
				lista7_valor.Add(valor7_porArea);

				double valor8_porArea = double.Parse(valor8) * Math.Round(volumen.AsDouble() / 35.3147);
				lista8_valor.Add(valor8_porArea);

				double valor9_porArea = double.Parse(valor9) * Math.Round(volumen.AsDouble() / 35.3147);
				lista9_valor.Add(valor9_porArea);

				double valor10_porArea = double.Parse(valor10) * Math.Round(volumen.AsDouble() / 35.3147);
				lista10_valor.Add(valor10_porArea);

				double valor11_porArea = double.Parse(valor11) * Math.Round(volumen.AsDouble() / 35.3147);
				lista11_valor.Add(valor11_porArea);


				double sumaTotal_valor_porVolumen = valor2_porArea + valor3_porArea + valor4_porArea + valor5_porArea + valor6_porArea + valor7_porArea +
									valor8_porArea + valor9_porArea + valor10_porArea + valor11_porArea;
				lista_sumaTotal_valor_porVolumen.Add(sumaTotal_valor_porVolumen);

			}

			#region Obtener los mismos datos del EXCEL con el dictionario

			double v1 = 0;
            for (int i = 0; i < lista_sumaTotal_valor_porArea.Count(); i++)
            {
				v1 = v1 + lista_sumaTotal_valor_porArea[i];
            }
			double v2 = 0;
            for (int i = 0; i < lista_sumaTotal_valor_porVolumen.Count(); i++)
            {
				v2 = v2 + lista_sumaTotal_valor_porVolumen[i];
            }



			double valor2_final = 0; //07 07 01 - aqueous washing liquids
			for (int i = 0; i < lista2_valor.Count(); i++)
            {
				valor2_final = valor2_final + lista2_valor[i];
            }

			double valor3_final = 0;
			for (int i = 0; i < lista3_valor.Count(); i++)
			{
				valor3_final = valor3_final + lista3_valor[i];
			}

			double valor4_final = 0;
			for (int i = 0; i < lista4_valor.Count(); i++)
			{
				valor4_final = valor4_final + lista4_valor[i];
			}

			double valor5_final = 0;
			for (int i = 0; i < lista5_valor.Count(); i++)
			{
				valor5_final = valor5_final + lista5_valor[i];
			}

			double valor6_final = 0;
			for (int i = 0; i < lista6_valor.Count(); i++)
			{
				valor6_final = valor6_final + lista6_valor[i];
			}

			double valor7_final = 0;
			for (int i = 0; i < lista7_valor.Count(); i++)
			{
				valor7_final = valor7_final + lista7_valor[i];
			}

			double valor8_final = 0;
			for (int i = 0; i < lista8_valor.Count(); i++)
			{
				valor8_final = valor8_final + lista8_valor[i];
			}

			double valor9_final = 0;
			for (int i = 0; i < lista9_valor.Count(); i++)
			{
				valor9_final = valor9_final + lista9_valor[i];
			}

			double valor10_final = 0;
			for (int i = 0; i < lista10_valor.Count(); i++)
			{
				valor10_final = valor10_final + lista10_valor[i];
			}

			double valor11_final = 0;
			for (int i = 0; i < lista11_valor.Count(); i++)
			{
				valor11_final = valor11_final + lista11_valor[i];
			}

			// Create a list of accounts.
			var bankAccounts = new List<Account> {
					new Account {
								  ID = data_forjado["Structural element"],//Hormgon
				                  Balance = v1//valor_porAres solo hormigon
				                },
					new Account {
								  ID = data_pilar_hormigon["Structural element"],//suelos por defecto
				                  Balance = v2// valor_porArea solo suelos por defecto
				                },
					new Account {
									ID = "",
									Balance = 0
					},
					new Account {
									ID = "Total",
									Balance =  (v1+v2)// valor_Area de total
					}
				};

			var bankAccounts2 = new List<Account> {
					new Account {
								  ID = key2,//Hormgon
				                  Balance = valor2_final//valor_porAres solo hormigon
				                },
					new Account {
								  ID = key3,//suelos por defecto
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
									Balance =  (v1+v2)
					}
				};

			// Display the list in an Excel spreadsheet.
			DisplayInExcel(bankAccounts, bankAccounts2);



			#endregion



			// se abre ventana con chart



			// se crean key schedule con mismas tablas de excel





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
