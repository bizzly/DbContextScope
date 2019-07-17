namespace EntityFrameworkCore.DbContextScope.NetCore
{
    public interface ITenantAware
    {
        int TenantId { get; set; }
    }
}
