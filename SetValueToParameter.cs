using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;

namespace AddinExportCDW
{
    public static class SetValueToParameter
    {
        public static void SetArea(Dictionary<string, string> data,
                                Element sc,
                                Document doc)
        {
            int i = 2;
            foreach (string key in Dictionary.DictionaryListKeys(data))
            {
                Parameter parametro = sc.LookupParameter(key);
                if (parametro != null)
                {
                    using (Transaction t = new Transaction(doc, "Add CDW value to each Element"))
                    {
                        t.Start();
                        parametro.Set(CalcArea.GetByValueOfKey(data, sc, i.ToString()).ToString());
                        t.Commit();
                    }
                }
                i++;
            }
        }

        public static void SetVolume(ExternalCommandData commandData,
                                    Dictionary<string, string> data,
                                    Element sc,
                                    Document doc)
        {
            int i = 2;
            foreach (string key in Dictionary.DictionaryListKeys(data))
            {
                Parameter parametro = sc.LookupParameter(key);
                if (parametro != null)
                {
                    using (Transaction t = new Transaction(doc, "Add CDW value to each Element"))
                    {
                        t.Start();
                        parametro.Set(CalcVolume.GetByValueOfKey(commandData, data, sc, i.ToString()).ToString());
                        t.Commit();
                    }
                }
                i++;
            }
        }

        public static void SetVolume_SteelColumnSpecialCommand(ExternalCommandData commandData,
                                                                Dictionary<string,
                                                                string> data,
                                                                Element sc,
                                                                Document doc)
        {
            int i = 2;
            foreach (string key in Dictionary.DictionaryListKeys(data))
            {
                Parameter parametro = sc.LookupParameter(key);
                if (parametro != null)
                {
                    using (Transaction t = new Transaction(doc, "Add CDW value to each Element"))
                    {
                        t.Start();
                        parametro.Set(CalcVolume.GetByValueOfKey_SteelColumnSpecialCommand(commandData, data, sc, i.ToString()).ToString());
                        t.Commit();
                    }
                }
                i++;
            }
        }
    }
}