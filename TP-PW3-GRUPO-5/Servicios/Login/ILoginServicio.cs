using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Login
{
    public interface ILoginServicio
    {
        public bool ValidarLogin(string email, string password);
    }
}
