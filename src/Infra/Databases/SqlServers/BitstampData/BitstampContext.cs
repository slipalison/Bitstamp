using Microsoft.EntityFrameworkCore;
using Infra.Databases.SqlServers.BitstampData.Configurations;

namespace Infra.Databases.SqlServers.BitstampData;

public class BitstampContext : DbContext
{
    public BitstampContext(DbContextOptions<BitstampContext> options) : base(options)
    {
    }

    public DbSet<ToDoItemEntity> ToDos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BitstampEntityConfiguration());
    }
}
