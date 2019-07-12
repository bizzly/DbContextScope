using Microsoft.EntityFrameworkCore;
using Numero3.EntityFramework.Demo.DomainModel;
using Numero3.EntityFramework.Demo.DomainModel.NetCore;

namespace Numero3.EntityFramework.Demo.DatabaseContext.NetCore
{
    public class UserManagementDbContext : DbContext {
        // Map our 'User' model by convention
        public DbSet<User> Users { get; set; }

        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(builder => {
                builder.Property(m => m.Name).IsRequired();
                builder.Property(m => m.Email).IsRequired();
            });
        }
    }
}
