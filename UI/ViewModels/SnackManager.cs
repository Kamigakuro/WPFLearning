using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class SnackManager : INotifyPropertyChanged
    {
        private bool _snackshown;
        private string _text;
        public bool SnackShown
        {
            get => _snackshown;
            set { _snackshown = value;  OnPropertyChanged("SnackShown");}
        }

        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged("Text"); }
        }

        public async void ShowMessage(string text, int time)
        {
            Text = text;
            SnackShown = true;
            await Task.Delay(time);
            SnackShown = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
