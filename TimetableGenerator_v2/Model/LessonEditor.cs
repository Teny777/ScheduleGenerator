using System;
using System.ComponentModel;

namespace Generator.Model
{
    [Serializable]
    public class LessonEditor : INotifyPropertyChanged // todo: назвать нормально
    {
        public LessonEditor(Lesson les, int cnt)
        {
            Count = cnt;
            Lesson = les;
        }

        public int Count { get; set; }
        public Lesson Lesson { get; set; }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
