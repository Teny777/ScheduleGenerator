using System;
using System.ComponentModel;

namespace Generator.Model
{
    [Serializable]
    public class Subject : INotifyPropertyChanged
    {
        public Subject(string name, int id)
        {
            Id = id;
            Name = name; 
        }

        public Subject()
        {
            Id = 0;
        }

        public string Name { get; set; }
        public bool IsTechnical { get; set; }
        public int Id { get; set; }



        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
