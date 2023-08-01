using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.ConfigurationServiceAuthentication
{
    public interface IConfigurationServiceAuthentication
    {
        public string OnGetValidAudience();
        public string OnGetValidIssuer();
    }
}
