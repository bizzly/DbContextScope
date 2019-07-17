using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.DbContextScope.NetCore
{
    public interface ITenantAwareAmbientDbContextLocator
    {
        TDbContext Get<TDbContext>() where TDbContext : DbContext, ITenantAware;
    }
}
