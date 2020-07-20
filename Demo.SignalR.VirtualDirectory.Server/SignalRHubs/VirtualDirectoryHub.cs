using Demo.SignalR.VirtualDirectory.Common.DataModel;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Demo.SignalR.VirtualDirectory.Server.SignalRHubs
{
    internal class VirtualDirectoryHub : Hub, IVirtualDirectoryHub
    {
        public async Task CreateFile()
        {
            var file = new File() { ObjectKey = Guid.NewGuid() };
            await Clients.All.SendAsync(nameof(IVirtualDirectoryHubClient.InvokeCreateFile), file);
        }

        public async Task UpdateFile(File file)
        {
            await Clients.All.SendAsync(nameof(IVirtualDirectoryHubClient.InvokeUpdateFile), file);
        }

        public async Task DeleteFile(File file)
        {
            await Clients.All.SendAsync(nameof(IVirtualDirectoryHubClient.InvokeDeleteFile), file);
        }


        public async Task CreateFolder()
        {
            var folder = new Folder() { ObjectKey = Guid.NewGuid() };
            await Clients.All.SendAsync(nameof(IVirtualDirectoryHubClient.InvokeCreateFolder), folder);
        }

        public async Task UpdateFolder(Folder folder)
        {
            await Clients.All.SendAsync(nameof(IVirtualDirectoryHubClient.InvokeUpdateFolder), folder);
        }

        public async Task DeleteFolder(Folder folder)
        {
            await Clients.All.SendAsync(nameof(IVirtualDirectoryHubClient.InvokeDeleteFolder), folder);
        }
    }
}
