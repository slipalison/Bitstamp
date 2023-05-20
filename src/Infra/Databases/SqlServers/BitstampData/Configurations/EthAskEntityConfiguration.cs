using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infra.Databases.SqlServers.BitstampData.Configurations;

public class EthAskEntityConfiguration : IEntityTypeConfiguration<EthAsk>
{
    public void Configure(EntityTypeBuilder<EthAsk> builder)
    {
        builder.ToTable("EthAsk");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Amount).HasPrecision(18, 8);
        builder.Property(p => p.Price).HasPrecision(18, 2);

        builder.HasIndex(p => p.InsertAt);
        builder.HasIndex(p => p.Price);
        builder.HasIndex(p => p.Amount);
    
    }

}


public class EthBidEntityConfiguration : IEntityTypeConfiguration<EthBid>
{
    public void Configure(EntityTypeBuilder<EthBid> builder)
    {
        builder.ToTable("EthBid");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Amount).HasPrecision(18,8);
        builder.Property(p => p.Price).HasPrecision(18,2);

        builder.HasIndex(p => p.InsertAt);
        builder.HasIndex(p => p.Price);
        builder.HasIndex(p => p.Amount);

    }

}
