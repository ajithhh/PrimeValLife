﻿namespace PrimeValLife.Core.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using PrimeValLife.Core.IRepository;
using PrimeValLife.Core.IServices;
using PrimeValLife.Core.Repository;
using PrimeValLife.Core.Services;
using TUT.IAuth.IServices;
using TUT.IAuth.Services;

public static class ResolveDependencyBuilder
{
    public static void ResolveDependency(this IServiceCollection services)
    {
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IAuditRepository,AuditRepository>();
        services.AddTransient<IAuditService, AuditService>();
    }
}