﻿using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;

namespace WebAPI.Extensions
{
    public static class ServicesExtensions
    {
    
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => /*Kullanmayacağız ama vermek zorundayız.*/
           services.AddDbContext<RepositoryContext>(optios => optios.UseSqlServer(configuration.GetConnectionString("SQLConnection")));
        
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

    }
}
