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
    public partial class SubjectsWindow : Window, INotifyPropertyChanged
    {
        public SubjectsWindow()
        {
            InitializeComponent();
            DataContext = this;

            AddCommand = new RelayCommand(Add);
            RemoveCommand = new RelayCommand(Remove, () => SelectedSubject != null);
        }

        public ObservableCollection<Subject> Subjects => Data.Instance.Subjects;

        public Subject SelectedSubject { get; set; }

        public bool IsTextEnabled => SelectedSubject != null;

        public string Text
        {
            get => SelectedSubject?.Name;
            set
            {
                SelectedSubject.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Add()
        {
            var subject = new Subject
            {
                Name = "Имя предмета",
                Id = Data.Instance.Subjects.Count != 0 ? Data.Instance.Subjects.Last().Id + 1 : 1,
            };
            Subjects.Add(subject);
            SelectedSubject = subject;
        }

        private void Remove()
        {
            if (Data.Instance.Teachers.Any(x => x.Value.Subjects.Any(subject => subject == SelectedSubject)))
            {
                System.Windows.MessageBox.Show($"Предмет {SelectedSubject.Name} Выбран для педагога. Удаление невозможно.");
                return;
            }
            Subjects.Remove(SelectedSubject);
        }
    }
}
