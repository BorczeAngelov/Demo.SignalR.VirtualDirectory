using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using Demo.SignalR.VirtualDirectory.Common.DataModel;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;
using System;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public class FolderVM : EditableItemBaseVM
    {
        private Folder _folder;

        public FolderVM(
            Folder folder,
            IVirtualDirectoryHubClientTwoWayComm virtualDirectoryHubClientTwoWayComm) : base(virtualDirectoryHubClientTwoWayComm)
        {
            _folder = folder;
            DeleteCommand = new DelegateCommand(Delete);
        }

        public override DelegateCommand SaveCommand { get; }
        public override DelegateCommand CancelCommand { get; }
        public override DelegateCommand DeleteCommand { get; }


        internal bool HasDataModelWithObjectKey(Guid objectKey)
        {
            return _folder.ObjectKey == objectKey;
        }

        private async void Delete(object obj)
        {
            await VirtualDirectoryHubClientTwoWayComm.ServerHubProxy.DeleteFolder(_folder);
        }
    }
}
