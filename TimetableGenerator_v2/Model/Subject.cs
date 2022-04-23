using System;
using System.ComponentModel;

namespace Generator.Model
{
    [Serializable]
    public class Subject : INotifyPropertyChanged
    {
        public Subject(string name, bool tech = false)
        {
            Name = name;
            IsTechnical = tech;
        }

        public string Name { get; set; }
        public bool IsTechnical { get; set; }



        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
