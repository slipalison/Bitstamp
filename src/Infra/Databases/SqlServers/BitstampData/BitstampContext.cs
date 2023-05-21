using Domain.Models.AggregationBook;
using Domain.Models.AggregationMetrics;
using Domain.Models.AggregationOrder;
using Infra.Databases.SqlServers.BitstampData.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infra.Databases.SqlServers.BitstampData;

public class BitstampContext : DbContext
{
    public BitstampContext(DbContextOptions<BitstampContext> options) : base(options)
    {
    }

    public DbSet<EthBid> EthBids { get; set; }
    public DbSet<EthAsk> EthAsks { get; set; }

    public DbSet<BtcBid> BtcBids { get; set; }
    public DbSet<BtcAsk> BtcAsks { get; set; }
    public DbSet<Metric> Metrics { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EthBidEntityConfiguration).Assembly);

        modelBuilder.Ignore<Metric>();
        modelBuilder.Ignore<OrderItem>();
        modelBuilder.Entity<Metric>().HasNoKey();
        modelBuilder.Entity<OrderItem>().HasNoKey();
    }
}