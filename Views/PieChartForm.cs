using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media;

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
            Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} m3   ({1:P})", chartPoint.Y, chartPoint.Participation);

            pieChart1.InnerRadius = 75;

            List<string> keys = Dictionary.DictionaryListKeys(Dictionary.Get("data_forjado"));
            int i = 0;
            foreach (var item in listaN_valor_NEW)
            {
                if (item < 1)
                {
                    pieChart1.Series.Add(new PieSeries
                    {
                        Title = keys[i],
                        Values = new ChartValues<double> { Math.Round(item, 2) },
                        LabelPoint = labelPoint,
                        LabelPosition = PieLabelPosition.OutsideSlice,
                        Foreground = Brushes.Black,
                        FontSize = 12
                    });
                }
                else
                {
                    pieChart1.Series.Add(new PieSeries
                    {
                        Title = keys[i],
                        Values = new ChartValues<double> { Math.Round(item, 2) },
                        DataLabels = true,
                        LabelPoint = labelPoint,
                        LabelPosition = PieLabelPosition.InsideSlice,
                        FontSize = 12
                    });
                }
                i++;
            }
            pieChart1.LegendLocation = LegendLocation.Right;
        }
    }
}