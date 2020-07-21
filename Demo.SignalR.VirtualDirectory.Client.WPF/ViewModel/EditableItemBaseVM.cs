using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;
using System;
using System.ComponentModel;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public abstract class EditableItemBaseVM : ObservableBase
    {
        private bool _name;
        protected IVirtualDirectoryHubClientTwoWayComm VirtualDirectoryHubClientTwoWayComm { get; }

        protected EditableItemBaseVM(IVirtualDirectoryHubClientTwoWayComm virtualDirectoryHubClientTwoWayComm)
        {
            VirtualDirectoryHubClientTwoWayComm = virtualDirectoryHubClientTwoWayComm;
            CancelCommand = new DelegateCommand(Cancel, arg => IsModified);

            PropertyChanged += WhenAnyPropertyIsChangedMarkAsModified;
        }

        public Type Type { get => GetType(); }
        public DelegateCommand CancelCommand { get; }
        public abstract DelegateCommand SaveCommand { get; }
        public abstract DelegateCommand DeleteCommand { get; }
        protected abstract void CopyDataFromDataModel();

        public bool IsModified
        {
            get => _name;
            protected set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                    CancelCommand.RaiseCanExecuteChanged();
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private void WhenAnyPropertyIsChangedMarkAsModified(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(IsModified))
            {
                IsModified = true;
            }
        }

        private void Cancel(object obj)
        {
            CopyDataFromDataModel();
        }
    }
}
