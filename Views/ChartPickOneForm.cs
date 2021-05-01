using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AddinExportCDW.Views
{
    public partial class ChartPickOneForm : Form
    {
        private List<double> listaN_valor_NEW { get; set; }
        private List<Dictionary<string, string>> lista_Dictionarios_NEW { get; set; }
        private List<double> lista_desperdicios_NEW { get; set; }
        private double desperdicioTotal_NEW { get; set; }

        private bool pasoPorAqui_NEW { get; set; }

        public ChartPickOneForm(bool pasoPorAqui,
                             List<double> listaN_valor,
                             List<Dictionary<string, string>> lista_Dictionarios,
                             List<double> lista_desperdicios,
                             double desperdicioTotal)
        {
            InitializeComponent();
            listaN_valor_NEW = listaN_valor;
            lista_Dictionarios_NEW = lista_Dictionarios;
            lista_desperdicios_NEW = lista_desperdicios;
            desperdicioTotal_NEW = desperdicioTotal;
            pasoPorAqui_NEW = pasoPorAqui;
        }

        private void ChartPickOneForm_Load(object sender, EventArgs e)
        {
            cartesianChart1.Series = new SeriesCollection { };
            cartesianChart1.Series.Add(new RowSeries
            {
                Values = new ChartValues<ObservablePoint> { },
                MaxRowHeigth = 15,
                RowPadding = 2
            });

            if (!pasoPorAqui_NEW)
            {
                listaN_valor_NEW.Reverse();
            }
            for (int i = 0; i < listaN_valor_NEW.Count(); i++)
            {
                cartesianChart1.Series[0].Values.Add(new ObservablePoint(listaN_valor_NEW[i], i));
            }
            List<string> array = Dictionary.DictionaryListKeys(Dictionary.Get("data_forjado"));
            array.Reverse();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Código",
                Labels = array
            });
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Cantidad",
                LabelFormatter = value => value.ToString("N") + " m3"
            });
            var tooltip = new DefaultTooltip
            {
                SelectionMode = TooltipSelectionMode.SharedYValues
            };
            cartesianChart1.DataTooltip = tooltip;
        }
    }
}