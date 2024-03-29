﻿using Data.Repository;
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
            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            services.AddSingleton<IPerfilUsuarioRepository, PerfilUsuarioRepository>();

            // Services
            services.AddSingleton<INoticiaService, NoticiaService>();

            // Validations
            services.AddSingleton<NoticiaValidation>();
            services.AddSingleton<UsuarioValidation>();
            services.AddSingleton<PerfilUsuarioValidation>();

            //Jobs
            services.AddSingleton<ImportarNoticiasG1PrincipaisJob>();
            services.AddSingleton<ImportarNoticiasGooglePrincipaisJob>();
            services.AddSingleton<ImportarNoticiasGoogleCienciaTecnologiaJob>();
            services.AddSingleton<ImportarNoticiasGoogleMundoJob>();
            services.AddSingleton<ImportarNoticiasGoogleNegociosJob>();
            services.AddSingleton<ImportarNoticiasGoogleEntretenimentoJob>();
            services.AddSingleton<ImportarNoticiasGoogleEsportesJob>();
            services.AddSingleton<ImportarNoticiasIGJob>();
            services.AddSingleton<ImportarNoticiasG1MundoJob>();
            services.AddSingleton<ImportarNoticiasG1EconomiaJob>();
            services.AddSingleton<ImportarNoticiasG1TecnologiaJob>();
            services.AddSingleton<ImportarNoticiasG1CienciaJob>();
            services.AddSingleton<ImportarNoticiasG1EntretenimentoJob>();

            var container = services.BuildServiceProvider();

            //Registrando
            QuartzConfig.Config(container);
        }
    }
}
