using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Infrastructure.Data;
using LibraryManagementSystem.Infrastructure.Persistence;
using LibraryManagementSystem.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepositery, BookRepository>();
            services.AddScoped<DatabaseInitializer>();
            return services;
        }
    }

}
