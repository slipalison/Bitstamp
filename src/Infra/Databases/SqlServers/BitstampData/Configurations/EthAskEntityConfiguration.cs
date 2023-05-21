using Domain.Models.AggregationBook;
using Domain.Models.AggregationMetrics;
using Domain.Models.AggregationOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infra.Databases.SqlServers.BitstampData.Configurations;

public class EthAskEntityConfiguration : IEntityTypeConfiguration<EthAsk>
{
    public void Configure(EntityTypeBuilder<EthAsk> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Amount).HasPrecision(18, 8);
        builder.Property(p => p.Price).HasPrecision(18, 2);

        builder.HasIndex(p => p.InsertAt);
        builder.HasIndex(p => p.Price);
        builder.HasIndex(p => p.Amount);


        builder.HasIndex(p => new { p.Amount, p.Price }).IsUnique();
    }

}


public class EthBidEntityConfiguration : IEntityTypeConfiguration<EthBid>
{
    public void Configure(EntityTypeBuilder<EthBid> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Amount).HasPrecision(18, 8);
        builder.Property(p => p.Price).HasPrecision(18, 2);

        builder.HasIndex(p => p.InsertAt);
        builder.HasIndex(p => p.Price);
        builder.HasIndex(p => p.Amount);

        builder.HasIndex(p => new { p.Amount, p.Price }).IsUnique();

    }
}
public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Amount).HasPrecision(18, 8);
        builder.Property(p => p.Price).HasPrecision(18, 2);
        builder.Property(p => p.Type).HasConversion<string>();
        builder.Property(p => p.Crypto).HasConversion<string>();
        builder.Property(p => p.AmountTotal).HasPrecision(18, 8);
        builder.Property(p => p.Stock).HasConversion(
                attr => JsonConvert.SerializeObject(attr),
                json => JsonConvert.DeserializeObject<OrderItem[]>(json)
            );

        builder.HasIndex(p => p.InsertAt);

    }
}


public class BtcBidEntityConfiguration : IEntityTypeConfiguration<BtcBid>
{
    public void Configure(EntityTypeBuilder<BtcBid> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Amount).HasPrecision(18, 8);
        builder.Property(p => p.Price).HasPrecision(18, 2);

        builder.HasIndex(p => p.InsertAt);
        builder.HasIndex(p => p.Price);
        builder.HasIndex(p => p.Amount);

        builder.HasIndex(p => new { p.Amount, p.Price }).IsUnique();

    }
}


public class BtcAskEntityConfiguration : IEntityTypeConfiguration<BtcAsk>
{
    public void Configure(EntityTypeBuilder<BtcAsk> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Amount).HasPrecision(18, 8);
        builder.Property(p => p.Price).HasPrecision(18, 2);

        builder.HasIndex(p => p.InsertAt);
        builder.HasIndex(p => p.Price);
        builder.HasIndex(p => p.Amount);

        builder.HasIndex(p => new { p.Amount, p.Price }).IsUnique();

    }
}
