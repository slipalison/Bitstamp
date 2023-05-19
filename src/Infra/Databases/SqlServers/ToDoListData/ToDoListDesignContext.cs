using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infra.Databases.SqlServers.BitstampData;

public class BitstampDesignContext : IDesignTimeDbContextFactory<BitstampContext>
{
    public BitstampContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BitstampContext>();

        optionsBuilder.UseSqlServer(
            "Data Source=(localdb)\\MsSqlLocalDb;initial catalog=ProductsDbDev;Integrated Security=True; MultipleActiveResultSets=True");

        return new BitstampContext(optionsBuilder.Options);
    }
}