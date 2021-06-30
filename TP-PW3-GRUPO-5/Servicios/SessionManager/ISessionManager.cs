using Clases_auxiliares;

namespace Servicios.SessionManager
{
    public interface ISessionManager
    {
        SessionManager ValidarUsuario(UsuarioSesion usuarioSesion);
    }
}
