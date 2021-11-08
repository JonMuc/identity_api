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
            services.AddSingleton<IDbConnection>(_ => new SqlConnection("Server=tcp:noticia-bd.database.windows.net,1433;Initial Catalog=noticia-base;Persist Security Info=False;User ID=user157923admin;Password=nOv5vMJ&n!@cH2l;MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            services.AddSingleton<INoticiaRepository, NoticiaRepository>();

            // Services
            services.AddSingleton<INoticiaService, NoticiaService>();

            // Validations
            services.AddSingleton<NoticiaValidation>();

            //Jobs
            services.AddSingleton<ImportarNoticiasG1PrincipaisJob>();
            services.AddSingleton<ImportarNoticiasGooglePrincipaisJob>();
            services.AddSingleton<ImportarNoticiasGoogleCienciaTecnologiaJob>();
            services.AddSingleton<ImportarNoticiasGoogleMundoJob>();
            services.AddSingleton<ImportarNoticiasGoogleNegociosJob>();
            services.AddSingleton<ImportarNoticiasGoogleEntretenimentoJob>();
            services.AddSingleton<ImportarNoticiasGoogleEsportesJob>();
            services.AddSingleton<ImportarNoticiasIGJob>();

            var container = services.BuildServiceProvider();

            //Registrando
            QuartzConfig.Config(container);
        }
    }
}
