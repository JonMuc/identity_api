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
            services.AddTransient<IPessoaFisicaRepository, PessoaFisicaRepository>();
            services.AddTransient<IPessoaJuridicaRepository, PessoaJuridicaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            // Services
            services.AddTransient<ICrawlingGoogleService, CrawlingGoogleService>();
            services.AddTransient<IPessoaFisicaService, PessoaFisicaService>();
            services.AddTransient<IPessoaJuridicaService, PessoaJuridicaService>();
            services.AddTransient<ICrawlingG1Service, CrawlingG1Service>();

            // Validations
            services.AddTransient<PessoaFisicaValidation>();
            services.AddTransient<PessoaJuridicaValidation>();

            // AppServices
            services.AddTransient<NoticiaAppService>();
            services.AddTransient<PessoaFisicaAppService>();
            services.AddTransient<PessoaJuridicaAppService>();
            services.AddTransient<UsuarioAppService>();
        }
    }
}
