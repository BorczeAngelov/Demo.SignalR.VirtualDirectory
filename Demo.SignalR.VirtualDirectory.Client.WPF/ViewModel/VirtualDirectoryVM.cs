using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using Demo.SignalR.VirtualDirectory.Common.DataModel;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;
using System.Collections.ObjectModel;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public class VirtualDirectoryVM : ObservableBase
    {
        private IVirtualDirectoryHubClientTwoWayComm _virtualDirectoryHubClientTwoWayComm;

        public VirtualDirectoryVM(IVirtualDirectoryHubClientTwoWayComm virtualDirectoryHubClientTwoWayComm)
        {
            _virtualDirectoryHubClientTwoWayComm = virtualDirectoryHubClientTwoWayComm;
            _virtualDirectoryHubClientTwoWayComm.FileCreated += OnFileCreated;
            _virtualDirectoryHubClientTwoWayComm.FolderCreated += OnFolderCreated;

            ExplorerVM = new ExplorerVM(FileCollection, FolderCollection);

            CreateFileCommand = new DelegateCommand(CreateFile);
            CreateFolderCommand = new DelegateCommand(CreateFolder);
        }

        public ExplorerVM ExplorerVM { get; }
        public ObservableCollection<FileVM> FileCollection { get; } = new ObservableCollection<FileVM>();
        public ObservableCollection<FolderVM> FolderCollection { get; } = new ObservableCollection<FolderVM>();

        public DelegateCommand CreateFileCommand { get; }
        public DelegateCommand CreateFolderCommand { get; }

        private async void CreateFile(object arg)
        {
            await _virtualDirectoryHubClientTwoWayComm.ServerHubProxy.CreateFile();
        }

        private async void CreateFolder(object arg)
        {
            await _virtualDirectoryHubClientTwoWayComm.ServerHubProxy.CreateFile();
        }

        private void OnFileCreated(File file)
        {
            var fileVM = new FileVM(file, _virtualDirectoryHubClientTwoWayComm);
            FileCollection.Add(fileVM);
        }

        private void OnFolderCreated(Folder folder)
        {
            var folderVM = new FolderVM(folder, _virtualDirectoryHubClientTwoWayComm);
            FolderCollection.Add(folderVM);
        }
    }
}
