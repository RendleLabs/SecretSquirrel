using System.Reflection;
using k8s;
using k8s.KubeConfigModels;

namespace SecretSquirrel.Hacky
{
    public static class K8SConfigurationExtensions
    {
        public static KubernetesClientConfiguration CreateClientConfiguration(this K8SConfiguration config, string currentContext = null, string masterUrl = null)
        {
            var method = typeof(KubernetesClientConfiguration).GetMethod("GetKubernetesClientConfiguration", BindingFlags.NonPublic | BindingFlags.Static);
            return method.Invoke(null, new object[] { currentContext, masterUrl, config }) as KubernetesClientConfiguration;
        }
    }
}