using eCashbook.Infrastructure.Abstract;
using eCashbook.Infrastructure.Concrete;

namespace eCashbook.WebApi.Extensions;

public static class ServiceExtension
{
  public static void ConfigureDIServices(this IServiceCollection services)
  {
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddScoped<IPasswordHasher, PasswordHasher>();
    services.AddScoped<JwtService>();
    services.AddTransient<IUserService, UserService>();
    // services.AddTransient<ISalesPersonService, SalesPersonService>();
  }
}
