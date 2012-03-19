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
using MWMP.Models.DAL;

namespace MWMP
{
	/// <summary>
	/// Interaction logic for LibraryUserControl.xaml
	/// </summary>
	public partial class LibraryUserControl : UserControl
	{
		public LibraryUserControl()
		{
			this.InitializeComponent();
            ILibrary lib = ModuleManager.GetInstanceOf<ILibrary>("LibraryViewModel");
            DataContext = lib;
		}

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IMedia track = ((ListViewItem)sender).Content as IMedia;
            if (track != null)
                ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel").Source = track.Path;

        }
	}
}