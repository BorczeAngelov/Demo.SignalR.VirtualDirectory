using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using System;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public abstract class EditableItemBaseVM : ObservableBase
    {
        public Type Type { get => GetType(); }
    }
}
