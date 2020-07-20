using Demo.SignalR.VirtualDirectory.Common.DataModel;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;
using Demo.SignalR.VirtualDirectory.Common.Utils;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.HubClientTwoWayComm
{
    internal class VirtualDirectoryHubClientTwoWayComm : IVirtualDirectoryHubClientTwoWayComm
    {
        private readonly HubConnection _connection;

        public event Action<File> FileCreated;
        public event Action<File> FileUpdated;
        public event Action<File> FileDeleted;
        public event Action<Folder> FolderCreated;
        public event Action<Folder> FolderUpdated;
        public event Action<Folder> FolderDeleted;

        internal VirtualDirectoryHubClientTwoWayComm()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(DemoUrlConstants.LocalHostUrl + DemoUrlConstants.VirtualDirectoryHubEndpoint)
                .WithAutomaticReconnect()
                .Build();

            _connection.On<File>(nameof(InvokeCreateFile), InvokeCreateFile);
            _connection.On<File>(nameof(InvokeUpdateFile), InvokeUpdateFile);
            _connection.On<File>(nameof(InvokeDeleteFile), InvokeDeleteFile);
            _connection.On<Folder>(nameof(InvokeCreateFolder), InvokeCreateFolder);
            _connection.On<Folder>(nameof(InvokeUpdateFolder), InvokeUpdateFolder);
            _connection.On<Folder>(nameof(InvokeDeleteFolder), InvokeDeleteFolder);

            ServerHubProxy = new ServerHubProxyImp(_connection);
        }

        public IVirtualDirectoryHub ServerHubProxy { get; }

        public async Task ConnectWithServerHub()
        {
            await _connection.StartAsync();
        }

        public void InvokeCreateFile(File file)
        {
            FileCreated?.Invoke(file);
        }

        public void InvokeUpdateFile(File file)
        {
            FileUpdated?.Invoke(file);
        }

        public void InvokeDeleteFile(File file)
        {
            FileDeleted?.Invoke(file);
        }


        public void InvokeCreateFolder(Folder folder)
        {
            FolderCreated?.Invoke(folder);
        }

        public void InvokeUpdateFolder(Folder folder)
        {
            FolderUpdated?.Invoke(folder);
        }

        public void InvokeDeleteFolder(Folder folder)
        {
            FolderDeleted?.Invoke(folder);
        }

        private class ServerHubProxyImp : IVirtualDirectoryHub
        {
            private readonly HubConnection _connection;

            public ServerHubProxyImp(HubConnection connection)
            {
                _connection = connection;
            }

            public Task CreateFile()
            {
                return _connection.InvokeAsync(nameof(CreateFile));
            }

            public Task UpdateFile(File file)
            {
                return _connection.InvokeAsync(nameof(UpdateFile), file);
            }

            public Task DeleteFile(File file)
            {
                return _connection.InvokeAsync(nameof(DeleteFile), file);
            }


            public Task CreateFolder()
            {
                return _connection.InvokeAsync(nameof(CreateFolder));
            }

            public Task UpdateFolder(Folder folder)
            {
                return _connection.InvokeAsync(nameof(UpdateFolder), folder);
            }

            public Task DeleteFolder(Folder folder)
            {
                return _connection.InvokeAsync(nameof(DeleteFolder), folder);
            }
        }
    }
}
