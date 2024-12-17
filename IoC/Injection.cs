using Application.AppServices;
using Data.Repository;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Services;
using Domain.Validations;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace IoC
{
    public static class Injection
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDbConnection>(_ => new SqlConnection("Server=tcp:noticia-bd.database.windows.net,1433;Initial Catalog=noticia-base;Persist Security Info=False;User ID=user157923admin;Password=nOv5vMJ&n!@cH2l;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();


            // Services
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IAwsApiService, AwsApiService>();
            services.AddTransient<IPushService, PushService>();

            // Validations
            //services.AddTransient<LoginValidation>();
            services.AddTransient<UsuarioValidation>();

            // AppServices
            services.AddTransient<LoginAppService>();
            services.AddTransient<UsuarioAppService>();
        }
    }
}
