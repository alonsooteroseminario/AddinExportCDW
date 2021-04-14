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

			// llamada a Class Collectora de Elementos.

			IList<Element> floors = CollectorElement.Get(doc, activeView, "floors");
			IList<Element> structuralColumns = CollectorElement.Get(doc, activeView, "structuralColumns");
			IList<Element> strFoundation = CollectorElement.Get(doc, activeView, "strFoundation");
			IList<Element> strFramming = CollectorElement.Get(doc, activeView, "strFramming");
			IList<Element> walls = CollectorElement.Get(doc, activeView, "walls");


			// llamada a Class Dictionario

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

			#region Datos y Variables iniciales

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

			List<List<double>> lista_DeListasValores = new List<List<double>>();
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

			#endregion

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

						double sumaTotal_valor_porArea = CalcArea.Get(data, sc);

						lista_sumaTotal_valor_porArea_Forjado.Add(sumaTotal_valor_porArea);
					}
				}
				lista_Dictionarios.Add(data_forjado);

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

						double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);

						lista_sumaTotal_valor_porVolumen_PilarConcreto.Add(sumaTotal_valor_porVolumen);
					}
				}
				lista_Dictionarios.Add(data_pilar_hormigon);

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

						double sumaTotal_valor_porArea = CalcArea.Get(data, sc);

						lista_sumaTotal_valor_porArea_Concreto.Add(sumaTotal_valor_porArea);
					}
				}
				lista_Dictionarios.Add(data_floors_concreto);

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

						double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
						lista_sumaTotal_valor_porVolumen_Cimentaciones.Add(sumaTotal_valor_porVolumen);
					}
				}
				lista_Dictionarios.Add(data_Cimentaciones);

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

						double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
						lista_sumaTotal_valor_porArea_ConcretoDeck.Add(sumaTotal_valor_porArea);
					}
				}
				lista_Dictionarios.Add(data_ConcretoDeck);

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

						double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
						lista_sumaTotal_valor_porArea_Droppedbeam.Add(sumaTotal_valor_porVolumen);
					}
				}
				lista_Dictionarios.Add(data_Droppedbeam);

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

						double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
						lista_sumaTotal_valor_porArea_ConcreteSlab.Add(sumaTotal_valor_porArea);

					}
				}
				lista_Dictionarios.Add(data_ConcreteSlab);

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

						double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
						lista_sumaTotal_valor_porArea_Beamembbededm.Add(sumaTotal_valor_porVolumen);
					}
				}
				lista_Dictionarios.Add(data_Beamembbeded);
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

						double sumaTotal_valor_porArea = CalcArea.Get(data, sc);
						lista_sumaTotal_valor_porArea_ConcreteInclinedSlab.Add(sumaTotal_valor_porArea);

					}
				}
				lista_Dictionarios.Add(data_ConcreteInclinedSlab);
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

						double sumaTotal_valor_porVolumen = CalcVolume.Get(data, sc);
						lista_sumaTotal_valor_porArea_walls_Concrete.Add(sumaTotal_valor_porVolumen);
					}
				}
				lista_Dictionarios.Add(data_walls);

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

            #region mensaje en Pantalla

            string mensaje = "Código LER     |     RCD (m3) " + Environment.NewLine + Environment.NewLine;
			for (int i = 0; i < lista_Dictionarios.Count(); i++)
			{
				mensaje = mensaje + lista_Dictionarios[i]["Structural element"] + " / " + lista_Dictionarios[i]["Código"] + "    =    " + lista_desperdicios[i].ToString() + Environment.NewLine;

			}
			mensaje = mensaje + Environment.NewLine + Environment.NewLine + "Total    =     " + desperdicioTotal.ToString();

			TaskDialog.Show("CDW Estimación", mensaje);

			#endregion


			//agregar Class ExportExcel
			#region Obtener Excel
			var bankAccounts = new List<Account>();

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

            // se crean Class key schedule con mismas tablas de excel
            #region keyschedule

            ViewSchedule keySchedule = null;

			using (Transaction t = new Transaction(doc, "Crear key Schedulable"))
            {
				t.Start();
				keySchedule = ViewSchedule.CreateKeySchedule(doc, new ElementId(BuiltInCategory.OST_Floors));



				string keySchParamName = keySchedule.KeyScheduleParameterName;
				ScheduleDefinition definition = keySchedule.Definition;

				IList<SchedulableField> schedulableFields = definition.GetSchedulableFields(); // [a,b,c,s,d,f,....]
				List<SchedulableField> listashparam = new List<SchedulableField>();
				foreach (SchedulableField element in schedulableFields) // Aquí se evalua un SchedulableField
				{
					if (element.ParameterId.IntegerValue > 0)
					{
						listashparam.Add(element);
					}
				}
				double nro_items_listahpram = listashparam.Count();

				keySchedule.Definition.AddField(listashparam[0]);
				keySchedule.Definition.AddField(listashparam[1]);

				TableData tableData = keySchedule.GetTableData();

				TableSectionData tableSection = tableData.GetSectionData(SectionType.Body);

				//for (int i = 0; i < lista_Dictionarios.Count(); i++)
    //            {
				//	tableSection.InsertRow(i);
				//}


                tableSection.InsertRow(0);
                tableSection.InsertRow(1);
				tableSection.InsertRow(2);



				doc.Regenerate();



				if (null != keySchedule)
				{
					t.Commit();
				}
				else
				{
					t.RollBack();
				}

			}
			#endregion

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
