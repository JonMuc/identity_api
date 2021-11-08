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
            services.AddTransient<INoticiaRepository, NoticiaRepository>();
            services.AddTransient<INoticiaFavoritoRepository, NoticiaFavoritoRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IComentarioRepository, ComentarioRepository>();
            services.AddTransient<IAvaliacaoRepository, AvaliacaoRepository>();

            // Services
            services.AddTransient<ICrawlingGoogleService, CrawlingGoogleService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<ICrawlingG1Service, CrawlingG1Service>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<INoticiaService, NoticiaService>();
            services.AddTransient<INoticiaFavoritoService, NoticiaFavoritoService>();
            services.AddTransient<IChatService, ChatService>();
            services.AddTransient<IAwsApiService, AwsApiService>();
            services.AddTransient<IComentarioService, ComentarioService>();
            services.AddTransient<IAvaliacaoService, AvaliacaoService>();

            // Validations
            services.AddTransient<LoginValidation>();
            services.AddTransient<UsuarioValidation>();
            services.AddTransient<NoticiaValidation>();
            services.AddTransient<NoticiaFavoritoValidation>();
            services.AddTransient<ChatValidation>();
            services.AddTransient<ComentarioValidation>();

            // AppServices
            services.AddTransient<NoticiaAppService>();
            services.AddTransient<NoticiaFavoritoAppService>();
            services.AddTransient<LoginAppService>();
            services.AddTransient<UsuarioAppService>();
            services.AddTransient<ComentarioAppService>();
            services.AddTransient<ChatAppService>();
            services.AddTransient<AvaliacaoAppService>();
        }
    }
}
