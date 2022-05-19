using Application.Abstract.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using Persistence.Options;
using Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence
{
    public static class Startup
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, PersistenceOptions persistenceOptions)
        {
            if (persistenceOptions is null)
            {
                throw new ArgumentNullException(nameof(persistenceOptions));
            }

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {

                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

                options.UseNpgsql(persistenceOptions.ConnectionString, b =>
                {
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                });


                options.UseSnakeCaseNamingConvention();

                ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                options.UseLoggerFactory(loggerFactory)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
            });

            services.AddScoped(typeof(IJournaledGenericRepository<>), typeof(JournaledGenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
