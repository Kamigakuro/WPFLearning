using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Models;

namespace UI.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public ReplayInfoVm ReplayPage;
        private SettingsVM SettingWindow;
        private MainVM MainPage;
        public ICommand ScanPage { get; set; }
        public ICommand SettingsPage { get; set; }
        public ICommand NewsPage { get; set; }
        public ICommand ExitApp { get; set; }

        private object _selectedPage;

        private SnackManager _snack;
        public SnackManager Snack
        {
            get => Settings.snack;
        }
        public InfoPanelVM InfoPanel
        {
            get => Settings.panel;
        }

        public object SelectedPage
        {
            get => _selectedPage;
            set { _selectedPage = value; OnPropertyChanged("SelectedPage"); }
        }

        public MainWindowVM()
        {
            ScanPage = new BaseCommand(OpenReplayTab);
            SettingsPage = new BaseCommand(OpenSettingsPage);
            ExitApp = new BaseCommand(OnExitApp);
            NewsPage = new BaseCommand(OpenMainPage);
        }

        private void OnExitApp(object obj)
        {
            Application.Current.Shutdown();
        }
        private void OpenMainPage(object obj)
        {
            if (MainPage != null)
                SelectedPage = MainPage;
            else
                SelectedPage = MainPage = new MainVM();

        }
        private void OpenSettingsPage(object obj)
        {
            if (SettingWindow != null)
                SelectedPage = SettingWindow;
            else
                SelectedPage = SettingWindow = new SettingsVM();
        }
        private void OpenReplayTab(object obj)
        {
            if (ReplayPage != null)
                SelectedPage = ReplayPage;
            else
                SelectedPage = ReplayPage = new ReplayInfoVm();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
