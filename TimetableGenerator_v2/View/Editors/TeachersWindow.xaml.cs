﻿using Generator.Model;
using Generator.Singleton;
using Generator.Tools;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Generator.View.Editors
{
    /// <summary>
    /// Логика взаимодействия для TeachersWindow.xaml
    /// </summary>
    public partial class TeachersWindow : Window, INotifyPropertyChanged
    {
        public TeachersWindow()
        {
            InitializeComponent();
            DataContext = this;

            Teachers = new ObservableCollection<Teacher>(Data.Instance.Teachers.Values);

            AddTeacherCommand = new RelayCommand(Add);
            RemoveTeacherCommand = new RelayCommand(Remove, () => SelectedTeacher != null);
        }

        public ObservableCollection<Teacher> Teachers { get; private set; }
        public ObservableCollection<Classroom> Classrooms => new ObservableCollection<Classroom>(Data.Instance.Classrooms);

        public Teacher SelectedTeacher { get; set; }

        public ICommand AddTeacherCommand { get; private set; }
        public ICommand RemoveTeacherCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Add()
        {
            var teacher = new Teacher
            {
                Id = Data.Instance.Teachers.Count != 0 ? Data.Instance.Teachers.Last().Key + 1 : 1,
                Name = "Новый педагог"
            };

            Data.Instance.Teachers.Add(teacher.Id, teacher);
            Teachers.Add(teacher);
        }

        private void Remove()
        {
            if (Data.Instance.LessonEditors.Any(x => x.Lesson.Teacher == SelectedTeacher))
            {
                System.Windows.MessageBox.Show($"Педагог {SelectedTeacher.Name} Задействован в учебном плане. Удаление невозможно.");
                return;
            }
            Data.Instance.Teachers.Remove(SelectedTeacher.Id);
            Teachers.Remove(SelectedTeacher);
        }

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {

            ComboBox comboBox = (ComboBox)sender;
            if (SelectedTeacher is null || comboBox.SelectedItem is null) return;
            SelectedTeacher.PriorityClassroom = (Classroom)comboBox.SelectedItem;

        }
    }
}
