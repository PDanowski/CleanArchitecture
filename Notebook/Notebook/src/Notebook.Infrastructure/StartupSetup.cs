using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notebook.Infrastructure.Data;

namespace Notebook.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString)); // will be created in web project root
    }
}