using Autodesk.Revit.UI;
using LiveCharts;
using LiveCharts.Defaults;
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
    public partial class ChartForm : Form
    {
        private List<List<double>> listaDe_listaN_valor_NEW { get; set; }
        private List<Dictionary<string, string>> lista_Dictionarios_NEW { get; set; }
        private List<double> listaN_valor_NEW { get; set; }
        int selectedIndex { get; set; }
        string Eleccion { get; set; }
        public ChartForm(ExternalCommandData commandData,
                         List<double> listaN_valor,
                         List<Dictionary<string, string>> lista_Dictionarios,
                         List<List<double>> listaDe_listaN_valor)
        {
            InitializeComponent();

            listaDe_listaN_valor_NEW = listaDe_listaN_valor;
            lista_Dictionarios_NEW = lista_Dictionarios;
            listaN_valor_NEW = listaN_valor;

            cartesianChart1.Series = new SeriesCollection { };
            cartesianChart1.Series.Add(new RowSeries
            {
                Values = new ChartValues<ObservablePoint> { },
                MaxRowHeigth = 5,
                RowPadding = 2
            });

            Eleccion = "07 07 01 - aqueous washing liquids";

            if (Eleccion == "07 07 01 - aqueous washing liquids")
            {
                //Hacer calculos de nuevo
                for (int i = 0; i < listaDe_listaN_valor[0].Count(); i++)
                {
                    cartesianChart1.Series[0].Values.Add(new ObservablePoint(i, listaDe_listaN_valor[0][i]));
                }
            }

            List<string> array = new List<string>();

            for (int i = 0; i < lista_Dictionarios_NEW.Count(); i++)
            {
                array.Add(lista_Dictionarios_NEW[i]["Structural element"] + " / " + lista_Dictionarios_NEW[i]["Código"]);
            }

            cartesianChart1.AxisY.Add(new Axis
            {
                // deberia ser de l Lista de Dictionarios
                Title = "Código",
                Labels = array.ToArray()
            });


            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Cantidad",
                LabelFormatter = value => value.ToString("N")
            });
            var tooltip = new DefaultTooltip
            {
                SelectionMode = TooltipSelectionMode.SharedYValues
            };
 
            cartesianChart1.DataTooltip = tooltip;
        }

        private void CambiarChart(int selectedIndex)
        {
            if(selectedIndex == 0)
            {
                Eleccion = "07 07 01 - aqueous washing liquids";
                cartesianChart1.Series.Clear();
                cartesianChart1.Series.Add(new RowSeries
                {
                    Values = new ChartValues<ObservablePoint> { },
                    MaxRowHeigth = 5,
                    RowPadding = 2
                });
                for (int i = 0; i < listaDe_listaN_valor_NEW[selectedIndex].Count(); i++)
                {
                    cartesianChart1.Series[0].Values.Add(new ObservablePoint(i, listaDe_listaN_valor_NEW[selectedIndex][i]));
                }
                List<string> array = new List<string>();

                for (int i = 0; i < lista_Dictionarios_NEW.Count(); i++)
                {
                    array.Add(lista_Dictionarios_NEW[i]["Structural element"] + " / " + lista_Dictionarios_NEW[i]["Código"]);
                }
                cartesianChart1.AxisY.Clear();
                cartesianChart1.AxisY.Add(new Axis
                {
                    // deberia ser de l Lista de Dictionarios
                    Title = "Código",
                    Labels = array.ToArray()
                });
            }
            else if (selectedIndex == 1)
            {
                Eleccion = "15 01 02 - plastic packaging";
                cartesianChart1.Series.Clear();
                cartesianChart1.Series.Add(new RowSeries
                {
                    Values = new ChartValues<ObservablePoint> { },
                    MaxRowHeigth = 5,
                    RowPadding = 2
                });
                for (int i = 0; i < listaDe_listaN_valor_NEW[selectedIndex].Count(); i++)
                {
                    cartesianChart1.Series[0].Values.Add(new ObservablePoint(i, listaDe_listaN_valor_NEW[selectedIndex][i]));
                }
                List<string> array = new List<string>();

                for (int i = 0; i < lista_Dictionarios_NEW.Count(); i++)
                {
                    array.Add(lista_Dictionarios_NEW[i]["Structural element"] + " / " + lista_Dictionarios_NEW[i]["Código"]);
                }
                cartesianChart1.AxisY.Clear();
                cartesianChart1.AxisY.Add(new Axis
                {
                    // deberia ser de l Lista de Dictionarios
                    Title = "Código",
                    Labels = array.ToArray()
                });
            }
            else if (selectedIndex == 2)
            {
                Eleccion = "15 01 03 - wooden packaging";
                cartesianChart1.Series.Clear();
                cartesianChart1.Series.Add(new RowSeries
                {
                    Values = new ChartValues<ObservablePoint> { },
                    MaxRowHeigth = 5,
                    RowPadding = 2
                });
                for (int i = 0; i < listaDe_listaN_valor_NEW[selectedIndex].Count(); i++)
                {
                    cartesianChart1.Series[0].Values.Add(new ObservablePoint(i, listaDe_listaN_valor_NEW[selectedIndex][i]));
                }
                List<string> array = new List<string>();

                for (int i = 0; i < lista_Dictionarios_NEW.Count(); i++)
                {
                    array.Add(lista_Dictionarios_NEW[i]["Structural element"] + " / " + lista_Dictionarios_NEW[i]["Código"]);
                }
                cartesianChart1.AxisY.Clear();
                cartesianChart1.AxisY.Add(new Axis
                {
                    // deberia ser de l Lista de Dictionarios
                    Title = "Código",
                    Labels = array.ToArray()
                });
            }
            else if (selectedIndex == 3)
            {
                Eleccion = "15 01 04 - metallic packaging";
                cartesianChart1.Series.Clear();
                cartesianChart1.Series.Add(new RowSeries
                {
                    Values = new ChartValues<ObservablePoint> { },
                    MaxRowHeigth = 5,
                    RowPadding = 2
                });
                for (int i = 0; i < listaDe_listaN_valor_NEW[selectedIndex].Count(); i++)
                {
                    cartesianChart1.Series[0].Values.Add(new ObservablePoint(i, listaDe_listaN_valor_NEW[selectedIndex][i]));
                }
                List<string> array = new List<string>();

                for (int i = 0; i < lista_Dictionarios_NEW.Count(); i++)
                {
                    array.Add(lista_Dictionarios_NEW[i]["Structural element"] + " / " + lista_Dictionarios_NEW[i]["Código"]);
                }
                cartesianChart1.AxisY.Clear();
                cartesianChart1.AxisY.Add(new Axis
                {
                    // deberia ser de l Lista de Dictionarios
                    Title = "Código",
                    Labels = array.ToArray()
                });
            }
            else if (selectedIndex == 4)
            {
                Eleccion = "15 01 06 - mixed packaging";
                cartesianChart1.Series.Clear();
                cartesianChart1.Series.Add(new RowSeries
                {
                    Values = new ChartValues<ObservablePoint> { },
                    MaxRowHeigth = 5,
                    RowPadding = 2
                });
                for (int i = 0; i < listaDe_listaN_valor_NEW[selectedIndex].Count(); i++)
                {
                    cartesianChart1.Series[0].Values.Add(new ObservablePoint(i, listaDe_listaN_valor_NEW[selectedIndex][i]));
                }
                List<string> array = new List<string>();

                for (int i = 0; i < lista_Dictionarios_NEW.Count(); i++)
                {
                    array.Add(lista_Dictionarios_NEW[i]["Structural element"] + " / " + lista_Dictionarios_NEW[i]["Código"]);
                }
                cartesianChart1.AxisY.Clear();
                cartesianChart1.AxisY.Add(new Axis
                {
                    // deberia ser de l Lista de Dictionarios
                    Title = "Código",
                    Labels = array.ToArray()
                });
            }
            else if (selectedIndex == 5)
            {
                Eleccion = "17 01 01 - concrete";
                cartesianChart1.Series.Clear();
                cartesianChart1.Series.Add(new RowSeries
                {
                    Values = new ChartValues<ObservablePoint> { },
                    MaxRowHeigth = 5,
                    RowPadding = 2
                });
                for (int i = 0; i < listaDe_listaN_valor_NEW[selectedIndex].Count(); i++)
                {
                    cartesianChart1.Series[0].Values.Add(new ObservablePoint(i, listaDe_listaN_valor_NEW[selectedIndex][i]));
                }
                List<string> array = new List<string>();

                for (int i = 0; i < lista_Dictionarios_NEW.Count(); i++)
                {
                    array.Add(lista_Dictionarios_NEW[i]["Structural element"] + " / " + lista_Dictionarios_NEW[i]["Código"]);
                }
                cartesianChart1.AxisY.Clear();
                cartesianChart1.AxisY.Add(new Axis
                {
                    // deberia ser de l Lista de Dictionarios
                    Title = "Código",
                    Labels = array.ToArray()
                });
            }
            else if (selectedIndex == 6)
            {
                Eleccion = "17 02 01 - wood";
                cartesianChart1.Series.Clear();
                cartesianChart1.Series.Add(new RowSeries
                {
                    Values = new ChartValues<ObservablePoint> { },
                    MaxRowHeigth = 5,
                    RowPadding = 2
                });
                for (int i = 0; i < listaDe_listaN_valor_NEW[selectedIndex].Count(); i++)
                {
                    cartesianChart1.Series[0].Values.Add(new ObservablePoint(i, listaDe_listaN_valor_NEW[selectedIndex][i]));
                }
                List<string> array = new List<string>();

                for (int i = 0; i < lista_Dictionarios_NEW.Count(); i++)
                {
                    array.Add(lista_Dictionarios_NEW[i]["Structural element"] + " / " + lista_Dictionarios_NEW[i]["Código"]);
                }
                cartesianChart1.AxisY.Clear();
                cartesianChart1.AxisY.Add(new Axis
                {
                    // deberia ser de l Lista de Dictionarios
                    Title = "Código",
                    Labels = array.ToArray()
                });
            }
            else if (selectedIndex == 7)
            {
                Eleccion = "17 02 03 - plastic";
                cartesianChart1.Series.Clear();
                cartesianChart1.Series.Add(new RowSeries
                {
                    Values = new ChartValues<ObservablePoint> { },
                    MaxRowHeigth = 5,
                    RowPadding = 2
                });
                for (int i = 0; i < listaDe_listaN_valor_NEW[selectedIndex].Count(); i++)
                {
                    cartesianChart1.Series[0].Values.Add(new ObservablePoint(i, listaDe_listaN_valor_NEW[selectedIndex][i]));
                }
                List<string> array = new List<string>();

                for (int i = 0; i < lista_Dictionarios_NEW.Count(); i++)
                {
                    array.Add(lista_Dictionarios_NEW[i]["Structural element"] + " / " + lista_Dictionarios_NEW[i]["Código"]);
                }
                cartesianChart1.AxisY.Clear();
                cartesianChart1.AxisY.Add(new Axis
                {
                    // deberia ser de l Lista de Dictionarios
                    Title = "Código",
                    Labels = array.ToArray()
                });
            }
            else if (selectedIndex == 8)
            {
                Eleccion = "17 04 05 - iron and steel";
                cartesianChart1.Series.Clear();
                cartesianChart1.Series.Add(new RowSeries
                {
                    Values = new ChartValues<ObservablePoint> { },
                    MaxRowHeigth = 5,
                    RowPadding = 2
                });
                for (int i = 0; i < listaDe_listaN_valor_NEW[selectedIndex].Count(); i++)
                {
                    cartesianChart1.Series[0].Values.Add(new ObservablePoint(i, listaDe_listaN_valor_NEW[selectedIndex][i]));
                }
                List<string> array = new List<string>();

                for (int i = 0; i < lista_Dictionarios_NEW.Count(); i++)
                {
                    array.Add(lista_Dictionarios_NEW[i]["Structural element"] + " / " + lista_Dictionarios_NEW[i]["Código"]);
                }
                cartesianChart1.AxisY.Clear();
                cartesianChart1.AxisY.Add(new Axis
                {
                    // deberia ser de l Lista de Dictionarios
                    Title = "Código",
                    Labels = array.ToArray()
                });
            }
            else if (selectedIndex == 9)
            {
                Eleccion = "17 09 04 - mixed";
                cartesianChart1.Series.Clear();
                cartesianChart1.Series.Add(new RowSeries
                {
                    Values = new ChartValues<ObservablePoint> { },
                    MaxRowHeigth = 5,
                    RowPadding = 2
                });
                for (int i = 0; i < listaDe_listaN_valor_NEW[selectedIndex].Count(); i++)
                {
                    cartesianChart1.Series[0].Values.Add(new ObservablePoint(i, listaDe_listaN_valor_NEW[selectedIndex][i]));
                }
                List<string> array = new List<string>();

                for (int i = 0; i < lista_Dictionarios_NEW.Count(); i++)
                {
                    array.Add(lista_Dictionarios_NEW[i]["Structural element"] + " / " + lista_Dictionarios_NEW[i]["Código"]);
                }
                cartesianChart1.AxisY.Clear();
                cartesianChart1.AxisY.Add(new Axis
                {
                    // deberia ser de l Lista de Dictionarios
                    Title = "Código",
                    Labels = array.ToArray()
                });
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            selectedIndex = comboBox1.SelectedIndex;
            CambiarChart(selectedIndex);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Recoge valor del UI
            selectedIndex = comboBox1.SelectedIndex;
        }
    }
}
