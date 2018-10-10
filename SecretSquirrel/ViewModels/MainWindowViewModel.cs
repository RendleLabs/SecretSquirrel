using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using k8s;
using k8s.KubeConfigModels;
using ReactiveUI;
using SecretSquirrel.Hacky;

namespace SecretSquirrel.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private K8SConfiguration _config;
        private string _currentContext;
        private string[] _contexts;

        public MainWindowViewModel()
        {
            LoadKubeConfigAsync();
            PropertyChanged += OnPropertyChanged;
        }

        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(CurrentContext)) && !string.IsNullOrWhiteSpace(CurrentContext))
            {
                await LoadSecrets();
            }
        }

        private async Task LoadSecrets()
        {
            System.Console.WriteLine(nameof(LoadSecrets));
            System.Console.WriteLine($"BuildConfig using {CurrentContext}");
            var clientConfig = _config.CreateClientConfiguration(CurrentContext);
            System.Console.WriteLine("Create client...");
            var client = new Kubernetes(clientConfig);
            System.Console.WriteLine("Starting request...");
            var secrets = await client.ListSecretForAllNamespacesWithHttpMessagesAsync();
            System.Console.WriteLine("Request complete.");
            if (secrets.Response.IsSuccessStatusCode)
            {
                foreach (var secret in secrets.Body.Items)
                {
                    var svm = new SecretViewModel
                    {
                        Namespace = secret.Metadata.NamespaceProperty,
                        Name = secret.Metadata.Name,
                    };
                    Secrets.Add(svm);
                }
            }
        }

        private async void LoadKubeConfigAsync()
        {
            try
            {
                Config = await KubernetesClientConfiguration.LoadKubeConfigAsync();
                Contexts = Config.Contexts.Select(c => c.Name).ToArray();
                CurrentContext = Config.CurrentContext ?? Contexts.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public K8SConfiguration Config
        {
            get => _config;
            set => this.RaiseAndSetIfChanged(ref _config, value);
        }

        public string CurrentContext
        {
            get => _currentContext;
            set => this.RaiseAndSetIfChanged(ref _currentContext, value);
        }

        public string[] Contexts
        {
            get => _contexts;
            set => this.RaiseAndSetIfChanged(ref _contexts, value);
        }

        public ObservableCollection<SecretViewModel> Secrets { get; } = new ObservableCollection<SecretViewModel>();
    }

    public class SecretViewModel : ViewModelBase
    {
        public string Namespace { get; set; }
        public string Name { get; set; }
    }
}
