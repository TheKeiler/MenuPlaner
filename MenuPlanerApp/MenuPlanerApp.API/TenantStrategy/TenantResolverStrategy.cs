using Autofac.Multitenant;
using Microsoft.AspNetCore.Http;

namespace MenuPlanerApp.API.TenantStrategy
{
    public class TenantResolverStrategy : ITenantIdentificationStrategy
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantResolverStrategy(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public bool TryIdentifyTenant(out object tenantId)
        {
            tenantId = null;
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return false;
            var hostName = context?.Request?.Host.Value;
            tenantId = hostName;

            return tenantId != null || tenantId == (object)"";
        }
    }
}