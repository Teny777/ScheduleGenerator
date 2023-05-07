using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Diagnostics;
using System.Windows;

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

                var lastNotEmpty = -1;
                // rows
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (var j = 0; j < table.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1] = table.Rows[i][j];
                    }
                    
                    if (table.Rows[i][0].GetType() != typeof(DBNull))
                    {
                        workSheet.Range[workSheet.Cells[lastNotEmpty + 2, 1], workSheet.Cells[i + 1, 1]].Merge();
                        workSheet.Range[workSheet.Cells[lastNotEmpty + 2, 2], workSheet.Cells[i + 1, 2]].Merge();
                        workSheet.Range[workSheet.Cells[lastNotEmpty + 2, 3], workSheet.Cells[i + 1, 3]].Merge();
                        workSheet.Range[workSheet.Cells[lastNotEmpty + 2, 5], workSheet.Cells[i + 1, 5]].Merge();
                        lastNotEmpty = i;
                    }
                }
                workSheet.Range[workSheet.Cells[lastNotEmpty + 2, 1], workSheet.Cells[table.Rows.Count + 1, 1]].Merge();
                workSheet.Rows.AutoFit();
                workSheet.Columns.AutoFit();
                workSheet.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                workSheet.Columns.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                workSheet.Columns[1].ColumnWidth = 5;
                workSheet.Columns[2].ColumnWidth = 20;
                workSheet.Columns[3].ColumnWidth = 20;
                workSheet.Columns[4].ColumnWidth = 10;
                workSheet.Columns[5].ColumnWidth = 10;
                workSheet.Columns[6].ColumnWidth = 10;
                for (int i = 7; i < 14; ++i)
                {
                    workSheet.Columns[i].ColumnWidth = 15;
                }
                workSheet.Cells.WrapText = true;
                workSheet.Rows[1].Font.Bold = true;
                workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[table.Rows.Count + 1, table.Columns.Count]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

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
