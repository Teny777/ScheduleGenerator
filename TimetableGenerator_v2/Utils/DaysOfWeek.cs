using System;
using System.ComponentModel;

namespace Generator.Utils
{
    // public enum DayOfWeek
    // {
    //     [Description("Понедельник")]
    //     Monday,
    //     [Description("Вторник")]
    //     Tuesday,
    //     [Description("Среда")]
    //     Wednesday,
    //     [Description("Четверг")]
    //     Thursday,
    //     [Description("Пятница")]
    //     Friday,
    //     [Description("Суббота")]
    //     Saturday,
    //     [Description("Воскресенье")]
    //     Sunday
    // }

    [Serializable]
    public class DayOfWeekModel
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public DayOfWeekModel(int index, string name, string shortName)
        {
            Index = index;
            Name = name;
            ShortName = shortName;
        }
    }
}