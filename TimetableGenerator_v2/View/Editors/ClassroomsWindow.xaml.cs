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
    /// Логика взаимодействия для ClassroomsWindow.xaml
    /// </summary>
    public partial class ClassroomsWindow : Window, INotifyPropertyChanged
    {
        public ClassroomsWindow()
        {
            InitializeComponent();
            DataContext = this;

            AddCommand = new RelayCommand(Add);
            RemoveCommand = new RelayCommand(Remove, () => SelectedClassroom != null);
        }

        public ObservableCollection<Classroom> Classrooms => Data.Instance.Classrooms;

        public Classroom SelectedClassroom { get; set; }

        public bool IsTextEnabled => SelectedClassroom != null;

        public string Text
        {
            get => SelectedClassroom?.Name;
            set
            {
                SelectedClassroom.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;


        private void Add()
        {
            var classroom = new Classroom("Имя кабинета");
            Classrooms.Add(classroom);
            SelectedClassroom = classroom;
        }

        private void Remove()
        {
            if (Data.Instance.Teachers.Any(x => x.Value.Classrooms.Any(classroom => classroom == SelectedClassroom)))
            {
                System.Windows.MessageBox.Show($"Кабинет {SelectedClassroom.Name} Выбран для педагога. Удаление невозможно.");
                return;
            }
            Classrooms.Remove(SelectedClassroom);
        }
    }
}
