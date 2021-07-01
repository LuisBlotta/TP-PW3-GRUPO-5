using Clases_auxiliares;
using Contexto_de_datos.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Servicios.Login
{
    public class LoginServicio : ILoginServicio
    {
        private _20211CTPContext context;
        private IUsuarioServicio usuarioServicio;

        public LoginServicio(_20211CTPContext ctx)
        {
            context = ctx;
            usuarioServicio = new UsuarioServicio(context);
        }

        public bool ValidarLogin(string email, string password,ISession sesion)
        {
            Usuario user = usuarioServicio.ObtenerPorEmail(email);
            if (user != null && password == user.Password)
            {
                user.FechaUltLogin = DateTime.Now;
                context.SaveChanges();

                UsuarioSesion usuarioSesion = new UsuarioSesion() { IdUsuario = user.IdUsuario, Nombre = user.Nombre, EsAdmin = user.EsAdmin };
                sesion.SetString("User", JsonSerializer.Serialize(usuarioSesion));               

                return true;
            }
            return false;
        }
    }
}
