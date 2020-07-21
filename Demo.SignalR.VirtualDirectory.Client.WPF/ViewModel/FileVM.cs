using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using Demo.SignalR.VirtualDirectory.Common.DataModel;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public class FileVM : ObservableBase
    {
        private File _file;
        private IVirtualDirectoryHubClientTwoWayComm _virtualDirectoryHubClientTwoWayComm;

        public FileVM(
            File file,
            IVirtualDirectoryHubClientTwoWayComm virtualDirectoryHubClientTwoWayComm)
        {
            _file = file;
            _virtualDirectoryHubClientTwoWayComm = virtualDirectoryHubClientTwoWayComm;
        }
    }
}
