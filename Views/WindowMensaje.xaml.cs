using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace AddinExportCDW.Views
{
    /// <summary>
    /// Interaction logic for WindowMensaje.xaml
    /// </summary>
    public partial class WindowMensaje : Window
    {
        private bool pasoPorAqui = false;
        private List<double> ListaN_valor_NEW { get; set; }
        private List<Dictionary<string, string>> Lista_Dictionarios_NEW { get; set; }
        private List<double> Lista_desperdicios_NEW { get; set; }
        private double DesperdicioTotal_NEW { get; set; }
        private List<List<List<double>>> ListaDe_listaN_valorSeparaadaPorDataElemento_NEW { get; set; }
        private ExternalCommandData CommandData_NEW { get; set; }

        private double Count_NEW { get; set; }

        public WindowMensaje(double count,
                             ExternalCommandData commandData,
                             List<double> listaN_valor,
                             List<Dictionary<string, string>> lista_Dictionarios,
                             List<double> lista_desperdicios,
                             double desperdicioTotal,
                             List<List<List<double>>> listaDe_listaN_valorSeparaadaPorDataElemento)
        {
            InitializeComponent();

            CommandData_NEW = commandData;
            Count_NEW = count;

            Dictionary<string, string> dictionary = Dictionary.Get("data_forjado");
            List<string> ListKeys = Dictionary.DictionaryListKeys(dictionary);

            DataTable dt = new DataTable();
            DataColumn CodLer = new DataColumn("European Waste Code (EWC)", typeof(string));
            DataColumn rcd = new DataColumn("CDW (m3)", typeof(string));

            dt.Columns.Add(CodLer);
            dt.Columns.Add(rcd);

            //depende de cuanto dictionarios hay tantas Rows
            for (int i = 0; i < ListKeys.Count(); i++)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ListKeys[i];//Código LER
                newRow[1] = listaN_valor[i];// RCD m3
                dt.Rows.Add(newRow);
            }
            DataRow newRowVoid = dt.NewRow();
            newRowVoid[0] = "";
            newRowVoid[1] = "";
            dt.Rows.Add(newRowVoid);

            DataRow newRowTotal = dt.NewRow();
            newRowTotal[0] = "Total";
            newRowTotal[1] = desperdicioTotal;
            dt.Rows.Add(newRowTotal);

            ExcelDataGrid.ItemsSource = dt.DefaultView;

            ExcelDataGrid.GridLinesVisibility = System.Windows.Controls.DataGridGridLinesVisibility.None;

            ListaN_valor_NEW = listaN_valor;
            Lista_Dictionarios_NEW = lista_Dictionarios;
            Lista_desperdicios_NEW = lista_desperdicios;
            DesperdicioTotal_NEW = desperdicioTotal;
            ListaDe_listaN_valorSeparaadaPorDataElemento_NEW = listaDe_listaN_valorSeparaadaPorDataElemento;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PieChartForm pieChartForm = new PieChartForm(ListaN_valor_NEW);
            pieChartForm.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ExportExcel.Export(ListaN_valor_NEW, Lista_Dictionarios_NEW, Lista_desperdicios_NEW, DesperdicioTotal_NEW);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Count_NEW == 1)
            {
                if (!pasoPorAqui)
                {
                    ChartPickOneForm chartPickOneForm = new ChartPickOneForm(pasoPorAqui,
                                                     ListaN_valor_NEW);
                    chartPickOneForm.ShowDialog();
                    pasoPorAqui = true;
                }
                else
                {
                    ChartPickOneForm chartPickOneForm = new ChartPickOneForm(pasoPorAqui,
                                                     ListaN_valor_NEW);
                    chartPickOneForm.ShowDialog();
                }
            }
            else
            {
                ChartForm chartForm = new ChartForm(CommandData_NEW,
                                    Lista_Dictionarios_NEW,
                                    ListaDe_listaN_valorSeparaadaPorDataElemento_NEW);
                chartForm.ShowDialog();
            }
        }
    }
}