using Clases_auxiliares;
using Microsoft.AspNetCore.Http;

namespace Servicios.Session
{
    public interface ISessionManager
    {
        bool EsAdmin();
        UsuarioSesion ObtenerUsuarioLogueado();
        int ObtenerIDUsuarioLogueado();
        public void Salir();
        public bool EstaLogueado();
    }
}
