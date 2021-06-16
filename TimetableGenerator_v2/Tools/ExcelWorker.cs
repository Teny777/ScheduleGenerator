using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Diagnostics;

namespace Generator.Tools
{
    // не моя реализация. Ctrl+c Ctrl+v
    public static class ExcelWorker
    {
        public static void ExportToExcel(this DataTable table, string excelFilePath = null)
        {
            try
            {
                if (table == null || table.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                var excelApp = new Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Excel._Worksheet workSheet = (Excel._Worksheet)excelApp.ActiveSheet;

                // column headings
                for (var i = 0; i < table.Columns.Count; i++)
                {
                    workSheet.Cells[1, i + 1] = table.Columns[i].ColumnName;
                }

                // rows
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (var j = 0; j < table.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1] = table.Rows[i][j];
                    }
                }
                workSheet.Range["A2:A7"].Merge();
                workSheet.Range["A8:A13"].Merge();
                workSheet.Range["A14:A19"].Merge();
                workSheet.Range["A20:A25"].Merge();
                workSheet.Range["A26:A31"].Merge();
                workSheet.Range["A2:A31"].Orientation = Excel.XlOrientation.xlUpward;

                // check file path
                if (!string.IsNullOrEmpty(excelFilePath))
                {
                    // не тестировал
                    try
                    {
                        excelFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + '\\' + excelFilePath;
                        workSheet.SaveAs(excelFilePath);
                        excelApp.Quit();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                            + ex.Message);
                    }
                }
                else
                { // no file path is given
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // коммент потому что не надо бы чтобы вылетала программа. а ещё лучше сделать так чтобы пользователь узнал о том, что что-то не так.
                //throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
    }
}
