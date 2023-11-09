using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sitanshu.Blogs.Application.Common.Interfaces;
using Sitanshu.Blogs.Infrastructure.Identity;
using Sitanshu.Blogs.Infrastructure.Persistence;
using Sitanshu.Blogs.Infrastructure.Persistence.Interceptors;
using Sitanshu.Blogs.Infrastructure.Services;

namespace Sitanshu.Blogs.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("BlogsDb"));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
    }
}
