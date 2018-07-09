using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataModule;
using UI.ViewModels;

namespace UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ReplayInfoWindow.xaml
    /// </summary>
    public partial class ReplayInfoWindow : Window
    {
        public ReplayInfoWindow(ReplayData replay)
        {
            InitializeComponent();
            DataContext = new ReplayInfoWindowVM(replay);
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            Close();
        }
    }
   
}
