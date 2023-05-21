using Generator.Core;
using Generator.Core.Restriction;
using Generator.Model;
using Generator.Singleton;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using Xceed.Wpf.Toolkit;
using System.Windows.Media;



namespace Generator.Utils
{

    public class Time
    {
        public Time(string start, string end)
        {
            Start = start;
            End = end;
        }
        public Time(int color, Shift shift)
        {
            Time time =  GetTime(color, shift);
            Start = time.Start;
            End = time.End;
        }

        private static Time GetTime(int color, Shift shift)
        {
            if (shift == Shift.First)
            {
                return ((color - 1) % 6 + 1) switch
                {
                    1 => new Time("8.00", "8.50"),
                    2 => new Time("8.50", "9.40"),
                    3 => new Time("9.40", "10.30"),
                    4 => new Time("10.30", "11.20"),
                    5 => new Time("11.20", "12.10"),
                    6 => new Time("12.10", "13.00"),
                    _ => null
                };
            }
            else
            {
                return ((color - 1) % 6 + 1) switch
                {
                    1 => new Time("14.00", "14.50"),
                    2 => new Time("14.50", "15.40"),
                    3 => new Time("15.40", "16.30"),
                    4 => new Time("16.30", "17.20"),
                    5 => new Time("17.20", "18.10"),
                    6 => new Time("18.10", "19.00"),
                    _ => null
                };
            }
            
        }
        public string Start { get; set; }   
        public string End { get; set; } 


    }
    public class GroupInfo
    {
        public GroupInfo()
        {
            Colors = new List<int>();
        }
        public string Classroom { get; set; } 
        public List<int> Colors { get; set; }
        public Shift Shift { get; set; }


        public List<string> GetSchedule()
        {
            Colors.Sort();
            var result = new List<string>();
            for (int i = 0; i < 6; ++i) result.Add(string.Empty);
            for (int i = 0; i < Colors.Count; ++i)
            {
                int startIdx = i;
                var start = new Time(Colors[i], Shift);
                while (i + 1 < Colors.Count && Colors[i + 1] - 1 == Colors[i] && (Colors[i + 1] - 1) % 6 != 0) i++;
                var end = new Time(Colors[i], Shift);
                result[(Colors[i] - 1) / 6] += $"{start.Start}-{end.End} ({i - startIdx + 1})\n";
            }
            return result;
        }
    }

    public static class ExtensionMethods
    {  

        public static DataTable CreateTimeTable(this Individual individual)
        {
            var outputTable = new DataTable();
            var timetable = new Dictionary<int, Dictionary<int, Dictionary<int, GroupInfo>>>();
            var subjects = new Dictionary<int, Subject>();

            foreach (var subject in Data.Instance.Subjects)
            {
                timetable.Add(subject.Id, new Dictionary<int, Dictionary<int, GroupInfo>>());
                subjects.Add(subject.Id, subject); 
            }
  
            for (int i = 0; i < Data.Instance.N; i++)
            {
                
                if (!timetable[Data.Instance.Lessons[i].Subject.Id].ContainsKey(Data.Instance.Lessons[i].Teacher.Id)) timetable[Data.Instance.Lessons[i].Subject.Id].Add(Data.Instance.Lessons[i].Teacher.Id, new Dictionary<int, GroupInfo>());
                if (!timetable[Data.Instance.Lessons[i].Subject.Id][Data.Instance.Lessons[i].Teacher.Id].ContainsKey(Data.Instance.Lessons[i].Class.Id)) timetable[Data.Instance.Lessons[i].Subject.Id][Data.Instance.Lessons[i].Teacher.Id].Add(Data.Instance.Lessons[i].Class.Id, new GroupInfo());
                timetable[Data.Instance.Lessons[i].Subject.Id][Data.Instance.Lessons[i].Teacher.Id][Data.Instance.Lessons[i].Class.Id].Colors.Add(individual.Colors[i]);
                timetable[Data.Instance.Lessons[i].Subject.Id][Data.Instance.Lessons[i].Teacher.Id][Data.Instance.Lessons[i].Class.Id].Classroom = Data.Instance.Lessons[i].Classroom.Name;
                timetable[Data.Instance.Lessons[i].Subject.Id][Data.Instance.Lessons[i].Teacher.Id][Data.Instance.Lessons[i].Class.Id].Shift = Data.Instance.Lessons[i].Shift;
            }


            outputTable.Columns.Add(@"№");
            outputTable.Columns.Add(@"Наименование детских объединений");
            outputTable.Columns.Add(@"ФИО педагога");
            outputTable.Columns.Add(@"Группа");
            outputTable.Columns.Add(@"Общая нагрузка");
            outputTable.Columns.Add(@"Текущая нагрузка");
            outputTable.Columns.Add(@"Понедельник");
            outputTable.Columns.Add(@"Вторник");
            outputTable.Columns.Add(@"Среда");
            outputTable.Columns.Add(@"Четверг");
            outputTable.Columns.Add(@"Пятница");
            outputTable.Columns.Add(@"Суббота");
            outputTable.Columns.Add(@"Воскресенье");
            outputTable.Columns.Add(@"Кабинет");
            int currentNumber = 1;

            foreach(var subject in timetable)
            {
                     
                foreach (var teacher in subject.Value)
                {
                    DataRow newRow = outputTable.NewRow();
                    newRow[0] = currentNumber++.ToString();
                    newRow[1] = subjects[subject.Key].Name;
                    newRow[2] = Data.Instance.Teachers[teacher.Key].Name;
                    int totalLoad = 0;

                    foreach (var curClass in teacher.Value)
                    {
                        totalLoad += curClass.Value.Colors.Count;
                    }
                    newRow[4] = totalLoad.ToString();

                    foreach (var curClass in teacher.Value)
                    {
                        newRow[3] = Data.Instance.Classes[curClass.Key].Name;
                        newRow[5] = curClass.Value.Colors.Count.ToString();
                        newRow[13] = curClass.Value.Classroom;
                        var getSchedule = curClass.Value.GetSchedule();
                        for (int i = 0; i < getSchedule.Count; ++i)
                            newRow[6 + i] = getSchedule[i];
                        outputTable.Rows.Add(newRow);
                        newRow = outputTable.NewRow();
                    }
                    
                }
            }


            return outputTable;
        }


        //public static DataTable CreateTimeTable1(this Individual individual)
        //{
        //    var timetable = new Dictionary<int, Dictionary<int, List<Lesson>>>(Data.Instance.Classes.Count);

        //    foreach (int key in Data.Instance.Classes.Keys)
        //    {
        //        timetable.Add(key, new Dictionary<int, List<Lesson>>());
        //    }
        //    // подготовка таблицы для расписания ^^^
        //    for (int i = 0; i < Data.Instance.N; i++)
        //    {
        //        //timetable[Data.Instance.Lessons[i].Class.Id].Add(individual.Colors[i], Data.Instance.Lessons[i]);

        //        if (!timetable[Data.Instance.Lessons[i].Class.Id].ContainsKey(individual.Colors[i])) timetable[Data.Instance.Lessons[i].Class.Id].Add(individual.Colors[i], new List<Lesson>());
        //        timetable[Data.Instance.Lessons[i].Class.Id][individual.Colors[i]].Add(Data.Instance.Lessons[i]);
        //    }

        //    var dt = new DataTable();
        //    dt.Columns.Add(@"уроки\учителя");
        //    var tmpList = new string[37];
        //    for (int i = 1; i <= 36; i++)
        //    {
        //        dt.Columns.Add(i.ToString());
        //    }

        //    foreach (var classesTimeTable in timetable.Values)
        //    {
        //        if (classesTimeTable.Count == 0) continue;
        //        tmpList[0] = classesTimeTable.First().Value.First().Class.Name;
        //        for (int i = 1; i <= 36; i++)
        //        {
        //            classesTimeTable.TryGetValue(i, out List<Lesson> curLessons);
        //            //int new_i = ChangeOrder(i);
        //            int new_i = i;
        //            if (curLessons is null)
        //            {
        //                tmpList[new_i] = "-----------";
        //                continue;
        //            }

        //            tmpList[new_i] = string.Empty;
        //            tmpList[new_i] += curLessons[0].ToString() + "\n";
        //            if (curLessons.Count == 2)
        //                tmpList[new_i] += curLessons[1].ToString();

        //        }
        //        dt.Rows.Add(tmpList);
        //    }
        //    var correctTable = dt.GenerateTransposedTable();
        //    return correctTable;
        //}

        //private static int ChangeOrder(int i)
        //{

        //    switch (i)
        //    {
        //        case 6:
        //            return 31;
        //        case 12:
        //            return 32;
        //        case 18:
        //            return 33;


        //        case 31:
        //            return 6;
        //        case 32:
        //            return 12;
        //        case 33:
        //            return 18;
        //        default:
        //            return i;
        //    }
        //}

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
                if (teacherTimeTable.Count == 0) continue;
                headers[j] = teacherTimeTable.First().Value.Teacher.Name; // first ???  first or default may be
                if (headers[j] == null) continue;
                for (int i = 1; i <= 36; i++)
                {
                    teacherTimeTable.TryGetValue(i, out Lesson curLes);
                    //int new_i = ChangeOrder(i);
                    int new_i = i;
                    table[new_i - 1, j] = curLes?.Info ?? "-----------";
                }
                j++;
            }

            var dataTable = ArrayToDatatable(table, headers);
            return dataTable;
        }

        public static DataTable ArrayToDatatable(string[,] values, string[] headers)
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

        //public static List<Row> TableToRows1(this Individual individual)
        //{
        //    var result = new List<Row>();

        //    // таблица для классов
        //    var clsTimeTable = new Dictionary<int, Dictionary<int, List<Lesson>>>(Data.Instance.Classes.Count);
        //    foreach (int key in Data.Instance.Classes.Keys)
        //        clsTimeTable.Add(key, new Dictionary<int, List<Lesson>>());

        //    // подготовка таблицы для расписания ^^^
        //    for (int i = 0; i < Data.Instance.N; i++)
        //    {
        //        if (!clsTimeTable[Data.Instance.Lessons[i].Class.Id].ContainsKey(individual.Colors[i])) clsTimeTable[Data.Instance.Lessons[i].Class.Id].Add(individual.Colors[i], new List<Lesson>());
        //        clsTimeTable[Data.Instance.Lessons[i].Class.Id][individual.Colors[i]].Add(Data.Instance.Lessons[i]);
        //    }

        //    foreach (var classTimetable in clsTimeTable.Values)
        //    {
        //        for (int i = 1; i < 37; i++) // 36
        //        {
        //            int x = (i - 1) % 6 + 1;
        //            int dayOfWeek = (i - 1) / 6 + 1;

        //            if (classTimetable.TryGetValue(i, out List<Lesson> curLessons))
        //            {
        //                foreach (var lesson in curLessons)
        //                {
        //                    var c = lesson;
        //                    c.Classroom = c.Teacher.Classrooms[0];
        //                    var row = new Row(c.Teacher.Name, c.Subject.Name, c.Teacher.Classrooms[0].Name, c.Class.Name, x, dayOfWeek);
        //                    result.Add(row);
        //                }

        //            }
        //        }
        //    }
        //    return result;
        //}

        static List<string> GetClassrooms(List<List<string>> a)
        {
            int n = a.Count;
            List<string> result = new List<string>();
            Dictionary<string, int> reIdx = new Dictionary<string, int>();
            int start_cnt = n + 1;
            for (int i = 0; i < n; ++i)
            {

                for (int j = 0; j < a[i].Count; ++j)
                {
                    if (reIdx.ContainsKey(a[i][j])) continue;
                    reIdx.Add(a[i][j], start_cnt++);

                }
            }
            MinMaxFlow minMaxFlow = new MinMaxFlow(start_cnt + 1, 0, start_cnt);
            for (int i = 0; i < n; i++)
                minMaxFlow.AddEdge(0, i + 1, 1, 2);

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < a[i].Count - 1; ++j)
                {
                    minMaxFlow.AddEdge(i + 1, reIdx[a[i][j]], 1, 2);
                }
                minMaxFlow.AddEdge(i + 1, reIdx[a[i][a[i].Count - 1]], 1, 1);
            }
            foreach (var u in reIdx)
                minMaxFlow.AddEdge(u.Value, start_cnt, 1, 2);

            int flow = minMaxFlow.Flow();

            int cur_edge = n * 2;
            for (int i = 0; i < n; ++i)
            {
                bool flag = false;
                for (int j = 0; j < a[i].Count; ++j)
                {
                    if (minMaxFlow.Edges[cur_edge].f != 0)
                    {
                        result.Add(a[i][j]);
                        flag = true;
                    }
                    cur_edge += 2;
                }
                if (!flag) result.Add("0");
            }

            return result;
        }

        public static List<Row> TableToRows(this Individual individual)
        {
            var result = new List<Row>();
            var lessonModelEqualityComparer = new LessonModelEqualityComparer();
            var intersectionsLessons = new Dictionary<LessonModel, HashSet<LessonModel>>(lessonModelEqualityComparer);
            
            var timeTable = new Dictionary<int, List<Lesson>>();
            for (int i = 1; i < 50; ++i)
                timeTable.Add(i, new List<Lesson>());

            for (int i = 0; i < Data.Instance.N; ++i)
            {
                timeTable[individual.Colors[i]].Add(Data.Instance.Lessons[i]);
                var subject = Data.Instance.Lessons[i].Subject;
                var teacher = Data.Instance.Lessons[i].Teacher;
                var lessonModel = new LessonModel(subject, teacher);
                
                if (!intersectionsLessons.ContainsKey(lessonModel))
                {
                    intersectionsLessons.Add(lessonModel, new HashSet<LessonModel>(lessonModelEqualityComparer));
                }
            }

            for (int i = 1; i < 43; i++) 
            {
                for (int j = 0; j < timeTable[i].Count; ++j)
                {
                    for (int z = 0; z < timeTable[i].Count; ++z)
                    {
                        if (j == z || timeTable[i][j].Shift != timeTable[i][z].Shift) continue;
                        var lessonModel1 = new LessonModel(timeTable[i][j].Subject, timeTable[i][j].Teacher);
                        var lessonModel2 = new LessonModel(timeTable[i][z].Subject, timeTable[i][z].Teacher);
                        intersectionsLessons[lessonModel1].Add(lessonModel2);
                    }
                }
            }

            var classrooms = GetClassrooms(intersectionsLessons);
            
            for (int i = 1; i < 43; i++) 
            {
                var x = (i - 1) % 6 + 1;
                var dayOfWeek = (i - 1) / 6 + 1;
                for (int j = 0; j < timeTable[i].Count; ++j)
                {
                    var c = timeTable[i][j];
                    c.Classroom = new Classroom(string.Empty);
                    if (classrooms.TryGetValue(new LessonModel(c.Subject, c.Teacher), out var value))
                    {
                        c.Classroom = value;
                    }
                    var row = new Row(c.Teacher.Name, c.Subject.Name, c.Classroom.Name, c.Class.Name, x, dayOfWeek);
                    result.Add(row);
                }
            }
            
            return result;
        }


        private static Dictionary<LessonModel, Classroom> GetClassrooms(
            Dictionary<LessonModel, 
            HashSet<LessonModel>> intersectionsLessons)
        {
            var result = new Dictionary<LessonModel, Classroom>(new LessonModelEqualityComparer());
            foreach (var intersectionsLesson in intersectionsLessons)
            {
                var assigned = new HashSet<Classroom>();
                foreach (var lesson in intersectionsLesson.Value)
                {
                    if (result.TryGetValue(lesson, out var value))
                    {
                        assigned.Add(value);
                    }
                }

                if (!assigned.Contains(intersectionsLesson.Key.Teacher.PriorityClassroom))
                {
                    result[intersectionsLesson.Key] = intersectionsLesson.Key.Teacher.PriorityClassroom;
                }
                else
                {
                    foreach (var classroom in intersectionsLesson.Key.Teacher.Classrooms)
                    {
                        if (!assigned.Contains(classroom))
                        {
                            result[intersectionsLesson.Key] = classroom;
                        }
                    }
                }
            }

            return result;
        }

        public static Dictionary<Class, List<List<KeyValuePair<int, Shift>>>> TableToLessonsForClass(this Individual individual)
        {
            var result = new Dictionary<Class, List<List<KeyValuePair<int, Shift>>>>();
            foreach (var cClass in Data.Instance.Classes)
            {
                result.Add(cClass.Value, new List<List<KeyValuePair<int, Shift>>>());
                for (int i = 0; i < 7; ++i)
                {
                    result.Last().Value.Add(new List<KeyValuePair<int, Shift>>());
                }
            }

            for (int i = 0; i < Data.Instance.Lessons.Count; ++i)
            {
                var lesson = Data.Instance.Lessons[i];
                var color = individual.Colors[i];
                var position = (color - 1) % 6 + 1;
                var dayOfWeek = (color - 1) / 6 + 1;
                result[lesson.Class][dayOfWeek].Add(new KeyValuePair<int, Shift>(position, lesson.Shift));
            }

            return result;
        }
    }
}
