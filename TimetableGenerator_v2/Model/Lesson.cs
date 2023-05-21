using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Generator.Utils;

namespace Generator.Model
{
    [Serializable]
    public class Lesson : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString() => $"{Subject.Name}, {Teacher.Name}, Каб: {Classroom.Name}";

        [XmlIgnore]
        public string Info => $"{Class.Name}, {Subject.Name}, Каб: {Classroom.Name}";

        public int Color { get; set; }

        public Teacher Teacher { get; set; }

        public Class Class { get; set; }

        public Subject Subject { get; set; }

        public Classroom Classroom { get; set; }
        public int Id { get; set; }
        public Shift Shift { get; set; }
        public string ShiftDescription => Shift.GetDescription();

        public Lesson(Teacher teacher, Class tclass, Subject subject, Shift shift)
        {
            Teacher = teacher;
            Class = tclass;
            Subject = subject;
            Shift = shift;
        }

    }
}
