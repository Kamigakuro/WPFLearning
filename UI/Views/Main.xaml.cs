using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace UI.Views
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri)); //e.Uri.AbsoluteUri
            e.Handled = true;
        }
    }
}
