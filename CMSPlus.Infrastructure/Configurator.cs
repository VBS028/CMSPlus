using CMSPlus.Domain.Interfaces;
using CMSPlus.Domain.Interfaces.Repositories;
using CMSPlus.Domain.Persistence;
using CMSPlus.Domain.Repositories;
using CMSPlus.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSPlus.Infrastructure
{
    public static class Configurator
    {
        public static void AddInfrastructure(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(connectionString));
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<IBlogCommentsRepository, BlogCommentsRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void AddMigrations(this IServiceCollection services, string? connectionString)
        {
            services.AddSingleton<IMigrationService>(new MigrationService(connectionString));
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var migrationService = serviceProvider.GetRequiredService<IMigrationService>();
                migrationService.Migrate();
            }
        }
    }
}
