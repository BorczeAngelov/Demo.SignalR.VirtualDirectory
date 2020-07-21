using Demo.SignalR.VirtualDirectory.Client.WPF.Utils;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel
{
    public class ExplorerVM : ObservableBase
    {
        private readonly ObservableCollection<FileVM> _fileCollection;
        private readonly ObservableCollection<FolderVM> _folderCollection;
        private EditableItemBaseVM _selectedItem;

        internal ExplorerVM(
            ObservableCollection<FileVM> fileCollection,
            ObservableCollection<FolderVM> folderCollection)
        {
            _fileCollection = fileCollection;
            _folderCollection = folderCollection;

            _fileCollection.CollectionChanged += SyncWithCollection;
            _folderCollection.CollectionChanged += SyncWithCollection;
        }

        public ObservableCollection<EditableItemBaseVM> FilesAndFolders { get; } = new ObservableCollection<EditableItemBaseVM>();
        public EditableItemBaseVM SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }

        private void SyncWithCollection(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (EditableItemBaseVM item in e.NewItems)
                {
                    Debug.Assert(!FilesAndFolders.Contains(item));
                    FilesAndFolders.Add(item);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (EditableItemBaseVM item in e.OldItems)
                {
                    Debug.Assert(FilesAndFolders.Contains(item));
                    FilesAndFolders.Remove(item);
                }
            }
        }
    }
}
