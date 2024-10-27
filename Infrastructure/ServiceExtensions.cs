using Application.IRepository.IFileRepository;
using Infrastructure.Context;
using Infrastructure.Repository.FileRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    // Pour faire de l'injection de dépendance.

    public static class ServiceExtensions
    {
        public static void InfrastructureConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(/*options => options.UseSqlServer(connection)*/);
            services.AddScoped<IFileRepository, FileRepository>();
        }
    }
}
