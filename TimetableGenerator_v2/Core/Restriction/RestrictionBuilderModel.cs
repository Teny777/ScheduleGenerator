using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Generator.Model;
using Generator.Utils;

namespace Generator.Core.Restriction
{
    [Serializable]
    public class RestrictionBuilderModel: INotifyPropertyChanged
    {
        public Subject Subject { get; set; }
        public Class Class { get; set; }
        public ObservableCollection<DayOfWeekModel> DaysOfWeek { get; set; }
        public int Count { get; set; }
        public int WeightPositive { get; set; }
        public int WeightNegative { get; set; }
        public string DaysOfWeekText => string.Join(", ", DaysOfWeek.OrderBy(x => x.Index).Select(x => x.ShortName)); 
            
        public void RefreshDaysOfWeekText()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DaysOfWeekText)));
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}