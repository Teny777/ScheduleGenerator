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
    /// Логика взаимодействия для SubjectsWindow.xaml
    /// </summary>
    public partial class ClassesWindow : Window, INotifyPropertyChanged
    {
        public ClassesWindow()
        {
            InitializeComponent();
            DataContext = this;

            AddCommand = new RelayCommand(Add);
            RemoveCommand = new RelayCommand(Remove, () => SelectedClass != null);

            Classes = new ObservableCollection<Class>(Data.Instance.Classes.Values);
        }
            
        public ObservableCollection<Class> Classes { get; private set; } 

        public Class SelectedClass { get; set; }

        public bool IsTextEnabled => SelectedClass != null;

        public string Text
        {
            get => SelectedClass?.Name;
            set
            {
                SelectedClass.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Add()
        {
            var class_ = new Class
            {
                Name = "00А",
                Id = Data.Instance.Classes.Count != 0 ? Data.Instance.Classes.Last().Key + 1 : 1
            };

            Data.Instance.Classes.Add(class_.Id, class_);
            Classes.Add(class_);

            SelectedClass = class_;
        }

        private void Remove()
        {
            if (Data.Instance.LessonEditors.Any(x => x.Lesson.Class == SelectedClass))
            {
                System.Windows.MessageBox.Show($"Класс {SelectedClass.Name} Задействован в учебном плане. Удаление невозможно.");
                return;
            }

            Data.Instance.Classes.Remove(SelectedClass.Id);
            Classes.Remove(SelectedClass);
        }
    }
}
