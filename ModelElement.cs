﻿using Autodesk.Revit.ApplicationServices;
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
using AddinExportCDW.Views;

namespace AddinExportCDW
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    class ModelElement : IExternalCommand
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

			#region Collector de Elementos
			// llamada a Class Collectora 

			IList<Element> floors = CollectorElement.Get(doc, activeView, "floors");
			IList<Element> structuralColumns = CollectorElement.Get(doc, activeView, "structuralColumns");
			IList<Element> strFoundation = CollectorElement.Get(doc, activeView, "strFoundation");
			IList<Element> strFramming = CollectorElement.Get(doc, activeView, "strFramming");
			IList<Element> walls = CollectorElement.Get(doc, activeView, "walls");
			#endregion

			List<double> listaN_valor = Core.GetListValores(commandData,
																floors,
																structuralColumns,
																strFoundation,
																strFramming,
																walls);
			List<Dictionary<string, string>> lista_Dictionarios = Core.GetListDictionary(commandData,
													floors,
													structuralColumns,
													strFoundation,
													strFramming,
													walls);

			List<double> lista_desperdicios = Core.GetListDesperdicio(commandData,
													floors,
													structuralColumns,
													strFoundation,
													strFramming,
													walls);

			double desperdicioTotal = Core.GetDesperdicioTotal(commandData,
													floors,
													structuralColumns,
													strFoundation,
													strFramming,
													walls);

			List<List<double>> listaDe_listaN_valor = Core.GetListValoresByName(commandData,
													floors,
													structuralColumns,
													strFoundation,
													strFramming,
													walls);


			#region mensaje en Pantalla

			WindowMensaje MainMensaje = new WindowMensaje(commandData,
														  listaN_valor, 
														  lista_Dictionarios, 
														  lista_desperdicios, 
														  desperdicioTotal,
														  listaDe_listaN_valor);
			MainMensaje.ShowDialog();

            #endregion

            if (CreateSchedule.ExistParameters(commandData, lista_Dictionarios[0], floors))//si sí existen parametros
            {
				CreateSchedule.CreateSchedules(commandData, lista_Dictionarios[0]);
			}
            else
            {
				CreateSchedule.CreateParameters(commandData, lista_Dictionarios[0], floors);
				CreateSchedule.CreateSchedules(commandData, lista_Dictionarios[0]);
            }

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
