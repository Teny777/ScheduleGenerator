using Generator.Core.Restriction;
using Generator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using Generator.Utils;

namespace Generator.Singleton
{
    [Serializable]
    public class Data
    {
        private static Data _instance;

        public static Data Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Data();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        private Data() { }

        public ObservableCollection<Restriction> Restrictions { get; set; } = new ObservableCollection<Restriction>();
        public ObservableCollection<Subject> Subjects { get; set; } = new ObservableCollection<Subject>();
        public ObservableCollection<LessonEditor> LessonEditors { get; set; } = new ObservableCollection<LessonEditor>();
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
        public Dictionary<int, Class> Classes { get; set; } = new Dictionary<int, Class>();
        public Dictionary<int, Teacher> Teachers { get; set; } = new Dictionary<int, Teacher>();
        public ObservableCollection<Classroom> Classrooms { get; set; } = new ObservableCollection<Classroom>();
        public ObservableCollection<Shift> Shifts { get; set; } = new ObservableCollection<Shift>
        {
            Shift.First,
            Shift.Second
        };
        public ObservableCollection<DayOfWeekModel> DaysOfWeek { get; set; } = new ObservableCollection<DayOfWeekModel>
        {
            new DayOfWeekModel(0, "Понедельник", "Пн"),
            new DayOfWeekModel(1, "Вторник", "Вт"),
            new DayOfWeekModel(2, "Среда", "Ср"),
            new DayOfWeekModel(3, "Четверг", "Чт"),
            new DayOfWeekModel(4, "Пятница", "Пт"),
            new DayOfWeekModel(5, "Суббота", "Сб"),
            new DayOfWeekModel(6, "Воскресенье", "Вс"),
        };
        public ObservableCollection<RestrictionBuilderModel> RestrictionBuilderModels { get; set; } = 
            new ObservableCollection<RestrictionBuilderModel>();

        public int N => Lessons.Count;

        [field: NonSerialized]
        //[XmlIgnore]
        public bool[,] Mas { get; set; }
        /// <summary>
        /// Просто подряд идущие числа, необходимы для преоброзований, чтобы каждый раз не считать
        /// </summary>
        [field: NonSerialized]
        public int[] Numbers { get; private set; }

        public int GenerationCount { get; set; } = 300;
        public int IndividualCount { get; set; } = 700;


        public bool StudentsWindows { get; set; } = true;
        public bool LessonRotation { get; set; } = true;
        public bool TeacherWindows { get; set; } = true;
        public bool IsMaxTeacherHoursChecked { get; set; } = true;
        public bool IsAlternateSubjectsChecked { get; set; }

        public void InitializeSupportData()
        {
            for (int i = 0; i < N; i++)
            {
                Lessons[i].Id = i;
            }

            Numbers = Enumerable.Range(0, N).ToArray();
            CreateAdjacencyMatrix();
        }

        private void CreateAdjacencyMatrix()
        {
            Mas = new bool[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    if (Lessons[i].Teacher == Lessons[j].Teacher || Lessons[i].Class == Lessons[j].Class || Lessons[i].Teacher.PriorityClassroom == Lessons[j].Teacher.PriorityClassroom)
                    {
                        Mas[i, j] = Mas[j, i] = true;
                    }
                }
            }
        }

    }
}
