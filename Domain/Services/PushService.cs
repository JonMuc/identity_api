using Domain.Interfaces;
using Domain.Models;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PushService : IPushService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        //URL API ONESIGNAL
        private readonly string _urlApiOneSignal = "https://onesignal.com/api/v1/notifications";
        private readonly string _appIdOneSignal = "8cae9b11-beab-4ffc-afdb-c6bdd3800fe6";
        private readonly string _authorizationOneSignal = $"Basic MjBhY2I0NWQtNjU4YS00YTdhLWFhODgtMDJhZDJkMDNjMGVh";


        public PushService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task EnviarPush(Usuario usuario)
        {
            var user = _usuarioRepository.GetUsuarioById(usuario.Id);
            var requestPush = new
            {
                app_id = _appIdOneSignal,
                contents = new { en = "Teste 001" },
                android_led_color = "1e4999",
                android_accent_color = "1e4999",
                include_player_ids = new string[] { usuario.PushToken }
            };

            //WebRequest
            using var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("Authorization", _authorizationOneSignal);
            client.UploadString(new Uri(_urlApiOneSignal), JsonSerializer.Serialize(requestPush));
        }

    }
}
