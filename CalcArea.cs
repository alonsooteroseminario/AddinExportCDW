using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddinExportCDW
{
    public static class CalcArea
    {
        /// <summary>
        /// Devuelve la lista de valores del diccionario ingresado como parámetro.
        /// </summary>
        /// <param name="dictionary">Diccionario el cual se quiene analizar.</param>
        public static List<string> DictionaryListValues(Dictionary<string, string> dictionary)
        {
            List<string> salida = new List<string>();
            foreach (KeyValuePair<string, string> entry in dictionary)
            {
                salida.Add(entry.Value);
            }
            salida.RemoveRange(0, 2);
            return salida;
        }

        public static double Get(Dictionary<string, string> dictionary, Element element)
        {
            Parameter param = element.LookupParameter("Área");
            List<string> entrada = DictionaryListValues(dictionary);
            double sumaTotal_valor_porArea = 0;
            foreach (string c in entrada)
            {
                string valor = c;
                string key = dictionary.FirstOrDefault(x => x.Value == valor).Key;
                double valor_porArea = double.Parse(valor) * Math.Round(param.AsDouble() / 10.7639, 4);//"17 09 04 - mixed"  X AREA
                sumaTotal_valor_porArea = sumaTotal_valor_porArea + valor_porArea;
            }
            return sumaTotal_valor_porArea;
        }

        public static double GetByValueOfKey(Dictionary<string, string> dictionary, Element element, string numeroKey) // key del 2 al 11
        {
            double salida = 0;
            Parameter param = element.LookupParameter("Área");
            List<string> lista_keys = new List<string>()
            {
                "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"
            };
            List<string> entrada = DictionaryListValues(dictionary);
            for (int i = 0; i < lista_keys.Count(); i++)
            {
                if (numeroKey == lista_keys[i])
                {
                    string valor = entrada[i];
                    salida = double.Parse(valor) * Math.Round(param.AsDouble() / 10.7639, 4);
                }
            }
            return salida;
        }
    }
}