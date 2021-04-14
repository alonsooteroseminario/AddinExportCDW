using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddinExportCDW
{
    public static class CalcArea
    {
        public static double Get(Dictionary<string, string>  dictionary, Element element)
        {
			Parameter param = element.LookupParameter("Área");

			#region codigos de diccionarios
			string valor2 = dictionary["07 07 01 - aqueous washing liquids"];//
			string valor3 = dictionary["15 01 02 - plastic packaging"];
			string valor4 = dictionary["15 01 03 - wooden packaging"];
			string valor5 = dictionary["15 01 04 - metallic packaging"];//
			string valor6 = dictionary["15 01 06 - mixed packaging"];//
			string valor7 = dictionary["17 01 01 - concrete"];//
			string valor8 = dictionary["17 02 01 - wood"];
			string valor9 = dictionary["17 02 03 - plastic"];
			string valor10 = dictionary["17 04 05 - iron and steel"];//
			string valor11 = dictionary["17 09 04 - mixed"];//
			#endregion

			#region keys
			string key2 = dictionary.FirstOrDefault(x => x.Value == valor2).Key;//07 07 01 - aqueous washing liquids
			string key3 = dictionary.FirstOrDefault(x => x.Value == valor3).Key;//"15 01 02 - plastic packaging
			string key4 = dictionary.FirstOrDefault(x => x.Value == valor4).Key;//"15 01 03 - wooden packaging"
			string key5 = dictionary.FirstOrDefault(x => x.Value == valor5).Key;//"15 01 04 - metallic packaging"
			string key6 = dictionary.FirstOrDefault(x => x.Value == valor6).Key;//"15 01 06 - mixed packaging"
			string key7 = dictionary.FirstOrDefault(x => x.Value == valor7).Key;//"17 01 01 - concrete"
			string key8 = dictionary.FirstOrDefault(x => x.Value == valor8).Key;//"17 02 01 - wood"
			string key9 = dictionary.FirstOrDefault(x => x.Value == valor9).Key;//"17 02 03 - plastic"
			string key10 = dictionary.FirstOrDefault(x => x.Value == valor10).Key;//"17 04 05 - iron and steel"
			string key11 = dictionary.FirstOrDefault(x => x.Value == valor11).Key;//"17 09 04 - mixed"
			#endregion


			double valor2_porArea = double.Parse(valor2) * Math.Round(param.AsDouble() / 10.7639);//07 07 01 - aqueous washing liquids X AREA
			double valor3_porArea = double.Parse(valor3) * Math.Round(param.AsDouble() / 10.7639);//"15 01 02 - plastic packaging" X AREA
			double valor4_porArea = double.Parse(valor4) * Math.Round(param.AsDouble() / 10.7639);//"15 01 03 - wooden packaging" X AREA
			double valor5_porArea = double.Parse(valor5) * Math.Round(param.AsDouble() / 10.7639);//"15 01 04 - metallic packaging" X AREA
			double valor6_porArea = double.Parse(valor6) * Math.Round(param.AsDouble() / 10.7639);//"15 01 06 - mixed packaging"]  X AREA
			double valor7_porArea = double.Parse(valor7) * Math.Round(param.AsDouble() / 10.7639);//"17 01 01 - concrete"  X AREA
			double valor8_porArea = double.Parse(valor8) * Math.Round(param.AsDouble() / 10.7639);//"17 02 01 - wood"  X AREA
			double valor9_porArea = double.Parse(valor9) * Math.Round(param.AsDouble() / 10.7639);//"17 02 03 - plastic"  X AREA
			double valor10_porArea = double.Parse(valor10) * Math.Round(param.AsDouble() / 10.7639);//"17 04 05 - iron and steel"  X AREA
			double valor11_porArea = double.Parse(valor11) * Math.Round(param.AsDouble() / 10.7639);//"17 09 04 - mixed"  X AREA

			double sumaTotal_valor_porArea = valor2_porArea + valor3_porArea + valor4_porArea 
											+ valor5_porArea + valor6_porArea + valor7_porArea +
											valor8_porArea + valor9_porArea + valor10_porArea + 
											valor11_porArea;

			return sumaTotal_valor_porArea;

		}

		public static double GetByValueOfKey(Dictionary<string, string> dictionary, Element element, string numeroKey) // key del 2 al 11
        {
			double salida = 0;
			Parameter param = element.LookupParameter("Área");
			if (numeroKey == "2")
            {
				string valor2 = dictionary["07 07 01 - aqueous washing liquids"];//
				salida = double.Parse(valor2) * Math.Round(param.AsDouble() / 10.7639);//07 07 01 - aqueous washing liquids X AREA
			}
			if (numeroKey == "3")
			{
				string valor3 = dictionary["15 01 02 - plastic packaging"];
				salida = double.Parse(valor3) * Math.Round(param.AsDouble() / 10.7639);//"15 01 02 - plastic packaging" X AREA
			}
			if (numeroKey == "4")
			{
				string valor4 = dictionary["15 01 03 - wooden packaging"];
				salida = double.Parse(valor4) * Math.Round(param.AsDouble() / 10.7639);//"15 01 03 - wooden packaging" X AREA
			}
			if (numeroKey == "5")
			{
				string valor5 = dictionary["15 01 04 - metallic packaging"];//
				salida = double.Parse(valor5) * Math.Round(param.AsDouble() / 10.7639);//"15 01 04 - metallic packaging" X AREA
			}
			if (numeroKey == "6")
			{
				string valor6 = dictionary["15 01 06 - mixed packaging"];//
				salida = double.Parse(valor6) * Math.Round(param.AsDouble() / 10.7639);//"15 01 06 - mixed packaging"]  X AREA
			}
			if (numeroKey == "7")
			{
				string valor7 = dictionary["17 01 01 - concrete"];//
				salida = double.Parse(valor7) * Math.Round(param.AsDouble() / 10.7639);//"17 01 01 - concrete"  X AREA
			}
			if (numeroKey == "8")
			{
				string valor8 = dictionary["17 02 01 - wood"];
				salida = double.Parse(valor8) * Math.Round(param.AsDouble() / 10.7639);//"17 02 01 - wood"  X AREA
			}
			if (numeroKey == "9")
			{
				string valor9 = dictionary["17 02 03 - plastic"];
				salida = double.Parse(valor9) * Math.Round(param.AsDouble() / 10.7639);//"17 02 03 - plastic"  X AREA
			}
			if (numeroKey == "10")
			{
				string valor10 = dictionary["17 04 05 - iron and steel"];//
				salida = double.Parse(valor10) * Math.Round(param.AsDouble() / 10.7639);//"17 04 05 - iron and steel"  X AREA
			}
			if (numeroKey == "11")
			{
				string valor11 = dictionary["17 09 04 - mixed"];//
				salida = double.Parse(valor11) * Math.Round(param.AsDouble() / 10.7639);//"17 09 04 - mixed"  X AREA
			}

			return salida;
		}
	}
}
