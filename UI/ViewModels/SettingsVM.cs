using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Models;

namespace UI.ViewModels
{
    public class SettingsVM : INotifyPropertyChanged
    {

        private string _selectedTheme;
        public ICommand SaveSettings { get; set; }
        public ICommand ChangeBroken { get; set; }
        public ICommand ChangeLow { get; set; }
        private ObservableCollection<string> _themes;
        public string SelectedTheme
        {
            get => _selectedTheme;
            set { _selectedTheme = value;
                Settings.Theme = value; PropertyChangedEvent("SelectedTheme"); ChangeTheme(_selectedTheme);}
        }
        public ObservableCollection<string> Themes { get => _themes; set { _themes = value; PropertyChangedEvent("Themes");}}
        public SettingsVM()
        {
            SaveSettings = new BaseCommand(SaveSettingsCommand);
            //ChangeBroken = new BaseCommand(OnChangeBroken);
            //ChangeLow = new BaseCommand(SaveSettingsCommand);
            Themes = new ObservableCollection<string>(Settings.Themes);
            SelectedTheme = Settings.Theme;
        }
        public bool ScanSubDir
        {
            get => Settings.ScanSubDir;
            set { Settings.ScanSubDir = value; PropertyChangedEvent("ScanSubDir"); }
        }
        public bool ScanZips
        {
            get => Settings.ScanZips;
            set { Settings.ScanZips = value; PropertyChangedEvent("ScanZips"); }
        }
        public bool Reserving
        {
            get => Settings.CreateReplayCopy;
            set { Settings.CreateReplayCopy = value; PropertyChangedEvent("Reserving"); }
        }
        public bool ScanOnRun
        {
            get => Settings.ScanOnRun;
            set { Settings.ScanOnRun = value; PropertyChangedEvent("ScanOnRun"); }
        }
        public bool AllertAlien
        {
            get => Settings.AttentAlienFiles;
            set { Settings.AttentAlienFiles = value; PropertyChangedEvent("AllertAlien"); }
        }
        public bool MarkBroken
        {
            get => Settings.CheckBadReplays;
            set { Settings.CheckBadReplays = value; PropertyChangedEvent("MarkBroken"); }
        }
        public bool MarkLow
        {
            get => Settings.CheckLowSizeReplays;
            set { Settings.CheckLowSizeReplays = value; PropertyChangedEvent("MarkLow"); }
        }
        public string BrokenColor
        {
            get => Settings.BadColor;
            set { Settings.BadColor = value; PropertyChangedEvent("BrokenColor"); }
        }
        public string WarningColor
        {
            get => Settings.LowSizeColor;
            set { Settings.LowSizeColor = value; PropertyChangedEvent("WarningColor"); }
        }
        public string TempFolder
        {
            get => Settings.TempFolder;
            set { Settings.TempFolder = value; PropertyChangedEvent("TempFolder"); }
        }
        public string ReservDir
        {
            get => Settings.Reservingfolder;
            set { Settings.Reservingfolder = value; PropertyChangedEvent("ReservDir"); }
        }
        public bool IsDarken
        {
            get => Settings.IsDark;
            set { Settings.IsDark = value; PropertyChangedEvent("IsDarken"); ChangeDark(value); }
        }
        public string URLUpdate
        {
            get => Settings.UpdateURL;
            set { Settings.UpdateURL = value; PropertyChangedEvent("URLUpdate"); }
        }
        public bool CheckUpdates
        {
            get => Settings.CheckUpdates;
            set { Settings.CheckUpdates = value; PropertyChangedEvent("CheckUpdates"); }
        }
        public int HistoryCount
        {
            get => Settings.MaxPathHistory;
            set { Settings.MaxPathHistory = value; PropertyChangedEvent("HistoryCount"); }
        }
        public int LowSizeValue
        {
            get => Settings.LowSizeValue;
            set { Settings.LowSizeValue = value; PropertyChangedEvent("HistoryCount"); }
        }

        public static void ChangeDark(bool isDark)
        {
            ThemeSwither.ChangeDark(isDark);
        }
        public static void ChangeTheme(string theme)
        {
            
            //MessageBox.Show(theme);
            //if (theme == "System.Windows.Controls.ComboBoxItem: Dark Cyan")
        }

        private void SaveSettingsCommand(object obj)
        {
            if (Settings.SaveSettings())
            {
                Settings.snack.ShowMessage("Настройки сохранены успешно!", 5000);
                Settings.panel.Color = "#AAAA00";
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
