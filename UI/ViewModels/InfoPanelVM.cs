using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class InfoPanelVM: INotifyPropertyChanged
    {
        private string _color;
        private string _icon;
        private string _text;
        public string Color
        {
            get => _color;
            set { _color = value; OnPropertyChanged("Color"); }
        }

        public string Icon
        {
            get => _icon;
            set { _icon = value; OnPropertyChanged("Icon"); }
        }

        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged("Text"); }
        }

        public InfoPanelVM()
        {
            SetDefaultIcon();
            Color = "#0277BD";
        }

        public void SetDefaultIcon()
        {
            Icon = "Atlassian";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
