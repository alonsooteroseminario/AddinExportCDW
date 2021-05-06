using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace AddinExportCDW
{
    public static class ExportExcel
    {
        public static void Export(List<double> listaN_valor,
            List<Dictionary<string, string>> lista_Dictionarios,
            List<double> lista_desperdicios,
            double desperdicioTotal)
        {
            #region Obtener Excel

            var bankAccounts = new List<Account>();
            for (int i = 0; i < lista_Dictionarios.Count(); i++)
            {
                Account cuenta = new Account
                {
                    ID = lista_Dictionarios[i]["Structural element"],
                    Code = lista_Dictionarios[i]["Código"],
                    Balance = lista_desperdicios[i]
                };
                bankAccounts.Add(cuenta);
            }
            Account nuevo1 = new Account
            {
                ID = "",
                Code = "",
                Balance = 0
            };
            Account nuevo2 = new Account
            {
                ID = "Total",
                Code = "",
                Balance = desperdicioTotal
            };
            bankAccounts.Add(nuevo1);
            bankAccounts.Add(nuevo2);

            var bankAccounts2 = new List<Account> {
                    new Account {
                                      ID = "07 07 01 aqueous washing liquids",
                                      Balance = listaN_valor[0]
                                },
                    new Account {
                                      ID = "15 01 02 plastic packaging",
                                      Balance = listaN_valor[1]
                                },
                    new Account {
                                    ID = "15 01 03 wooden packaging",
                                    Balance = listaN_valor[2]
                    },
                    new Account {
                                    ID = "15 01 04 metallic packaging",
                                    Balance = listaN_valor[3]
                    },
                    new Account {
                                    ID = "15 01 06 mixed packaging",
                                    Balance = listaN_valor[4]
                    },
                    new Account {
                                    ID = "17 01 01 concrete",
                                    Balance = listaN_valor[5]
                    },
                    new Account {
                                    ID = "17 02 01 wood",
                                    Balance = listaN_valor[6]
                    },
                    new Account {
                                    ID = "17 02 03 plastic",
                                    Balance = listaN_valor[7]
                    },
                    new Account {
                                    ID = "17 04 05 iron and steel",
                                    Balance = listaN_valor[8]
                    },
                    new Account {
                                    ID = "17 09 04 mixed",
                                    Balance = listaN_valor[9]
                    },
                    new Account {
                                    ID = "",
                                    Balance = 0
                    },
                    new Account {
                                    ID = "Total",
                                    Balance =  desperdicioTotal
                    }
                };

            DisplayInExcel(bankAccounts, bankAccounts2);

            #endregion Obtener Excel
        }

        private static void DisplayInExcel(IEnumerable<Account> accounts, IEnumerable<Account> accounts2)
        {
            Excel.Application excelApp = new Excel.Application
            {
                // Make the object visible.
                Visible = true
            };

            // Create a new, empty workbook and add it to the collection returned
            // by property Workbooks. The new workbook becomes the active workbook.
            // Add has an optional parameter for specifying a praticular template.
            // Because no argument is sent in this example, Add creates a new workbook.
            Excel.Workbook xlWorkBook = excelApp.Workbooks.Add();

            // This example uses a single workSheet. The explicit type casting is
            // removed in a later procedure.
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            workSheet.Name = "CDW by type of waste";

            // Establish column headings in cells A1 and B1.
            workSheet.Cells[1, "A"] = "Building element";
            workSheet.Cells[1, "B"] = "Code";
            workSheet.Cells[1, "C"] = "CDW (m3)";

            var row = 1;
            foreach (var acct in accounts)
            {
                row++;
                workSheet.Cells[row, "A"] = acct.ID;
                workSheet.Cells[row, "B"] = acct.Code;
                workSheet.Cells[row, "C"] = acct.Balance;
            }

            ((Excel.Range)workSheet.Columns[1]).AutoFit();
            ((Excel.Range)workSheet.Columns[2]).AutoFit();
            ((Excel.Range)workSheet.Columns[3]).AutoFit();

            Excel.Sheets worksheets = xlWorkBook.Worksheets;

            var xlNewSheet = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlNewSheet.Name = "CDW by building element";

            xlNewSheet.Cells[1, "A"] = "European Waste Code (EWC)";
            xlNewSheet.Cells[1, "B"] = "CDW (m3)";

            var row2 = 1;
            foreach (var acct in accounts2)
            {
                row2++;
                xlNewSheet.Cells[row2, "A"] = acct.ID;
                xlNewSheet.Cells[row2, "B"] = acct.Balance;
            }

            ((Excel.Range)xlNewSheet.Columns[1]).AutoFit();
            ((Excel.Range)xlNewSheet.Columns[2]).AutoFit();
        }
    }

    internal class Account
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public double Balance { get; set; }
    }
}