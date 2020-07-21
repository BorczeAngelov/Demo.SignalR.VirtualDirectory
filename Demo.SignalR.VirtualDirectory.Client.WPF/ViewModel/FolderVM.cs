using Demo.SignalR.VirtualDirectory.Common.DataModel;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public class FolderVM : EditableItemBaseVM
    {
        private Folder _folder;
        private IVirtualDirectoryHubClientTwoWayComm _virtualDirectoryHubClientTwoWayComm;

        public FolderVM(
            Folder folder,
            IVirtualDirectoryHubClientTwoWayComm virtualDirectoryHubClientTwoWayComm)
        {
            _folder = folder;
            _virtualDirectoryHubClientTwoWayComm = virtualDirectoryHubClientTwoWayComm;
        }
    }
}
