using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UI.Models;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (File.Exists("settings.xml")) Settings.LoadSettings();
            else {Settings.CreateSettings();
                Settings.LoadSettings();
            }
            ThemeSwither.ChangeDark(Settings.IsDark);
            Settings.Themes.Add("Cyan");
            Settings.Themes.Add("Amber");
            Settings.Themes.Add("DeepOrange");
            Settings.Themes.Add("Yellow");
            base.OnStartup(e);
            if (Settings.CheckUpdates)
            {

            }
        }
    }
}
