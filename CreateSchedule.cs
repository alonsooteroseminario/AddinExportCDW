using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AddinExportCDW
{
    public static class CreateSchedule
    {
        //Crea desde cero el SharedParameterFile
        public static void CreateSharedParameterFile(ExternalCommandData commandData,
                                                        Dictionary<string, string> dictionary)
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

            List<string> keys = Dictionary.DictionaryListKeys(dictionary);
            string paramName = keys[0];
            string paramName2 = keys[1];
            string paramName3 = keys[2];
            string paramName4 = keys[3];
            string paramName5 = keys[4];
            string paramName6 = keys[5];
            string paramName7 = keys[6];
            string paramName8 = keys[7];
            string paramName9 = keys[8];
            string paramName10 = keys[9];

            DefinitionFile myDefinitionFile = app.OpenSharedParameterFile();
            StepLog.Write(commandData, myDefinitionFile.ToString());
            DefinitionGroup myGroup = myDefinitionFile.Groups.Create("Create CDW Parameters");
            StepLog.Write(commandData, myGroup.ToString());

            ExternalDefinitionCreationOptions option = new ExternalDefinitionCreationOptions(paramName, ParameterType.Text);
            ExternalDefinitionCreationOptions option2 = new ExternalDefinitionCreationOptions(paramName2, ParameterType.Text);
            ExternalDefinitionCreationOptions option3 = new ExternalDefinitionCreationOptions(paramName3, ParameterType.Text);
            ExternalDefinitionCreationOptions option4 = new ExternalDefinitionCreationOptions(paramName4, ParameterType.Text);
            ExternalDefinitionCreationOptions option5 = new ExternalDefinitionCreationOptions(paramName5, ParameterType.Text);
            ExternalDefinitionCreationOptions option6 = new ExternalDefinitionCreationOptions(paramName6, ParameterType.Text);
            ExternalDefinitionCreationOptions option7 = new ExternalDefinitionCreationOptions(paramName7, ParameterType.Text);
            ExternalDefinitionCreationOptions option8 = new ExternalDefinitionCreationOptions(paramName8, ParameterType.Text);
            ExternalDefinitionCreationOptions option9 = new ExternalDefinitionCreationOptions(paramName9, ParameterType.Text);
            ExternalDefinitionCreationOptions option10 = new ExternalDefinitionCreationOptions(paramName10, ParameterType.Text);

            option.UserModifiable = true;
            option2.UserModifiable = true;
            option3.UserModifiable = true;
            option4.UserModifiable = true;
            option5.UserModifiable = true;
            option6.UserModifiable = true;
            option7.UserModifiable = true;
            option8.UserModifiable = true;
            option9.UserModifiable = true;
            option10.UserModifiable = true;

            Definition myDefinition_ProductDate = myGroup.Definitions.Create(option);
            Definition myDefinition_ProductDate2 = myGroup.Definitions.Create(option2);
            Definition myDefinition_ProductDate3 = myGroup.Definitions.Create(option3);
            Definition myDefinition_ProductDate4 = myGroup.Definitions.Create(option4);
            Definition myDefinition_ProductDate5 = myGroup.Definitions.Create(option5);
            Definition myDefinition_ProductDate6 = myGroup.Definitions.Create(option6);
            Definition myDefinition_ProductDate7 = myGroup.Definitions.Create(option7);
            Definition myDefinition_ProductDate8 = myGroup.Definitions.Create(option8);
            Definition myDefinition_ProductDate9 = myGroup.Definitions.Create(option9);
            Definition myDefinition_ProductDate10 = myGroup.Definitions.Create(option10);

            CategorySet categories = app.Create.NewCategorySet();

            BuiltInCategory[] bics = new BuiltInCategory[]  // lista de BuiltInCategory
            {
                    BuiltInCategory.OST_Floors,
                    BuiltInCategory.OST_StructuralColumns,
                    BuiltInCategory.OST_StructuralFraming,
                    BuiltInCategory.OST_Walls,
                    BuiltInCategory.OST_StructuralFoundation,
                    BuiltInCategory.OST_Columns,
            };
            foreach (BuiltInCategory bic in bics)
            {
                Category MECat = doc.Settings.Categories.get_Item(bic);
                categories.Insert(MECat);
            }

            InstanceBinding instanceBinding = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding2 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding3 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding4 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding5 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding6 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding7 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding8 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding9 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding10 = app.Create.NewInstanceBinding(categories);

            using (Transaction t = new Transaction(doc, "Create CDW Parameters"))
            {
                t.Start();
                doc.ParameterBindings.Insert(myDefinition_ProductDate, instanceBinding, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate2, instanceBinding2, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate3, instanceBinding3, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate4, instanceBinding4, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate5, instanceBinding5, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate6, instanceBinding6, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate7, instanceBinding7, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate8, instanceBinding8, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate9, instanceBinding9, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate10, instanceBinding10, BuiltInParameterGroup.PG_CONSTRAINTS);
                StepLog.Write(commandData, "Create CDW Parameters");
                t.Commit();
            }
        }

        public static void CreateParametersWithSharedParameterFile(ExternalCommandData commandData,
                                                                    Dictionary<string, string> dictionary)
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

            List<string> keys = Dictionary.DictionaryListKeys(dictionary);
            string paramName = keys[0];
            string paramName2 = keys[1];
            string paramName3 = keys[2];
            string paramName4 = keys[3];
            string paramName5 = keys[4];
            string paramName6 = keys[5];
            string paramName7 = keys[6];
            string paramName8 = keys[7];
            string paramName9 = keys[8];
            string paramName10 = keys[9];
            DefinitionFile myDefinitionFile = app.OpenSharedParameterFile();
            DefinitionGroup myGroup = myDefinitionFile.Groups.get_Item("Create CDW Parameters");
            Definition myDefinition_ProductDate = myGroup.Definitions.get_Item(paramName);
            Definition myDefinition_ProductDate2 = myGroup.Definitions.get_Item(paramName2);
            Definition myDefinition_ProductDate3 = myGroup.Definitions.get_Item(paramName3);
            Definition myDefinition_ProductDate4 = myGroup.Definitions.get_Item(paramName4);
            Definition myDefinition_ProductDate5 = myGroup.Definitions.get_Item(paramName5);
            Definition myDefinition_ProductDate6 = myGroup.Definitions.get_Item(paramName6);
            Definition myDefinition_ProductDate7 = myGroup.Definitions.get_Item(paramName7);
            Definition myDefinition_ProductDate8 = myGroup.Definitions.get_Item(paramName8);
            Definition myDefinition_ProductDate9 = myGroup.Definitions.get_Item(paramName9);
            Definition myDefinition_ProductDate10 = myGroup.Definitions.get_Item(paramName10);
            CategorySet categories = app.Create.NewCategorySet();
            BuiltInCategory[] bics = new BuiltInCategory[]  // lista de BuiltInCategory
            {
                    BuiltInCategory.OST_Floors,
                    BuiltInCategory.OST_StructuralColumns,
                    BuiltInCategory.OST_StructuralFraming,
                    BuiltInCategory.OST_Walls,
                    BuiltInCategory.OST_StructuralFoundation,
                    BuiltInCategory.OST_Columns,
            };
            foreach (BuiltInCategory bic in bics)
            {
                Category MECat = doc.Settings.Categories.get_Item(bic);
                categories.Insert(MECat);
            }
            InstanceBinding instanceBinding = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding2 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding3 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding4 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding5 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding6 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding7 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding8 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding9 = app.Create.NewInstanceBinding(categories);
            InstanceBinding instanceBinding10 = app.Create.NewInstanceBinding(categories);
            using (Transaction t = new Transaction(doc, "Create CDW Parameters"))
            {
                t.Start();
                doc.ParameterBindings.Insert(myDefinition_ProductDate, instanceBinding, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate2, instanceBinding2, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate3, instanceBinding3, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate4, instanceBinding4, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate5, instanceBinding5, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate6, instanceBinding6, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate7, instanceBinding7, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate8, instanceBinding8, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate9, instanceBinding9, BuiltInParameterGroup.PG_CONSTRAINTS);
                doc.ParameterBindings.Insert(myDefinition_ProductDate10, instanceBinding10, BuiltInParameterGroup.PG_CONSTRAINTS);
                t.Commit();
            }
        }

        public static void CreateParameters(ExternalCommandData commandData,
                                    Dictionary<string, string> dictionary,
                                    IList<Element> listaElementos)
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

            List<string> keys = Dictionary.DictionaryListKeys(dictionary);
            string paramName = keys[0];
            // create shared parameter file
            String paramFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                             + "\\CDWParameters.txt";
            if (File.Exists(paramFile))
            {
                File.Delete(paramFile);
            }
            FileStream fs = File.Create(paramFile);
            fs.Close();
            // prepare shared parameter file
            commandData.Application.Application.SharedParametersFilename = paramFile;
            CreateSharedParameterFile(commandData, dictionary);
            StepLog.Write(commandData, "CreateSharedParameterFile");
        }

        public static bool ExistParameters(ExternalCommandData commandData,
                            Dictionary<string, string> dictionary,
                            IList<Element> TodosLosElementosCDW)
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

            bool salida = false;
            string key = Dictionary.DictionaryListKeys(dictionary).First();
            if (TodosLosElementosCDW.First().LookupParameter(key) == null)//no existe parametro
            {
                StepLog.Write(commandData, "TodosLosElementosCDW.First().LookupParameter(key) == null");
            }
            else//si existe parametro
            {
                salida = true;
                StepLog.Write(commandData, "TodosLosElementosCDW.First().LookupParameter(key) !== null");
            }
            return salida;
        }

        public static void CreateSchedules(ExternalCommandData commandData,
                                    Dictionary<string, string> dictionary)
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

            List<string> keys = Dictionary.DictionaryListKeys(dictionary);
            string paramName = keys[0];
            string paramName2 = keys[1];
            string paramName3 = keys[2];
            string paramName4 = keys[3];
            string paramName5 = keys[4];
            string paramName6 = keys[5];
            string paramName7 = keys[6];
            string paramName8 = keys[7];
            string paramName9 = keys[8];
            string paramName10 = keys[9];
            BuiltInCategory[] bics = new BuiltInCategory[]  // lista de BuiltInCategory
            {
                    BuiltInCategory.OST_Floors,
                    BuiltInCategory.OST_StructuralColumns,
                    BuiltInCategory.OST_StructuralFraming,
                    BuiltInCategory.OST_Walls,
                    BuiltInCategory.OST_StructuralFoundation,
                    BuiltInCategory.OST_Columns,
            };
            foreach (BuiltInCategory bic in bics)
            {
                ViewSchedule clashSchedule = null;
                using (Transaction transaction = new Transaction(doc, "Creating CDW Schedule"))
                {
                    transaction.Start();
                    clashSchedule = ViewSchedule.CreateSchedule(doc, new ElementId(bic));
                    StepLog.Write(commandData, "Creating CDW Schedule");
                    doc.Regenerate();
                    ScheduleDefinition definition = clashSchedule.Definition;
                    IList<SchedulableField> schedulableFields = definition.GetSchedulableFields(); // [a,b,c,s,d,f,....]
                    List<SchedulableField> listashparam = new List<SchedulableField>();
                    foreach (SchedulableField element in schedulableFields)
                    {
                        if (element.ParameterId.IntegerValue > 0)
                        {
                            listashparam.Add(element);
                            StepLog.Write(commandData, listashparam.Count().ToString());
                        }
                    }
                    clashSchedule.Definition.AddField(schedulableFields.FirstOrDefault(o => o.GetName(doc).ToString() == "Familia y tipo"));

                    bool verificarSiPasoPorAqui = false;
                    bool verificarSiPasoPorAqui2 = false;
                    bool verificarSiPasoPorAqui3 = false;
                    bool verificarSiPasoPorAqui4 = false;
                    bool verificarSiPasoPorAqui5 = false;
                    bool verificarSiPasoPorAqui6 = false;
                    bool verificarSiPasoPorAqui7 = false;
                    bool verificarSiPasoPorAqui8 = false;
                    bool verificarSiPasoPorAqui9 = false;
                    bool verificarSiPasoPorAqui10 = false;

                    for (int i = 0; i < listashparam.Count(); i++)
                    {
                        if (listashparam[i].GetName(doc).ToString() == paramName)
                        {
                            if (!verificarSiPasoPorAqui)
                            {
                                clashSchedule.Definition.AddField(listashparam[i]);
                                verificarSiPasoPorAqui = true;
                            }
                        }
                        if (listashparam[i].GetName(doc).ToString() == paramName2)
                        {
                            if (!verificarSiPasoPorAqui2)
                            {
                                clashSchedule.Definition.AddField(listashparam[i]);
                                verificarSiPasoPorAqui2 = true;
                            }
                        }
                        if (listashparam[i].GetName(doc).ToString() == paramName3)
                        {
                            if (!verificarSiPasoPorAqui3)
                            {
                                clashSchedule.Definition.AddField(listashparam[i]);
                                verificarSiPasoPorAqui3 = true;
                            }
                        }
                        if (listashparam[i].GetName(doc).ToString() == paramName4)
                        {
                            if (!verificarSiPasoPorAqui4)
                            {
                                clashSchedule.Definition.AddField(listashparam[i]);
                                verificarSiPasoPorAqui4 = true;
                            }
                        }
                        if (listashparam[i].GetName(doc).ToString() == paramName5)
                        {
                            if (!verificarSiPasoPorAqui5)
                            {
                                clashSchedule.Definition.AddField(listashparam[i]);
                                verificarSiPasoPorAqui5 = true;
                            }
                        }
                        if (listashparam[i].GetName(doc).ToString() == paramName6)
                        {
                            if (!verificarSiPasoPorAqui6)
                            {
                                clashSchedule.Definition.AddField(listashparam[i]);
                                verificarSiPasoPorAqui6 = true;
                            }
                        }
                        if (listashparam[i].GetName(doc).ToString() == paramName7)
                        {
                            if (!verificarSiPasoPorAqui7)
                            {
                                clashSchedule.Definition.AddField(listashparam[i]);
                                verificarSiPasoPorAqui7 = true;
                            }
                        }
                        if (listashparam[i].GetName(doc).ToString() == paramName8)
                        {
                            if (!verificarSiPasoPorAqui8)
                            {
                                clashSchedule.Definition.AddField(listashparam[i]);
                                verificarSiPasoPorAqui8 = true;
                            }
                        }
                        if (listashparam[i].GetName(doc).ToString() == paramName9)
                        {
                            if (!verificarSiPasoPorAqui9)
                            {
                                clashSchedule.Definition.AddField(listashparam[i]);
                                verificarSiPasoPorAqui9 = true;
                            }
                        }
                        if (listashparam[i].GetName(doc).ToString() == paramName10)
                        {
                            if (!verificarSiPasoPorAqui10)
                            {
                                clashSchedule.Definition.AddField(listashparam[i]);
                                verificarSiPasoPorAqui10 = true;
                            }
                        }
                    }
                    if (null != clashSchedule)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.RollBack();
                    }
                    using (Transaction tran = new Transaction(doc, "Cambiar nombre"))
                    {
                        StepLog.Write(commandData, "Cambiar nombre Schedule");
                        tran.Start();
                        TableData td = clashSchedule.GetTableData(); // get viewschedule table data
                        TableSectionData tsd = td.GetSectionData(SectionType.Header); // get header section data
                        string text = tsd.GetCellText(0, 0);
                        string nombreBorrado_OST = (bic.ToString() + " CDW ESTIMACIÓN SCHEDULE").Remove(0, 4);
                        tsd.SetCellText(0, 0, nombreBorrado_OST);
                        clashSchedule.Name = nombreBorrado_OST;
                        tsd.InsertColumn(0);
                        tran.Commit();
                    }
                }
            }
        }
    }
}