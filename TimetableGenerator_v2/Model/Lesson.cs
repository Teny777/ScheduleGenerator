using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Generator.Model
{
    [Serializable]
    public class Lesson : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString() => $"{Subject.Name}, {Teacher.Name}, ({Subgroup.Name})";

        [XmlIgnore]
        public string Info => $"{Class.Name} ({Subgroup.Name}), {Subject.Name}";

        public int Color { get; set; }

        public Teacher Teacher { get; set; }

        public Class Class { get; set; }

        public Subject Subject { get; set; }

        public Subgroup Subgroup { get; set; }

        public int Id { get; set; }

        public Lesson(Teacher teacher, Class tclass, Subject subject, Subgroup subgroup)
        {
            Teacher = teacher;
            Class = tclass;
            Subject = subject;
            Subgroup = subgroup;
        }

        public static bool CheckLessonFromOneClass(Lesson a, Lesson b)
        {
            return a.Class == a.Class && a.Subject == a.Subject && Subgroup.CheckDifferentSubgroups(a.Subgroup, b.Subgroup);
        }
    }
}
