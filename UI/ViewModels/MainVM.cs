using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UI.Classes;
using UI.Models;

namespace UI.ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {
        private HTTPContent _model;
        private ObservableCollection<WGNew> news;
        public MainVM()
        {
            _model = new HTTPContent();
            news = _model.GetNews("https://worldoftanks.ru/ru/news/");
        }

        public ObservableCollection<WGNew> News
        {
            get => news;
            set { news = value; OnPropertyChanged("News"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
