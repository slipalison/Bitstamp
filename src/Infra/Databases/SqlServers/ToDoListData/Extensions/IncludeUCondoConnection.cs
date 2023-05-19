﻿using Domain.Contracts.Repositories;
using Infra.Databases.SqlServers.BitstampData.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Databases.SqlServers.BitstampData.Extensions;

public static class IncludeUCondoConnection
{
    public static IServiceCollection AddUCondoContext(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .AddDbContextPool<BitstampContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"))).AddRepositories();

        return serviceCollection;
    }

    private static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBitstampRepository, BitstampRepository>();
    }

    public static void ExecuteMigartions(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<BitstampContext>();
        dbContext.Database.EnsureCreated();
    }
}