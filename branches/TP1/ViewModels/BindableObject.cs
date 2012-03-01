using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace TP1.ViewModels
{
    /// <summary>
    /// A basic class for bindable object
    /// </summary>
    class BindableObject : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChange(string propertyName)
        {
            VerifyProperty(propertyName);
            PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, args);
        }

        [Conditional("DEBUG")]
        private void VerifyProperty(string propertyName)
        {
            Type type = this.GetType();

            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new Exception(string.Format("{0} is not a public property of {1}", propertyName, type.FullName));
            }
        }
    }
}
