﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.Utils
{
    public abstract class ObservableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
