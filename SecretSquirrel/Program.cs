using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using SecretSquirrel.ViewModels;
using SecretSquirrel.Views;

namespace SecretSquirrel
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>();
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToDebug();
    }
}
