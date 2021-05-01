using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AddinExportCDW
{
    public static class StepLog
    {
        public static void Write(ExternalCommandData commandData,
                                                string mensaje)
        {
            #region Comandos entrada

            //Get application and document objects
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            Document doc = uiApp.ActiveUIDocument.Document;
            Application app = uiApp.Application;
            // Get Active View
            View activeView = uidoc.ActiveView;
            string ruta = App.ExecutingAssemblyPath;

            #endregion Comandos entrada

            String paramFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                 + "\\StepLog.txt";
            FileStream fs = File.Create(paramFile);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(mensaje + " " + DateTime.Now.ToString());
            sw.Close();
        }

        public static void Write(ExternalCommandData commandData,
                                        List<string> lista_mensajes)
        {
            #region Comandos entrada

            //Get application and document objects
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            Document doc = uiApp.ActiveUIDocument.Document;
            Application app = uiApp.Application;
            // Get Active View
            View activeView = uidoc.ActiveView;
            string ruta = App.ExecutingAssemblyPath;

            #endregion Comandos entrada

            String paramFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                 + "\\StepLog.txt";
            FileStream fs = File.Create(paramFile);
            StreamWriter sw = new StreamWriter(fs);
            foreach (string mensaje in lista_mensajes)
            {
                sw.WriteLine(mensaje + " " + DateTime.Now.ToString());
            }
            sw.Close();
        }
    }
}