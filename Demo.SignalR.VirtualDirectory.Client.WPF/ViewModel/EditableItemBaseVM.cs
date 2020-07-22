using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;
using System;
using System.ComponentModel;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public abstract class EditableItemBaseVM : ObservableAndValidationalBase
    {
        private bool _isModified;
        protected IVirtualDirectoryHubClientTwoWayComm VirtualDirectoryHubClientTwoWayComm { get; }

        protected EditableItemBaseVM(IVirtualDirectoryHubClientTwoWayComm virtualDirectoryHubClientTwoWayComm)
        {
            VirtualDirectoryHubClientTwoWayComm = virtualDirectoryHubClientTwoWayComm;
            CancelCommand = new DelegateCommand(Cancel, arg => IsModified);

            PropertyChanged += WhenAnyPropertyIsChangedMarkAsModified;
            PropertyChanged += WhenHasErrorsChangedRefreshSaveCommand;
        }

        public Type Type { get => GetType(); }
        public DelegateCommand CancelCommand { get; }
        public abstract DelegateCommand SaveCommand { get; }
        public abstract DelegateCommand DeleteCommand { get; }
        protected abstract void CopyDataFromDataModel();

        public bool IsModified
        {
            get => _isModified;
            protected set
            {
                if (_isModified != value)
                {
                    _isModified = value;
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

        private void WhenHasErrorsChangedRefreshSaveCommand(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(HasErrors))
            {
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private void Cancel(object obj)
        {
            CopyDataFromDataModel();
        }
    }
}
