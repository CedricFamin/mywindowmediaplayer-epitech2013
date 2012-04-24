using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Controls;
using System.Timers;
using System.Windows.Threading;

namespace MWMP.Utils
{
    /// <summary>
    /// Provide 2 DependencyProperty on a mediaElement which allow to control a slider
    /// </summary>
    class MediaSliderBehavior : Behavior<MediaElement>
    {
        #region DependencyProperty
        public TimeSpan Position
        {
            get { return (TimeSpan)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(TimeSpan), typeof(MediaSliderBehavior));

        public TimeSpan Maximum
        {
            get { return (TimeSpan)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(TimeSpan), typeof(MediaSliderBehavior));
        #endregion

        #region Fields
        private readonly Timer _timer = new Timer(1000);
        #endregion

        protected override void OnAttached()
        {
            _timer.AutoReset = true;
            _timer.Elapsed += ElapseTime;
            AssociatedObject.MediaOpened += new RoutedEventHandler(MediaOpened);
            _timer.Start();
        }

        #region private methods
        private void MediaOpened(object sender, RoutedEventArgs eArgs)
        {
            MediaElement e = sender as MediaElement;
            if (e == null)
                return ;
            if (e.NaturalDuration.HasTimeSpan)
                Maximum = e.NaturalDuration.TimeSpan;
            else
                Maximum = new TimeSpan();
        }

        private void UpdatePos()
        {
            Position = AssociatedObject.Position;
        }

        private void ElapseTime(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(new Action(UpdatePos));
        }
        #endregion
    }
}
