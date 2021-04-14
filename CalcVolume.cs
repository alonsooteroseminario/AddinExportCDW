using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

			int mensaje = salida.Count();

			return salida;

		}
		public static double Get(Dictionary<string, string> dictionary, Element element)
		{
			Parameter param = element.LookupParameter("Volumen");

			List<string> entrada = DictionaryListValues(dictionary);

			double sumaTotal_valor_porVolumen = 0;

			foreach (string c in entrada)
            {
				string valor = c;
				string key = dictionary.FirstOrDefault(x => x.Value == valor).Key;
				double valor_porVolumen = double.Parse(valor) * Math.Round(param.AsDouble() / 35.3147, 4);
				sumaTotal_valor_porVolumen = sumaTotal_valor_porVolumen + valor_porVolumen;
			}

			return sumaTotal_valor_porVolumen;
		}

		public static double GetByValueOfKey(Dictionary<string, string> dictionary, Element element, string numeroKey) // key del 2 al 11
        {
			double salida = 0;

			Parameter param = element.LookupParameter("Volumen");

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
					salida = double.Parse(valor) * Math.Round(param.AsDouble() / 35.3147, 4);
				}
			}


			return salida;
		}
	}
}
