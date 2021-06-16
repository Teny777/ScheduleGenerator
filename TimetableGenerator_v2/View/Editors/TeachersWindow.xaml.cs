using Generator.Model;
using Generator.Singleton;
using Generator.Tools;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
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

        public Teacher SelectedTeacher { get; set; }

        public ICommand AddTeacherCommand { get; private set; }
        public ICommand RemoveTeacherCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Add()
        {
            var teacher = new Teacher
            {
                Id = Data.Instance.Teachers.Last().Key + 1,
                Name = "Новый учитель"
            };

            Data.Instance.Teachers.Add(teacher.Id, teacher);
            Teachers.Add(teacher);
        }

        private void Remove()
        {
            if (Data.Instance.LessonEditors.Any(x => x.Lesson.Teacher == SelectedTeacher))
            {
                MessageBox.Show($"Учитель {SelectedTeacher.Name} Задействован в учебном плане. Удаление невозможно.");
                return;
            }
            Data.Instance.Teachers.Remove(SelectedTeacher.Id);
            Teachers.Remove(SelectedTeacher);
        }
    }
}
