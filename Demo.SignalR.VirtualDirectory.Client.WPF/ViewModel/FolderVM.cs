using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using Demo.SignalR.VirtualDirectory.Common.DataModel;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;
using System;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public class FolderVM : EditableItemBaseVM
    {
        private Folder _folder;

        private string _name;
        private Guid _parentFolderObjectKey;

        public FolderVM(
            Folder folder,
            IVirtualDirectoryHubClientTwoWayComm virtualDirectoryHubClientTwoWayComm) : base(virtualDirectoryHubClientTwoWayComm)
        {
            _folder = folder;
            VirtualDirectoryHubClientTwoWayComm.FolderUpdated += UpdateData;
            DeleteCommand = new DelegateCommand(Delete);
            SaveCommand = new DelegateCommand(Save, arg => IsModified);

            CopyDataFromDataModel();
        }

        public override DelegateCommand SaveCommand { get; }
        public override DelegateCommand DeleteCommand { get; }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public Guid ParentFolderObjectKey
        {
            get => _parentFolderObjectKey;
            set
            {
                if (_parentFolderObjectKey != value)
                {
                    _parentFolderObjectKey = value;
                    OnPropertyChanged();
                }
            }
        }

        protected override void CopyDataFromDataModel()
        {
            Name = _folder.Name;
            ParentFolderObjectKey = _folder.ParentFolderObjectKey;
            IsModified = false;
        }

        internal bool HasDataModelWithObjectKey(Guid objectKey)
        {
            return _folder.ObjectKey == objectKey;
        }

        private async void Delete(object obj)
        {
            await VirtualDirectoryHubClientTwoWayComm.ServerHubProxy.DeleteFolder(_folder);
        }

        private async void Save(object obj)
        {
            _folder.Name = Name;
            _folder.ParentFolderObjectKey = ParentFolderObjectKey;
            await VirtualDirectoryHubClientTwoWayComm.ServerHubProxy.UpdateFolder(_folder);
        }

        private void UpdateData(Folder updatedFolder)
        {
            if (_folder.ObjectKey == updatedFolder.ObjectKey)
            {
                //You can invoke a messageBox here
                _folder = updatedFolder;
                CopyDataFromDataModel();
            }
        }
    }
}
