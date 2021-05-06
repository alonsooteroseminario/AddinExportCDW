using Autodesk.Revit.UI;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AddinExportCDW.Views
{
    public partial class ChartForm : Form
    {
        private List<Dictionary<string, string>> Lista_Dictionarios_NEW { get; set; }
        private List<List<List<double>>> ListaDe_listaN_valorSeparaadaPorDataElemento_NEW { get; set; }
        private readonly ExternalCommandData commandData_NEW;
        private int SelectedIndex { get; set; }

        public ChartForm(ExternalCommandData commandData,
                         List<Dictionary<string, string>> lista_Dictionarios,
                         List<List<List<double>>> listaDe_listaN_valorSeparaadaPorDataElemento)
        {
            InitializeComponent();
            Lista_Dictionarios_NEW = lista_Dictionarios;
            ListaDe_listaN_valorSeparaadaPorDataElemento_NEW = listaDe_listaN_valorSeparaadaPorDataElemento;
            commandData_NEW = commandData;

            cartesianChart1.Series = new SeriesCollection { };
            cartesianChart1.Series.Add(new RowSeries
            {
                Values = new ChartValues<ObservablePoint> { },
                MaxRowHeigth = 15,
                RowPadding = 2
            });
        }

        private void CambiarChart(int selectedIndex)
        {
            List<string> keys = Dictionary.DictionaryListKeys(Dictionary.Get("data_forjado"));

            for (int i = 0; i < keys.Count(); i++)//10 keys
            {
                if (selectedIndex == i)
                {
                    cartesianChart1.Series.Clear();
                    cartesianChart1.Series.Add(new RowSeries
                    {
                        Values = new ChartValues<ObservablePoint> { },
                        MaxRowHeigth = 15,
                        RowPadding = 2
                    });

                    for (int ii = 0; ii < ListaDe_listaN_valorSeparaadaPorDataElemento_NEW.Count(); ii++)
                    {
                        double suma = 0;
                        foreach (var item in ListaDe_listaN_valorSeparaadaPorDataElemento_NEW[ii][selectedIndex])
                        {
                            suma += item;
                        }
                        cartesianChart1.Series[0].Values.Add(new ObservablePoint(suma, ii));
                    }

                    List<string> array = new List<string>();
                    for (int ii = 0; ii < Lista_Dictionarios_NEW.Count(); ii++)
                    {
                        array.Add(Lista_Dictionarios_NEW[ii]["Structural element"] + " / " + Lista_Dictionarios_NEW[ii]["Código"]);
                        StepLog.Write(commandData_NEW, Lista_Dictionarios_NEW[ii]["Structural element"] + " / " + Lista_Dictionarios_NEW[ii]["Código"]);
                    }
                    cartesianChart1.AxisY.Clear();
                    cartesianChart1.AxisY.Add(new Axis
                    {
                        Title = "Structural element/Code",
                        Labels = array.ToArray()
                    });
                    cartesianChart1.AxisX.Clear();
                    cartesianChart1.AxisX.Add(new Axis
                    {
                        Title = "Quantity",
                        LabelFormatter = value => (value).ToString("N") + " m3"
                    });
                    var tooltip = new DefaultTooltip
                    {
                        SelectionMode = TooltipSelectionMode.SharedYValues
                    };
                    cartesianChart1.DataTooltip = tooltip;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SelectedIndex = comboBox1.SelectedIndex;
            CambiarChart(SelectedIndex);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndex = comboBox1.SelectedIndex;
        }
    }
}