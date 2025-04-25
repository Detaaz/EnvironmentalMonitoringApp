using Microsoft.EntityFrameworkCore;
using EnvironmentalMonitoringApp.Models;

namespace EnvironmentalMonitoringApp.Data;
public class EnvMonitorDbContext : DbContext
{
    public EnvMonitorDbContext()
    { }
    public EnvMonitorDbContext(DbContextOptions<EnvMonitorDbContext> options) : base(options)
    {
    }
    public DbSet<Login> Logins { get; set; }
    public DbSet<Users> Users { get; set; }
}
