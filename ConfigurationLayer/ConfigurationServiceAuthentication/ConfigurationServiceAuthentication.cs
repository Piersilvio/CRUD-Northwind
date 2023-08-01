using ConfigurationLayer.MapAppSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.ConfigurationServiceAuthentication
{
    public class ConfigurationServiceAuthentication : IConfigurationServiceAuthentication
    {
        private static ConfigurationServiceAuthentication? configurationServiceAuthentication; //istanza
        private static readonly object _lock = new();
        private static JWTAuthSettings? jWTAuthSettingsOptions;

        public ConfigurationServiceAuthentication() 
        {
        }

        public static ConfigurationServiceAuthentication GetIstance(IOptions<JWTAuthSettings> option)
        {
            if (configurationServiceAuthentication is null)
            {
                lock(_lock)
                {
                    if(configurationServiceAuthentication is null)
                    {
                        jWTAuthSettingsOptions = option.Value;
                        configurationServiceAuthentication = new ConfigurationServiceAuthentication();
                    }
                    else
                    {
                        throw new InvalidOperationException("error");
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("error");
            }

            return configurationServiceAuthentication;
        }

        public string OnGetValidAudience() => jWTAuthSettingsOptions.ValidAudience;
        public string OnGetValidIssuer() => jWTAuthSettingsOptions.ValidIssuer;
        public string OnGetSecretKey() => jWTAuthSettingsOptions.Secret;
    }
}
