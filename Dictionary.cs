using System.Collections.Generic;

namespace AddinExportCDW
{
    public static class Dictionary
    {
        public static Dictionary<string, string> Get(string elementName)
        {
            #region Dictionarios

            Dictionary<string, string> data_forjado = new Dictionary<string, string>(){
                {"Structural element", "Concrete waffle slab (20+5) cm"},
                {"Código", "05WCH80110N"},
                {"07 07 01 aqueous washing liquids", "0,000008"},
                {"15 01 02 plastic packaging", "0,000554"},//
				{"15 01 03 wooden packaging", "0,004619"},//
				{"15 01 04 metallic packaging", "0,000468"},
                {"15 01 06 mixed packaging", "0,000056"},
                {"17 01 01 concrete", "0,004070"},
                {"17 02 01 wood", "0,001020"},//
				{"17 02 03 plastic", "0,004385"},//
				{"17 04 05 iron and steel", "0,000055"},
                {"17 09 04 mixed", "0,000095"},
            };
            Dictionary<string, string> data_pilar_hormigon = new Dictionary<string, string>(){
                {"Structural element", "Concrete column"},
                {"Código", "05HRP80020"},
                {"07 07 01 aqueous washing liquids", "0,000344"},
                {"15 01 02 plastic packaging", "0"},//
				{"15 01 03 wooden packaging", "0"},//
				{"15 01 04 metallic packaging", "0,019147"},
                {"15 01 06 mixed packaging", "0,000191"},
                {"17 01 01 concrete", "0,022000"},
                {"17 02 01 wood", "0"},//
				{"17 02 03 plastic", "0"},//
				{"17 04 05 iron and steel", "0,000990"},
                {"17 09 04 mixed", "0,000233"},
            };
            Dictionary<string, string> data_floors_concreto = new Dictionary<string, string>(){
                {"Structural element", "Concrete ground slab"},
                {"Código", "03HRL80090"},//
                {"07 07 01 aqueous washing liquids", "0"},
                {"15 01 02 plastic packaging", "0"},
                {"15 01 03 wooden packaging", "0"},
                {"15 01 04 metallic packaging", "0"},
                {"15 01 06 mixed packaging", "0"},
                {"17 01 01 concrete", "0,022"},//
                {"17 02 01 wood", "0"},
                {"17 02 03 plastic", "0"},
                {"17 04 05 iron and steel", "0,000050"},//
                {"17 09 04 mixed", "0,000221"},//
            };
            Dictionary<string, string> data_Cimentaciones = new Dictionary<string, string>(){
                {"Structural element", "Foundation wall"},
                {"Código", "03HRM80080"},//
                {"07 07 01 aqueous washing liquids", "0,000017"},//
				{"15 01 02 plastic packaging", "0"},
                {"15 01 03 wooden packaging", "0"},
                {"15 01 04 metallic packaging", "0,001018"},//
				{"15 01 06 mixed packaging", "0,000010"},//
				{"17 01 01 concrete", "0,022000"},//
                {"17 02 01 wood", "0,003944"},//
				{"17 02 03 plastic", "0"},
                {"17 04 05 iron and steel", "0,000193"},//
                {"17 09 04 mixed", "0,000262"},//
            };
            Dictionary<string, string> data_ConcretoDeck = new Dictionary<string, string>(){
                {"Structural element", "Concrete deck"},//
				{"Código", "05HRL80020"},//
                {"07 07 01 aqueous washing liquids", "0,000049"},//
				{"15 01 02 plastic packaging", "0"},
                {"15 01 03 wooden packaging", "0"},
                {"15 01 04 metallic packaging", "0,002867"},//
				{"15 01 06 mixed packaging", "0,000029"},//
				{"17 01 01 concrete", "0,022000"},//
                {"17 02 01 wood", "0,008330"},//
				{"17 02 03 plastic", "0"},
                {"17 04 05 iron and steel", "0,000378"},//
                {"17 09 04 mixed", "0,000308"},//
            };
            Dictionary<string, string> data_Droppedbeam = new Dictionary<string, string>(){
                {"Structural element", "Dropped beam"},//
				{"Código", "05HRJ80110"},//
                {"07 07 01 aqueous washing liquids", "0,000041"},//
				{"15 01 02 plastic packaging", "0"},
                {"15 01 03 wooden packaging", "0"},
                {"15 01 04 metallic packaging", "0,003045"},//
				{"15 01 06 mixed packaging", "0,000030"},//
				{"17 01 01 concrete", "0,022000"},//
                {"17 02 01 wood", "0,008893"},//
				{"17 02 03 plastic", "0"},
                {"17 04 05 iron and steel", "0,000078"},//
                {"17 09 04 mixed", "0,000310"},//
            };
            Dictionary<string, string> data_Beamembbeded = new Dictionary<string, string>(){
                {"Structural element", "Beam embbeded floor"},//
				{"Código", "05HRJ80020"},//
				{"07 07 01 aqueous washing liquids", "0,000039"},//
				{"15 01 02 plastic packaging", "0"},
                {"15 01 03 wooden packaging", "0"},
                {"15 01 04 metallic packaging", "0,002907"},//
				{"15 01 06 mixed packaging", "0,000029"},//
				{"17 01 01 concrete", "0,022000"},//
                {"17 02 01 wood", "0,008488"},//
				{"17 02 03 plastic", "0"},
                {"17 04 05 iron and steel", "0,000078"},//
                {"17 09 04 mixed", "0,000306"},//
            };
            Dictionary<string, string> data_ConcreteInclinedSlab = new Dictionary<string, string>(){
                {"Structural element", "Concrete inclined slab"},//
				{"Código", "05HRL80080"},//
				{"07 07 01 aqueous washing liquids", "0,000066"},//
				{"15 01 02 plastic packaging", "0"},
                {"15 01 03 wooden packaging", "0"},
                {"15 01 04 metallic packaging", "0,000001"},//
				{"15 01 06 mixed packaging", "0,000000"},//
				{"17 01 01 concrete", "0,022000"},//
                {"17 02 01 wood", "0,011390"},//
				{"17 02 03 plastic", "0"},
                {"17 04 05 iron and steel", "0,000403"},//
                {"17 09 04 mixed", "0,000339"},//
            };
            Dictionary<string, string> data_walls = new Dictionary<string, string>(){
                {"Structural element", "Concrete wall"},//
				{"Código", "05HRM80050"},//
				{"07 07 01 aqueous washing liquids", "0,000344"},//
				{"15 01 02 plastic packaging", "0"},
                {"15 01 03 wooden packaging", "0"},
                {"15 01 04 metallic packaging", "0,019147"},//
				{"15 01 06 mixed packaging", "0,000191"},//
				{"17 01 01 concrete", "0,022000"},//
                {"17 02 01 wood", "0"},
                {"17 02 03 plastic", "0"},
                {"17 04 05 iron and steel", "0,000146"},//
                {"17 09 04 mixed", "0,000225"},//
            };
            Dictionary<string, string> data_SteelColumns = new Dictionary<string, string>(){
                {"Structural element", "Steel column"},
                {"Código", "05ACS00000"},
                {"07 07 01 aqueous washing liquids", "0"},
                {"15 01 02 plastic packaging", "0"},
                {"15 01 03 wooden packaging", "0"},
                {"15 01 04 metallic packaging", "0"},
                {"15 01 06 mixed packaging", "0"},
                {"17 01 01 concrete", "0"},
                {"17 02 01 wood", "0"},
                {"17 02 03 plastic", "0"},
                {"17 04 05 iron and steel", "0,000001"},//
                {"17 09 04 mixed", "0,00000001"},//
            };
            Dictionary<string, string> data_forjado35 = new Dictionary<string, string>(){
                {"Structural element", "Concrete waffle slab (30+5) cm"},
                {"Código", "05WCH80110"},
                {"07 07 01 aqueous washing liquids", "0,000009"},//
                {"15 01 02 plastic packaging", "0,000609"},//
				{"15 01 03 wooden packaging", "0,005081"},//
				{"15 01 04 metallic packaging", "0,000515"},//
                {"15 01 06 mixed packaging", "0,000062"},//
                {"17 01 01 concrete", "0,004477"},//
                {"17 02 01 wood", "0,001122"},//
				{"17 02 03 plastic", "0,004824"},//
				{"17 04 05 iron and steel", "0,000061"},//
                {"17 09 04 mixed", "0,000105"},//
            };

            Dictionary<string, string> data_foundation = new Dictionary<string, string>(){
                {"Structural element", "Foundation"},
                {"Código", "03HRZ80010"},
                {"07 07 01 aqueous washing liquids", "0"},
                {"15 01 02 plastic packaging", "0"},
                {"15 01 03 wooden packaging", "0"},
                {"15 01 04 metallic packaging", "0"},
                {"15 01 06 mixed packaging", "0"},
                {"17 01 01 concrete", "0,022"},
                {"17 02 01 wood", "0"},
                {"17 02 03 plastic", "0"},
                {"17 04 05 iron and steel", "0,00005"},
                {"17 09 04 mixed", "0,000221"},
            };
            Dictionary<string, string> data_CollaboratingSheetMetal = new Dictionary<string, string>(){
                {"Structural element", "Collaborating sheet metal"},
                //{"Código", "05FWW90100"},
                {"Código", "05FWW90003"},
                {"07 07 01 aqueous washing liquids", "0,000049"},
                {"15 01 02 plastic packaging", "0"},
                {"15 01 03 wooden packaging", "0"},
                {"15 01 04 metallic packaging", "0,002867"},
                {"15 01 06 mixed packaging", "0,000029"},
                {"17 01 01 concrete", "0,022000"},
                {"17 02 01 wood", "0,008330"},
                {"17 02 03 plastic", "0"},
                {"17 04 05 iron and steel", "0,000378"},
                {"17 09 04 mixed", "0,000308"},
            };
            Dictionary<string, string> data_Steelbeam = new Dictionary<string, string>(){
                {"Structural element", "Steel beam"},
                {"Código", "05ACJ00040"},
                {"07 07 01 aqueous washing liquids", "0"},
                {"15 01 02 plastic packaging", "0"},
                {"15 01 03 wooden packaging", "0"},
                {"15 01 04 metallic packaging", "0"},
                {"15 01 06 mixed packaging", "0"},
                {"17 01 01 concrete", "0"},
                {"17 02 01 wood", "0"},
                {"17 02 03 plastic", "0"},
                {"17 04 05 iron and steel", "0,000001"},
                {"17 09 04 mixed", "0"},
            };

            #endregion Dictionarios

            Dictionary<string, string> DictionaryOutPut = new Dictionary<string, string>();
            if (elementName == "data_forjado")
            {
                DictionaryOutPut = data_forjado;
            }
            if (elementName == "data_pilar_hormigon")
            {
                DictionaryOutPut = data_pilar_hormigon;
            }
            if (elementName == "data_floors_concreto")
            {
                DictionaryOutPut = data_floors_concreto;
            }
            if (elementName == "data_Cimentaciones")
            {
                DictionaryOutPut = data_Cimentaciones;
            }
            if (elementName == "data_ConcretoDeck")
            {
                DictionaryOutPut = data_ConcretoDeck;
            }
            if (elementName == "data_Droppedbeam")
            {
                DictionaryOutPut = data_Droppedbeam;
            }
            if (elementName == "data_Beamembbeded")
            {
                DictionaryOutPut = data_Beamembbeded;
            }
            if (elementName == "data_ConcreteInclinedSlab")
            {
                DictionaryOutPut = data_ConcreteInclinedSlab;
            }
            if (elementName == "data_walls")
            {
                DictionaryOutPut = data_walls;
            }
            if (elementName == "data_SteelColumns")
            {
                DictionaryOutPut = data_SteelColumns;
            }
            if (elementName == "data_forjado35")
            {
                DictionaryOutPut = data_forjado35;
            }
            if (elementName == "data_foundation")
            {
                DictionaryOutPut = data_foundation;
            }
            if (elementName == "data_CollaboratingSheetMetal")
            {
                DictionaryOutPut = data_CollaboratingSheetMetal;
            }
            if (elementName == "data_Steelbeam")
            {
                DictionaryOutPut = data_Steelbeam;
            }
            return DictionaryOutPut;
        }

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

        public static List<string> DictionaryListKeys(Dictionary<string, string> dictionary)
        {
            List<string> salida = new List<string>();
            foreach (KeyValuePair<string, string> entry in dictionary)
            {
                salida.Add(entry.Key);
            }
            salida.RemoveRange(0, 2);
            return salida;
        }
    }
}