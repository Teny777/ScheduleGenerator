using Generator.Core.Restriction;
using Generator.Model;
using Generator.Singleton;
using RestrictionAnalyzer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Generator.Tools
{
    public static class Garbage
    {
        public static void FillStartData()
        {
            FillRestrictions();
            
            // Data.Instance.Lessons = new List<Lesson>();
            // Data.Instance.LessonEditors = new ObservableCollection<LessonEditor>();
            //
            // Data.Instance.Classrooms = new ObservableCollection<Classroom>()
            // {
            //     new Classroom("13"), //0
            //     new Classroom("9"),  //1
            //     new Classroom("14a"),//2
            //     new Classroom("19"), //3
            //     new Classroom("18"), //4
            //     new Classroom("16"), //5
            //     new Classroom("4"),  //6
            //     new Classroom("8"),  //7
            //     new Classroom("5"),  //8
            //     new Classroom("2"),  //9
            //     new Classroom("10"), //10
            //     new Classroom("11"), //11
            //     new Classroom("14"), //12
            //     new Classroom("1"),  //13
            //     new Classroom("3"),  //14
            //     new Classroom("7"),  //15
            //     new Classroom("12"),  //16
            //     new Classroom("101"),
            //     new Classroom("102"),
            //     new Classroom("103"),
            // };
            //
            // #region SubjectsWrite
            // Data.Instance.Subjects = new ObservableCollection<Subject>()
            // {
            //     new Subject("Театральная студия «СвеТ»", 0),        // 0
            //     new Subject("«Развивайка»", 1),           // 1
            //     new Subject("Изостудия «Талант»", 2),         // 2
            //     new Subject("«Танцевальная студия «Эльф»", 3),            // 3
            //     new Subject("«Танцевальная студия «Эльф» - группа совершенствования»", 4),              // 4 
            //     new Subject("Детская творческая мастерская", 5),        // 5
            //     new Subject("Мастерская Самоделкина", 6),                 // 6
            //     new Subject("«Умелые руки»", 7),          // 7
            //     new Subject("«Арт-терапия»", 8),         // 8
            //     new Subject("«Спортивное АВИАмоделирование»", 9),          // 9
            //     new Subject("Компьютерные технологии в современном мире: модуль «Робототехника»", 10),   // 10
            //     new Subject("«Ушу» - группа совершенствования»", 11),                     // 11
            //     new Subject("«Ушу»", 12),              // 12
            //     new Subject("Подготовка юных шахматистов", 13),     // 13
            //     new Subject("Студия моды «А-Элита»", 14),            // 14
            //     new Subject("Студия «Спектр»", 15),             // 15
            //     new Subject("«Эстрадный вокальный коллектив «Конфетти»", 16), //16
            //     new Subject("«Ансамбль народной песни «Исток» - группа совершенствования»", 17), //17
            //     new Subject("«Рукопашный бой»", 18), //18
            //     new Subject("Студия «Декоративно – прикладное творчество»", 19), //19
            //     new Subject("ОФП с коррекцией осанки", 20), //20
            //     new Subject("Спортивная акробатика", 21), //21
            //     new Subject("«Флористический дизайн»", 22),// 22
            //     new Subject("«Эко-дизайн»", 23), //23
            //
            // };
            // #endregion
            //
            // #region TeacherWrite
            // Data.Instance.Teachers = new Dictionary<int, Teacher>
            // {
            //
            //     { 1, new Teacher(1, "Айдуганова Александра Александровна", new ObservableCollection<Subject>{ Data.Instance.Subjects[0]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[0]) },
            //     { 2, new Teacher(2, "Белькова Татьяна Семеновна", new ObservableCollection<Subject>{ Data.Instance.Subjects[1]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[2]) },
            //     { 3, new Teacher(3, "Бисерова Марина Юрьевна", new ObservableCollection<Subject>{ Data.Instance.Subjects[2]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[3]) },
            //     { 4, new Teacher(4, "Булдакова Наталья Александровна", new ObservableCollection<Subject>{ Data.Instance.Subjects[3], Data.Instance.Subjects[4]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[4]) },
            //     { 5, new Teacher(5, "Волоскова Анастасия Александровна", new ObservableCollection<Subject>{ Data.Instance.Subjects[1]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[5])},
            //     { 6, new Teacher(6, "Гладких Любовь Эдуардовна", new ObservableCollection<Subject>{ Data.Instance.Subjects[5], Data.Instance.Subjects[6] }, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[6]) },
            //     { 7, new Teacher(7, "Дерюшева Марина Александровна", new ObservableCollection<Subject>{ Data.Instance.Subjects[7], Data.Instance.Subjects[5], Data.Instance.Subjects[8]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[7]) },
            //     { 8, new Teacher(8, "Кашин Дмитрий Александрович", new ObservableCollection<Subject>{ Data.Instance.Subjects[9], Data.Instance.Subjects[10] }, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[6]) },
            //     { 9, new Teacher(9, "Керов Дмитрий Андреевич", new ObservableCollection<Subject>{ Data.Instance.Subjects[11]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[9]) },  
            //     { 10, new Teacher(10, "Кочетов Андрей Борисович", new ObservableCollection<Subject>{ Data.Instance.Subjects[12]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[9]) },
            //     { 11, new Teacher(11, "Кряжевских Олег Васильевич", new ObservableCollection<Subject>{ Data.Instance.Subjects[13], Data.Instance.Subjects[4]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[10])}, 
            //     { 12, new Teacher(12, "Мартынова Татьяна Геннадьевна", new ObservableCollection<Subject>{ Data.Instance.Subjects[14], Data.Instance.Subjects[5], Data.Instance.Subjects[8]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[7]) },
            //     { 13, new Teacher(13, "Панина Алиса Антоновна", new ObservableCollection<Subject>{ Data.Instance.Subjects[15]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[11])},
            //     { 14, new Teacher(14, "Печенкина Екатерина Валерьевна", new ObservableCollection<Subject>{ Data.Instance.Subjects[16], Data.Instance.Subjects[17]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[12]) },
            //     { 15, new Teacher(15, "Рычин Владислав Викторович", new ObservableCollection<Subject>{ Data.Instance.Subjects[18]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[9]) },
            //     { 16, new Teacher(16, "Страхов Матвей Сергеевич", new ObservableCollection<Subject>{ Data.Instance.Subjects[18]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms),  Data.Instance.Classrooms[4] ) },
            //     { 17, new Teacher(17, "Тарантина Татьяна Владимировна", new ObservableCollection<Subject>{ Data.Instance.Subjects[19]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[13]) }, 
            //     { 18, new Teacher(18, "Тарасова Анна Сергеевна", new ObservableCollection<Subject>{ Data.Instance.Subjects[20], Data.Instance.Subjects[21]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[1])},
            //     { 19, new Teacher(19, "Хилютич Ольга Федоровна", new ObservableCollection<Subject>{ Data.Instance.Subjects[5], Data.Instance.Subjects[22], Data.Instance.Subjects[23]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[14])}, 
            //     { 20, new Teacher(20, "Якупова Инга Сергеевна", new ObservableCollection<Subject>{ Data.Instance.Subjects[1]}, new ObservableCollection<Classroom>(Data.Instance.Classrooms), Data.Instance.Classrooms[15])},
            //
            // };
            // #endregion
            //
            // #region ClassWrite
            // Data.Instance.Classes = new Dictionary<int, Class>()
            // {
            //     {1, new Class("A1", 1) },
            //     {2, new Class("A2", 2) },
            //     {3, new Class("A3", 3) },
            //     {4, new Class("A4", 4) },
            //     {5, new Class("A5", 5) },
            //     {6, new Class("B1", 6) },
            //     {7, new Class("B2", 7) },
            //     {8, new Class("B3", 8) },
            //     {9, new Class("B4", 9) },
            //     {10, new Class("C1", 10) },
            //     {11, new Class("C2", 11) },
            //     {12, new Class("C3", 12) },
            //     {13, new Class("C4", 13) },
            //     {14, new Class("D1", 14) },
            //     {15, new Class("D2", 15) },
            //     {16, new Class("D3", 16) },
            //     {17, new Class("E1", 17) },
            //     {18, new Class("F1", 18) },
            //     {19, new Class("F2", 19) },
            //     {20, new Class("F3", 20) },
            //     {21, new Class("F4", 21) },
            //     {22, new Class("F5", 22) },
            //     {23, new Class("F6", 23) },
            //     {24, new Class("F7", 24) },
            //     {25, new Class("G1", 25) },
            //     {26, new Class("H1", 26) },
            //     {27, new Class("I1", 27) },
            //     {28, new Class("I2", 28) },
            //     {29, new Class("I3", 29) },
            //     {30, new Class("I4", 30) },
            //     {31, new Class("J1", 31) },
            //     {32, new Class("J2", 32) },
            //     {33, new Class("K1", 33) },
            //     {34, new Class("L1", 34) },
            //     {35, new Class("M1", 35) },
            //     {36, new Class("M2", 36) },
            //     {37, new Class("M3", 37) },
            //     {38, new Class("N1", 38) },
            //     {39, new Class("O1", 39) },
            //     {40, new Class("O2", 40) },
            //     {41, new Class("O3", 41) },
            //     {42, new Class("O4", 42) },
            //     {43, new Class("P1", 43) },
            //     {44, new Class("P2", 44) },
            //     {45, new Class("P3", 45) },
            //     {46, new Class("P4", 46) },
            //     {47, new Class("P5", 47) },
            //     {48, new Class("Q1", 48) },
            //     {49, new Class("Q2", 49) },
            //     {50, new Class("R1", 50) },
            //     {51, new Class("R2", 51) },
            //     {52, new Class("R3", 52) },
            //     {53, new Class("R4", 53) },
            //     {54, new Class("S1", 54) },
            //     {55, new Class("T1", 55) },
            //     {56, new Class("T2", 56) },
            //     {57, new Class("T3", 57) },
            //     {58, new Class("T4", 58) },
            //     {59, new Class("T5", 59) },
            //     {60, new Class("U1", 60) },
            //     {61, new Class("U2", 61) },
            //     {62, new Class("V1", 62) },
            //     {63, new Class("V2", 63) },
            //     {64, new Class("W1", 64) },
            //     {65, new Class("W2", 65) },
            //     {66, new Class("W3", 66) },
            //     {67, new Class("W4", 67) },
            //     {68, new Class("W5", 68) },
            //     {69, new Class("X1", 69) },
            //     {70, new Class("X2", 70) },
            //     {71, new Class("X3", 71) },
            //     {72, new Class("X4", 72) },
            //     {73, new Class("Y1", 73) },
            //     {74, new Class("Y2", 74) },
            //     {75, new Class("Y3", 75) },
            //     {76, new Class("Y4", 76) },
            //     {77, new Class("Y5", 77) },
            //     {78, new Class("Y6", 78) },
            //     {79, new Class("Z1", 79) },
            //     {80, new Class("Z2", 80) },
            //     {81, new Class("Z3", 81) },
            //     {82, new Class("Z4", 82) },
            //     {83, new Class("AA1", 83) },
            //     {84, new Class("AA2", 84) },
            //     {85, new Class("AA3", 85) },
            //     {86, new Class("AB1", 86) },
            //     {87, new Class("AB2", 87) },
            //     {88, new Class("AB3", 88) },
            //     {89, new Class("AC1", 89) },
            //     {90, new Class("AC2", 90) },
            //     {91, new Class("AD1", 91) },
            //     {92, new Class("AE1", 92) },
            //     {93, new Class("AE2", 93) },
            //
            //
            // };
            // #endregion
            //
            //
            //
            // #region LessonWrite
            // var lessons = Data.Instance.Lessons;
            // var lesEdit = Data.Instance.LessonEditors;
            // var teachers = Data.Instance.Teachers;
            // var classes = Data.Instance.Classes;
            // var subjects = Data.Instance.Subjects;
            //
            //
            //
            // for (int i = 1; i < 5; ++i)
            // {
            //     for (int j = 0; j < 4; ++j)
            //         lessons.Add(new Lesson(teachers[1], classes[i], subjects[0]));
            //     lesEdit.Add(new LessonEditor(new Lesson(teachers[1], classes[i], subjects[0]), 4));
            // }
            // for (int i = 0; i < 6; ++i)
            //     lessons.Add(new Lesson(teachers[1], classes[5], subjects[0]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[1], classes[5], subjects[0]), 6));
            //
            // for (int i = 6; i <= 7; ++i)
            // {
            //     for (int j = 0; j < 4; ++j)
            //         lessons.Add(new Lesson(teachers[2], classes[i], subjects[1]));
            //
            //     lesEdit.Add(new LessonEditor(new Lesson(teachers[2], classes[i], subjects[1]), 4));
            // }
            //
            // for (int i = 8; i <= 9; ++i)
            // {
            //     for (int j = 0; j < 2; ++j)
            //         lessons.Add(new Lesson(teachers[2], classes[i], subjects[1]));
            //
            //     lesEdit.Add(new LessonEditor(new Lesson(teachers[2], classes[i], subjects[1]), 2));
            // }
            //
            // for (int i = 0; i < 4; ++i)
            //     lessons.Add(new Lesson(teachers[3], classes[10], subjects[2]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[3], classes[10], subjects[2]), 4));
            //
            // for (int i = 0; i < 6; ++i)
            //     lessons.Add(new Lesson(teachers[3], classes[11], subjects[2]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[3], classes[11], subjects[2]), 6));
            //
            // for (int i = 0; i < 8; ++i)
            //     lessons.Add(new Lesson(teachers[3], classes[12], subjects[2]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[3], classes[12], subjects[2]), 8));
            //
            // for (int i = 0; i < 9; ++i)
            //     lessons.Add(new Lesson(teachers[3], classes[13], subjects[2]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[3], classes[13], subjects[2]), 9));
            //
            // for (int i = 0; i < 4; ++i)
            //     lessons.Add(new Lesson(teachers[4], classes[14], subjects[3]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[14], subjects[3]), 4));
            //
            //
            // for (int i = 0; i < 6; ++i)
            //     lessons.Add(new Lesson(teachers[4], classes[15], subjects[3]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[15], subjects[3]), 6));
            //
            // for (int i = 0; i < 8; ++i)
            //     lessons.Add(new Lesson(teachers[4], classes[16], subjects[3]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[16], subjects[3]), 8));
            //
            //
            // for (int i = 0; i < 9; ++i)
            //     lessons.Add(new Lesson(teachers[4], classes[17], subjects[4]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[17], subjects[4]), 9));
            //
            //
            // for (int i = 18; i <= 24; ++i)
            // {
            //     for (int j = 0; j < 2; ++j)
            //         lessons.Add(new Lesson(teachers[5], classes[i], subjects[1]));
            //
            //     lesEdit.Add(new LessonEditor(new Lesson(teachers[5], classes[i], subjects[1]), 2));
            // }
            //
            // for (int i = 0; i < 4; ++i)
            //     lessons.Add(new Lesson(teachers[6], classes[25], subjects[5]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[6], classes[25], subjects[5]), 4));
            //
            // for (int i = 0; i < 6; ++i)
            //     lessons.Add(new Lesson(teachers[6], classes[26], subjects[6]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[6], classes[26], subjects[6]), 6));
            //
            // for (int i = 0; i < 4; ++i)
            //     lessons.Add(new Lesson(teachers[7], classes[27], subjects[7]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[7], classes[27], subjects[7]), 4));
            //
            // for (int i = 28; i <= 30; ++i)
            // {
            //     for (int j = 0; j < 6; ++j)
            //         lessons.Add(new Lesson(teachers[7], classes[i], subjects[7]));
            //
            //     lesEdit.Add(new LessonEditor(new Lesson(teachers[7], classes[i], subjects[7]), 6));
            // }
            //
            // for (int i = 31; i <= 32; ++i)
            // {
            //     for (int j = 0; j < 4; ++j)
            //         lessons.Add(new Lesson(teachers[7], classes[i], subjects[5]));
            //
            //     lesEdit.Add(new LessonEditor(new Lesson(teachers[7], classes[i], subjects[5]), 4));
            //
            // }
            //
            // for (int j = 0; j < 4; ++j)
            //     lessons.Add(new Lesson(teachers[7], classes[33], subjects[8]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[7], classes[33], subjects[8]), 4));
            //
            // for (int j = 0; j < 6; ++j)
            //     lessons.Add(new Lesson(teachers[8], classes[34], subjects[9]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[34], subjects[9]), 6));
            //
            //
            // for (int j = 0; j < 2; ++j)
            //     lessons.Add(new Lesson(teachers[8], classes[35], subjects[10]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[35], subjects[10]), 2));
            //
            // for (int j = 0; j < 3; ++j)
            //     lessons.Add(new Lesson(teachers[8], classes[36], subjects[10]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[36], subjects[10]), 3));
            //
            // for (int j = 0; j < 6; ++j)
            //     lessons.Add(new Lesson(teachers[8], classes[37], subjects[10]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[37], subjects[10]), 6));
            //
            // for (int j = 0; j < 8; ++j)
            //     lessons.Add(new Lesson(teachers[9], classes[38], subjects[11]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[9], classes[38], subjects[11]), 8));
            //
            // for (int j = 0; j < 4; ++j)
            //     lessons.Add(new Lesson(teachers[10], classes[39], subjects[12]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[10], classes[39], subjects[12]), 4));
            //
            // for (int i = 40; i <= 41; ++i)
            // {
            //     for (int j = 0; j < 6; ++j)
            //         lessons.Add(new Lesson(teachers[10], classes[i], subjects[12]));
            //
            //     lesEdit.Add(new LessonEditor(new Lesson(teachers[10], classes[i], subjects[12]), 6));
            // }
            //
            //
            // for (int j = 0; j < 8; ++j)
            //     lessons.Add(new Lesson(teachers[10], classes[42], subjects[12]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[10], classes[42], subjects[12]), 8));
            //
            // for (int i = 43; i <= 44; ++i)
            // {
            //     for (int j = 0; j < 4; ++j)
            //         lessons.Add(new Lesson(teachers[11], classes[i], subjects[13]));
            //
            //     lesEdit.Add(new LessonEditor(new Lesson(teachers[11], classes[i], subjects[13]), 4));
            // }
            //
            // for (int i = 45; i <= 46; ++i)
            // {
            //     for (int j = 0; j < 6; ++j)
            //         lessons.Add(new Lesson(teachers[11], classes[i], subjects[13]));
            //
            //     lesEdit.Add(new LessonEditor(new Lesson(teachers[11], classes[i], subjects[13]), 6));
            // }
            //
            // for (int j = 0; j < 9; ++j)
            //     lessons.Add(new Lesson(teachers[11], classes[47], subjects[13]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[11], classes[47], subjects[13]), 9));
            //
            //
            // for (int j = 0; j < 6; ++j)
            //     lessons.Add(new Lesson(teachers[12], classes[48], subjects[14]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[12], classes[48], subjects[14]), 6));
            //
            // for (int j = 0; j < 8; ++j)
            //     lessons.Add(new Lesson(teachers[12], classes[49], subjects[14]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[12], classes[49], subjects[14]), 8));
            //
            // for (int i = 50; i <= 53; ++i)
            // {
            //     for (int j = 0; j < 4; ++j)
            //         lessons.Add(new Lesson(teachers[12], classes[i], subjects[5]));
            //
            //     lesEdit.Add(new LessonEditor(new Lesson(teachers[12], classes[i], subjects[5]), 4));
            // }
            //
            // for (int j = 0; j < 4; ++j)
            //     lessons.Add(new Lesson(teachers[12], classes[54], subjects[8]));
            //
            // lesEdit.Add(new LessonEditor(new Lesson(teachers[12], classes[54], subjects[8]), 4));
            //
            // for (int i = 55; i <= 57; ++i)
            //     AddLessonEditor(15, 13, i, 4);
            //
            // AddLessonEditor(15, 13, 58, 6);
            // AddLessonEditor(15, 13, 59, 9);
            //
            // AddLessonEditor(16, 14, 60, 4);
            // AddLessonEditor(16, 14, 61, 6);
            //
            // for (int i = 62; i <= 63; ++i)
            //     AddLessonEditor(17, 14, i, 8);
            //
            // AddLessonEditor(18, 15, 64, 4);
            //
            // for (int i = 65; i <= 68; ++i)
            //     AddLessonEditor(18, 15, i, 6);
            //
            // for (int i = 69; i < 71; ++i)
            //     AddLessonEditor(18, 16, i, 4);
            //
            // for (int i = 71; i < 73; ++i)
            //     AddLessonEditor(18, 16, i, 6);
            //
            // for (int i = 73; i < 75; ++i)
            //     AddLessonEditor(19, 17, i, 4);
            //
            // for (int i = 75; i < 79; ++i)
            //     AddLessonEditor(19, 17, i, 6);
            //
            // for (int i = 79; i < 83; ++i)
            //     AddLessonEditor(20, 18, i, 4);
            //
            // for (int i = 83; i < 85; ++i)
            //     AddLessonEditor(21, 18, i, 4);
            //
            // AddLessonEditor(21, 18, 85, 6);
            //
            // for (int i = 86; i < 89; ++i)
            //     AddLessonEditor(5, 19, i, 4);
            //
            // AddLessonEditor(22, 19, 89, 4);
            // AddLessonEditor(22, 19, 90, 6);
            //
            // AddLessonEditor(23, 19, 91, 4);
            // for (int i = 92; i < 94; ++i)
            //     AddLessonEditor(1, 20, i, 4);
            //
            // #endregion
        }

        private static void AddLessonEditor(int subjectId, int teacherId, int classId, int cnt)
        {
            var lessons = Data.Instance.Lessons;
            var lesEdit = Data.Instance.LessonEditors;
            var teachers = Data.Instance.Teachers;
            var classes = Data.Instance.Classes;
            var subjects = Data.Instance.Subjects;

            for (int j = 0; j < cnt; ++j)
                lessons.Add(new Lesson(teachers[teacherId], classes[classId], subjects[subjectId]));

            lesEdit.Add(new LessonEditor(new Lesson(teachers[teacherId], classes[classId], subjects[subjectId]), cnt));



        }
        private static void FillRestrictions()
        {
            Data.Instance.Restrictions = new ObservableCollection<Restriction>();
            var expressions = new List<(int, string, int, string, int, bool)>()
            {
               // (1, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И t1 = t2 И x1 + 2 = x2 И d1 = d2 -> R(t3, s3, k3, c3, x3, d3) И t2 = t3 И x1 + 1 = x3 И d2 = d3 И d1 = d3", 7, "окон нет", 30, false),
               // (1, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И t1 = t2 И x1 + 2 = x2 И d1 = d2 -> R(t3, s3, k3, c3, x3, d3) И t2 = t3 И x1 + 1 = x3 И d2 = d3 И d1 = d3", 30, "Нет 1 окна", 60, false),
               // (3, "R(t1, s1, k1, c1, x1, d1, b1) И R(t2, s2, k2, c2, x2, d2, b2) И c1 = c2 И d1 = d2 И x1 = x2 + 3 -> R(t3, s3, k3, c3, x3, d3, b3) И R(t4, s4, k4, c4, x4, d4, b4) И c3 = c1 И d3 = d1 И x3 = x2 + 1 И c4 = c1 И d4 = d1 И x4 = x2 + 2", 30, "Нет 2 окн", 60, false)
               (1, "R(t1, s1, k1, c1, x1, d1) -> k1 != \"\"", 10, "Отсутствие конфликтов с кабинетами", 20, false),
                //(3, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x1 = x2 + 1 -> R(t3, s3, k3, c3, x3, d3) И R(t4, s4, k4, c4, x4, d4) И d3 = d4 И c3 = c4 И x3 = x4 + 1 ", -60, "По 2", 0, false),
               // (3, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x2 > x1 -> x2 = x1 + 2", -30, "По два урока", 0, false),
               // (3, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x2 > x1 -> x2 = x1 + 3", -30, "По два урока", 0, false),
               // (4, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x2 > x1 -> x2 = x1 + 4", -30, "По два урока", 0, false),
               // (5, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x2 > x1 -> x2 = x1 + 5", -30, "По два урока", 0, false),
               // (6, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x2 > x1 -> x2 = x1 + 6", -30, "По два урока", 0, false),
               // (2, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x2 > x1 -> x2 = x1 + 1", 20, "По два урока", 30, false),
               // (4, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И t1 = t2 И x1 + 3 = x2 И d1 = d2 -> R(t3, s3, k3, c3, x3, d3) И R(t4, s4, k4, c4, x4, d4) И t2 = t3 И x1 + 1 = x3 И d2 = d3 И t2 = t4 И x1 + 2 = x4 И d2 = d4", 5, "окон 2 нет", 15, false),

            }; 

            foreach (var expression in expressions)
            {
                var (errors, expr) = Analyzer.Analyze(expression.Item2);
                if (errors.Count != 0) continue;

                var text = Compilier.CreateFunction($"{expression.Item3} {expr}", expression.Item5);

                var compiler = Compilier.Compile(new string[1] { text });
                var method = Compilier.CreateMethod(compiler);


                Data.Instance.Restrictions.Add(new Restriction()
                {
                    Number = expression.Item1,
                    Comment = expression.Item4,
                    Method = method,
                    Expression = expression.Item2,
                    WeightPozitive = expression.Item3,
                    WeightNegative = expression.Item5,
                    IsRequirement = expression.Item6,
                });
            }
        }
        
        
        public static void FillRestrictions(ObservableCollection<Restriction> restrictions)
        {
            // Data.Instance.Restrictions = new ObservableCollection<Restriction>();
            // var expressions = new List<(int, string, int, string, int, bool)>()
            // {
            //    // (1, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И t1 = t2 И x1 + 2 = x2 И d1 = d2 -> R(t3, s3, k3, c3, x3, d3) И t2 = t3 И x1 + 1 = x3 И d2 = d3 И d1 = d3", 7, "окон нет", 30, false),
            //    // (1, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И t1 = t2 И x1 + 2 = x2 И d1 = d2 -> R(t3, s3, k3, c3, x3, d3) И t2 = t3 И x1 + 1 = x3 И d2 = d3 И d1 = d3", 30, "Нет 1 окна", 60, false),
            //    // (3, "R(t1, s1, k1, c1, x1, d1, b1) И R(t2, s2, k2, c2, x2, d2, b2) И c1 = c2 И d1 = d2 И x1 = x2 + 3 -> R(t3, s3, k3, c3, x3, d3, b3) И R(t4, s4, k4, c4, x4, d4, b4) И c3 = c1 И d3 = d1 И x3 = x2 + 1 И c4 = c1 И d4 = d1 И x4 = x2 + 2", 30, "Нет 2 окн", 60, false)
            //    (1, "R(t1, s1, k1, c1, x1, d1) -> k1 != \"\"", 30, "Отсутствие конфликтов с кабинетами", 40, false),
            //     //(3, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x1 = x2 + 1 -> R(t3, s3, k3, c3, x3, d3) И R(t4, s4, k4, c4, x4, d4) И d3 = d4 И c3 = c4 И x3 = x4 + 1 ", -60, "По 2", 0, false),
            //    // (3, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x2 > x1 -> x2 = x1 + 2", -30, "По два урока", 0, false),
            //    // (3, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x2 > x1 -> x2 = x1 + 3", -30, "По два урока", 0, false),
            //    // (4, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x2 > x1 -> x2 = x1 + 4", -30, "По два урока", 0, false),
            //    // (5, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x2 > x1 -> x2 = x1 + 5", -30, "По два урока", 0, false),
            //    // (6, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x2 > x1 -> x2 = x1 + 6", -30, "По два урока", 0, false),
            //    (2, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И d1 = d2 И c1 = c2 И x2 > x1 -> x2 = x1 + 1", 10, "По два урока", 20, false),
            //    // (4, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И t1 = t2 И x1 + 3 = x2 И d1 = d2 -> R(t3, s3, k3, c3, x3, d3) И R(t4, s4, k4, c4, x4, d4) И t2 = t3 И x1 + 1 = x3 И d2 = d3 И t2 = t4 И x1 + 2 = x4 И d2 = d4", 5, "окон 2 нет", 15, false),
            //
            // }; 

            foreach (var restriction in restrictions)
            {
                var (errors, expr) = Analyzer.Analyze(restriction.Expression);
                if (errors.Count != 0) continue;

                var text = Compilier.CreateFunction($"{restriction.WeightPozitive} {expr}", restriction.WeightNegative);

                var compiler = Compilier.Compile(new string[1] { text });
                var method = Compilier.CreateMethod(compiler);

                restriction.Method = method;
            }
        }
    }
}
