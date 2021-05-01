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
            Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} m3   ({1:P})", Math.Round(chartPoint.Y, 2), chartPoint.Participation);

            pieChart1.InnerRadius = 75;

            List<string> keys = Dictionary.DictionaryListKeys(Dictionary.Get("data_forjado"));

            // calculo por porcentaje
            double suma = 0;
            foreach (var item in listaN_valor_NEW)
            {
                suma = suma + item;
            }
            var porcentaje = 1;
            var valorPorcentaje = (porcentaje * suma) / 100;

            int i = 0;
            foreach (var item in listaN_valor_NEW)
            {
                if (item < valorPorcentaje)
                {
                    pieChart1.Series.Add(new PieSeries
                    {
                        Title = keys[i],
                        Values = new ChartValues<double> { item },
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
                        Values = new ChartValues<double> { item },
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