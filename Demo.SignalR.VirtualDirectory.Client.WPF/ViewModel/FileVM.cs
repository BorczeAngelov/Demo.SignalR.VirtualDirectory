using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using Demo.SignalR.VirtualDirectory.Common.DataModel;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;
using System;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public class FileVM : EditableItemBaseVM
    {
        private File _file;

        private string _name;
        private Guid _folderObjectKey;

        public FileVM(
            File file,
            IVirtualDirectoryHubClientTwoWayComm virtualDirectoryHubClientTwoWayComm) : base(virtualDirectoryHubClientTwoWayComm)
        {
            _file = file;
            VirtualDirectoryHubClientTwoWayComm.FileUpdated += UpdateData;
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

        public Guid FolderObjectKey
        {
            get => _folderObjectKey;
            set
            {
                if (_folderObjectKey != value)
                {
                    _folderObjectKey = value;
                    OnPropertyChanged();
                }
            }
        }

        protected override void CopyDataFromDataModel()
        {
            Name = _file.Name;
            FolderObjectKey = _file.FolderObjectKey;
            IsModified = false;
        }

        internal bool HasDataModelWithObjectKey(Guid objectKey)
        {
            return _file.ObjectKey == objectKey;
        }

        private async void Delete(object obj)
        {
            await VirtualDirectoryHubClientTwoWayComm.ServerHubProxy.DeleteFile(_file);
        }

        private async void Save(object obj)
        {
            _file.Name = Name;
            _file.FolderObjectKey = FolderObjectKey;            
            await VirtualDirectoryHubClientTwoWayComm.ServerHubProxy.UpdateFile(_file);
        }

        private void UpdateData(File updatedFile)
        {
            if (_file.ObjectKey == updatedFile.ObjectKey)
            {
                //You can invoke a messageBox here
                _file = updatedFile;
                CopyDataFromDataModel();
            }
        }
    }
}
