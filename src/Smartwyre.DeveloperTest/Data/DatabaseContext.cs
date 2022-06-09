using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Smartwyre.DeveloperTest.Data
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<T> GetDataSet<T>() where T : class => Set<T>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            RegisterEntityTypeConfigurations(builder);

            RegisterAttributeConfigurations(builder);
        }

        private static void RegisterAttributeConfigurations(ModelBuilder builder)
        {
            var types = TypeHelper
                .GetTypes(x => typeof(IEntity).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Distinct()
                .ToList();
            foreach (var type in types)
            {
                builder.Entity(type);
            }
        }

        private static void RegisterEntityTypeConfigurations(ModelBuilder builder)
        {
            var entityConfigurationName = typeof(IEntityTypeConfiguration<>).Name;

            var mapTypes = TypeHelper.GetTypes(x => x.GetInterface(entityConfigurationName) != null && !x.IsAbstract);

            foreach (var type in mapTypes)
            {
                dynamic mapType = Activator.CreateInstance(type);
                builder.ApplyConfiguration(mapType);
            }
        }
    }

    public static class DataServiceCollectionExtensions
    {
        public static void AddDatabaseContext(this IServiceCollection services, Action<DbContextOptionsBuilder> builder,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifeTime = ServiceLifetime.Scoped)
        {
            services.AddTransient<IDatabaseContext, DatabaseContext>();
            services.AddDbContext<DatabaseContext>(builder, contextLifetime, optionsLifeTime);
        }
    }
}