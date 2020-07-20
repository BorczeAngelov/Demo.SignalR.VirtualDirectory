using System;

namespace Demo.SignalR.VirtualDirectory.Common.DataModel
{
    public class Folder
    {
        public string Name { get; set; }
        public Guid ObjectKey { get; set; }
        public Guid ParentFolderObjectKey { get; set; }
    }
}
