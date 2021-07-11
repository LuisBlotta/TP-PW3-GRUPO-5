using Clases_auxiliares;
using Contexto_de_datos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using Servicios.Login;
using Servicios.Session;
using System.Text.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private _20211CTPContext context;
        private ILoginServicio loginServicio;
        private IUsuarioServicio usuarioServicio;
        private ISessionManager sessionManager;

        public AuthController(_20211CTPContext ctx, IHttpContextAccessor _httpContextAccessor)
        {
            context = ctx;
            loginServicio = new LoginServicio(context,_httpContextAccessor);
            usuarioServicio = new UsuarioServicio(context, _httpContextAccessor);
            sessionManager = new SessionManager(_httpContextAccessor);

        }
        [Route("Login")]
        [HttpPost]
        public string Login(UsuarioLogin user)
        {
            if (loginServicio.ValidarLogin(user.Email, user.Password))
            {
     
                return JsonSerializer.Serialize(usuarioServicio.ObtenerPorEmail(user.Email));
            }
            else
            {
                return JsonSerializer.Serialize(new MensajeJSON { Mensaje = "No se pudo iniciar sesion en el sistema"});
            }
        }
        [Route("Logout")]
        [HttpPost]
        public void Logout()
        {
            sessionManager.Salir();
        }

    }
}
