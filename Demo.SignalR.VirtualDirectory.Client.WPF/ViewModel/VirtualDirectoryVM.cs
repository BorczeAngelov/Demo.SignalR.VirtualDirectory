using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using Demo.SignalR.VirtualDirectory.Common.DataModel;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;
using System.Collections.ObjectModel;
using System.Linq;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public class VirtualDirectoryVM : ObservableBase
    {
        private readonly ObservableCollection<FileVM> _fileCollection  = new ObservableCollection<FileVM>();
        private readonly ObservableCollection<FolderVM> _folderCollection  = new ObservableCollection<FolderVM>();
        private IVirtualDirectoryHubClientTwoWayComm _virtualDirectoryHubClientTwoWayComm;

        public VirtualDirectoryVM(IVirtualDirectoryHubClientTwoWayComm virtualDirectoryHubClientTwoWayComm)
        {
            _virtualDirectoryHubClientTwoWayComm = virtualDirectoryHubClientTwoWayComm;
            _virtualDirectoryHubClientTwoWayComm.FileCreated += OnFileCreated;
            _virtualDirectoryHubClientTwoWayComm.FileDeleted += OnFileDeleted;

            _virtualDirectoryHubClientTwoWayComm.FolderCreated += OnFolderCreated;
            _virtualDirectoryHubClientTwoWayComm.FolderDeleted += OnFolderDeleted;

            ExplorerVM = new ExplorerVM(_fileCollection, _folderCollection);

            CreateFileCommand = new DelegateCommand(CreateFile);
            CreateFolderCommand = new DelegateCommand(CreateFolder);
        }

        public ExplorerVM ExplorerVM { get; }

        public DelegateCommand CreateFileCommand { get; }
        public DelegateCommand CreateFolderCommand { get; }

        private async void CreateFile(object arg)
        {
            await _virtualDirectoryHubClientTwoWayComm.ServerHubProxy.CreateFile();
        }

        private async void CreateFolder(object arg)
        {
            await _virtualDirectoryHubClientTwoWayComm.ServerHubProxy.CreateFolder();
        }

        private void OnFileCreated(File file)
        {
            var fileVM = new FileVM(file, _virtualDirectoryHubClientTwoWayComm, _folderCollection);
            _fileCollection.Add(fileVM);
        }

        private void OnFileDeleted(File file)
        {
            var fileVM = _fileCollection.FirstOrDefault(x => x.HasDataModelWithObjectKey(file.ObjectKey));
            if (fileVM != null)
            {
                _fileCollection.Remove(fileVM);
            }
        }

        private void OnFolderCreated(Folder folder)
        {
            var folderVM = new FolderVM(folder, _virtualDirectoryHubClientTwoWayComm, _folderCollection);
            _folderCollection.Add(folderVM);
        }

        private void OnFolderDeleted(Folder folder)
        {
            var folderVM = _folderCollection.FirstOrDefault(x => x.HasDataModelWithObjectKey(folder.ObjectKey));
            if (folderVM != null)
            {
                _folderCollection.Remove(folderVM);
            }
        }
    }
}
