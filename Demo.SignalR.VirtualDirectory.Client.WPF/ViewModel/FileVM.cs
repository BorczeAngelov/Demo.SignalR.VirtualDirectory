using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using Demo.SignalR.VirtualDirectory.Common.DataModel;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;
using System;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public class FileVM : EditableItemBaseVM
    {
        private File _file;

        public FileVM(
            File file,
            IVirtualDirectoryHubClientTwoWayComm virtualDirectoryHubClientTwoWayComm) : base(virtualDirectoryHubClientTwoWayComm)
        {
            _file = file;
            DeleteCommand = new DelegateCommand(Delete);
        }

        public override DelegateCommand SaveCommand { get; }
        public override DelegateCommand CancelCommand { get; }
        public override DelegateCommand DeleteCommand { get; }

        internal bool HasDataModelWithObjectKey(Guid objectKey)
        {
            return _file.ObjectKey == objectKey;
        }

        private async void Delete(object obj)
        {
            await VirtualDirectoryHubClientTwoWayComm.ServerHubProxy.DeleteFile(_file);
        }
    }
}
