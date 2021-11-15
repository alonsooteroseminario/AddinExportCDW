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
        private List<double> ListaN_valor_NEW { get; set; }

        private bool PasoPorAqui_NEW { get; set; }

        public ChartPickOneForm(bool pasoPorAqui,
                             List<double> listaN_valor)
        {
            InitializeComponent();
            ListaN_valor_NEW = listaN_valor;
            PasoPorAqui_NEW = pasoPorAqui;
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

            if (!PasoPorAqui_NEW)
            {
                ListaN_valor_NEW.Reverse();
            }
            for (int i = 0; i < ListaN_valor_NEW.Count(); i++)
            {
                cartesianChart1.Series[0].Values.Add(new ObservablePoint(ListaN_valor_NEW[i], i));
            }
            List<string> array = Dictionary.DictionaryListKeys(Dictionary.Get("data_forjado"));
            array.Reverse();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Code",
                Labels = array
            });
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Quantity",
                LabelFormatter = value => value.ToString() + " m3"
            });
            var tooltip = new DefaultTooltip
            {
                SelectionMode = TooltipSelectionMode.SharedYValues
            };
            cartesianChart1.DataTooltip = tooltip;
        }
    }
}