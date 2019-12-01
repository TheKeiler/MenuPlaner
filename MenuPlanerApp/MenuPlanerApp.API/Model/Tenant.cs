using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuPlanerApp.API.Model
{
    public class Tenant
    {
        public string TenantCode { get; set; }
        public string HostName { get; set; }
        public string ConnectionString { get; set; }
    }
}
