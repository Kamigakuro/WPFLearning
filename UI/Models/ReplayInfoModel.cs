using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private ObservableCollection<string> _pathes;
        private ObservableCollection<ReplayData> _replays;

        public ObservableCollection<string> Pathes { get => _pathes;
            set { _pathes = value; OnPropertyChanged("Pathes"); }
        }

        public ObservableCollection<ReplayData> Replays
        {
            get => _replays;
            set { 
                _replays = value;
                OnPropertyChanged("Replays");
            }
        }

        public ReplayInfoModel()
        {
            Pathes = new ObservableCollection<string>();
            Pathes.Add("Test");
            Pathes.Add("Test1");
        }
        public void ScanReplays(string path)
        {
            LAction scan = new LAction();
            FileInfo[] files = scan.ScanFolder(path, false);
            if (files.Length > 0)
            {              
                ConvertToCollection(scan.ScanReplays(files));
            }
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
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
