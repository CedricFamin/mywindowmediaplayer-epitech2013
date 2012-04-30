using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MWMP
{
    public class FullScreenBehavior : Behavior<ContentControl>
    {
        private Window window { get; set; }
        private bool isFullScreen { get; set; }
        private DispatcherTimer timer { get; set; }
        private object Content { get; set; }
        
        #region CTor
        public FullScreenBehavior()
        {
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 200)
            };

            timer.Tick += StopTimer;
        }
        #endregion

        #region Method
        private void StopTimer(object sender, EventArgs e)
        {
            timer.Stop();
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            isFullScreen = false;
            AssociatedObject.MouseLeftButtonDown += new MouseButtonEventHandler(OnMouseButtonDown);
        }

        private void OnMouseButtonDown(object sender, EventArgs e)
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
                return;
            }
            timer.Stop();
            OnDoubleClick();
        }

        private void MouseDoubleClick(object sender, EventArgs e)
        {
            window.Content = null;
            AssociatedObject.Content = Content;
            window.Close();
            isFullScreen = false; 
        }

        private void OnDoubleClick()
        {
            if (isFullScreen == false)
            {
                Window win = new Window();
                win.MouseDoubleClick += new MouseButtonEventHandler(MouseDoubleClick);
                Content = AssociatedObject.Content;
                AssociatedObject.Content = null;
                win.Content = Content;
                win.Show();
                win.WindowStyle = WindowStyle.None;
                win.WindowState = WindowState.Maximized;
                window = win;
                isFullScreen = true;
            }
        }

        #endregion
    } 
}
