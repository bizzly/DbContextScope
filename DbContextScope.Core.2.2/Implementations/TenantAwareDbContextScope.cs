using System.Data;

namespace EntityFrameworkCore.DbContextScope.NetCore
{
    public class TenantAwareDbContextScope : DbContextScope
    {
        public int TenantId { get; set; }

        public TenantAwareDbContextScope(int tenantId, DbContextScopeOption joiningOption, bool readOnly, IsolationLevel? isolationLevel, IDbContextFactory dbContextFactory = null) :
            base(joiningOption, readOnly, isolationLevel, dbContextFactory)
        {
            TenantId = tenantId;
        }
    }

    public class TenantAwareDbContextReadOnlyScope : DbContextReadOnlyScope
    {
        public int TenantId { get; set; }

        public TenantAwareDbContextReadOnlyScope(int tenantId, DbContextScopeOption joiningOption, IsolationLevel? isolationLevel, IDbContextFactory dbContextFactory = null) :
            base(joiningOption, isolationLevel, dbContextFactory)
        {
            TenantId = tenantId;
        }
    }
}
