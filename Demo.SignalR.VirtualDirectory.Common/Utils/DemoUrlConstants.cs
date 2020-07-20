namespace Demo.SignalR.VirtualDirectory.Common.Utils
{
    /// Note: 
    /// This is demo class. 
    /// In real project these values should not be hardcoded, 
    /// but instead defined in .config files or etc.
    public static class DemoUrlConstants
    {
        public static string LocalHostUrl { get => "http://localhost:5000"; }
        public static string VirtualDirectoryHubEndpoint { get => "/VirtualDirectoryHub"; }
    }
}
