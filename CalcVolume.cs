using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddinExportCDW
{
    public static class CalcVolume
    {
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
            Parameter param = element.LookupParameter("Volume");
            if (param == null)
            {
                param = element.LookupParameter("Volumen");
            }
            List<string> entrada = DictionaryListValues(dictionary);
            double sumaTotal_valor_porVolumen = 0;
            foreach (string c in entrada)
            {
                string valor = c;
                string key = dictionary.FirstOrDefault(x => x.Value == valor).Key;
                double valor_porVolumen = double.Parse(valor) * Math.Round(param.AsDouble() / 35.3147, 8);
                sumaTotal_valor_porVolumen += valor_porVolumen;
            }
            return sumaTotal_valor_porVolumen;
        }

        public static double Get_SteelColumnSpecialCommand(Dictionary<string, string> dictionary, Element element)
        {
            //constante
            double areaSeccion_SteelColumns = 0.00271; //m2
            //constante
            Parameter paramAltura_SteelColumns = element.LookupParameter("Desfase superior");// 3.940 m pero revit api lo entrega en pies (1 m = 3.28084 pie )
            if (paramAltura_SteelColumns == null)
            {
                paramAltura_SteelColumns = element.LookupParameter("Top Offset");
            }
            double altura_SteelColumns = (paramAltura_SteelColumns.AsDouble() / 3.28084);// convertimos pies a m
            double paramVolumenCalculated = areaSeccion_SteelColumns * altura_SteelColumns;// m2 * m = m3

            List<string> entrada = DictionaryListValues(dictionary);
            double sumaTotal_valor_porVolumen = 0;
            foreach (string valor in entrada)
            {
                double valor_porVolumen = double.Parse(valor) * Math.Round(paramVolumenCalculated, 8);
                sumaTotal_valor_porVolumen += valor_porVolumen;
            }
            return sumaTotal_valor_porVolumen;
        }

        public static double GetByValueOfKey(Dictionary<string, string> dictionary, Element element, string numeroKey)
        {
            double salida = 0;
            Parameter param = element.LookupParameter("Volume");
            if (param == null)
            {
                param = element.LookupParameter("Volumen");
            }
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
                    salida = double.Parse(valor) * Math.Round(param.AsDouble() / 35.3147, 8);
                }
            }
            return salida;
        }

        public static double GetByValueOfKey_SteelColumnSpecialCommand(Dictionary<string, string> dictionary, Element element, string numeroKey)
        {
            double salida = 0;
            //constante
            double areaSeccion_SteelColumns = 0.00271; //m2
            //constante
            Parameter paramAltura_SteelColumns = element.LookupParameter("Desfase superior");// 3.940 m pero revit api lo entrega en pies (1 m = 3.28084 pie )
            if (paramAltura_SteelColumns == null)
            {
                paramAltura_SteelColumns = element.LookupParameter("Top Offset");
            }
            double altura_SteelColumns = (paramAltura_SteelColumns.AsDouble() / 3.28084);// convertimos pies a m
            double paramVolumenCalculated = areaSeccion_SteelColumns * altura_SteelColumns;// m2 * m = m3

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
                    salida = double.Parse(valor) * Math.Round(paramVolumenCalculated, 8);
                }
            }
            return salida;
        }
    }
}