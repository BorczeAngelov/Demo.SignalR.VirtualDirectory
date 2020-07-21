using Demo.SignalR.VirtualDirectory.Client.WPF.HubClientTwoWayComm;
using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using System;
using System.Windows;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public class MainWindowVM : ObservableBase
    {
        private readonly VirtualDirectoryHubClientTwoWayComm _virtualDirectoryHubClientTwoWayComm;
        private VirtualDirectoryVM _virtualDirectoryVM;
        private bool _isConnected;

        public MainWindowVM()
        {
            _virtualDirectoryHubClientTwoWayComm = new VirtualDirectoryHubClientTwoWayComm();
            ConnectCommand = new DelegateCommand(Connect, arg => !IsConnected);
        }

        public DelegateCommand ConnectCommand { get; }
        public bool IsConnected
        {
            get => _isConnected;
            private set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    OnPropertyChanged();
                }
            }
        }

        public VirtualDirectoryVM VirtualDirectoryVM
        {
            get => _virtualDirectoryVM;
            private set
            {
                if (_virtualDirectoryVM != value)
                {
                    _virtualDirectoryVM = value;
                    OnPropertyChanged();
                }
            }
        }

        private async void Connect(object obj)
        {
            try
            {
                await _virtualDirectoryHubClientTwoWayComm.ConnectWithServerHub();
                IsConnected = true;

                //TODO: VirtualDirectoryVM should be set when user is successfully connected, and loads starting data
                VirtualDirectoryVM = new VirtualDirectoryVM(_virtualDirectoryHubClientTwoWayComm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ConnectCommand.RaiseCanExecuteChanged();
        }
    }
}
