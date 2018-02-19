using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace KeyVaultTest
{
    public class GenericStartupFixture
    {
        public string EnvironmentContext { get; set; }
        public IConfigurationRoot Configuration { get; set; }

        public GenericStartupFixture()
        {
            LoadEnvironmentSettings();
            HydrateConfigurationSettings();
        }

        private void LoadEnvironmentSettings()
        {
            try
            {
                var launchSettingsPath = Path.Combine("Properties", "launchSettings.json");

                using (var file = File.OpenText(launchSettingsPath))
                {
                    var reader = new JsonTextReader(file);
                    var jObject = JObject.Load(reader);

                    var variables = jObject
                        .GetValue("profiles")
                        .SelectMany(profiles => profiles.Children())
                        .SelectMany(profile => profile.Children<JProperty>())
                        .Where(prop => prop.Name == "environmentVariables")
                        .SelectMany(prop => prop.Value.Children<JProperty>())
                        .ToList();

                    foreach (var variable in variables)
                    {
                        Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
                    }

                    EnvironmentContext = Environment.GetEnvironmentVariable("someenvironmentname");

                }
            }
            catch { }
        }

        private void HydrateConfigurationSettings()
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{EnvironmentContext}.json", optional: true);
            var cfg = configurationBuilder.Build();

            configurationBuilder.AddAzureKeyVault(cfg["KeyVaultUri"], keyVaultClient, new DefaultKeyVaultSecretManager()).AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();
        }
    }
}
