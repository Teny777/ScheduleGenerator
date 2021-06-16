using System;
using System.ComponentModel;

namespace Generator.Model
{
    [Serializable]
    public class Class : INotifyPropertyChanged
    {
        public Class(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public Class()
        {
            Id = -1;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
