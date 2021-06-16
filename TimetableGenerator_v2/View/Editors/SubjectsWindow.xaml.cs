using Generator.Model;
using Generator.Singleton;
using Generator.Tools;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            var subject = new Subject("Имя предмета");
            Subjects.Add(subject);
            SelectedSubject = subject;
        }

        private void Remove()
        {
            Subjects.Remove(SelectedSubject);
        }
    }
}
