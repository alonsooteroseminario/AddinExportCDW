using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace AddinExportCDW
{

    [Transaction(TransactionMode.Manual)]
    public class CmdKeySchedules : IExternalCommand
    {

        /// <summary>
        /// This is the data we'll export
        /// </summary>
        public List<KeySchedule> Schedules { get; set; }

        /// <summary>
        /// Key Schedules
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="msg"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elements)
        {
            try
            {
                Schedules = new List<KeySchedule>();
                Document m_doc = commandData.Application.ActiveUIDocument.Document;

                // Get the key schedules in the project
                IEnumerable<ViewSchedule> m_ks = from e in new FilteredElementCollector(m_doc)
                                              .OfClass(typeof(ViewSchedule))
                                                 let f = e as ViewSchedule
                                                 where f.Definition.IsKeySchedule
                                                 select f;

                foreach (var x in m_ks.ToList())
                {
                    Schedules.Add(new KeySchedule(x));
                }

                // Export to json
                //ExportJson(Schedules, "Key Schedules");

                // Done
                return Result.Succeeded;

            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                return Result.Failed;
            }
        }

        ///// <summary>
        ///// Write an Object to Json
        ///// </summary>
        ///// <param name="sourceData">Top level object to write out to file</param>
        ///// <param name="objName">command kind to use in file name</param>
        ///// <returns>TRUE if file exists after main function</returns>
        ///// <remarks></remarks>
        //private void ExportJson(object sourceData, string objName)
        //{
        //    try
        //    {

        //        // Export Path - change this path to suit your neeeds        
        //        string m_exportPath = @"c:\temp";
        //        if (!Directory.Exists(m_exportPath)) return;

        //        string m_fileName = string.Format("{0} {1}.json",
        //          DateTime.Now.ToString("yyMMdd hhmmss"), objName);
        //        string m_finalPath = Path.Combine(m_exportPath, m_fileName);
        //        using (StreamWriter sw = new StreamWriter(m_finalPath, false))
        //        {
        //            sw.WriteLine(JsonConvert.SerializeObject(sourceData));
        //        }
        //    }
        //    catch { }
        //}
    }

    /// <summary>
    /// Key Schedule Object
    /// </summary>
    public class KeySchedule
    {

        public string Category { get; set; }
        public string Name { get; set; }
        public string KeyParameterName { get; set; }
        public List<KeyScheduleRow> Rows { get; set; }

        /// <summary>
        /// Key Schedule
        /// </summary>
        /// <param name="v"></param>
        public KeySchedule(ViewSchedule v)
        {
            Rows = new List<KeyScheduleRow>();
            Name = v.Name;
            foreach (Category x in v.Document.Settings.Categories)
            {
                if (x.Id.IntegerValue == v.Definition.CategoryId.IntegerValue)
                {
                    Category = x.Name;
                    break;
                }
            }
            KeyParameterName = v.KeyScheduleParameterName;
            IEnumerable<Element> m_e = from e in new FilteredElementCollector(v.Document, v.Id) select e;
            foreach (var elem in m_e)
            {
                Rows.Add(new KeyScheduleRow(elem));
            }
        }

    }

    /// <summary>
    /// Key Schedule Row
    /// Skips readonly parameters
    /// </summary>
    public class KeyScheduleRow
    {

        public string KeyName { get; set; }
        public List<KeyScheduleRowParameter> Parameters { get; set; }

        public KeyScheduleRow(Element e)
        {
            Parameters = new List<KeyScheduleRowParameter>();
            KeyName = e.Name;
            foreach (Parameter x in e.Parameters)
            {
                if (x.IsReadOnly) continue;
                var m_name = x.Definition.Name.ToLower();
                switch (m_name)
                {
                    case "family name":
                        continue;
                    case "type name":
                        continue;
                    case "category":
                        continue;
                    case "design option":
                        continue;
                }
                Parameters.Add(new KeyScheduleRowParameter(x));
            }
        }

    }

    /// <summary>
    /// Key Schedule Row Parameter
    /// </summary>
    public class KeyScheduleRowParameter
    {

        public string Name { get; set; }
        public string Value { get; set; }

        public KeyScheduleRowParameter(Parameter p)
        {
            Name = p.Definition.Name;
            Value = GetValueString(p);
        }

        /// <summary>
        /// Get a string parameter value as a string
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string GetValueString(Parameter p)
        {
            try
            {
                if (p != null)
                {
                    if (p.StorageType == StorageType.None)
                        return "";

                    return p.StorageType == StorageType.String
                      ? p.AsString()
                      : p.AsValueString();

                }
            }
            catch { }
            return "{error}";
        }

    }
}
