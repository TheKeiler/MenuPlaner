namespace MenuPlanerApp.API.TenantStrategy
{
    public class TenantResolver // : ITenantResolver
    {
        /*private readonly ITenantIdentificationStrategy _tenantIdentificationStrategy;
        private readonly IMemoryCache _memoryCache;
        private readonly ITenantService _tenantService;
        

        public TenantResolver(
            ITenantIdentificationStrategy tenantIdentificationStrategy,
            IMemoryCache memoryCache,
            ITenantService tenantService
        )
        {
            this._tenantIdentificationStrategy = tenantIdentificationStrategy;
            this._memoryCache = memoryCache;
            this._tenantService = tenantService;
        }

        public async Task<Tenant> ResolveAsync(object tenantId)
        {
            Tenant tenant;
            var hostName = (string)tenantId;
            if (_memoryCache.TryGetValue(hostName, out object cached))
            {
                tenant = (Tenant)cached;
            }
            else
            {
                tenant = await _tenantService.GetTenantByHostNameAsync(hostName);
            }
            return tenant ?? new Tenant();
        }*/
    }
}