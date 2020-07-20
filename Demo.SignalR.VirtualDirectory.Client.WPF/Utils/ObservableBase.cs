using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.Utils
{
    abstract internal class ObservableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
