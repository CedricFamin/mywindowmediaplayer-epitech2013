using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MWMP.ViewModels;
using MWMP.Models;

namespace MWMP
{
	/// <summary>
	/// Interaction logic for PlayerControl.xaml
	/// </summary>
	public partial class PlayerControl : UserControl
	{
		public PlayerControl()
		{
			this.InitializeComponent();
            IMediaPlayer player = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
            if (player != null)
            {
                player.MediaElement = this.MediaPlayer;
                DataContext = player;
            }
		}

        private void VolumeChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider me = sender as Slider;
            ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel").Volume = e.NewValue / me.Maximum;
        }

        protected void DoubleCLickPlayerList(object sender, MouseButtonEventArgs e)
        {
            IMedia track = ((ListViewItem)sender).Content as IMedia;
            if (track != null)
            {
                ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel").Source = track.Path;
                ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel").Play.Execute(null);
            }

        }

        private void MediaPlayer_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }
	}
}