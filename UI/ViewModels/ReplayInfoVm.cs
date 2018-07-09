using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DataModule;
using System.Windows.Input;
using LogicModule;
using UI.Models;
using UI.Views;

namespace UI.ViewModels
{
    public class ReplayInfoVm: INotifyPropertyChanged
    {
        private ObservableCollection<ReplayData> _replays;
        //public readonly ComboBoxItem _currentItem;
        private ObservableCollection<string> _comboitems;
        private readonly ReplayInfoModel _model;
        private ReplayData _selecteditem;
        private Visibility _gridvisible;


        public ICommand ScanReplays { get; set; }
        public ICommand FileBrowser { get; set; }
        public ICommand OnOtherClick { get; set; }

        public ReplayInfoVm()
        {
            ScanReplays = new BaseCommand(OnScanReplays);
            FileBrowser = new BaseCommand(OnFileDialog);
            OnOtherClick = new BaseCommand(ReplayInformation);
            GridVisible = Visibility.Visible;
            _model = new ReplayInfoModel();
            _model.PropertyChanged += OnModelChanged;
            Pathes = _model.Pathes;
            Scomboitem = Pathes[0];
        }

        private void ReplayInformation(object obj)
        {
            if (SelectedItem != null)
            {
                ReplayInfoWindow replayinfo;
                replayinfo = new ReplayInfoWindow(SelectedItem);
                replayinfo.Show();
            }
        }

        public Visibility GridVisible
        {
            get => _gridvisible;
            set { _gridvisible = value; NotifyPropertyChanged("GridVisible"); }
        }
        public ReplayData SelectedItem
        {
            get => _selecteditem;
            set
            {
                _selecteditem = value;
                NotifyPropertyChanged("SelectedItem");
            }
        }

        private void OnModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Replays")
            {
                Replays = _model.Replays;
                GridVisible = Visibility.Visible;
            }
        }
        private void OnScanReplays(object obj)
        {
            GridVisible = Visibility.Hidden;
            Task.Run(() => _model.ScanReplays(Scomboitem));
            //_model.ScanReplays(Scomboitem);

        }

        public ObservableCollection<string> Pathes
        {
            get => _comboitems;
            set => _comboitems = value;
        }

        public ObservableCollection<ReplayData> Replays
        {
            get => _replays;
            set { _replays = value; NotifyPropertyChanged("Replays"); }
        }

        private string _scomboitem;
        public string Scomboitem
        {
            get => _scomboitem;
            set
            {
                if (_scomboitem != value)
                {
                    _scomboitem = value;
                    NotifyPropertyChanged("Scomboitem");
                }
            }
        }


        private void OnFileDialog(object obj)
        {
            using (System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
                _model.Pathes.Insert(0, dialog.SelectedPath);
                Scomboitem = Pathes.First();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
