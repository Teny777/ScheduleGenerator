using System.Collections.Generic;
using Generator.Model;

namespace Generator.Utils
{
    public class LessonModel
    {
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }

        public LessonModel(Subject subject, Teacher teacher)
        {
            Subject = subject;
            Teacher = teacher;
        }
    }

    public class LessonModelEqualityComparer : IEqualityComparer<LessonModel>
    {
        public bool Equals(LessonModel x, LessonModel y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return Equals(x.Subject, y.Subject) && Equals(x.Teacher, y.Teacher);
        }

        public int GetHashCode(LessonModel obj)
        {
            unchecked
            {
                return ((obj.Subject != null ? obj.Subject.GetHashCode() : 0) * 397) ^ (obj.Teacher != null ? obj.Teacher.GetHashCode() : 0);
            }
        }
    }
}