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
        private List<Dictionary<string, string>> lista_Dictionarios_NEW { get; set; }
        private List<List<List<double>>> listaDe_listaN_valorSeparaadaPorDataElemento_NEW { get; set; }

        private double count_NEW { get; set; }

        private int selectedIndex { get; set; }

        public ChartForm(double count,
                         ExternalCommandData commandData,
                         List<double> listaN_valor,
                         List<Dictionary<string, string>> lista_Dictionarios,
                         List<List<double>> listaDe_listaN_valor,
                         List<List<List<double>>> listaDe_listaN_valorSeparaadaPorDataElemento)
        {
            InitializeComponent();
            lista_Dictionarios_NEW = lista_Dictionarios;
            listaDe_listaN_valorSeparaadaPorDataElemento_NEW = listaDe_listaN_valorSeparaadaPorDataElemento;
            count_NEW = count;

            cartesianChart1.Series = new SeriesCollection { };
            cartesianChart1.Series.Add(new RowSeries
            {
                Values = new ChartValues<ObservablePoint> { },
                MaxRowHeigth = 15,
                RowPadding = 2
            });

            //for (int i = 0; i < listaDe_listaN_valorSeparaadaPorDataElemento_NEW.Count(); i++)
            //{
            //    double suma = 0;
            //    foreach (var item in listaDe_listaN_valorSeparaadaPorDataElemento_NEW[i][0])
            //    {
            //        suma = suma + item;
            //    }
            //    cartesianChart1.Series[0].Values.Add(new ObservablePoint(suma, i));
            //}

            //List<string> array = new List<string>();
            //for (int i = 0; i < lista_Dictionarios_NEW.Count(); i++)
            //{
            //    array.Add(lista_Dictionarios_NEW[i]["Structural element"] + " / " + lista_Dictionarios_NEW[i]["Código"]);
            //}
            //cartesianChart1.AxisY.Add(new Axis
            //{
            //    Title = "Código",
            //    Labels = array.ToArray()
            //});
            //cartesianChart1.AxisX.Add(new Axis
            //{
            //    Title = "Cantidad",
            //    LabelFormatter = value => (value).ToString("N") + " m3"
            //});
            //var tooltip = new DefaultTooltip
            //{
            //    SelectionMode = TooltipSelectionMode.SharedYValues
            //};
            //cartesianChart1.DataTooltip = tooltip;
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

                    for (int ii = 0; ii < listaDe_listaN_valorSeparaadaPorDataElemento_NEW.Count(); ii++)
                    {
                        double suma = 0;
                        foreach (var item in listaDe_listaN_valorSeparaadaPorDataElemento_NEW[ii][selectedIndex])
                        {
                            suma = suma + item;
                        }
                        cartesianChart1.Series[0].Values.Add(new ObservablePoint(suma, ii));
                    }

                    List<string> array = new List<string>();
                    for (int ii = 0; ii < lista_Dictionarios_NEW.Count(); ii++)
                    {
                        array.Add(lista_Dictionarios_NEW[ii]["Structural element"] + " / " + lista_Dictionarios_NEW[ii]["Código"]);
                    }
                    cartesianChart1.AxisY.Clear();
                    cartesianChart1.AxisY.Add(new Axis
                    {
                        Title = "Código",
                        Labels = array.ToArray()
                    });
                    cartesianChart1.AxisX.Clear();
                    cartesianChart1.AxisX.Add(new Axis
                    {
                        Title = "Cantidad",
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

        private void button1_Click(object sender, EventArgs e)
        {
            selectedIndex = comboBox1.SelectedIndex;
            CambiarChart(selectedIndex);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = comboBox1.SelectedIndex;
        }
    }
}