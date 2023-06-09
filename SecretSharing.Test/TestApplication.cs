﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SecretSharing.Infrastructure.Data;

namespace SecretSharing.Test
{
    public class TestApplication : WebApplicationFactory<Program>
    {
        private static bool _databaseInitialized;
        private static object _lockObject = new object();
        private static readonly InMemoryDatabaseRoot DatabaseRoot = new InMemoryDatabaseRoot();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                lock (_lockObject)
                {
                    if (!_databaseInitialized)
                    {
                        services.RemoveAll(typeof(DbContextOptions<StoreContext>));

                        services.AddDbContext<StoreContext>(options =>
                            options.UseInMemoryDatabase("Testing", DatabaseRoot));

                        // Perform any additional database initialization, if needed

                        _databaseInitialized = true;
                    }
                }
            });
        }
    }
}
