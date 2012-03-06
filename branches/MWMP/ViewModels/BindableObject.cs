﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;

namespace MWMP.ViewModels
{
    /// <summary>
    /// A basic class for bindable object
    /// </summary>
    public class BindableObject : INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Fields
        private Dictionary<int, PropertyChangedEventArgs> cacheEventArgs;
        #endregion

        #region Ctor
        public BindableObject()
        {
            this.cacheEventArgs = new Dictionary<int, PropertyChangedEventArgs>();
        }
        #endregion /// Ctor

        #region Protected Methods
        protected PropertyChangedEventArgs GetEventArgs(string propertyName)
        {
            PropertyChangedEventArgs result;

            this.cacheEventArgs.TryGetValue(propertyName.GetHashCode(), out result);
            if (result == null)
            {
                result = new PropertyChangedEventArgs(propertyName);
                this.cacheEventArgs[propertyName.GetHashCode()] = result;
            }
            return result;
        }

        protected void RaisePropertyChange(string propertyName)
        {
            VerifyProperty(propertyName);
            PropertyChangedEventArgs args = GetEventArgs(propertyName);
            PropertyChanged(this, args);
        }
        #endregion

        #region Private Methods
        [Conditional("DEBUG")]
        private void VerifyProperty(string propertyName)
        {
            Type type = this.GetType();

            type.GetHashCode();

            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new Exception(string.Format("{0} is not a public property of {1}", propertyName, type.FullName));
            }
        }
        #endregion
    }
}
