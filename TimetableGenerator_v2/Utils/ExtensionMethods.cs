using Generator.Core;
using Generator.Core.Restriction;
using Generator.Model;
using Generator.Singleton;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;

namespace Generator.Utils
{
    public static class ExtensionMethods
    {
        public static Individual LocalOptimization (this Individual individual)
        {
            var timetable = new Dictionary<int, Dictionary<int, List<Lesson>>>(Data.Instance.Classes.Count);
            foreach (int key in Data.Instance.Classes.Keys)
                timetable.Add(key, new Dictionary<int, List<Lesson>>());

            foreach (int key in Data.Instance.Classes.Keys)
                for (int j = 1; j < 50; ++j)
                    timetable[key][j] = new List<Lesson>();

            for (int i = 0; i < Data.Instance.N; i++)
                timetable[Data.Instance.Lessons[i].Class.Id][individual.Colors[i]].Add(Data.Instance.Lessons[i]);
            
            foreach (int class_id in Data.Instance.Classes.Keys)
            {
                for (int color = 1; color <= timetable[class_id].Count; ++color)
                {
                    if (timetable[class_id][color].Count != 1 || timetable[class_id][color][0].Subgroup.Group == Subgroups.All) continue;

                    for (int next_color = color + 1; next_color <= timetable[class_id].Count; ++next_color)
                    {
                        if (timetable[class_id][next_color].Count != 1 || timetable[class_id][next_color][0].Subgroup.Group == Subgroups.All || !Lesson.CheckLessonFromOneClass(timetable[class_id][color][0], timetable[class_id][next_color][0]))
                            continue;
                        if (CheckTecherInLessons(timetable, class_id, next_color, timetable[class_id][color][0].Teacher) && CheckCanMove(timetable, class_id, color))
                        {
                            individual.Colors[timetable[class_id][color][0].Id] = individual.Colors[timetable[class_id][next_color][0].Id];
                            Lesson lesson = timetable[class_id][color][0];
                            timetable[class_id][color].Remove(lesson);
                            timetable[class_id][next_color].Add(lesson);
                            break;
                        }

                        if (CheckTecherInLessons(timetable, class_id, color, timetable[class_id][next_color][0].Teacher) && CheckCanMove(timetable, class_id, next_color))
                        {
                            individual.Colors[timetable[class_id][next_color][0].Id] = individual.Colors[timetable[class_id][color][0].Id];
                            Lesson lesson = timetable[class_id][next_color][0];
                            timetable[class_id][next_color].Remove(lesson);
                            timetable[class_id][color].Add(lesson);
                            break;
                        }

                    }
                }
            }

            return individual;
        }

        private static bool CheckTecherInLessons(Dictionary<int, Dictionary<int, List<Lesson>>> timetable, int class_id, int color, Teacher teacher)
        {
            foreach (int i in Data.Instance.Classes.Keys)
            {
                if (i == class_id) continue;
                for (int j = 0; j < timetable[i][color].Count; ++j)
                    if (timetable[i][color][j].Teacher == teacher) return false;
            }
            return true;
        }

        private static bool CheckCanMove(Dictionary<int, Dictionary<int, List<Lesson>>> timetable, int class_id, int color)
        {
            if ((color - 1) % 6 + 1 == 1 || (color - 1) % 6 + 1 == 6) return true;
            if (Math.Abs(((color - 1) % 6 + 1) - ((color - 2) % 6 + 1)) != 1 || timetable[class_id][color - 1].Count == 0) return true;
            if (Math.Abs(((color - 1) % 6 + 1) - ((color) % 6 + 1)) != 1 || timetable[class_id][color + 1].Count == 0) return true;
            return false;
        }

        public static DataTable CreateTimeTable(this Individual individual)
        {
            var timetable = new Dictionary<int, Dictionary<int, List<Lesson>>>(Data.Instance.Classes.Count);

            foreach (int key in Data.Instance.Classes.Keys)
            {
                timetable.Add(key, new Dictionary<int, List<Lesson>>());
            }
            // подготовка таблицы для расписания ^^^
            for (int i = 0; i < Data.Instance.N; i++)
            {
                //timetable[Data.Instance.Lessons[i].Class.Id].Add(individual.Colors[i], Data.Instance.Lessons[i]);

                if (!timetable[Data.Instance.Lessons[i].Class.Id].ContainsKey(individual.Colors[i])) timetable[Data.Instance.Lessons[i].Class.Id].Add(individual.Colors[i], new List<Lesson>());
                timetable[Data.Instance.Lessons[i].Class.Id][individual.Colors[i]].Add(Data.Instance.Lessons[i]);
            }

            var dt = new DataTable();
            dt.Columns.Add(@"уроки\классы");
            var tmpList = new string[37];
            for (int i = 1; i <= 36; i++)
            {
                dt.Columns.Add(i.ToString());
            }

            foreach (var classesTimeTable in timetable.Values)
            {
                tmpList[0] = classesTimeTable.First().Value.First().Class.Name;
                for (int i = 1; i <= 36; i++)
                {
                    classesTimeTable.TryGetValue(i, out List<Lesson> curLessons);
                    if(curLessons is null)
                    {
                        tmpList[i] = "-----------";
                        continue;
                    }

                    tmpList[i] = string.Empty;
                    tmpList[i] += curLessons[0].ToString() + "\n";
                    if (curLessons.Count == 2)
                        tmpList[i] += curLessons[1].ToString();

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

            var table = new string[37, 2 + timetable.Values.Count];
            // первые два столбца таблицы
            for (int i = 0; i <= 36; i++)
            {
                table[i, 0] = (i + 1) switch
                {
                    1 => "Понедельник",
                    7 => "Вторник",
                    13 => "Среда",
                    19 => "Четверг",
                    25 => "Пятница",
                    31 => "Суббота",
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
                for (int i = 1; i <= 36; i++)
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
                    31 => "Суббота",
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
            var clsTimeTable = new Dictionary<int, Dictionary<int, List<Lesson>>>(Data.Instance.Classes.Count);
            foreach (int key in Data.Instance.Classes.Keys)
                clsTimeTable.Add(key, new Dictionary<int, List<Lesson>>());

            // подготовка таблицы для расписания ^^^
            for (int i = 0; i < Data.Instance.N; i++)
            {
                if (!clsTimeTable[Data.Instance.Lessons[i].Class.Id].ContainsKey(individual.Colors[i])) clsTimeTable[Data.Instance.Lessons[i].Class.Id].Add(individual.Colors[i], new List<Lesson>());
                clsTimeTable[Data.Instance.Lessons[i].Class.Id][individual.Colors[i]].Add(Data.Instance.Lessons[i]);
            }

            foreach (var classTimetable in clsTimeTable.Values)
            {
                for (int i = 1; i < 37; i++) // 36
                {
                    int x = (i - 1) % 6 + 1;
                    int dayOfWeek = (i - 1) / 6 + 1;

                    if (classTimetable.TryGetValue(i, out List<Lesson> curLessons))
                    {
                        foreach (var lesson in curLessons)
                        {
                            var c = lesson;
                            var row = new Row(c.Teacher.Name, c.Subject.Name, 0/*кабинетов нет*/, c.Class.Name, x, dayOfWeek, (int)c.Subgroup.Group);
                            result.Add(row);
                        }
                        
                    }
                }
            }
            return result;
        }


    }
}
