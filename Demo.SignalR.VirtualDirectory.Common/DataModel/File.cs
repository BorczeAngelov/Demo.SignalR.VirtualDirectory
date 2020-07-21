using System;

namespace Demo.SignalR.VirtualDirectory.Common.DataModel
{
    public class File
    {
        public string Name { get; set; }
        public Guid ObjectKey { get; set; }
        public Guid FolderObjectKey { get; set; }
    }
}
