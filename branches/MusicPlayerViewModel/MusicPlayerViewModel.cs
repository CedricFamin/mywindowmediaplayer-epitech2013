using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Utils;
using MWMP.Models;
using MWMP.ViewModels;
using System.Windows;
using System.Windows.Controls;


namespace MusicPlayerViewModel
{
    public class MusicPlayerViewModel : BindableObject, IMusicPlayerVM
    {
        private string _source;

        public MediaElement MediaElement { set; get; }
        public string Source 
        { 
            set
            {
                this._source = value;
                RaisePropertyChange("Source");
            }
            get
            {
                return this._source;
            }
        }
        public MediaState LoadedBehavior { set; get; }
        public bool CanCommandExecute { set; get; }
        
        public MusicPlayerViewModel()
        {

            Play = new RelayCommand((param) =>
            {
                MediaElement.Play();
            });
            Stop = new RelayCommand((param) => MediaElement.Stop());
            Pause = new RelayCommand((param) => MediaElement.Pause());
            //Next = new RelayCommand((param) => this.player.Next());
            Open = new RelayCommand((param) =>
            {
                Source = param as string;
            });
        }
        public RelayCommand Stop { get; protected set; }
        public RelayCommand Next { get; protected set; }
        public RelayCommand Pause { get; protected set; }
        public RelayCommand Play { get; protected set; }
        public RelayCommand Open { get; protected set; }
    }
}
