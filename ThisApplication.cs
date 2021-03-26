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
			string codigo1 = "";
			string codigo2 = "";

			string elemento1 = "Forjado - 30 cm";
			string elemento2 = "300 x 300 mm";

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

			#region Separacion de Elementos Forjado - 30 cm y Columns 300 x 300 mm


			List<Element> floors_Forjados = new List<Element>();
			List<Element> structuralColumns_PilarHormigon = new List<Element>();

			foreach (Element f in floors)
			{
				if (f.Name.ToString() == data_forjado["Structural element"])
				{
					floors_Forjados.Add(f);
					//					TaskDialog.Show("mensaje", f.Name.ToString() + Environment.NewLine + floors_Forjados.Count());
				}
				ElementType type = doc.GetElement(f.GetTypeId()) as ElementType;

				//CAMBIAR IDIOMA A ESPAÑOL "Structural Material" por "Material estructural"
				Parameter pamType = type.LookupParameter("Material estructural");

				if (pamType.AsValueString() == data_forjado["Código"])
				{
					//					TaskDialog.Show("si funciono", pamType.AsValueString() );
					codigo1 = pamType.AsValueString();
				}


			}

			foreach (Element sc in structuralColumns)
			{
				if (sc.Name.ToString() == data_pilar_hormigon["Structural element"])
				{
					structuralColumns_PilarHormigon.Add(sc);
					//					TaskDialog.Show("mensaje", sc.Name.ToString() + Environment.NewLine + structuralColumns_PilarHormigon.Count());
				}
				//CAMBIAR IDIOMA A ESPAÑOL "Structural Material" por "Material estructural"
				if (sc.LookupParameter("Material estructural").AsValueString() == data_pilar_hormigon["Código"])
				{
					//					TaskDialog.Show("si funciono", sc.LookupParameter("Structural Material").AsValueString());
					codigo2 = sc.LookupParameter("Material estructural").AsValueString();
				}


			}

			#region mensaje

			string mensaje = "";
			for (int i = 0; i < floors_Forjados.Count(); i++)
			{
				mensaje = mensaje + floors_Forjados[i].Name.ToString() + Environment.NewLine;
			}
			for (int i = 0; i < structuralColumns_PilarHormigon.Count(); i++)
			{

				mensaje = mensaje + Environment.NewLine + structuralColumns_PilarHormigon[i].Name.ToString() + Environment.NewLine;
			}
			//			TaskDialog.Show("Mensaje de Prueba", mensaje);
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

			string mensaje2 = "";
			for (int i = 0; i < lista_Areas.Count(); i++)
			{
				mensaje2 = mensaje2 + lista_Areas[i] + Environment.NewLine;
			}
			for (int i = 0; i < lista_Volumenes.Count(); i++)
			{
				mensaje2 = mensaje2 + Environment.NewLine + lista_Volumenes[i] + Environment.NewLine;
			}
			//			TaskDialog.Show("Mensaje de Prueba 2", mensaje2);
			#endregion
			#endregion

			#region Obtener Materiales

			FilteredElementCollector collector = new FilteredElementCollector(doc).OfClass(typeof(Material));
			IEnumerable<Material> materialsEnum = collector.ToElements().Cast<Material>();

			// 			TaskDialog.Show("materiales", materialsEnum.Count().ToString());

			var materialReturn1 = from materialElement in materialsEnum where materialElement.Name == codigo1 select materialElement;
			var materialReturn2 = from materialElement in materialsEnum where materialElement.Name == codigo2 select materialElement;

			//			TaskDialog.Show("material", materialReturn1.First().Name.ToString() + Environment.NewLine +
			//			               				materialReturn2.First().Name.ToString());

			// Obtener el material de los elementos deberian ser 2 materiales

			#endregion

			#region Obtener los mismos datos del EXCEL con el dictionario



			string mensajeFinal = "";

			if (data_forjado.ContainsValue(codigo1) && data_pilar_hormigon.ContainsValue(codigo2))
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


				double suma0 = double.Parse(valor2) + double.Parse(valor3) + double.Parse(valor4) + double.Parse(valor5) +
								double.Parse(valor6) + double.Parse(valor7) + double.Parse(valor8) + double.Parse(valor9) + double.Parse(valor10) + double.Parse(valor11);

				double resultado0 = (suma0) * Math.Round(areaAcumulada / 10.7639);


				mensajeFinal = mensajeFinal + "Elemento    |     RCD (m3) " + Environment.NewLine + Environment.NewLine
							+ data_forjado["Structural element"] + " = " + resultado0.ToString();


				double valor2_porArea = double.Parse(valor2) * Math.Round(areaAcumulada / 10.7639);
				double valor3_porArea = double.Parse(valor3) * Math.Round(areaAcumulada / 10.7639);
				double valor4_porArea = double.Parse(valor4) * Math.Round(areaAcumulada / 10.7639);
				double valor5_porArea = double.Parse(valor5) * Math.Round(areaAcumulada / 10.7639);
				double valor6_porArea = double.Parse(valor6) * Math.Round(areaAcumulada / 10.7639);
				double valor7_porArea = double.Parse(valor7) * Math.Round(areaAcumulada / 10.7639);
				double valor8_porArea = double.Parse(valor8) * Math.Round(areaAcumulada / 10.7639);
				double valor9_porArea = double.Parse(valor9) * Math.Round(areaAcumulada / 10.7639);
				double valor10_porArea = double.Parse(valor10) * Math.Round(areaAcumulada / 10.7639);
				double valor11_porArea = double.Parse(valor11) * Math.Round(areaAcumulada / 10.7639);

				#region

				//				TaskDialog.Show("final", resultado0.ToString() );

				string key0 = data_forjado.FirstOrDefault(x => x.Value == valor0).Key;
				string key1 = data_forjado.FirstOrDefault(x => x.Value == valor1).Key;
				string key2 = data_forjado.FirstOrDefault(x => x.Value == valor2).Key;
				string key3 = data_forjado.FirstOrDefault(x => x.Value == valor3).Key;
				string key4 = data_forjado.FirstOrDefault(x => x.Value == valor4).Key;
				string key5 = data_forjado.FirstOrDefault(x => x.Value == valor5).Key;
				string key6 = data_forjado.FirstOrDefault(x => x.Value == valor6).Key;
				string key7 = data_forjado.FirstOrDefault(x => x.Value == valor7).Key;
				string key8 = data_forjado.FirstOrDefault(x => x.Value == valor8).Key;
				string key9 = data_forjado.FirstOrDefault(x => x.Value == valor9).Key;
				string key10 = data_forjado.FirstOrDefault(x => x.Value == valor10).Key;
				string key11 = data_forjado.FirstOrDefault(x => x.Value == valor11).Key;
				#endregion

				string valor00 = data_pilar_hormigon["Structural element"];
				//				string valor_11 = data_pilar_hormigon["Código"];
				string valor22 = data_pilar_hormigon["07 07 01 - aqueous washing liquids"];
				string valor33 = data_pilar_hormigon["15 01 02 - plastic packaging"];
				string valor44 = data_pilar_hormigon["15 01 03 - wooden packaging"];
				string valor55 = data_pilar_hormigon["15 01 04 - metallic packaging"];
				string valor66 = data_pilar_hormigon["15 01 06 - mixed packaging"];
				string valor77 = data_pilar_hormigon["17 01 01 - concrete"];
				string valor88 = data_pilar_hormigon["17 02 01 - wood"];
				string valor99 = data_pilar_hormigon["17 02 03 - plastic"];
				string valor1010 = data_pilar_hormigon["17 04 05 - iron and steel"];
				string valor1111 = data_pilar_hormigon["17 09 04 - mixed"];

				double suma00 = double.Parse(valor22) + double.Parse(valor33) + double.Parse(valor44) + double.Parse(valor55) +
								double.Parse(valor66) + double.Parse(valor77) + double.Parse(valor88) + double.Parse(valor99) + double.Parse(valor1010) + double.Parse(valor1111);

				double resultado00 = (suma00) * Math.Round(volumenAcumulado / 35.3147);

				mensajeFinal = mensajeFinal + Environment.NewLine + data_pilar_hormigon["Structural element"] + " = " + resultado00.ToString();

				double valor22_porArea = double.Parse(valor22) * Math.Round(volumenAcumulado / 35.3147);
				double valor33_porArea = double.Parse(valor33) * Math.Round(volumenAcumulado / 35.3147);
				double valor44_porArea = double.Parse(valor44) * Math.Round(volumenAcumulado / 35.3147);
				double valor55_porArea = double.Parse(valor55) * Math.Round(volumenAcumulado / 35.3147);
				double valor66_porArea = double.Parse(valor66) * Math.Round(volumenAcumulado / 35.3147);
				double valor77_porArea = double.Parse(valor77) * Math.Round(volumenAcumulado / 35.3147);
				double valor88_porArea = double.Parse(valor88) * Math.Round(volumenAcumulado / 35.3147);
				double valor99_porArea = double.Parse(valor99) * Math.Round(volumenAcumulado / 35.3147);
				double valor1010_porArea = double.Parse(valor1010) * Math.Round(volumenAcumulado / 35.3147);
				double valor1111_porArea = double.Parse(valor1111) * Math.Round(volumenAcumulado / 35.3147);

				//				TaskDialog.Show("final", resultado00.ToString() );

				mensajeFinal = mensajeFinal + Environment.NewLine + Environment.NewLine + "Total = " + (resultado0 + resultado00).ToString();


				mensajeFinal = mensajeFinal + Environment.NewLine + Environment.NewLine +
								"Código LER     |     RCD (m3) " + Environment.NewLine + Environment.NewLine +

								key2 + " = " + (valor2_porArea + valor22_porArea).ToString() + Environment.NewLine +

								key3 + " = " + (valor3_porArea + valor33_porArea).ToString() + Environment.NewLine +

								key4 + " = " + (valor4_porArea + valor44_porArea).ToString() + Environment.NewLine +

								key5 + " = " + (valor5_porArea + valor55_porArea).ToString() + Environment.NewLine +

								key6 + " = " + (valor6_porArea + valor66_porArea).ToString() + Environment.NewLine +

								key7 + " = " + (valor7_porArea + valor77_porArea).ToString() + Environment.NewLine +

								key8 + " = " + (valor8_porArea + valor88_porArea).ToString() + Environment.NewLine +

								key9 + " = " + (valor9_porArea + valor99_porArea).ToString() + Environment.NewLine +

								key10 + " = " + (valor10_porArea + valor1010_porArea).ToString() + Environment.NewLine +

								key11 + " = " + (valor11_porArea + valor1111_porArea).ToString() + Environment.NewLine + Environment.NewLine +

								"Total = " + (resultado0 + resultado00).ToString();

				TaskDialog.Show("Cálculo", mensajeFinal);

				#region write text

				//				string path = @"C:\ProgramData\Autodesk\Revit\Macros\2021\Revit\AppHookup\RocioEspana\Source\RocioEspana\MyTest.txt";
				//				
				//				
				//				     // Create a file to write to.
				//				 	using (StreamWriter sw = File.CreateText(path))
				//				     {
				//				     	sw.WriteLine(mensajeFinal.ToString());
				//				     }	
				//				
				//				
				//				// Open the file to read from.
				//				using (StreamReader sr = File.OpenText(path))
				//				{
				//				    string s = "";
				//				    while ((s = sr.ReadLine()) != null)
				//				    {
				//				       	s = s + sr.ReadLine();
				//				       		
				//				    }
				//
				//				}
				#endregion

				// Create a list of accounts.
				var bankAccounts = new List<Account> {
					new Account {
								  ID = elemento1,//Hormgon
				                  Balance = resultado0//valor_porAres solo hormigon
				                },
					new Account {
								  ID = elemento2,//suelos por defecto
				                  Balance = resultado00// valor_porArea solo suelos por defecto
				                },
					new Account {
									ID = "",
									Balance = 0
					},
					new Account {
									ID = "Total",
									Balance =  (resultado0+resultado00)// valor_Area de total
					}
				};

				var bankAccounts2 = new List<Account> {
					new Account {
								  ID = key2,//Hormgon
				                  Balance = (valor2_porArea + valor22_porArea)//valor_porAres solo hormigon
				                },
					new Account {
								  ID = key3,//suelos por defecto
				                  Balance = (valor3_porArea + valor33_porArea)// valor_porArea solo suelos por defecto
				                },
					new Account {
									ID = key4,
									Balance = (valor4_porArea + valor44_porArea)
					},
					new Account {
									ID = key5,
									Balance = (valor5_porArea + valor55_porArea)
					},
					new Account {
									ID = key6,
									Balance = (valor6_porArea + valor66_porArea)
					},
					new Account {
									ID = key7,
									Balance = (valor7_porArea + valor77_porArea)
					},
					new Account {
									ID = key8,
									Balance = (valor8_porArea + valor88_porArea)
					},
					new Account {
									ID = key9,
									Balance = (valor9_porArea + valor99_porArea)
					},
					new Account {
									ID = key10,
									Balance = (valor10_porArea + valor1010_porArea)
					},
					new Account {
									ID = key11,
									Balance = (valor11_porArea + valor1111_porArea)
					},
					new Account {
									ID = "",
									Balance = 0
					},
					new Account {
									ID = "Total",
									Balance =  (resultado0+resultado00)// valor_Area de total
					}
				};

				// Display the list in an Excel spreadsheet.
				DisplayInExcel(bankAccounts, bankAccounts2);


			}


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
