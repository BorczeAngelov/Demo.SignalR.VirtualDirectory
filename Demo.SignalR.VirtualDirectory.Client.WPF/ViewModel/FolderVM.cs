using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using Demo.SignalR.VirtualDirectory.Common.DataModel;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public class FolderVM : EditableItemBaseVM
    {
        private Folder _folder;

        private string _name;
        private FolderVM _parentFolder;

        internal FolderVM(
            Folder folder,
            IVirtualDirectoryHubClientTwoWayComm virtualDirectoryHubClientTwoWayComm,
            ObservableCollection<FolderVM> folderCollection) : base(virtualDirectoryHubClientTwoWayComm)
        {
            _folder = folder;
            VirtualDirectoryHubClientTwoWayComm.FolderUpdated += UpdateData;
            FolderCollection = folderCollection;

            DeleteCommand = new DelegateCommand(Delete);
            SaveCommand = new DelegateCommand(Save, arg => IsModified && !HasErrors);

            CopyDataFromDataModel();
        }

        public override DelegateCommand SaveCommand { get; }
        public override DelegateCommand DeleteCommand { get; }
        public ObservableCollection<FolderVM> FolderCollection { get; }

        public Guid ObjectKey { get => _folder.ObjectKey; }

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

        public FolderVM ParentFolder
        {
            get => _parentFolder;
            set
            {
                if (_parentFolder != value)
                {
                    _parentFolder = value;
                    ValidateParentFolder();
                    OnPropertyChanged();
                }
            }
        }

        protected override void CopyDataFromDataModel()
        {
            Name = _folder.Name;
            ParentFolder = FolderCollection.FirstOrDefault(x => x.ObjectKey == _folder.ParentFolderObjectKey);

            CommandManager.InvalidateRequerySuggested(); //https://social.msdn.microsoft.com/Forums/vstudio/en-US/86986dbd-6af9-41f8-9ddf-d0749877c2ef/background-thread-canexecute-changes?forum=wpf
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
            _folder.ParentFolderObjectKey = (ParentFolder != null) ? ParentFolder.ObjectKey : Guid.Empty;
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

        private void ValidateParentFolder()
        {
            ClearErrors(nameof(ParentFolder));
            if (ParentFolder == this)
            {
                var error = "Cannot have itself as parent folder";
                AddError(nameof(ParentFolder), error);
            }
        }
    }
}
