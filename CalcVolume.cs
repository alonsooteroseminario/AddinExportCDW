using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
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

        public static double Get(ExternalCommandData commandData,
                                 Dictionary<string, string> dictionary,
                                 Element element)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            double volume = 0;
            foreach (ElementId id in element.GetMaterialIds(false))
            {
                Material material = doc.GetElement(id) as Material;
                volume = element.GetMaterialVolume(material.Id);
                StepLog.Write(commandData, volume.ToString());
            }

            List<string> entrada = DictionaryListValues(dictionary);
            double sumaTotal_valor_porVolumen = 0;
            foreach (string c in entrada)
            {
                string valor = c;
                string key = dictionary.FirstOrDefault(x => x.Value == valor).Key;
                double valor_porVolumen = double.Parse(valor) * Math.Round(volume / 35.3147, 8);
                sumaTotal_valor_porVolumen += valor_porVolumen;
                StepLog.Write(commandData, sumaTotal_valor_porVolumen.ToString());
            }
            return sumaTotal_valor_porVolumen;
        }

        public static double GetByValueOfKey(ExternalCommandData commandData,
                                            Dictionary<string, string> dictionary,
                                            Element element,
                                            string numeroKey)
        {
            double salida = 0;
            Document doc = commandData.Application.ActiveUIDocument.Document;
            double volume = 0;
            foreach (ElementId id in element.GetMaterialIds(false))
            {
                Material material = doc.GetElement(id) as Material;
                volume = element.GetMaterialVolume(material.Id);
                StepLog.Write(commandData, volume.ToString());
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
                    salida = double.Parse(valor) * Math.Round(volume / 35.3147, 8);
                    StepLog.Write(commandData, salida.ToString());
                }
            }
            return salida;
        }













        public static double Get_SteelBeamSpecialCommand(ExternalCommandData commandData,
                 Dictionary<string, string> dictionary,
                 Element element)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            double volume = 0;
            foreach (ElementId id in element.GetMaterialIds(false))
            {
                Material material = doc.GetElement(id) as Material;
                volume = element.GetMaterialVolume(material.Id);
                StepLog.Write(commandData, volume.ToString());
            }

            List<string> entrada = DictionaryListValues(dictionary);
            double sumaTotal_valor_porVolumen = 0;
            foreach (string c in entrada)
            {
                string valor = c;
                string key = dictionary.FirstOrDefault(x => x.Value == valor).Key;
                double valor_porVolumen = double.Parse(valor) * Math.Round(volume / 35.3147, 8) * (7850);
                sumaTotal_valor_porVolumen += valor_porVolumen;
                StepLog.Write(commandData, sumaTotal_valor_porVolumen.ToString());
            }
            return sumaTotal_valor_porVolumen;
        }

        public static double GetByValueOfKey_SteelBeamSpecialCommand(ExternalCommandData commandData,
                                    Dictionary<string, string> dictionary,
                                    Element element,
                                    string numeroKey)
        {
            double salida = 0;
            Document doc = commandData.Application.ActiveUIDocument.Document;
            double volume = 0;
            foreach (ElementId id in element.GetMaterialIds(false))
            {
                Material material = doc.GetElement(id) as Material;
                volume = element.GetMaterialVolume(material.Id);
                StepLog.Write(commandData, volume.ToString());
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
                    salida = double.Parse(valor) * Math.Round((volume / 35.3147), 8) * (7850);
                    StepLog.Write(commandData, salida.ToString());
                }
            }
            return salida;
        }










        public static double Get_SteelColumnSpecialCommand(ExternalCommandData commandData,
                                                            Dictionary<string, string> dictionary,
                                                            Element element)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            double volume = 0;
            foreach (ElementId id in element.GetMaterialIds(false))
            {
                Material material = doc.GetElement(id) as Material;
                volume = element.GetMaterialVolume(material.Id);
                StepLog.Write(commandData, volume.ToString());
            }

            List<string> entrada = DictionaryListValues(dictionary);
            double sumaTotal_valor_porVolumen = 0;
            foreach (string valor in entrada)
            {
                double valor_porVolumen = double.Parse(valor) * Math.Round((volume / 35.3147), 8) * (7850);
                sumaTotal_valor_porVolumen += valor_porVolumen;
                StepLog.Write(commandData, sumaTotal_valor_porVolumen.ToString());
            }
            return sumaTotal_valor_porVolumen;
        }

        public static double GetByValueOfKey_SteelColumnSpecialCommand(ExternalCommandData commandData,
                                                                        Dictionary<string,
                                                                        string> dictionary,
                                                                        Element element,
                                                                        string numeroKey)
        {
            double salida = 0;
            Document doc = commandData.Application.ActiveUIDocument.Document;
            double volume = 0;
            foreach (ElementId id in element.GetMaterialIds(false))
            {
                Material material = doc.GetElement(id) as Material;
                volume = element.GetMaterialVolume(material.Id);
                StepLog.Write(commandData, volume.ToString());
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
                    salida = double.Parse(valor) * Math.Round((volume / 35.3147), 8) * (7850);
                    StepLog.Write(commandData, salida.ToString());
                }
            }
            return salida;
        }
        public static double GetByValueOfKey_StairsSpecialCommand(ExternalCommandData commandData,
                                                                Dictionary<string,
                                                                string> dictionary,
                                                                Element element,
                                                                string numeroKey)
        {
            double salida = 0;
            Document doc = commandData.Application.ActiveUIDocument.Document;
            double volume = 0;
            foreach (ElementId id in element.GetMaterialIds(false))
            {
                Material material = doc.GetElement(id) as Material;
                volume = element.GetMaterialVolume(material.Id);
                StepLog.Write(commandData, volume.ToString());
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
                    salida = double.Parse(valor) * Math.Round((volume / 35.3147), 8) * (7850);
                    StepLog.Write(commandData, salida.ToString());
                }
            }
            return salida;
        }
        public static double Get_StairsSpecialCommand(ExternalCommandData commandData,
                                                    Dictionary<string, string> dictionary,
                                                    Element element)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            double volume = 0;
            foreach (ElementId id in element.GetMaterialIds(false))
            {
                Material material = doc.GetElement(id) as Material;
                volume = element.GetMaterialVolume(material.Id);
                StepLog.Write(commandData, volume.ToString());
            }

            List<string> entrada = DictionaryListValues(dictionary);
            double sumaTotal_valor_porVolumen = 0;
            foreach (string valor in entrada)
            {
                double valor_porVolumen = double.Parse(valor) * Math.Round((volume / 35.3147), 8) * (7850);
                sumaTotal_valor_porVolumen += valor_porVolumen;
                StepLog.Write(commandData, sumaTotal_valor_porVolumen.ToString());
            }
            return sumaTotal_valor_porVolumen;
        }
    }
}