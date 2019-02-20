using MobileSchedule2.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileSchedule2.Models;
using Xamarin.Forms;

namespace MobileSchedule2.ViewModels
{
    public class BaseViewModel<T1> : INotifyPropertyChanged where T1:BaseDbObject, new ()
    {
        public IDataStore<T1> DataStore => DependencyService.Get<IDataStore<T1>>() ?? new DataStore<T1>();

        private string _info;
        public string Info
        {
            get => _info;
            set => SetProperty(ref _info, value);
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;

            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
