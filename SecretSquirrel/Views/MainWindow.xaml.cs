using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SecretSquirrel.ViewModels;

namespace SecretSquirrel.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoaderPortableXaml.Load(this);
        }
    }
}
