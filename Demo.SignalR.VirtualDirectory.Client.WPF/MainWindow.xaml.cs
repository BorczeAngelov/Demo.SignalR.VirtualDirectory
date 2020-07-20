using Demo.SignalR.VirtualDirectory.Client.WPF.HubClientTwoWayComm;
using System.Windows;

namespace Demo.SignalR.VirtualDirectory.Client.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly VirtualDirectoryHubClientTwoWayComm _virtualDirectoryHubClientTwoWayComm;

        public MainWindow()
        {
            InitializeComponent();
            _virtualDirectoryHubClientTwoWayComm = new VirtualDirectoryHubClientTwoWayComm();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await _virtualDirectoryHubClientTwoWayComm.ConnectWithServerHub();

            await _virtualDirectoryHubClientTwoWayComm.ServerHubProxy.CreateFile();
            await _virtualDirectoryHubClientTwoWayComm.ServerHubProxy.CreateFolder();
        }
    }
}
