using System;
using System.ComponentModel;

namespace Generator.Model
{
    [Serializable]
    public enum Shift
    {
        [Description("Первая смена")]
        First,
        [Description("Вторая смена")]
        Second
    }
}