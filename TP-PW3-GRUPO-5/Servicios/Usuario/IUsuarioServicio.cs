using Clases_auxiliares;
using Entidades;
using System.Collections.Generic;

namespace Servicios
{
    public interface IUsuarioServicio
    {
        public List<Usuario> ObtenerUsuarios(UsuarioFiltro usuarioFiltro = null);
    }
}