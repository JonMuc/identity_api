using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ModelsDto
{
    public class LoginResponse : Usuario
    {
        public string Token { get; set; }
    }
}
