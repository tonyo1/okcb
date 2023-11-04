using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Name_Here
{
    public interface IConfiguration
    {
        string GoogleClientId { get; set; }
        string GoogleSecret { get; set; }
        string AzureEndpoint { get; set; }
        string AzureSecret { get; set; }
        string AzureDBName { get; set; }

    }
}
