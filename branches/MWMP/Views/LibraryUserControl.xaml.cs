using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MWMP.Models;
using MWMP.ViewModels;

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
		}

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IMedia track = ((ListViewItem)sender).Content as IMedia;
            if (track != null)
            {
               IMediaPlayer player = ModuleManager.GetInstanceOf<IMediaPlayer>("MusicPlayerViewModel");
                if (player == null)
                    return;
                player.Open.Execute(track);
            }

        }

        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(MusicList.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        private void ListView_Sort(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                        direction = ListSortDirection.Ascending;
                    else
                        direction = (_lastDirection == ListSortDirection.Ascending) ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    string header = headerClicked.Column.Header as string;
                    Sort(header, direction);
                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }
	}
}