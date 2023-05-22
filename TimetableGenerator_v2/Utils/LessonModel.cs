using System.Collections.Generic;
using Generator.Model;

namespace Generator.Utils
{
    public class LessonModel
    {
        public Class Class { get; set; }
        public Teacher Teacher { get; set; }

        public LessonModel(Class cClass, Teacher teacher)
        {
            Class = cClass;
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
            return Equals(x.Class, y.Class) && Equals(x.Teacher, y.Teacher);
        }

        public int GetHashCode(LessonModel obj)
        {
            unchecked
            {
                return ((obj.Class != null ? obj.Class.GetHashCode() : 0) * 397) ^ (obj.Teacher != null ? obj.Teacher.GetHashCode() : 0);
            }
        }
    }
}