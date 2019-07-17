using System;
using System.Data;

namespace EntityFrameworkCore.DbContextScope.NetCore
{
    public class TenantAwareDbContextScopeFactory : ITenantAwareDbContextScopeFactory
    {
        private readonly IDbContextFactory _dbContextFactory;

        public TenantAwareDbContextScopeFactory(IDbContextFactory dbContextFactory = null)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IDbContextScope Create(int tenantId, DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            return new TenantAwareDbContextScope(
                tenantId,
                joiningOption: joiningOption,
                readOnly: false,
                isolationLevel: null,
                dbContextFactory: _dbContextFactory);
        }

        public IDbContextReadOnlyScope CreateReadOnly(int tenantId, DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            return new TenantAwareDbContextReadOnlyScope(
                tenantId,
                joiningOption: joiningOption,
                isolationLevel: null,
                dbContextFactory: _dbContextFactory);
        }

        public IDbContextReadOnlyScope CreateReadOnlyWithTransaction(int tenantId, IsolationLevel isolationLevel)
        {
            return new TenantAwareDbContextReadOnlyScope(
                tenantId,
                joiningOption: DbContextScopeOption.ForceCreateNew,
                isolationLevel: isolationLevel,
                dbContextFactory: _dbContextFactory);
        }

        public IDbContextScope CreateWithTransaction(int tenantId, IsolationLevel isolationLevel)
        {
            return new TenantAwareDbContextScope(
                tenantId,
                joiningOption: DbContextScopeOption.ForceCreateNew,
                readOnly: false,
                isolationLevel: isolationLevel,
                dbContextFactory: _dbContextFactory);
        }

        public IDisposable SuppressAmbientContext()
        {
            return new AmbientContextSuppressor();
        }
    }
}
