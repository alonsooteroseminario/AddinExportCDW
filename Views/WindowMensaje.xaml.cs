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
        private List<double> listaN_valor_NEW { get; set; }
        private List<Dictionary<string, string>> lista_Dictionarios_NEW { get; set; }
        private List<double> lista_desperdicios_NEW { get; set; }
        private double desperdicioTotal_NEW { get; set; }

        private ExternalCommandData commandData_NEW { get; set; }

        private List<List<double>> listaDe_listaN_valor_NEW { get; set; }

        public WindowMensaje(ExternalCommandData commandData,
                             List<double> listaN_valor,
                             List<Dictionary<string, string>> lista_Dictionarios,
                             List<double> lista_desperdicios,
                             double desperdicioTotal,
                             List<List<double>> listaDe_listaN_valor)
        {
            InitializeComponent();

            commandData_NEW = commandData;
            listaDe_listaN_valor_NEW = listaDe_listaN_valor;

            Dictionary<string, string> dictionary = Dictionary.Get("data_forjado");
            List<string> ListKeys = Dictionary.DictionaryListKeys(dictionary);

            DataTable dt = new DataTable();
            DataColumn CodLer = new DataColumn("Código LER", typeof(string));
            DataColumn rcd = new DataColumn("RCD (m³)", typeof(string));

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

            listaN_valor_NEW = listaN_valor;
            lista_Dictionarios_NEW = lista_Dictionarios;
            lista_desperdicios_NEW = lista_desperdicios;
            desperdicioTotal_NEW = desperdicioTotal;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PieChartForm pieChartForm = new PieChartForm(listaN_valor_NEW, lista_Dictionarios_NEW, lista_desperdicios_NEW, desperdicioTotal_NEW);
            pieChartForm.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ExportExcel.Export(listaN_valor_NEW, lista_Dictionarios_NEW, lista_desperdicios_NEW, desperdicioTotal_NEW);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ChartForm chartForm = new ChartForm(commandData_NEW, listaN_valor_NEW, lista_Dictionarios_NEW, listaDe_listaN_valor_NEW);
            chartForm.ShowDialog();
        }
    }
}