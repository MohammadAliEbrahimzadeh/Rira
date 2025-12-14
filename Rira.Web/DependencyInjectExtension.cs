using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Rira.Application.Contracts;
using Rira.Application.Implementations;
using Rira.Application.Mappers;
using Rira.Persistence;
using Rira.Persistence.Context;
using System.Reflection;

namespace Rira.Web;

public static class DependencyInjectExtension
{

    internal static IServiceCollection InjectUnitOfWork(this IServiceCollection services) =>
     services.AddScoped<IUnitOfWork, UnitOfWork>();

    public static IServiceCollection InjectContext(this IServiceCollection services, IConfiguration configuration) =>
      services.AddDbContextFactory<RiraDbContext>(optionsAction =>
      {
          optionsAction.UseSqlServer(
              configuration.GetConnectionString("DefaultConnection"),
              sqlOptions =>
              {
                  sqlOptions.EnableRetryOnFailure(
                      maxRetryCount: 5,
                      maxRetryDelay: TimeSpan.FromSeconds(10),
                      errorNumbersToAdd: null
                  );
              });
      });

    public static IServiceCollection InjectGrpc(this IServiceCollection services) =>
        services.AddGrpc(options =>
        {
            options.Interceptors.Add<ExceptionInterceptor>();
        }).Services;

    internal static IServiceCollection InjectServices(this IServiceCollection services) =>
        services.AddScoped<IUserService, UserService>();


    internal static IServiceCollection InjectMapster(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(UserMapper).Assembly);

        return services;
    }

}
