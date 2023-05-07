using System;
using System.ComponentModel;

namespace Generator.Model
{
    [Serializable]
    public class Classroom : INotifyPropertyChanged
    {
        public Classroom(string name)
        {
            Name = name;
        }

        public string Name { get; set; }


        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
