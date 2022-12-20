using Medir.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection
            services, IConfiguration configuration)
        {
            var connectionString = configuration["PostgreSQLConnection"];
            services.AddDbContext<MedirDbContext>(op =>
            {
                op.UseNpgsql(connectionString);
            });            
            services.AddScoped<IMedirDbContext>(provider =>
                provider.GetService<MedirDbContext>());
            return services;
        }
    }
}
