using Autodesk.Revit.DB;
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
                    using (Transaction t = new Transaction(doc, "Agregar valor CDW a cada Elemento"))
                    {
                        t.Start();
                        parametro.Set(CalcArea.GetByValueOfKey(data, sc, i.ToString()).ToString());
                        t.Commit();
                    }
                }
                i = i + 1;
            }
        }

        public static void SetVolume(Dictionary<string, string> data,
                        Element sc,
                        Document doc)
        {
            int i = 2;
            foreach (string key in Dictionary.DictionaryListKeys(data))
            {
                Parameter parametro = sc.LookupParameter(key);
                if (parametro != null)
                {
                    using (Transaction t = new Transaction(doc, "Agregar valor CDW a cada Elemento"))
                    {
                        t.Start();
                        parametro.Set(CalcVolume.GetByValueOfKey(data, sc, i.ToString()).ToString());
                        t.Commit();
                    }
                }
                i = i + 1;
            }
        }
    }
}