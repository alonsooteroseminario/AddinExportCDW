using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddinExportCDW.Views
{
    public partial class PieChartForm : Form
    {
        private List<double> listaN_valor_NEW { get; set; }
        private List<Dictionary<string, string>> lista_Dictionarios_NEW { get; set; }
        private List<double> lista_desperdicios_NEW { get; set; }
        private double desperdicioTotal_NEW { get; set; }
        public PieChartForm(List<double> listaN_valor,
                             List<Dictionary<string, string>> lista_Dictionarios,
                             List<double> lista_desperdicios,
                             double desperdicioTotal)
        {
            InitializeComponent();
            listaN_valor_NEW = listaN_valor;
            lista_Dictionarios_NEW = lista_Dictionarios;
            lista_desperdicios_NEW = lista_desperdicios;
            desperdicioTotal_NEW = desperdicioTotal;
        }

        private void PieChartForm_Load(object sender, EventArgs e)
        {
            Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            pieChart1.InnerRadius = 100;
            // Define the collection of Values to display in the Pie Chart
            pieChart1.Series = new SeriesCollection
            {

                new PieSeries
                {
                    Title = "07 07 01 - aqueous washing liquids",
                    Values = new ChartValues<double> { listaN_valor_NEW[0] },
                    DataLabels = true,
                    LabelPoint = labelPoint,
                    Fill = System.Windows.Media.Brushes.Green
                },
                new PieSeries
                {
                    Title = "15 01 02 - plastic packaging",
                    Values = new ChartValues<double> { listaN_valor_NEW[1] },
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "15 01 03 - wooden packaging",
                    Values = new ChartValues<double> { listaN_valor_NEW[2] },
                    DataLabels = true,
                    LabelPoint = labelPoint,
                    //PushOut = 15,
                },
                new PieSeries
                {
                    Title = "15 01 04 - metallic packaging",
                    Values = new ChartValues<double> { listaN_valor_NEW[3] },
                    DataLabels = true,
                    LabelPoint = labelPoint,
                    Fill = System.Windows.Media.Brushes.Gray
                },
                new PieSeries
                {
                    Title = "15 01 06 - mixed packaging",
                    Values = new ChartValues<double> { listaN_valor_NEW[4] },
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "17 01 01 - concrete",
                    Values = new ChartValues<double> { listaN_valor_NEW[5] },
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "17 02 01 - wood",
                    Values = new ChartValues<double> { listaN_valor_NEW[6] },
                    DataLabels = true,
                    LabelPoint = labelPoint
                },                
                new PieSeries
                {
                    Title = "17 02 03 - plastic",
                    Values = new ChartValues<double> { listaN_valor_NEW[7] },
                    DataLabels = true,
                    LabelPoint = labelPoint
                },                
                new PieSeries
                {
                    Title = "17 04 05 - iron and steel",
                    Values = new ChartValues<double> { listaN_valor_NEW[8] },
                    DataLabels = true,
                    LabelPoint = labelPoint
                },                
                new PieSeries
                {
                    Title = "17 09 04 - mixed",
                    Values = new ChartValues<double> { listaN_valor_NEW[9] },
                    DataLabels = true,
                    LabelPoint = labelPoint
                }
            };

            // Set the legend location to appear in the bottom of the chart
            pieChart1.LegendLocation = LegendLocation.Right;
        }
    }
}
