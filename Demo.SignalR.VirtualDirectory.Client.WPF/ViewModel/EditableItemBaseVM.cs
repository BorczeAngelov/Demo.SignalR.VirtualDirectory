using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;
using System;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public abstract class EditableItemBaseVM : ObservableBase
    {
        protected IVirtualDirectoryHubClientTwoWayComm VirtualDirectoryHubClientTwoWayComm { get; }

        protected EditableItemBaseVM(IVirtualDirectoryHubClientTwoWayComm virtualDirectoryHubClientTwoWayComm)
        {
            VirtualDirectoryHubClientTwoWayComm = virtualDirectoryHubClientTwoWayComm;
        }

        public Type Type { get => GetType(); }
        public abstract DelegateCommand SaveCommand { get; }
        public abstract DelegateCommand CancelCommand { get; }
        public abstract DelegateCommand DeleteCommand { get; }
    }
}
