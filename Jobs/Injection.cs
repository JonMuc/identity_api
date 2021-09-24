using Data.Repository;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Services;
using Domain.Validations;
using Jobs.Job;
using Jobs.Quartz;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace Jobs
{
    public static class Injection
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Repository
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IDbConnection>(_ => new SqlConnection("Server=tcp:noticia-bd.database.windows.net,1433;Initial Catalog=noticia-base;Persist Security Info=False;User ID=user157923admin;Password=nOv5vMJ&n!@cH2l;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();

            // Services
            services.AddSingleton<IUsuarioService, UsuarioService>();

            // Validations
            services.AddSingleton<UsuarioValidation>();

            //Jobs
            services.AddSingleton<ImportarNoticiasG1PrincipaisJob>();

            var container = services.BuildServiceProvider();

            //Registrando
            QuartzConfig.Config(container);
        }
    }
}
