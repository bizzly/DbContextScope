using Microsoft.EntityFrameworkCore;
using System;

namespace EntityFrameworkCore.DbContextScope.NetCore
{
    public class TenantAwareAmbientDbContextLocator : ITenantAwareAmbientDbContextLocator
    {
        public TDbContext Get<TDbContext>() where TDbContext : DbContext, ITenantAware
        {
            var ambientDbContextScope = DbContextScope.GetAmbientScope();

            var tenantAwareContextScope = ambientDbContextScope as TenantAwareDbContextScope;

            if (tenantAwareContextScope?.TenantId == null)
            {
                throw new Exception("DbContext must inherit from ITenantAware");
            }

            var context = ambientDbContextScope?.DbContexts.Get<TDbContext>();

            context.TenantId = tenantAwareContextScope.TenantId;

            return context;
        }
    }
}
