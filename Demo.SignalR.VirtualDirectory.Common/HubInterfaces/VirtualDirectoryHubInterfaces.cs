using Demo.SignalR.VirtualDirectory.Common.DataModel;
using System;
using System.Threading.Tasks;

namespace Demo.SignalR.VirtualDirectory.Common.HubInterfaces
{
    public interface IVirtualDirectoryHub
    {
        Task CreateFile();
        Task UpdateFile(File file);
        Task DeleteFile(File file);

        Task CreateFolder();
        Task UpdateFolder(Folder folder);
        Task DeleteFolder(Folder folder);
    }

    public interface IVirtualDirectoryHubClient
    {
        void InvokeCreateFile(File file);
        void InvokeUpdateFile(File file);
        void InvokeDeleteFile(File file);

        void InvokeCreateFolder(Folder folder);
        void InvokeUpdateFolder(Folder folder);
        void InvokeDeleteFolder(Folder folder);

        //void InvokeLoadStartingValues();
    }

    public interface IVirtualDirectoryHubClientTwoWayComm : IVirtualDirectoryHubClient
    {
        event Action<File> FileCreated;
        event Action<File> FileUpdated;
        event Action<File> FileDeleted;

        event Action<Folder> FolderCreated;
        event Action<Folder> FolderUpdated;
        event Action<Folder> FolderDeleted;

        //event Action<> StartingValuesLoaded;

        IVirtualDirectoryHub ServerHub { get; }

        Task ConnectWithServerHub();
    }
}
