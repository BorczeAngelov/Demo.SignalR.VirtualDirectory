using Demo.SignalR.VirtualDirectory.Common.DataModel;
using Demo.SignalR.VirtualDirectory.Common.HubInterfaces;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Demo.SignalR.VirtualDirectory.Server.SignalRHubs
{
    internal class VirtualDirectoryHub : Hub, IVirtualDirectoryHub
    {
        public async Task CreateFile()
        {
            await Clients.All.SendAsync(nameof(IVirtualDirectoryHubClient.InvokeCreateFile));
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
            await Clients.All.SendAsync(nameof(IVirtualDirectoryHubClient.InvokeCreateFolder));
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
