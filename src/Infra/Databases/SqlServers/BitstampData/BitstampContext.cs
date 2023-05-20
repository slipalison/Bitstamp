using Microsoft.EntityFrameworkCore;
using Infra.Databases.SqlServers.BitstampData.Configurations;
using Domain.Models;

namespace Infra.Databases.SqlServers.BitstampData;

public class BitstampContext : DbContext
{
    public BitstampContext(DbContextOptions<BitstampContext> options) : base(options)
    {
    }

    public DbSet<EthBid> EthBids { get; set; }
    public DbSet<EthAsk> EthAsks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EthBidEntityConfiguration).Assembly);
    }
}
