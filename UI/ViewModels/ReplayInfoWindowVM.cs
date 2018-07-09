using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DataModule;
using LogicModule.Models;

namespace UI.ViewModels
{
    public class ReplayInfoWindowVM: INotifyPropertyChanged
    {
        private ReplayData _replay;
        private Visibility _progress;
        public ICommand CloseWindow { get; set; }

        public Visibility Progress
        {
            get => _progress;
            set { _progress = value; OnPropertyChanged("Progress"); }
        }
        public ReplayData ReplayInfo
        {
            get => _replay;
            set { _replay = value; OnPropertyChanged("ReplayInfo"); }
        }

        public ReplayInfoWindowVM(ReplayData replay)
        {
            ReplayInfo = replay;
            DataCollector collector = new DataCollector();
            foreach (var player in ReplayInfo.Players) collector.PlayerStatistic(player, ReplayReader.ReadReplay(ReplayInfo.FullPath));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
