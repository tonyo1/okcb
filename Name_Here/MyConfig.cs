using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Name_Here
{
    public class MyConfig : IConfiguration
    {
        public MyConfig() { }

        public string GoogleClientId { get; set; }
        public string GoogleSecret { get; set; }
        public string AzureEndpoint { get; set; }
        public string AzureSecret { get; set; }
        public string AzureDBName { get; set; }
    }
}