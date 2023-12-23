namespace PrimeValLife.Core.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using TUT.IAuth.IServices;
using TUT.IAuth.Services;

public static class ResolveDependencyBuilder
{
    public static void ResolveDependency(this IServiceCollection services)
    {
        services.AddTransient<IIdentityService, IdentityService>();
    }
}