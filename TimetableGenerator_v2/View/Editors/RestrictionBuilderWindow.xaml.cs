using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Generator.Core.Restriction;
using Generator.Model;
using Generator.Singleton;
using Generator.Tools;

namespace Generator.View.Editors
{
    public partial class RestrictionBuilderWindow : Window, INotifyPropertyChanged
    {
        public RestrictionBuilderWindow()
        {
            InitializeComponent();

            AddRestrictionBuilderCommand = new RelayCommand(AddRestrictionBuilder);
            EditRestrictionBuilderCommand = new RelayCommand(
                EditRestrictionBuilder, 
                () => SelectedRestrictionBuilderModel != null);
            RemoveRestrictionBuilderCommand = new RelayCommand(
                () => Data.Instance.RestrictionBuilderModels.Remove(SelectedRestrictionBuilderModel),
                () => SelectedRestrictionBuilderModel != null);
            
            DataContext = this;
        }


        public ObservableCollection<RestrictionBuilderModel> RestrictionBuilderModels => Data.Instance.RestrictionBuilderModels;
        public RestrictionBuilderModel SelectedRestrictionBuilderModel { get; set; }
        
        public ICommand AddRestrictionBuilderCommand { get; private set; }
        public ICommand EditRestrictionBuilderCommand { get; private set; }
        public ICommand RemoveRestrictionBuilderCommand { get; private set; }



        private void AddRestrictionBuilder()
        {
            var restrictionBuilderAddEditWindow = new RestrictionBuilderAddEditWindow { Owner = this };
            restrictionBuilderAddEditWindow.ShowDialog();
            if (restrictionBuilderAddEditWindow.DialogResult == true)
            {
                var restrictionBuilderModel = restrictionBuilderAddEditWindow.GetRestrictionBuilderModel();
                Data.Instance.RestrictionBuilderModels.Add(restrictionBuilderModel);
            }
        }

        private void EditRestrictionBuilder()
        {
            var restrictionBuilderAddEditWindow = new RestrictionBuilderAddEditWindow(SelectedRestrictionBuilderModel) { Owner = this };
            restrictionBuilderAddEditWindow.ShowDialog();
            SelectedRestrictionBuilderModel.RefreshDaysOfWeekText();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        
    }
}