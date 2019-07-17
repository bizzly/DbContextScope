using EntityFrameworkCore.DbContextScope.NetCore;
using Microsoft.EntityFrameworkCore;
using Numero3.EntityFramework.Demo.DomainModel.NetCore;

namespace Numero3.EntityFramework.Demo.DatabaseContext.NetCore
{
    public class UserManagementDbContext : DbContext, ITenantAware
    {
        // Map our 'User' model by convention
        public DbSet<User> Users { get; set; }
        public int TenantId { get; set; }
        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(builder => {
                builder.Property(m => m.Name).IsRequired();
                builder.Property(m => m.Email).IsRequired();
                builder.Property(m => m.TenantId).IsRequired();
            });

            modelBuilder.Entity<User>().HasQueryFilter(b => EF.Property<int>(b, "TenantId") == TenantId);

        }
    }
}
