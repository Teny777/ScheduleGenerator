using System;
using System.ComponentModel;

namespace Generator.Model
{


    [Serializable]
    public class Subgroup : INotifyPropertyChanged
    {
        public Subgroup(int group)
        {
            Group = (Subgroups)group;

            if (Group == Subgroups.First) Name = "1 подгруппа";
            else if (Group == Subgroups.Second) Name = "2 подгруппа";
            else Name = "Вся группа";
        }

        public Subgroup()
        {
            Group = Subgroups.All;
            Name = "Вся группа";
        }

        public Subgroups Group { get; set; }
        public string Name { get; set; }

        public static bool CheckDifferentSubgroups(Subgroup a, Subgroup b)
        {
            return a.Group == Subgroups.First && b.Group == Subgroups.Second || a.Group == Subgroups.Second && b.Group == Subgroups.First;
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public enum Subgroups
    {
        First,
        Second,
        All
    }
}
