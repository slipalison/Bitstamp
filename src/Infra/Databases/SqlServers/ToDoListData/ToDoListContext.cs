using Domain.ToDoItems;
using Infra.Databases.SqlServers.BitstampData.Configurations;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618

namespace Infra.Databases.SqlServers.BitstampData;

public class BitstampContext : DbContext
{
    public BitstampContext(DbContextOptions<BitstampContext> options) : base(options)
    {
    }

    public DbSet<ToDoItemEntity> ToDos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ToDoItemEntityConfiguration());
    }
}