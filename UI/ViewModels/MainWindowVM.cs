using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UI.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {

        public ICommand MainPage { get; set; }
        public ICommand SettingsPage { get; set; }
        public ICommand ExitApp { get; set; }

        private object _selectedPage;

        public object SelectedPage
        {
            get => _selectedPage;
            set { _selectedPage = value; OnPropertyChanged("SelectedPage"); }
        }

        public MainWindowVM()
        {
            MainPage = new BaseCommand(OpenMainPage);
            SettingsPage = new BaseCommand(OpenSettingsPage);
            ExitApp = new BaseCommand(OnExitApp);
        }

        private void OnExitApp(object obj)
        {
            Application.Current.Shutdown();
        }
        private void OpenMainPage(object obj) => SelectedPage = new ReplayInfoVm();
        private void OpenSettingsPage(object obj) => SelectedPage = new SettingsVM();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
