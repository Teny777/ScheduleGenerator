using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Generator.Core.Restriction;
using Generator.Model;
using Generator.Singleton;
using Generator.Tools;
using Generator.Utils;

namespace Generator.View.Editors
{
    public partial class RestrictionBuilderAddEditWindow : Window, INotifyPropertyChanged
    {
        private readonly RestrictionBuilderModel _restrictionBuilderModel;
        public RestrictionBuilderAddEditWindow(RestrictionBuilderModel restrictionBuilderModel = null)
        {
            InitializeComponent();
            _restrictionBuilderModel = restrictionBuilderModel;

            SubmitRestrictionBuilderCommand = new RelayCommand(SubmitRestrictionBuilder, () =>
                SelectedSubject != null &&
                SelectedClass != null &&
                DaysOfWeek.Any());

            if (restrictionBuilderModel is null)
            {
                Title = "Добавление ограничения";
                CommandText = "Добавить";
            }
            else
            {
                Title = "Изменение ограничения";
                CommandText = "Применить";
                
                SelectedSubject = _restrictionBuilderModel.Subject;
                SelectedClass = _restrictionBuilderModel.Class;
                DaysOfWeek = _restrictionBuilderModel.DaysOfWeek;
                Count = _restrictionBuilderModel.Count;
                WeightPositive = _restrictionBuilderModel.WeightPositive;
                WeightNegative = _restrictionBuilderModel.WeightNegative;
            }
            
            DataContext = this;
        }
        
        public Subject SelectedSubject { get; set; }
        public Class SelectedClass { get; set; }
        public int Count { get; set; } = 1;
        public int WeightPositive { get; set; }
        public int WeightNegative { get; set; }
        
        public ICommand SubmitRestrictionBuilderCommand { get; private set; }
        
        public ObservableCollection<Subject> Subjects => Data.Instance.Subjects;

        public ObservableCollection<Class> Classes => new ObservableCollection<Class>(
            Data.Instance.LessonEditors
                .Where(x => x.Lesson.Subject == SelectedSubject)
                .Select(x => x.Lesson.Class).Distinct());

        public ObservableCollection<CheckDayOfWeek> AllDaysOfWeek =>
            new ObservableCollection<CheckDayOfWeek>(Data.Instance.DaysOfWeek
                .Select(x => new CheckDayOfWeek(x, DaysOfWeek)));

        public ObservableCollection<DayOfWeekModel> DaysOfWeek { get; set; } =
            new ObservableCollection<DayOfWeekModel>();

        public event PropertyChangedEventHandler PropertyChanged;
        
        public string CommandText { get; private set; }

        public RestrictionBuilderModel GetRestrictionBuilderModel()
        {
            return new RestrictionBuilderModel
            {
                Subject = SelectedSubject,
                Class = SelectedClass,
                DaysOfWeek = DaysOfWeek,
                Count = Count,
                WeightPositive = WeightPositive,
                WeightNegative = WeightNegative
            };
        }

        private void SubmitRestrictionBuilder()
        {
            if (_restrictionBuilderModel != null)
            {
                _restrictionBuilderModel.Subject = SelectedSubject;
                _restrictionBuilderModel.Class = SelectedClass;
                _restrictionBuilderModel.DaysOfWeek = DaysOfWeek;
                _restrictionBuilderModel.Count = Count;
                _restrictionBuilderModel.WeightPositive = WeightPositive;
                _restrictionBuilderModel.WeightNegative = WeightNegative;
            }
            DialogResult = true;
            Close();
        }
    }
    
    public class CheckDayOfWeek : INotifyPropertyChanged
    {
        private readonly ObservableCollection<DayOfWeekModel> _daysOfWeek;

        public CheckDayOfWeek(DayOfWeekModel dayOfWeekModel, ObservableCollection<DayOfWeekModel> daysOfWeek)
        {
            DayOfWeekModel = dayOfWeekModel;
            _daysOfWeek = daysOfWeek;
        }

        public bool IsChecked
        {
            get => _daysOfWeek.Contains(DayOfWeekModel);
            set
            {
                if (value)
                {
                    _daysOfWeek.Add(DayOfWeekModel);
                }
                else
                {
                    _daysOfWeek.Remove(DayOfWeekModel);
                }
            }
        }
            
        public DayOfWeekModel DayOfWeekModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}