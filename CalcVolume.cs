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
                if ( !(entry.Key == "Structural element") || !(entry.Key == "Código") )
                {
					salida.Add(entry.Value);
				}
            }

            #region valor
            //string valor2 = dictionary["07 07 01 - aqueous washing liquids"];//
            //salida.Add(valor2);
            //string valor3 = dictionary["15 01 02 - plastic packaging"];
            //salida.Add(valor3);
            //string valor4 = dictionary["15 01 03 - wooden packaging"];
            //salida.Add(valor4);
            //string valor5 = dictionary["15 01 04 - metallic packaging"];//
            //salida.Add(valor5);
            //string valor6 = dictionary["15 01 06 - mixed packaging"];//
            //salida.Add(valor6);
            //string valor7 = dictionary["17 01 01 - concrete"];//
            //salida.Add(valor7);
            //string valor8 = dictionary["17 02 01 - wood"];
            //salida.Add(valor8);
            //string valor9 = dictionary["17 02 03 - plastic"];
            //salida.Add(valor9);
            //string valor10 = dictionary["17 04 05 - iron and steel"];//
            //salida.Add(valor10);
            //string valor11 = dictionary["17 09 04 - mixed"];//
            //salida.Add(valor11);
            #endregion

            #region keys
            //string key2 = dictionary.FirstOrDefault(x => x.Value == valor2).Key;//07 07 01 - aqueous washing liquids
            //string key3 = dictionary.FirstOrDefault(x => x.Value == valor3).Key;//"15 01 02 - plastic packaging
            //string key4 = dictionary.FirstOrDefault(x => x.Value == valor4).Key;//"15 01 03 - wooden packaging"
            //string key5 = dictionary.FirstOrDefault(x => x.Value == valor5).Key;//"15 01 04 - metallic packaging"
            //string key6 = dictionary.FirstOrDefault(x => x.Value == valor6).Key;//"15 01 06 - mixed packaging"
            //string key7 = dictionary.FirstOrDefault(x => x.Value == valor7).Key;//"17 01 01 - concrete"
            //string key8 = dictionary.FirstOrDefault(x => x.Value == valor8).Key;//"17 02 01 - wood"
            //string key9 = dictionary.FirstOrDefault(x => x.Value == valor9).Key;//"17 02 03 - plastic"
            //string key10 = dictionary.FirstOrDefault(x => x.Value == valor10).Key;//"17 04 05 - iron and steel"
            //string key11 = dictionary.FirstOrDefault(x => x.Value == valor11).Key;//"17 09 04 - mixed"
            #endregion

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
