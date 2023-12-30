namespace PrimeValLife.Core.Infrastructure;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PrimeValLife.Core.IRepository;
using PrimeValLife.Core.IServices;
using PrimeValLife.Core.Repository;
using PrimeValLife.Core.Services;
using TUT.IAuth;
using TUT.IAuth.IServices;
using TUT.IAuth.Services;

public static class ResolveDependencyBuilder
{
    public static void ResolveDependency(this IServiceCollection services)
    {        
        services.AddTransient<IAuditRepository,AuditRepository>();
        services.AddTransient<IAuditService, AuditService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();
    }
}