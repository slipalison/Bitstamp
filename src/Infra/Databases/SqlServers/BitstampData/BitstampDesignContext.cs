using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infra.Databases.SqlServers.BitstampData;

public class BitstampDesignContext : IDesignTimeDbContextFactory<BitstampContext>
{
    public BitstampContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BitstampContext>();

        //optionsBuilder.UseSqlServer(
        //    "Data Source=(localdb)\\MsSqlLocalDb;initial catalog=ProductsDbDev;Integrated Security=True; MultipleActiveResultSets=True");

        optionsBuilder.UseNpgsql("User ID=root;Password=myPassword;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;");

        return new BitstampContext(optionsBuilder.Options);
    }
}