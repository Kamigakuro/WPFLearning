using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using DataModule;
using LogicModule;

namespace UI.Models
{
    public class ReplayInfoModel: INotifyPropertyChanged
    {
        //private List<string> _pathes;
        private ObservableCollection<ReplayData> _replays;
        public Stopwatch ScanTime;
        public int RelayCount;
        private int _scanCount;
        private int _broken;
        private LAction scan;
        private int _scanpercent;
        public bool isScaning;
        public ObservableCollection<string> Pathes { get => Settings.Pathes;
            set { Settings.Pathes = value; OnPropertyChanged("Pathes"); }
        }

        public ObservableCollection<ReplayData> Replays
        {
            get => _replays;
            set { 
                _replays = value;
                OnPropertyChanged("Replays");
            }
        }
        public int ScanCount
        {
            get => _scanCount;
            set { _scanCount = value; OnPropertyChanged("ScanCount"); }
        }
        public int ScanPercent
        {
            get => _scanpercent;
            set { _scanpercent = value; OnPropertyChanged("ScanPercent"); }
        }
        public int BrokenRep
        {
            get => _broken;
            set { _broken = value; OnPropertyChanged("BrokenRep"); }
        }
        public ReplayInfoModel()
        {
            ScanPercent = 0;
            //Pathes = Settings.Pathes;
        }
        public void ScanReplays(string path)
        {
            ScanTime = Stopwatch.StartNew();
            scan = new LAction();
            ScanCount = 0;
            scan.Count += UpdateScanCount;
            if (Directory.Exists(path))
            {
                FileInfo[] files = scan.ScanFolder(path, Settings.ScanSubDir);
                if (files.Length > 0)
                {
                    isScaning = true;
                    RelayCount = files.Length;
                    ConvertToCollection(scan.ScanReplays(files, 10));
                }
            }
            scan.Count -= UpdateScanCount;
            ScanTime.Stop();
            isScaning = false;
        }

        public void ConvertToCollection(List<ReplayData> list)
        {
            ObservableCollection<ReplayData> tmp = new ObservableCollection<ReplayData>();
            foreach (var replay in list)
            {
                if (replay.DateTime == null) replay.DateTime = "NaN";
                if (replay.RegionCode == null) replay.RegionCode = "NaN";
                if (replay.ServerName == null) replay.ServerName = "NaN";
                if (replay.DateTime == null) replay.DateTime = "NaN";
                if (replay.DateTime == null) replay.DateTime = "NaN";
                if (replay.DateTime == null) replay.DateTime = "NaN";
                if (replay.DateTime == null) replay.DateTime = "NaN";
                if (replay.DateTime == null) replay.DateTime = "NaN";
                tmp.Add(replay);
            }

            Replays = tmp;
        }

        private void UpdateScanCount()
        {
            ScanCount++;
            BrokenRep = scan._broken;
            ScanPercent = (100 * ScanCount) / RelayCount;
        }

        public void StopScan()
        {
            isScaning = false;
            scan.ForceStopScan();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
