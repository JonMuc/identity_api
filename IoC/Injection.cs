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
            services.AddTransient<IDbConnection>(_ => new SqlConnection("Data Source=JOAOLUIZ;Initial Catalog=identityDB;Integrated Security=True"));
            services.AddTransient<IPessoaFisicaRepository, PessoaFisicaRepository>();
            services.AddTransient<IPessoaJuridicaRepository, PessoaJuridicaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            // Services
            services.AddTransient<IPessoaFisicaService, PessoaFisicaService>();
            services.AddTransient<IPessoaJuridicaService, PessoaJuridicaService>();

            // Validations
            services.AddTransient<PessoaFisicaValidation>();
            services.AddTransient<PessoaJuridicaValidation>();

            // AppServices
            services.AddTransient<PessoaFisicaAppService>();
            services.AddTransient<PessoaJuridicaAppService>();
        }
    }
}
