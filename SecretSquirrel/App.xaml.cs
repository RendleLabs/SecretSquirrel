using Avalonia;
using Avalonia.Markup.Xaml;
using k8s;
using k8s.KubeConfigModels;

namespace SecretSquirrel
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoaderPortableXaml.Load(this);
        }
    }
}
