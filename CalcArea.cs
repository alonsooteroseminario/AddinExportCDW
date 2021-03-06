using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (param == null)
            {
                param = element.LookupParameter("Area");
            }
            List<string> entrada = DictionaryListValues(dictionary);
            double sumaTotal_valor_porArea = 0;
            NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
            CultureInfo provider = new CultureInfo("fr-FR");
            foreach (string c in entrada)
            {
                string valor = c.Replace('.', ',');
                string key = dictionary.FirstOrDefault(x => x.Value == valor).Key;
                //double valor_porArea = double.Parse(valor) * Math.Round(param.AsDouble() / 10.7639, 8);
                double valor_porArea = double.Parse(valor, style, provider) * (param.AsDouble() / 10.7639);
                sumaTotal_valor_porArea += valor_porArea;
            }
            return sumaTotal_valor_porArea;
        }

        public static double GetByValueOfKey(Dictionary<string, string> dictionary, Element element, string numeroKey)
        {
            double salida = 0;
            Parameter param = element.LookupParameter("Área");//en revit aparece en m2 pero en la api te entrega el valor en pies2
            if (param == null)
            {
                param = element.LookupParameter("Area");
            }
            List<string> lista_keys = new List<string>()
            {
                "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"
            };
            List<string> entrada = DictionaryListValues(dictionary);
            NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
            CultureInfo provider = new CultureInfo("fr-FR");
            for (int i = 0; i < lista_keys.Count(); i++)
            {
                if (numeroKey == lista_keys[i])
                {
                    string valor = entrada[i].Replace('.', ',');
                    //salida = double.Parse(valor) * Math.Round(param.AsDouble() / 10.7639, 8);//convertimos "param" de pies a metros (1 m2 = 10,7639 pies2)
                    salida = double.Parse(valor, style, provider) * (param.AsDouble() / 10.7639);//convertimos "param" de pies a metros (1 m2 = 10,7639 pies2)
                }
            }
            return salida;
        }
    }
}