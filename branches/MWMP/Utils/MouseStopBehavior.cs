using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace MWMP.Utils
{
    class MouseStopBehavior : Behavior<FrameworkElement>
    {
        #region DependencyProperty
        public Storyboard storyboard
        {
            get { return (Storyboard)GetValue(storyboardProperty); }
            set { SetValue(storyboardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for storyboard.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty storyboardProperty =
            DependencyProperty.Register("storyboard", typeof(Storyboard), typeof(MouseStopBehavior), new UIPropertyMetadata(null));
        #endregion

        #region Properties
        private DispatcherTimer timer { get; set; }
        #endregion

        #region CTor
        public MouseStopBehavior()
        {
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 2, 00)
            };
            timer.Tick += OnStopMouse;
        }
        #endregion

        #region Attach/Detach
        protected override void OnAttached()
        {
            AssociatedObject.MouseMove += OnMouseMove;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseDown -= OnMouseMove;
        }
        #endregion

        #region Event
        private void OnStopMouse(object sender, EventArgs e)
        {
            if (storyboard != null)
                storyboard.Resume();
            timer.Stop();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            storyboard.Begin();
            storyboard.Pause();
            timer.Stop();
            timer.Start();
        }
        #endregion
    }
}
