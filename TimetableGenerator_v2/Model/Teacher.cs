using Generator.Singleton;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace Generator.Model
{
    [Serializable]
    public class Teacher : INotifyPropertyChanged
    {
        public Teacher(int id, string name, ObservableCollection<Subject> subjects)
        {
            Name = name;
            Id = id;
            Subjects = subjects;
        }

        public Teacher()
        {
            Subjects = new ObservableCollection<Subject>();
        }

        public string Name { get; set; }

        public int Id { get; set; }

        public ObservableCollection<Subject> Subjects { get; set; }

        [XmlIgnore]
        public ObservableCollection<CheckSubject> AllSubjects => 
            new ObservableCollection<CheckSubject>(Data.Instance.Subjects.Select(s => new CheckSubject(s, this)));

        // todo: не используется.
        [XmlIgnore]
        public string SubjectsText => string.Join(", ", Subjects.Select(x => x.Name));

        public void RefreshSubjectsText()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SubjectsText)));
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }

    /// <summary>
    /// Вспомогательный класс для отображения галочек в справочник -> учителя.
    /// </summary>
    public class CheckSubject : INotifyPropertyChanged
    {
        private readonly Teacher _teacher;

        public CheckSubject(Subject subject, Teacher teacher)
        {
            Subject = subject;
            _teacher = teacher;
        }

        [XmlIgnore]
        public bool IsChecked
        {
            get => _teacher.Subjects.Contains(Subject);
            set
            {
                if (value)
                {
                    _teacher.Subjects.Add(Subject);
                }
                else
                {
                    _teacher.Subjects.Remove(Subject);
                }
                _teacher.RefreshSubjectsText();
            }
        }

        [XmlIgnore]
        public Subject Subject { get; set; }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
