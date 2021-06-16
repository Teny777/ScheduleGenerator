using Generator.Core;
using Generator.Core.Restriction;
using Generator.Model;
using Generator.Singleton;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Generator.Utils
{
    public static class ExtensionMethods
    {
        public static DataTable CreateTimeTable(this Individual individual)
        {
            var timetable = new Dictionary<int, Dictionary<int, Lesson>>(Data.Instance.Classes.Count);
            foreach (int key in Data.Instance.Classes.Keys)
            {
                timetable.Add(key, new Dictionary<int, Lesson>());
            }
            // подготовка таблицы для расписания ^^^
            for (int i = 0; i < Data.Instance.N; i++)
            {
                timetable[Data.Instance.Lessons[i].Class.Id].Add(individual.Colors[i], Data.Instance.Lessons[i]);
            }

            var dt = new DataTable();
            dt.Columns.Add(@"уроки\классы");
            var tmpList = new string[31];
            for (int i = 1; i < 31; i++)
            {
                dt.Columns.Add(i.ToString());
            }

            foreach (var classesTimeTable in timetable.Values)
            {
                tmpList[0] = classesTimeTable.First().Value.Class.Name;
                for (int i = 1; i < 31; i++)
                {
                    classesTimeTable.TryGetValue(i, out Lesson curLes);
                    tmpList[i] = curLes?.ToString() ?? "-----------";
                }
                dt.Rows.Add(tmpList);
            }
            var correctTable = dt.GenerateTransposedTable();
            return correctTable;
        }

        public static DataTable CreateTeacherTimeTable(this Individual individual)
        {
            // подготовка таблицы для расписания
            var timetable = new Dictionary<int, Dictionary<int, Lesson>>(Data.Instance.Classes.Count);

            foreach (int key in Data.Instance.Teachers.Keys)
            {
                timetable.Add(key, new Dictionary<int, Lesson>());
            }

            for (int i = 0; i < Data.Instance.N; i++)
            {
                timetable[Data.Instance.Lessons[i].Teacher.Id].Add(individual.Colors[i], Data.Instance.Lessons[i]);
            }

            // первые два столбца таблицы (заголовки столбцов)
            var headers = new string[2 + timetable.Values.Count];
            headers[0] = @"День Недели";
            headers[1] = @"уроки\учителя";

            var table = new string[31, 2 + timetable.Values.Count];
            // первые два столбца таблицы
            for (int i = 0; i < 31; i++)
            {
                table[i, 0] = (i + 1) switch
                {
                    1 => "Понедельник",
                    7 => "Вторник",
                    13 => "Среда",
                    19 => "Четверг",
                    25 => "Пятница",
                    _ => "",
                };
                table[i, 1] = (i % 6 + 1).ToString();
            }

            // внутренняя часть таблицы + остальные заголовки столбцов
            int j = 2;
            foreach (var teacherTimeTable in timetable.Values)
            {
                headers[j] = teacherTimeTable.First().Value.Teacher.Name; // first ???  first or default may be
                if (headers[j] == null) continue;
                for (int i = 1; i < 31; i++)
                {
                    teacherTimeTable.TryGetValue(i, out Lesson curLes);
                    table[i - 1, j] = curLes?.Info ?? "-----------";
                }
                j++;
            }

            var dataTable = ArraytoDatatable(table, headers);
            return dataTable;
        }

        public static DataTable ArraytoDatatable(string[,] values, string[] headers)
        {
            DataTable dt = new DataTable();
            foreach (var header in headers)
            {
                dt.Columns.Add(header.Replace(".", " ").Replace(" ", ""));
            }

            for (var i = 0; i < values.GetLength(0) - 1; ++i)
            {
                DataRow row = dt.NewRow();
                for (var j = 0; j < values.GetLength(1); ++j)
                {
                    row[j] = values[i, j] ?? "-----------";
                }
                dt.Rows.Add(row);
            }
            return dt;
        }


        public static DataTable GenerateTransposedTable(this DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            // Add columns by looping rows

            // Header row's first column is same as in inputTable
            outputTable.Columns.Add("День недели", typeof(string));
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString(), typeof(string));

            // Header row's second column onwards, 'inputTable's first column taken
            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName, typeof(string));
            }

            // Add rows by looping columns        
            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();

                // First column is inputTable's Header row's second column
                //newRow[1] = inputTable.Columns[rCount].ColumnName.ToString();
                newRow[1] = ((rCount - 1) % 6 + 1).ToString();

                newRow[0] = rCount switch
                {
                    1 => "Понедельник",
                    7 => "Вторник",
                    13 => "Среда",
                    19 => "Четверг",
                    25 => "Пятница",
                    _ => "",
                };

                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 2] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }

            return outputTable;
        }

        public static List<Row> TableToRows(this Individual individual)
        {
            var result = new List<Row>();

            // таблица для классов
            var clsTimeTable = new Dictionary<int, Dictionary<int, Lesson>>(Data.Instance.Classes.Count);
            foreach (int key in Data.Instance.Classes.Keys)
                clsTimeTable.Add(key, new Dictionary<int, Lesson>());

            // подготовка таблицы для расписания ^^^
            for (int i = 0; i < Data.Instance.N; i++)
            {
                clsTimeTable[Data.Instance.Lessons[i].Class.Id].Add(individual.Colors[i], Data.Instance.Lessons[i]);
            }

            foreach (var classTimetable in clsTimeTable.Values)
            {
                for (int i = 1; i < 31; i++) // 36
                {
                    int x = (i - 1) % 6 + 1;
                    int dayOfWeek = (i - 1) / 6 + 1;

                    if (classTimetable.TryGetValue(i, out Lesson curLesson))
                    {
                        var c = curLesson;
                        var row = new Row(c.Teacher.Name, c.Subject.Name, 0/*кабинетов нет*/, c.Class.Name, x, dayOfWeek);

                        result.Add(row);
                    }
                }
            }

            return result;
        }
    }
}
