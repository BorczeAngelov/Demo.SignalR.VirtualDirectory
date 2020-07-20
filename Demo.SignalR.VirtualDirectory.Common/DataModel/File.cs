using System;

namespace Demo.SignalR.VirtualDirectory.Common.DataModel
{
    public class File
    {
        public enum FileType
        {
            txt,
            png,
            mp4
        }

        public string Name { get; set; }
        public FileType Type { get; set; }
        public Guid ObjectKey { get; set; }
        public Guid FolderObjectKey { get; set; }
    }
}
