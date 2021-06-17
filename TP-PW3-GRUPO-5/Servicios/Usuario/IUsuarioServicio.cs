using Clases_auxiliares;
using Contexto_de_datos.Models;
using System.Collections.Generic;

namespace Servicios
{
    public interface IUsuarioServicio
    {
        public List<Usuario> ObtenerUsuarios(UsuarioFiltro usuarioFiltro = null);
        public void Alta(Usuario usuario);
        public void Baja(int id);
        public void Modificar(Usuario usuario);
        public Usuario ObtenerPorId(int id);
    }
}