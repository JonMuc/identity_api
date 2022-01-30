using Domain.Models;
using Domain.Models.Enums;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Util.Jwt
{
    public class JWTManager
    {
        private static string Secret = "ZGoweUptazlOMnBJYXpsc1prMWlUekl4Sm1ROVdWZHJPV1ZFVW1wVk1GcFdUWHBSYldOSGJ6bE5RUzB0Sm5NOVkyOXVjM1Z0WlhKelpXTnlaWFFtZUQwME5B4oCT";
        public static string GenerateToken(Usuario usuario)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Secret);
            var base64 = System.Convert.ToBase64String(plainTextBytes);
            byte[] key = Convert.FromBase64String(base64);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("Id", usuario.Id.ToString());
            values.Add("Email", usuario.Email);
            values.Add("Nome", usuario.Nome);
            values.Add("Senha", usuario.Senha);
            values.Add("PushToken", usuario.PushToken == null ? "" : usuario.PushToken);
            values.Add("Telefone", usuario.Telefone == null ? "" : usuario.Telefone);
            values.Add("StatusRegistro", usuario.StatusRegistro.ToString());
            values.Add("Foto", usuario.Foto == null ? "" : usuario.Foto);

            var descriptor = GenerateDescriptor(values, securityKey);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public static string RegenerateToken(string token)
        {
            var usuario = ValidateToken(token);

            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("Id", usuario.Id.ToString());
            values.Add("Email", usuario.Email);
            values.Add("Nome", usuario.Nome);
            values.Add("Senha", usuario.Senha);
            values.Add("PushToken", usuario.PushToken);
            values.Add("Telefone", usuario.Telefone);
            values.Add("StatusRegistro", usuario.StatusRegistro.ToString());
            values.Add("Foto", usuario.Foto);

            var descriptor = GenerateDescriptor(values, securityKey);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken newtoken = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(newtoken);
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Secret);
                var base64 = System.Convert.ToBase64String(plainTextBytes);
                byte[] key = Convert.FromBase64String(base64);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public static Usuario ValidateToken(string token)
        {
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim idClam = identity.FindFirst("Id");
            Claim emailClam = identity.FindFirst("Email");
            Claim nomeClam = identity.FindFirst("Nome");
            Claim senhaClam = identity.FindFirst("Senha");
            Claim pushTokenClam = identity.FindFirst("PushToken");
            Claim telefoneClam = identity.FindFirst("Telefone");
            Claim statusRegistroClam = identity.FindFirst("StatusRegistro");
            Claim fotoClam = identity.FindFirst("Foto");

            int id = int.TryParse(idClam?.Value ?? string.Empty, out id) ? id : 0;
            int statusRegistro = int.TryParse(statusRegistroClam?.Value ?? string.Empty, out statusRegistro) ? statusRegistro : 0;

            return new Usuario()
            {
                Id = id,
                Email = emailClam?.Value ?? string.Empty,
                Nome = nomeClam?.Value ?? string.Empty,
                Senha = senhaClam?.Value ?? string.Empty,
                PushToken = pushTokenClam?.Value ?? string.Empty,
                Telefone = telefoneClam?.Value ?? string.Empty,
                StatusRegistro = (StatusRegistro)statusRegistro,
                Foto = fotoClam?.Value ?? string.Empty,
            };
        }

        private static SecurityTokenDescriptor GenerateDescriptor(Dictionary<string, string> values, SymmetricSecurityKey securityKey)
        {
            List<Claim> claims = new List<Claim>();
            foreach (var item in values)
                claims.Add(new Claim(item.Key, item.Value));
            var asda = DateTime.Now.AddMinutes(2);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(30),//TODO ALTERAR TEMPO UTIL DO TOKEN
                SigningCredentials = new SigningCredentials(securityKey,
               SecurityAlgorithms.HmacSha256Signature)
            };

            return descriptor;
        }

    }
}
