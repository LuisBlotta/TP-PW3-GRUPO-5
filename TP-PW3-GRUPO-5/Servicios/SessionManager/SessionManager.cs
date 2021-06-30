using Clases_auxiliares;

namespace Servicios.SessionManager
{
    public class SessionManager : ISessionManager
    {
        public bool EsAdmin { get; set; }

        public SessionManager ValidarUsuario(UsuarioSesion usuarioSesion)
        {
            SessionManager sessionManager = new SessionManager();
            sessionManager.EsAdmin = usuarioSesion.EsAdmin;
            return sessionManager;
        }
    }
}
