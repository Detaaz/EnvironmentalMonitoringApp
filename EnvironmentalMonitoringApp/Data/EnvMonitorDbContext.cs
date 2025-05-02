using Microsoft.EntityFrameworkCore;
using EnvironmentalMonitoringApp.Models;

namespace EnvironmentalMonitoringApp.Data;
public class EnvMonitorDbContext : DbContext
{
    
    public EnvMonitorDbContext(DbContextOptions<EnvMonitorDbContext> options) : base(options)
    {
    }
    public DbSet<Login> Logins { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<Measurement> Measurements { get; set; }
    public DbSet<PhysicalQuantity> PhysicalQuantities { get; set; }
}
