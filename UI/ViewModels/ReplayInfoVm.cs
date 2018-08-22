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
using MaterialDesignThemes.Wpf;
using UI.Models;
using UI.Views;
using Settings = UI.Models.Settings;

namespace UI.ViewModels
{
    public class ReplayInfoVm: INotifyPropertyChanged
    {
        private ObservableCollection<ReplayData> _replays;
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
            if (_model == null) _model = new ReplayInfoModel();
            _model.PropertyChanged += OnModelChanged;
            Pathes = _model.Pathes;
            if (Pathes.Count > 0)
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

        public bool ScanStatus
        {
            get => _model.isScaning;
        }

        public string BrokenColor
        {
            get => Settings.BadColor;
        }
        public string LowColor
        {
            get => Settings.LowSizeColor;
        }

        public bool MarkLow
        {
            get => Settings.CheckLowSizeReplays;
        }
        public bool MarkBroken
        {
            get => Settings.CheckBadReplays;
        }

        public int ScanPercent
        {
            get => _model.ScanPercent;
        }
        public Visibility GridVisible
        {
            get => _gridvisible;
            set { _gridvisible = value;  NotifyPropertyChanged("GridVisible"); NotifyPropertyChanged("ScanStatus"); }
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
                NotifyPropertyChanged("ScanStatus");
                GridVisible = Visibility.Visible;
            }
            if (e.PropertyName == "ScanCount")
            {
                Settings.panel.Text = "Total: " + _model.ScanCount + "/" + _model.RelayCount + "   Broken: " + _model.BrokenRep;
                NotifyPropertyChanged("ScanPercent");
                NotifyPropertyChanged("ScanStatus");
            }
        }
        private async void OnScanReplays(object obj)
        {
            if (_model.isScaning)
            {
                _model.StopScan();
                return;
            }
            GridVisible = Visibility.Hidden;
            Settings.panel.Icon = "Cached";
            Settings.panel.Color = "#E64A19";
            await Task.Run(() => _model.ScanReplays(Scomboitem));
            Settings.panel.Text += "    Scan Time: " + _model.ScanTime.ElapsedMilliseconds;
            Settings.panel.Color = "#4CAF50";
            Settings.panel.Icon = "CheckAll";
            //_model.ScanReplays(Scomboitem);

        }

        public ObservableCollection<string> Pathes
        {
            get => _model.Pathes;
            set {  _model.Pathes = value; NotifyPropertyChanged("Pathes"); }
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
                    Pathes.Move(Pathes.IndexOf(value), 0);
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
