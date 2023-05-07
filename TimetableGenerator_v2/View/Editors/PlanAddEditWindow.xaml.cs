using Generator.Model;
using Generator.Singleton;
using Generator.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Generator.View.Editors
{
    /// <summary>
    /// Логика взаимодействия для PlanAddEditWindow.xaml
    /// </summary>
    public partial class PlanAddEditWindow : Window, INotifyPropertyChanged
    {
        private readonly LessonEditor _lessonEditor;

        public PlanAddEditWindow(LessonEditor lessonEditor = null)
        {
            InitializeComponent();
            _lessonEditor = lessonEditor;

            DataContext = this;
            SubmitPlanCommand = new RelayCommand(SubmitPlan, () => SelectedTeacher != null && SelectedSubject != null && SelectedClass != null);

            if (_lessonEditor == null)
            {
                Title = "Добавление позиции учебного плана";
                CommandText = "Добавить";
            }
            else
            {
                Title = "Изменение позиции учебного плана";
                CommandText = "Применить";

                SelectedTeacher = _lessonEditor.Lesson.Teacher;
                SelectedSubject = _lessonEditor.Lesson.Subject;
                SelectedClass = _lessonEditor.Lesson.Class;
                Count = _lessonEditor.Count;
            }
        }

        public ObservableCollection<Teacher> Teachers => new ObservableCollection<Teacher>(Data.Instance.Teachers.Values);
        public ObservableCollection<Subject> Subjects => SelectedTeacher?.Subjects;
        public ObservableCollection<Class> Classes => new ObservableCollection<Class>(Data.Instance.Classes.Values);


        public Teacher SelectedTeacher { get; set; }
        public Subject SelectedSubject { get; set; }
        public Class SelectedClass { get; set; }

        public int Count { get; set; } = 1;

        public ICommand SubmitPlanCommand { get; private set; }
        public string CommandText { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LessonEditor GetLessonEditor()
        {
            if (_lessonEditor != null)
            {
                throw new Exception("нельзя использовать при изменении.");
            }
            
            return new LessonEditor(new Lesson(SelectedTeacher, SelectedClass, SelectedSubject), Count);
        }

        private void SubmitPlan()
        {
            if (_lessonEditor != null)
            {
                _lessonEditor.Lesson.Teacher = SelectedTeacher;
                _lessonEditor.Lesson.Subject = SelectedSubject;
                _lessonEditor.Lesson.Class = SelectedClass;
                _lessonEditor.Count = Count;
            }

            DialogResult = true;
            Close();
        }
    }
}
