using Generator.Core.Restriction;
using Generator.Singleton;
using Generator.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Generator.View.Editors
{
    /// <summary>
    /// Логика взаимодействия для RestrictionsWindow.xaml
    /// </summary>
    public partial class RestrictionsWindow : Window
    {
        public RestrictionsWindow()
        {
            InitializeComponent();

            AddRestrictionCommand = new RelayCommand(AddRestriction);
            AddRestrictionConstructorCommand = new RelayCommand(AddRestrictionConstructor);
            EditRestrictionCommand = new RelayCommand(EditRestriction, () => SelectedRestriction != null);
            RemoveRestrictionCommand = new RelayCommand(() => Restrictions.Remove(SelectedRestriction), () => SelectedRestriction != null);


            DataContext = this;
        }

        public ObservableCollection<Restriction> Restrictions => Data.Instance.Restrictions;
        public Restriction SelectedRestriction { get; set; }

        public ICommand AddRestrictionCommand { get; private set; }
        public ICommand AddRestrictionConstructorCommand { get; private set; }
        public ICommand EditRestrictionCommand { get; private set; }
        public ICommand RemoveRestrictionCommand { get; private set; }

        public int GenerationCount
        {
            get => Data.Instance.GenerationCount;
            set => Data.Instance.GenerationCount = value;
        }

        public int IndividualCount
        {
            get => Data.Instance.IndividualCount;
            set => Data.Instance.IndividualCount = value;
        }

        public bool IsMaxTeacherHoursChecked
        {
            get => Data.Instance.IsMaxTeacherHoursChecked;
            set => Data.Instance.IsMaxTeacherHoursChecked = value;
        }

        public bool IsAlternateSubjectsChecked
        {
            get => Data.Instance.IsAlternateSubjectsChecked;
            set => Data.Instance.IsAlternateSubjectsChecked = value;
        }

        private void AddRestriction()
        {
            var adder = new RestrictionAddEditWindow(EditorType.Add);
            adder.ShowDialog();
            if (adder.DialogResult == true)
            {
                var newRestrict = (Restriction)adder.Tag;
                int number = 0;
                for (int i = 1; i <= Restrictions.Count + 1; i++)
                {
                    if (!Restrictions.Any(x => x.Number == i))
                    {
                        number = i;
                        break;
                    }
                }
                newRestrict.Number = number;
                Restrictions.Add(newRestrict);
            }
        }

        private void AddRestrictionConstructor()
        {
            //var adder = new RestrictionConstructor();
            //adder.ShowDialog();
            //if (adder.DialogResult == true)
            //{
            //    var newRestrict = (Restriction)adder.Tag;
            //    int number = 0;
            //    for (int i = 1; i <= Restrictions.Count + 1; i++)
            //    {
            //        if (!Restrictions.Where(x => x.Number == i).Any())
            //        {
            //            number = i;
            //            break;
            //        }
            //    }
            //    newRestrict.Number = number;
            //    Restrictions.Add(newRestrict);
            //}
        }

        private void EditRestriction()
        {
            var editor = new RestrictionAddEditWindow(EditorType.Edit, SelectedRestriction);
            editor.ShowDialog();
            if (editor.DialogResult == true)
            {
                var newRestrict = (Restriction)editor.Tag;
                int index = Restrictions.IndexOf(SelectedRestriction);
                Restrictions[index] = newRestrict;
            }
        }
    }
}
