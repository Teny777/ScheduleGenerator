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

            Data.Instance.Lessons = new List<Lesson>();
            Data.Instance.LessonEditors = new ObservableCollection<LessonEditor>();

            #region SubjectsWrite
            Data.Instance.Subjects = new ObservableCollection<Subject>()
            {
                new Subject("Математика", true),        // 0
                new Subject("Алгебра", true),           // 1
                new Subject("Геометрия", true),         // 2
                new Subject("Русский язык"),            // 3
                new Subject("Литература"),              // 4 
                new Subject("Иностранный язык"),        // 5
                new Subject("История"),                 // 6
                new Subject("Обществознание"),          // 7
                new Subject("География", true),         // 8
                new Subject("Биология", true),          // 9
                new Subject("Музыка"),                  // 10
                new Subject("ИЗО"),                     // 11
                new Subject("Технология"),              // 12
                new Subject("Физическая культура"),     // 13
                new Subject("Физика", true),            // 14
                new Subject("Химия", true),             // 15
                new Subject("Информатика", true)        // 16
            };
            #endregion

            #region TeacherWrite
            Data.Instance.Teachers = new Dictionary<int, Teacher>
            {
                // 5 класс
                { 1, new Teacher(1, "Иванов В.В.", new ObservableCollection<Subject>{ Data.Instance.Subjects[3], Data.Instance.Subjects[4]} ) },// русский литература
                { 2, new Teacher(2, "Петров М.М.", new ObservableCollection<Subject>{ Data.Instance.Subjects[5]}) }, // Английский язык
                { 3, new Teacher(3, "Сидоров Д.Д.", new ObservableCollection<Subject>{ Data.Instance.Subjects[0], Data.Instance.Subjects[1], Data.Instance.Subjects[2]}) }, // математика алгебра геометрия
                { 4, new Teacher(4, "Иванова О.О.", new ObservableCollection<Subject>{ Data.Instance.Subjects[6], Data.Instance.Subjects[7]}) },// История обществознание
                { 5, new Teacher(5, "Петрова К.К.", new ObservableCollection<Subject>{ Data.Instance.Subjects[8]})}, // География
                { 6, new Teacher(6, "Сидорова М.М.", new ObservableCollection<Subject>{ Data.Instance.Subjects[9]}) }, // Биология
                { 7, new Teacher(7, "Ромашкина В.В.", new ObservableCollection<Subject>{ Data.Instance.Subjects[10]}) }, // Музыка
                { 8, new Teacher(8, "Романова С.С.", new ObservableCollection<Subject>{ Data.Instance.Subjects[11]}) }, // ИЗО
                { 9, new Teacher(9, "Петрова А.А.", new ObservableCollection<Subject>{ Data.Instance.Subjects[12]}) }, // Технология
                { 10, new Teacher(10, "Кошкина Г.П.", new ObservableCollection<Subject>{ Data.Instance.Subjects[13]}) },// физкультура
                // 6 класс
                { 11, new Teacher(11, "Птичкина Р.Р.", new ObservableCollection<Subject>{ Data.Instance.Subjects[3], Data.Instance.Subjects[4]})}, // Русский Литература
                { 12, new Teacher(12, "Воробей А.А.", new ObservableCollection<Subject>{ Data.Instance.Subjects[0]}) }, // Математика
                // 7 класс
                { 13, new Teacher(13, "Соловей Н.Н.", new ObservableCollection<Subject>{ Data.Instance.Subjects[3], Data.Instance.Subjects[4]})}, // Русский Литература
                { 14, new Teacher(14, "Орлова В.В.", new ObservableCollection<Subject>{ Data.Instance.Subjects[5]}) }, // Английский язык
                { 15, new Teacher(15, "Синицына Е.Е.", new ObservableCollection<Subject>{ Data.Instance.Subjects[0], Data.Instance.Subjects[1], Data.Instance.Subjects[2] }) }, // математика алгебра геометрия
                { 16, new Teacher(16, "Физикова Т.Т.", new ObservableCollection<Subject>{ Data.Instance.Subjects[14]} ) }, // Физика
                { 17, new Teacher(17, "Историкова К.К.", new ObservableCollection<Subject>{ Data.Instance.Subjects[6], Data.Instance.Subjects[7]}) }, // История/обществознание 
                // 8 класс
                { 18, new Teacher(18, "Березова А.А.", new ObservableCollection<Subject>{ Data.Instance.Subjects[3], Data.Instance.Subjects[4]})}, // Русский Литература
                { 19, new Teacher(19, "Осина Е.Е.", new ObservableCollection<Subject>{ Data.Instance.Subjects[0], Data.Instance.Subjects[1], Data.Instance.Subjects[2]})}, // математика алгебра геометрия
                { 20, new Teacher(20, "Дубова Н.Н.", new ObservableCollection<Subject>{ Data.Instance.Subjects[16]})}, // Информатика
                { 21, new Teacher(21, "Кедрова О.О.", new ObservableCollection<Subject>{ Data.Instance.Subjects[15]})}, // Химия
                { 22, new Teacher(22, "Елькин К.К.", new ObservableCollection<Subject>{ Data.Instance.Subjects[13]})}, // Физкультура
                // 9 класс
                { 23, new Teacher(23, "Русских Е.Е.", new ObservableCollection<Subject>{Data.Instance.Subjects[3], Data.Instance.Subjects[4] })}, // Русский Литература
                { 24, new Teacher(24, "Математических В.В.", new ObservableCollection<Subject>{ Data.Instance.Subjects[0]}) }, // Математика
                { 25, new Teacher(25, "Английских К.К.", new ObservableCollection<Subject>{ Data.Instance.Subjects[5]}) }, // Английский язык
                { 26, new Teacher(26, "Исторических М.М.", new ObservableCollection<Subject>{ Data.Instance.Subjects[6], Data.Instance.Subjects[7]}) }, // История/обществознание 
                { 27, new Teacher(27, "Географических Л.Л.", new ObservableCollection<Subject>{ Data.Instance.Subjects[8]})}, // География
                { 28, new Teacher(28, "Биологических Г.Г.", new ObservableCollection<Subject>{ Data.Instance.Subjects[9] }) }, // Биология
                { 29, new Teacher(29, "Физкультурных К.К.", new ObservableCollection<Subject>{ Data.Instance.Subjects[13] })}, // Физкультура
            };
            #endregion

            #region ClassWrite
            Data.Instance.Classes = new Dictionary<int, Class>()
            {
                {1, new Class("5А", 1) },
                {2, new Class("5Б", 2) },
                {3, new Class("5В", 3) },
                {4, new Class("6А", 4) },
                {5, new Class("6Б", 5) },
                {6, new Class("6В", 6) },
                {7, new Class("7A", 7) },
                {8, new Class("7Б", 8) },
                {9, new Class("7В", 9) },
                {10, new Class("8A", 10) },
                {11, new Class("8Б", 11) },
                {12, new Class("8В", 12) },
                {13, new Class("9A", 13) },
                {14, new Class("9Б", 14) },
                {15, new Class("9В", 15) }
            };
            #endregion

            #region LessonWrite
            var lessons = Data.Instance.Lessons;
            var lesEdit = Data.Instance.LessonEditors;
            var teachers = Data.Instance.Teachers;
            var classes = Data.Instance.Classes;
            var subjects = Data.Instance.Subjects;

            for (int i = 0; i < 4; i++)  // по 4 урока руского у каждого класса 7 класса
            {
                lessons.Add(new Lesson(teachers[13], classes[7], subjects[3]));
                lessons.Add(new Lesson(teachers[13], classes[8], subjects[3]));
                lessons.Add(new Lesson(teachers[13], classes[9], subjects[3]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[13], classes[7], subjects[3]), 4));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[13], classes[8], subjects[3]), 4));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[13], classes[9], subjects[3]), 4));

            for (int i = 0; i < 2; i++)  // по 2 урока литературы у каждого класса 7 класса
            {
                lessons.Add(new Lesson(teachers[13], classes[7], subjects[4]));
                lessons.Add(new Lesson(teachers[13], classes[8], subjects[4]));
                lessons.Add(new Lesson(teachers[13], classes[9], subjects[4]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[13], classes[7], subjects[4]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[13], classes[8], subjects[4]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[13], classes[9], subjects[4]), 2));


            for (int i = 0; i < 6; i++)  // по 6 уроков русского у каждого класса 6 класса
            {
                lessons.Add(new Lesson(teachers[11], classes[4], subjects[7]));
                lessons.Add(new Lesson(teachers[11], classes[5], subjects[3]));
                lessons.Add(new Lesson(teachers[11], classes[6], subjects[3]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[11], classes[4], subjects[3]), 6));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[11], classes[5], subjects[3]), 6));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[11], classes[6], subjects[3]), 6));

            for (int i = 0; i < 2; i++)  // по 2 урока литературы у каждого класса 6 класса
            {
                lessons.Add(new Lesson(teachers[11], classes[5], subjects[4]));
                lessons.Add(new Lesson(teachers[11], classes[6], subjects[4]));
                lessons.Add(new Lesson(teachers[11], classes[4], subjects[4]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[11], classes[5], subjects[4]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[11], classes[6], subjects[4]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[11], classes[4], subjects[4]), 2));


            for (int i = 0; i < 6; i++)  // по 6 уроков русского у каждого 5 класса
            {
                lessons.Add(new Lesson(teachers[1], classes[1], subjects[3]));
                lessons.Add(new Lesson(teachers[1], classes[2], subjects[3]));
                lessons.Add(new Lesson(teachers[1], classes[3], subjects[3]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[1], classes[1], subjects[3]), 6));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[1], classes[2], subjects[3]), 6));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[1], classes[3], subjects[3]), 6));

            for (int i = 0; i < 2; i++)  // по 2 уроков литературы у каждого 5 класса
            {
                lessons.Add(new Lesson(teachers[1], classes[1], subjects[4]));
                lessons.Add(new Lesson(teachers[1], classes[2], subjects[4]));
                lessons.Add(new Lesson(teachers[1], classes[3], subjects[4]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[1], classes[1], subjects[4]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[1], classes[2], subjects[4]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[1], classes[3], subjects[4]), 2));

            for (int i = 0; i < 5; i++)  // по 3 урока русского у каждого класса 8 класса
            {
                lessons.Add(new Lesson(teachers[18], classes[10], subjects[3]));
                lessons.Add(new Lesson(teachers[18], classes[11], subjects[3]));
                lessons.Add(new Lesson(teachers[18], classes[12], subjects[3]));
                // 9 класс
                lessons.Add(new Lesson(teachers[23], classes[13], subjects[3]));
                lessons.Add(new Lesson(teachers[23], classes[14], subjects[3]));
                lessons.Add(new Lesson(teachers[23], classes[15], subjects[3]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[18], classes[10], subjects[3]), 5));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[18], classes[11], subjects[3]), 5));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[18], classes[12], subjects[3]), 5));
            // 9 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[23], classes[13], subjects[3]), 5));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[23], classes[14], subjects[3]), 5));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[23], classes[15], subjects[3]), 5));

            for (int i = 0; i < 5; i++)  // по 2 урока литературы у каждого класса 8 класса
            {
                lessons.Add(new Lesson(teachers[18], classes[10], subjects[4]));
                lessons.Add(new Lesson(teachers[18], classes[11], subjects[4]));
                lessons.Add(new Lesson(teachers[18], classes[12], subjects[4]));
                // 9 класс
                lessons.Add(new Lesson(teachers[23], classes[13], subjects[4]));
                lessons.Add(new Lesson(teachers[23], classes[14], subjects[4]));
                lessons.Add(new Lesson(teachers[23], classes[15], subjects[4]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[18], classes[10], subjects[4]), 5));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[18], classes[11], subjects[4]), 5));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[18], classes[12], subjects[4]), 5));
            // 9 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[23], classes[13], subjects[4]), 5));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[23], classes[14], subjects[4]), 5));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[23], classes[15], subjects[4]), 5));

            for (int i = 0; i < 3; i++)  // три урока Англ
            {
                lessons.Add(new Lesson(teachers[2], classes[1], subjects[5]));
                lessons.Add(new Lesson(teachers[2], classes[2], subjects[5]));
                lessons.Add(new Lesson(teachers[2], classes[3], subjects[5]));
                // 6 класс
                lessons.Add(new Lesson(teachers[2], classes[4], subjects[5]));
                lessons.Add(new Lesson(teachers[2], classes[5], subjects[5]));
                lessons.Add(new Lesson(teachers[2], classes[6], subjects[5]));
                // 7 класс
                lessons.Add(new Lesson(teachers[14], classes[7], subjects[5]));
                lessons.Add(new Lesson(teachers[14], classes[8], subjects[5]));
                lessons.Add(new Lesson(teachers[14], classes[9], subjects[5]));
                // 8 класс
                lessons.Add(new Lesson(teachers[14], classes[10], subjects[5]));
                lessons.Add(new Lesson(teachers[14], classes[11], subjects[5]));
                lessons.Add(new Lesson(teachers[14], classes[12], subjects[5]));
                // 9 класс
                lessons.Add(new Lesson(teachers[25], classes[13], subjects[5]));
                lessons.Add(new Lesson(teachers[25], classes[14], subjects[5]));
                lessons.Add(new Lesson(teachers[25], classes[15], subjects[5]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[2], classes[1], subjects[5]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[2], classes[2], subjects[5]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[2], classes[3], subjects[5]), 3));
            // 6 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[2], classes[4], subjects[5]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[2], classes[5], subjects[5]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[2], classes[6], subjects[5]), 3));
            // 7 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[14], classes[7], subjects[5]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[14], classes[8], subjects[5]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[14], classes[9], subjects[5]), 3));
            // 8 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[14], classes[10], subjects[5]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[14], classes[11], subjects[5]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[14], classes[12], subjects[5]), 3));
            // 9 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[25], classes[13], subjects[5]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[25], classes[14], subjects[5]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[25], classes[15], subjects[5]), 3));


            for (int i = 0; i < 5; i++)  // по 5 уроков математики в 5-6 классах
            {
                lessons.Add(new Lesson(teachers[3], classes[1], subjects[0]));
                lessons.Add(new Lesson(teachers[3], classes[2], subjects[0]));
                lessons.Add(new Lesson(teachers[3], classes[3], subjects[0]));
                // 6 класс
                lessons.Add(new Lesson(teachers[12], classes[4], subjects[0]));
                lessons.Add(new Lesson(teachers[12], classes[5], subjects[0]));
                lessons.Add(new Lesson(teachers[12], classes[6], subjects[0]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[3], classes[1], subjects[0]), 5));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[3], classes[2], subjects[0]), 5));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[3], classes[3], subjects[0]), 5));
            // 6 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[12], classes[4], subjects[0]), 5));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[12], classes[5], subjects[0]), 5));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[12], classes[6], subjects[0]), 5));

            for (int i = 0; i < 3; i++)
            { // 3 урока алгебры
                // 7 класс
                lessons.Add(new Lesson(teachers[15], classes[7], subjects[1]));
                lessons.Add(new Lesson(teachers[15], classes[8], subjects[1]));
                lessons.Add(new Lesson(teachers[15], classes[9], subjects[1]));
                // 8 класс
                lessons.Add(new Lesson(teachers[19], classes[10], subjects[1]));
                lessons.Add(new Lesson(teachers[19], classes[11], subjects[1]));
                lessons.Add(new Lesson(teachers[19], classes[12], subjects[1]));
                // 9 класс
                lessons.Add(new Lesson(teachers[24], classes[13], subjects[1]));
                lessons.Add(new Lesson(teachers[24], classes[14], subjects[1]));
                lessons.Add(new Lesson(teachers[24], classes[15], subjects[1]));
            }
            // 7 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[15], classes[7], subjects[1]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[15], classes[8], subjects[1]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[15], classes[9], subjects[1]), 3));
            // 8 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[19], classes[10], subjects[1]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[19], classes[11], subjects[1]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[19], classes[12], subjects[1]), 3));
            // 9 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[24], classes[13], subjects[1]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[24], classes[14], subjects[1]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[24], classes[15], subjects[1]), 3));

            for (int i = 0; i < 2; i++)
            {  // 2 урока геометрии
                // 7 класс
                lessons.Add(new Lesson(teachers[15], classes[7], subjects[2]));
                lessons.Add(new Lesson(teachers[15], classes[8], subjects[2]));
                lessons.Add(new Lesson(teachers[15], classes[9], subjects[2]));
                // 8 класс
                lessons.Add(new Lesson(teachers[19], classes[10], subjects[2]));
                lessons.Add(new Lesson(teachers[19], classes[11], subjects[2]));
                lessons.Add(new Lesson(teachers[19], classes[12], subjects[2]));
                // 9 класс
                lessons.Add(new Lesson(teachers[24], classes[13], subjects[2]));
                lessons.Add(new Lesson(teachers[24], classes[14], subjects[2]));
                lessons.Add(new Lesson(teachers[24], classes[15], subjects[2]));
            }
            // 7 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[15], classes[7], subjects[2]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[15], classes[8], subjects[2]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[15], classes[9], subjects[2]), 2));
            // 8 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[19], classes[10], subjects[2]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[19], classes[11], subjects[2]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[19], classes[12], subjects[2]), 2));
            // 9 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[24], classes[13], subjects[2]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[24], classes[14], subjects[2]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[24], classes[15], subjects[2]), 2));


            // один урок информатики в 8 классе
            lessons.Add(new Lesson(teachers[20], classes[10], subjects[16]));
            lessons.Add(new Lesson(teachers[20], classes[11], subjects[16]));
            lessons.Add(new Lesson(teachers[20], classes[12], subjects[16]));

            lesEdit.Add(new LessonEditor(new Lesson(teachers[20], classes[10], subjects[16]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[20], classes[11], subjects[16]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[20], classes[12], subjects[16]), 1));
            // два урока в 9 классе
            for (int i = 0; i < 2; i++)
            {
                lessons.Add(new Lesson(teachers[20], classes[13], subjects[16]));
                lessons.Add(new Lesson(teachers[20], classes[14], subjects[16]));
                lessons.Add(new Lesson(teachers[20], classes[15], subjects[16]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[20], classes[13], subjects[16]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[20], classes[14], subjects[16]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[20], classes[15], subjects[16]), 2));

            for (int i = 0; i < 2; i++)  // 2 урока истории
            {
                lessons.Add(new Lesson(teachers[4], classes[1], subjects[6]));
                lessons.Add(new Lesson(teachers[4], classes[2], subjects[6]));
                lessons.Add(new Lesson(teachers[4], classes[3], subjects[6]));
                // 6 класс
                lessons.Add(new Lesson(teachers[4], classes[4], subjects[6]));
                lessons.Add(new Lesson(teachers[4], classes[5], subjects[6]));
                lessons.Add(new Lesson(teachers[4], classes[6], subjects[6]));
                // 7 класс
                lessons.Add(new Lesson(teachers[17], classes[7], subjects[6]));
                lessons.Add(new Lesson(teachers[17], classes[8], subjects[6]));
                lessons.Add(new Lesson(teachers[17], classes[9], subjects[6]));
                // 8 класс
                lessons.Add(new Lesson(teachers[17], classes[10], subjects[6]));
                lessons.Add(new Lesson(teachers[17], classes[11], subjects[6]));
                lessons.Add(new Lesson(teachers[17], classes[12], subjects[6]));
                // 9 класс
                lessons.Add(new Lesson(teachers[26], classes[13], subjects[6]));
                lessons.Add(new Lesson(teachers[26], classes[14], subjects[6]));
                lessons.Add(new Lesson(teachers[26], classes[15], subjects[6]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[1], subjects[6]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[2], subjects[6]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[3], subjects[6]), 2));
            // 6 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[4], subjects[6]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[5], subjects[6]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[6], subjects[6]), 2));
            // 7 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[17], classes[7], subjects[6]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[17], classes[8], subjects[6]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[17], classes[9], subjects[6]), 2));
            // 8 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[17], classes[10], subjects[6]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[17], classes[11], subjects[6]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[17], classes[12], subjects[6]), 2));
            // 9 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[26], classes[13], subjects[6]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[26], classes[14], subjects[6]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[26], classes[15], subjects[6]), 2));

            // 1 урок обществознания 
            lessons.Add(new Lesson(teachers[4], classes[1], subjects[7]));
            lessons.Add(new Lesson(teachers[4], classes[2], subjects[7]));
            lessons.Add(new Lesson(teachers[4], classes[3], subjects[7]));
            // 6 класс
            lessons.Add(new Lesson(teachers[4], classes[4], subjects[7]));
            lessons.Add(new Lesson(teachers[4], classes[5], subjects[7]));
            lessons.Add(new Lesson(teachers[4], classes[6], subjects[7]));
            // 7 класс
            lessons.Add(new Lesson(teachers[17], classes[7], subjects[7]));
            lessons.Add(new Lesson(teachers[17], classes[8], subjects[7]));
            lessons.Add(new Lesson(teachers[17], classes[9], subjects[7]));
            // 8 класс
            lessons.Add(new Lesson(teachers[17], classes[10], subjects[7]));
            lessons.Add(new Lesson(teachers[17], classes[11], subjects[7]));
            lessons.Add(new Lesson(teachers[17], classes[12], subjects[7]));
            // 9 класс
            lessons.Add(new Lesson(teachers[26], classes[13], subjects[7]));
            lessons.Add(new Lesson(teachers[26], classes[14], subjects[7]));
            lessons.Add(new Lesson(teachers[26], classes[15], subjects[7]));

            lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[1], subjects[7]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[2], subjects[7]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[3], subjects[7]), 1));
            // 6 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[4], subjects[7]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[5], subjects[7]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[4], classes[6], subjects[7]), 1));
            // 7 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[17], classes[7], subjects[7]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[17], classes[8], subjects[7]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[17], classes[9], subjects[7]), 1));
            // 8 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[17], classes[10], subjects[7]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[17], classes[11], subjects[7]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[17], classes[12], subjects[7]), 1));
            // 9 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[26], classes[13], subjects[7]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[26], classes[14], subjects[7]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[26], classes[15], subjects[7]), 1));

            // один урок географии
            lessons.Add(new Lesson(teachers[5], classes[1], subjects[8]));
            lessons.Add(new Lesson(teachers[5], classes[2], subjects[8]));
            lessons.Add(new Lesson(teachers[5], classes[3], subjects[8]));

            lesEdit.Add(new LessonEditor(new Lesson(teachers[5], classes[1], subjects[8]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[5], classes[2], subjects[8]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[5], classes[3], subjects[8]), 1));
            // 6 класс
            lessons.Add(new Lesson(teachers[5], classes[4], subjects[8]));
            lessons.Add(new Lesson(teachers[5], classes[5], subjects[8]));
            lessons.Add(new Lesson(teachers[5], classes[6], subjects[8]));

            lesEdit.Add(new LessonEditor(new Lesson(teachers[5], classes[4], subjects[8]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[5], classes[5], subjects[8]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[5], classes[6], subjects[8]), 1));
            for (int i = 0; i < 2; i++)  // 2 урока география 7 класс
            {
                lessons.Add(new Lesson(teachers[5], classes[7], subjects[8]));
                lessons.Add(new Lesson(teachers[5], classes[8], subjects[8]));
                lessons.Add(new Lesson(teachers[5], classes[9], subjects[8]));
                // 8 класс
                lessons.Add(new Lesson(teachers[27], classes[10], subjects[8]));
                lessons.Add(new Lesson(teachers[27], classes[11], subjects[8]));
                lessons.Add(new Lesson(teachers[27], classes[12], subjects[8]));
                // 9 класс
                lessons.Add(new Lesson(teachers[27], classes[13], subjects[8]));
                lessons.Add(new Lesson(teachers[27], classes[14], subjects[8]));
                lessons.Add(new Lesson(teachers[27], classes[15], subjects[8]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[5], classes[7], subjects[8]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[5], classes[8], subjects[8]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[5], classes[9], subjects[8]), 2));
            // 8 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[27], classes[10], subjects[8]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[27], classes[11], subjects[8]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[27], classes[12], subjects[8]), 2));
            // 9 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[27], classes[13], subjects[8]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[27], classes[14], subjects[8]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[27], classes[15], subjects[8]), 2));

            // 2 урока физики
            for (int i = 0; i < 2; i++)
            {
                // 7 класс
                lessons.Add(new Lesson(teachers[16], classes[7], subjects[14]));
                lessons.Add(new Lesson(teachers[16], classes[8], subjects[14]));
                lessons.Add(new Lesson(teachers[16], classes[9], subjects[14]));
                // 8 класс
                lessons.Add(new Lesson(teachers[16], classes[10], subjects[14]));
                lessons.Add(new Lesson(teachers[16], classes[11], subjects[14]));
                lessons.Add(new Lesson(teachers[16], classes[12], subjects[14]));
                // 9 класс
                lessons.Add(new Lesson(teachers[16], classes[13], subjects[14]));
                lessons.Add(new Lesson(teachers[16], classes[14], subjects[14]));
                lessons.Add(new Lesson(teachers[16], classes[15], subjects[14]));
            }
            // 7 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[16], classes[7], subjects[14]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[16], classes[8], subjects[14]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[16], classes[9], subjects[14]), 2));
            // 8 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[16], classes[10], subjects[14]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[16], classes[11], subjects[14]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[16], classes[12], subjects[14]), 2));
            // 9 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[16], classes[13], subjects[14]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[16], classes[14], subjects[14]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[16], classes[15], subjects[14]), 2));

            // 2 урока химии 
            for (int i = 0; i < 2; i++)
            {
                // 8 класс
                lessons.Add(new Lesson(teachers[21], classes[10], subjects[15]));
                lessons.Add(new Lesson(teachers[21], classes[11], subjects[15]));
                lessons.Add(new Lesson(teachers[21], classes[12], subjects[15]));
                // 9 класс
                lessons.Add(new Lesson(teachers[21], classes[13], subjects[15]));
                lessons.Add(new Lesson(teachers[21], classes[14], subjects[15]));
                lessons.Add(new Lesson(teachers[21], classes[15], subjects[15]));
            }
            // 8 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[21], classes[10], subjects[15]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[21], classes[11], subjects[15]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[21], classes[12], subjects[15]), 2));
            // 9 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[21], classes[13], subjects[15]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[21], classes[14], subjects[15]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[21], classes[15], subjects[15]), 2));

            // один урок Биологии
            lessons.Add(new Lesson(teachers[6], classes[1], subjects[9]));
            lessons.Add(new Lesson(teachers[6], classes[2], subjects[9]));
            lessons.Add(new Lesson(teachers[6], classes[3], subjects[9]));
            // 6 класс
            lessons.Add(new Lesson(teachers[6], classes[4], subjects[9]));
            lessons.Add(new Lesson(teachers[6], classes[5], subjects[9]));
            lessons.Add(new Lesson(teachers[6], classes[6], subjects[9]));
            // один урок Биологии
            lesEdit.Add(new LessonEditor(new Lesson(teachers[6], classes[1], subjects[9]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[6], classes[2], subjects[9]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[6], classes[3], subjects[9]), 1));
            // 6 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[6], classes[4], subjects[9]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[6], classes[5], subjects[9]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[6], classes[6], subjects[9]), 1));

            // 2 урока биологии 7 класс
            for (int i = 0; i < 2; i++)
            {
                lessons.Add(new Lesson(teachers[6], classes[7], subjects[9]));
                lessons.Add(new Lesson(teachers[6], classes[8], subjects[9]));
                lessons.Add(new Lesson(teachers[6], classes[9], subjects[9]));
                // 8 класс
                lessons.Add(new Lesson(teachers[28], classes[10], subjects[9]));
                lessons.Add(new Lesson(teachers[28], classes[11], subjects[9]));
                lessons.Add(new Lesson(teachers[28], classes[12], subjects[9]));
                // 9 класс
                lessons.Add(new Lesson(teachers[28], classes[13], subjects[9]));
                lessons.Add(new Lesson(teachers[28], classes[14], subjects[9]));
                lessons.Add(new Lesson(teachers[28], classes[15], subjects[9]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[6], classes[7], subjects[9]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[6], classes[8], subjects[9]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[6], classes[9], subjects[9]), 2));
            // 8 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[28], classes[10], subjects[9]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[28], classes[11], subjects[9]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[28], classes[12], subjects[9]), 2));
            // 9 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[28], classes[13], subjects[9]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[28], classes[14], subjects[9]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[28], classes[15], subjects[9]), 2));

            // один урок Музыки
            lessons.Add(new Lesson(teachers[7], classes[1], subjects[10]));
            lessons.Add(new Lesson(teachers[7], classes[2], subjects[10]));
            lessons.Add(new Lesson(teachers[7], classes[3], subjects[10]));
            // 6 класс
            lessons.Add(new Lesson(teachers[7], classes[4], subjects[10]));
            lessons.Add(new Lesson(teachers[7], classes[5], subjects[10]));
            lessons.Add(new Lesson(teachers[7], classes[6], subjects[10]));
            // 7 класс
            lessons.Add(new Lesson(teachers[7], classes[7], subjects[10]));
            lessons.Add(new Lesson(teachers[7], classes[8], subjects[10]));
            lessons.Add(new Lesson(teachers[7], classes[9], subjects[10]));

            // один урок Музыки
            lesEdit.Add(new LessonEditor(new Lesson(teachers[7], classes[1], subjects[10]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[7], classes[2], subjects[10]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[7], classes[3], subjects[10]), 1));
            // 6 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[7], classes[4], subjects[10]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[7], classes[5], subjects[10]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[7], classes[6], subjects[10]), 1));
            // 7 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[7], classes[7], subjects[10]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[7], classes[8], subjects[10]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[7], classes[9], subjects[10]), 1));


            // один урок ИЗО
            lessons.Add(new Lesson(teachers[8], classes[1], subjects[11]));
            lessons.Add(new Lesson(teachers[8], classes[2], subjects[11]));
            lessons.Add(new Lesson(teachers[8], classes[3], subjects[11]));
            // 6 класс
            lessons.Add(new Lesson(teachers[8], classes[4], subjects[11]));
            lessons.Add(new Lesson(teachers[8], classes[5], subjects[11]));
            lessons.Add(new Lesson(teachers[8], classes[6], subjects[11]));
            // 7 класс
            lessons.Add(new Lesson(teachers[8], classes[7], subjects[11]));
            lessons.Add(new Lesson(teachers[8], classes[8], subjects[11]));
            lessons.Add(new Lesson(teachers[8], classes[9], subjects[11]));
            // 8 класс
            lessons.Add(new Lesson(teachers[8], classes[10], subjects[11]));
            lessons.Add(new Lesson(teachers[8], classes[11], subjects[11]));
            lessons.Add(new Lesson(teachers[8], classes[12], subjects[11]));
            // 9 класс
            lessons.Add(new Lesson(teachers[8], classes[13], subjects[11]));
            lessons.Add(new Lesson(teachers[8], classes[14], subjects[11]));
            lessons.Add(new Lesson(teachers[8], classes[15], subjects[11]));

            // один урок ИЗО
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[1], subjects[11]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[2], subjects[11]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[3], subjects[11]), 1));
            // 6 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[4], subjects[11]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[5], subjects[11]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[6], subjects[11]), 1));
            // 7 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[7], subjects[11]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[8], subjects[11]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[9], subjects[11]), 1));
            // 8 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[10], subjects[11]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[11], subjects[11]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[12], subjects[11]), 1));
            // 9 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[13], subjects[11]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[14], subjects[11]), 1));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[8], classes[15], subjects[11]), 1));

            for (int i = 0; i < 2; i++)  // два урока технологии
            {
                lessons.Add(new Lesson(teachers[9], classes[1], subjects[12]));
                lessons.Add(new Lesson(teachers[9], classes[2], subjects[12]));
                lessons.Add(new Lesson(teachers[9], classes[3], subjects[12]));
                // 6 класс
                lessons.Add(new Lesson(teachers[9], classes[4], subjects[12]));
                lessons.Add(new Lesson(teachers[9], classes[5], subjects[12]));
                lessons.Add(new Lesson(teachers[9], classes[6], subjects[12]));
                // 7 класс
                lessons.Add(new Lesson(teachers[9], classes[7], subjects[12]));
                lessons.Add(new Lesson(teachers[9], classes[8], subjects[12]));
                lessons.Add(new Lesson(teachers[9], classes[9], subjects[12]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[9], classes[1], subjects[12]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[9], classes[2], subjects[12]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[9], classes[3], subjects[12]), 2));
            // 6 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[9], classes[4], subjects[12]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[9], classes[5], subjects[12]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[9], classes[6], subjects[12]), 2));
            // 7 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[9], classes[7], subjects[12]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[9], classes[8], subjects[12]), 2));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[9], classes[9], subjects[12]), 2));

            for (int i = 0; i < 3; i++)  // три урока физкультуры
            {
                lessons.Add(new Lesson(teachers[10], classes[1], subjects[13]));
                lessons.Add(new Lesson(teachers[10], classes[2], subjects[13]));
                lessons.Add(new Lesson(teachers[10], classes[3], subjects[13]));
                // 6 класс
                lessons.Add(new Lesson(teachers[10], classes[4], subjects[13]));
                lessons.Add(new Lesson(teachers[10], classes[5], subjects[13]));
                lessons.Add(new Lesson(teachers[10], classes[6], subjects[13]));
                // 7 класс
                lessons.Add(new Lesson(teachers[22], classes[7], subjects[13]));
                lessons.Add(new Lesson(teachers[22], classes[8], subjects[13]));
                lessons.Add(new Lesson(teachers[22], classes[9], subjects[13]));
                // 8 класс
                lessons.Add(new Lesson(teachers[22], classes[10], subjects[13]));
                lessons.Add(new Lesson(teachers[22], classes[11], subjects[13]));
                lessons.Add(new Lesson(teachers[22], classes[12], subjects[13]));
                // 9 класс
                lessons.Add(new Lesson(teachers[29], classes[13], subjects[13]));
                lessons.Add(new Lesson(teachers[29], classes[14], subjects[13]));
                lessons.Add(new Lesson(teachers[29], classes[15], subjects[13]));
            }
            lesEdit.Add(new LessonEditor(new Lesson(teachers[10], classes[1], subjects[13]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[10], classes[2], subjects[13]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[10], classes[3], subjects[13]), 3));
            // 6 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[10], classes[4], subjects[13]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[10], classes[5], subjects[13]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[10], classes[6], subjects[13]), 3));
            // 7 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[22], classes[7], subjects[13]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[22], classes[8], subjects[13]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[22], classes[9], subjects[13]), 3));
            // 8 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[22], classes[10], subjects[13]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[22], classes[11], subjects[13]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[22], classes[12], subjects[13]), 3));
            // 9 класс
            lesEdit.Add(new LessonEditor(new Lesson(teachers[29], classes[13], subjects[13]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[29], classes[14], subjects[13]), 3));
            lesEdit.Add(new LessonEditor(new Lesson(teachers[29], classes[15], subjects[13]), 3));

            #endregion
            var s = 1;
        }

        private static void FillRestrictions()
        {
            Data.Instance.Restrictions = new ObservableCollection<Restriction>();
            var expressions = new List<(int, string, int, string, int, bool)>()
            {
                (1, "R(t1, s1, k1, c1, x1, d1) И x1 = 1 -> d1 != 1", 5, "По ПН нет 1 урока", 15, false),
                (2, "R(t1, s1, k1, c1, x1, d1) И t1 = \"Иванов В.В.\" -> d1 != 5", 10, "Иванов В.В. в пятницу - малая нагрузка", 20, false),
                (3, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И t1 = t2 И x1 + 2 = x2 И d1 = d2 -> R(t3, s3, k3, c3, x3, d3) И t2 = t3 И x1 + 1 = x3 И d2 = d3 И d1 = d3", 7, "окон нет", 30, false),
                (4, "R(t1, s1, k1, c1, x1, d1) И t1 = \"Петрова А.А.\" -> d1 != 1", 15, "Петрова А.А. в понедельник - выходной", 50, true),
                (5, "R(t1, s1, k1, c1, x1, d1) -> d1 != 6", 0, "отдыхаем в субботу", 30, false),
                (6, "R(t1, s1, k1, c1, x1, d1) И x1 in [1, 2] -> c1 in [\"6А\"]", 30, "среди первых двух занятий должен быть 6а", 70, false),
                (7, "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И t1 = t2 И x1 + 3 = x2 И d1 = d2 -> R(t3, s3, k3, c3, x3, d3) И R(t4, s4, k4, c4, x4, d4) И t2 = t3 И x1 + 1 = x3 И d2 = d3 И t2 = t4 И x1 + 2 = x4 И d2 = d4", 5, "окон 2 нет", 15, false),
            };

            foreach (var expression in expressions)
            {
                var (errors, expr) = Analyzer.Analyze(expression.Item2);
                if (errors.Count == 0)
                {
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
        }
    }
}
