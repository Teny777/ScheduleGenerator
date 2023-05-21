using Generator.Model;
using Generator.Tools;
using Generator.Singleton;
using Generator.View.Editors;
using Generator.Core.Restriction;
using Generator.Core;
using Generator.Utils;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Generator.View
{
    // todo: GLOBAL Удаление сущности, которая используется где-то. (предупреждать / каскадно)
    // todo: GLOBAL Добавление сущностей с одинаковым именем. (не допускать)

    /// <summary>
    /// Main window.
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DataTable _shedule;
        private DataTable _teacherShedule;

        private const string TimetableIsCreating = "расписание создаётся...";
        private const string TimetableIsCreated = "расписание создано.";

        public MainWindow()
        {
            InitializeComponent();
            AutoScrollLogInitialize();

            // добавление изменение удаление составляющих учебного плана
            AddLessonEditorCommand = new RelayCommand(AddLessonEditor, () => GenerationStatus != TimetableIsCreating);
            EditLessonEditorCommand = new RelayCommand(EditLessonEditor, () => SelectedLessonEditor != null && GenerationStatus != TimetableIsCreating);
            RemoveLessonEditorCommand = new RelayCommand(() => Data.Instance.LessonEditors.Remove(SelectedLessonEditor), () => SelectedLessonEditor != null && GenerationStatus != TimetableIsCreating);

            // справочник - классы, предметы, учителя
            SubjectsMenuCommand = new RelayCommand(() => new SubjectsWindow { Owner = this }.ShowDialog(), () => GenerationStatus != TimetableIsCreating);
            ClassesMenuCommand = new RelayCommand(() => new ClassesWindow { Owner = this }.ShowDialog(), () => GenerationStatus != TimetableIsCreating);
            TeachersMenuCommand = new RelayCommand(() => new TeachersWindow { Owner = this }.ShowDialog(), () => GenerationStatus != TimetableIsCreating);
            ClassroomsMenuCommand = new RelayCommand(() => new ClassroomsWindow { Owner = this }.ShowDialog(), () => GenerationStatus != TimetableIsCreating);

            // работа с файлами
            FileMenuOpenCommand = new RelayCommand(() => 
            { 
                FileWorker.OpenGeneratorFile();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LessonEditors)));
            }, () => GenerationStatus != TimetableIsCreating);
            FileMenuSaveCommand = new RelayCommand(FileWorker.SaveGeneratorFile);
            FileMenuSaveAsCommand = new RelayCommand(FileWorker.SaveGeneratorFileAs);
            FileMenuExportToExcelCommand = new RelayCommand(() => _shedule.ExportToExcel(), () => _shedule != null && GenerationStatus != TimetableIsCreating);

            SettingsGenerationCommand = new RelayCommand(() => new RestrictionsWindow { Owner = this }.ShowDialog(), () => GenerationStatus != TimetableIsCreating);
            CreateTimetableCommand = new RelayCommand(async () => await CreateTimetableAsync(), () => Data.Instance.LessonEditors != null && Data.Instance.LessonEditors.Any() && GenerationStatus != TimetableIsCreating);
            ShowTeachersTimetableCommand = new RelayCommand(() => new SheduleWindow(_teacherShedule) { Owner = this }.Show(), () => _teacherShedule != null && GenerationStatus != TimetableIsCreating);
            ShowClassesTimetableCommand = new RelayCommand(() => new SheduleWindow(_shedule) { Owner = this }.Show(), () => _shedule != null && GenerationStatus != TimetableIsCreating);

            ChangeLogVisibilityCommand = new RelayCommand(ChangeLogVisibility);

            Garbage.FillStartData();

            DataContext = this;
        }

        private void AutoScrollLogInitialize()
        {
            var timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 2)
            };
            timer.Tick += ((sender, e) =>
            {
                _contentCtrl.Height += 10;
                if (_scrollViewer.VerticalOffset == _scrollViewer.ScrollableHeight)
                {
                    _scrollViewer.ScrollToEnd();
                }
            });
            timer.Start();
        }

        public ObservableCollection<LessonEditor> LessonEditors => Data.Instance.LessonEditors;
        public LessonEditor SelectedLessonEditor { get; set; }

        public ICommand AddLessonEditorCommand { get; private set; }
        public ICommand EditLessonEditorCommand { get; private set; }
        public ICommand RemoveLessonEditorCommand { get; private set; }

        public ICommand SubjectsMenuCommand { get; private set; }
        public ICommand ClassesMenuCommand { get; private set; }
        public ICommand TeachersMenuCommand { get; private set; }
        public ICommand ClassroomsMenuCommand { get; private set; }

        public ICommand FileMenuOpenCommand { get; private set; }
        public ICommand FileMenuSaveCommand { get; private set; }
        public ICommand FileMenuSaveAsCommand { get; private set; }
        public ICommand FileMenuExportToExcelCommand { get; private set; }

        public ICommand SettingsGenerationCommand { get; private set; }
        public ICommand CreateTimetableCommand { get; private set; }
        public ICommand ShowClassesTimetableCommand { get; private set; }
        public ICommand ShowTeachersTimetableCommand { get; private set; }

        public ICommand ChangeLogVisibilityCommand { get; private set; }

        public string Log { get; private set; }
        public int ProgressValue { get; private set; }
        public Visibility LogVisibility { get; private set; } = Visibility.Visible;

        public string GenerationStatus { get; private set; } = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        private void AddLessonEditor()
        {
            var planWindow = new PlanAddEditWindow { Owner = this };
            planWindow.ShowDialog();
            if (planWindow.DialogResult == true)
            {
                var newLessonEditor = planWindow.GetLessonEditor();
                Data.Instance.LessonEditors.Add(newLessonEditor);
            }
        }

        private void EditLessonEditor()
        {
            var planWindow = new PlanAddEditWindow(SelectedLessonEditor) { Owner = this };
            planWindow.ShowDialog();
        }

        private void ChangeLogVisibility()
        {
            if (LogVisibility == Visibility.Visible)
            {
                LogVisibility = Visibility.Collapsed;
            }
            else
            {
                LogVisibility = Visibility.Visible;
            }
        }

        private async Task CreateTimetableAsync()
        {
            //if (Data.Instance.Lessons == null) было так почему то.
            if (Data.Instance.LessonEditors == null)
            {
                MessageBox.Show("Данные не введены.");
                return;
            }

            Data.Instance.Lessons.Clear();
            foreach (LessonEditor le in Data.Instance.LessonEditors)
            {
                for (int i = 0; i < le.Count; i++)
                {
                    Data.Instance.Lessons.Add(new Lesson(le.Lesson.Teacher, le.Lesson.Class, le.Lesson.Subject, le.Lesson.Shift));
                }
            }
            await Task.Run(CalculationThread);
        }

        private void CalculationThread()
        {
            GenerationStatus = TimetableIsCreating;
            var gen = new Generation();
            Log = $"max rate    avg rate   dispersion{Environment.NewLine}";
            Log += $"{Environment.NewLine}{gen}{Environment.NewLine}";
            ProgressValue = 0;
            for (int _try = 0; _try < Data.Instance.GenerationCount; _try++)
            {
                gen.Next();
                Log += $"{gen}{Environment.NewLine}";
                ProgressValue = _try * 100 / Data.Instance.GenerationCount;
            }
            var rows = gen.GetMax().TableToRows();
            _shedule = gen.BestSolution();
            //_teacherShedule = gen.TeacherTable();
            GenerationStatus = TimetableIsCreated;
            ApplyRestrictions(rows);
            ProgressValue = 100;
        }

        private void ApplyRestrictions(List<Row> rows)
        {
            var restrictions = Data.Instance.Restrictions;
            var totalPositive = 0;
            var totalNegative = 0;
            var total = 0;
            var builder = new StringBuilder();

            bool isOk = true;

            foreach (var restriction in restrictions)
            {
                var result = Compilier.RunMethod(rows, restriction.Method).Split().Select(x => int.Parse(x)).ToList();
                builder.AppendLine($"{restrictions.IndexOf(restriction) + 1}) +{result[0]} -{result[1]} = {result[2]}");
                totalPositive += result[0];
                totalNegative += result[1];
                total += result[2];
                restriction.CountOk = result[0];
                restriction.CountFail = result[1];

                if (restriction.IsRequirement && restriction.CountFail > 0)
                {
                    isOk = false;
                }
            }

            if (isOk)
            {
                MessageBox.Show("Расписание успешно создано. Подробнее в параметрах.");
            }
            else
            {
                MessageBox.Show("Не удалось создать расписание, удовлетворяющее всем требованиям. Подробнее в параметрах.");
            }
        }
    }
}
