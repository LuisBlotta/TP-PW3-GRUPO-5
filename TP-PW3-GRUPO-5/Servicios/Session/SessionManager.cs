using Clases_auxiliares;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Servicios.Session
{
    public class SessionManager : ISessionManager
    {
        IHttpContextAccessor httpContextAccessor;
        ISession sesion;
        public SessionManager(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
            sesion = httpContextAccessor.HttpContext.Session;
        }
        public bool EsAdmin()
        {
            UsuarioSesion user = ObtenerUsuarioLogueado();
            return user.EsAdmin;
        }

        public bool EstaLogueado()
        {
            return ObtenerUsuarioLogueado() != null ? true : false;
        }

        public UsuarioSesion ObtenerUsuarioLogueado()
        {
            if (!string.IsNullOrEmpty(sesion.GetString("User")))
            {
                return JsonSerializer.Deserialize<UsuarioSesion>(sesion.GetString("User"));
            }
            return null;
        }

        public int ObtenerIDUsuarioLogueado()
        {
            return ObtenerUsuarioLogueado().IdUsuario;
        }

        public void Salir()
        {
            sesion.Remove(".MyApp.Session");
            sesion.Clear();
        }
    }
}
