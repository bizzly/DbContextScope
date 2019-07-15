using Demo.NetCore2._2;
using Microsoft.EntityFrameworkCore;
using Numero3.EntityFramework.Demo.DomainModel.NetCore;

namespace Numero3.EntityFramework.Demo.DatabaseContext.NetCore
{
    public class UserManagementDbContext : DbContext {
        // Map our 'User' model by convention
        private readonly IIdentity _identity;

        public DbSet<User> Users { get; set; }

        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
            : base(options) { }

        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options, IIdentity identity)
            : this(options)
        {
            _identity = identity;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(builder => {
                builder.Property(m => m.Name).IsRequired();
                builder.Property(m => m.Email).IsRequired();
                builder.Property(m => m.TenantId).IsRequired();
            });

            if (_identity != null)
            {
                modelBuilder.Entity<User>().HasQueryFilter(b => EF.Property<int>(b, "TenantId") == _identity.TenantId);
            }
        }
    }
}
