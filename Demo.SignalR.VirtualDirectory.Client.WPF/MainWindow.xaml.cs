using Demo.SignalR.VirtualDirectory.Client.WPF.ViewModel;
using System.Windows;

namespace Demo.SignalR.VirtualDirectory.Client.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }
    }
}
