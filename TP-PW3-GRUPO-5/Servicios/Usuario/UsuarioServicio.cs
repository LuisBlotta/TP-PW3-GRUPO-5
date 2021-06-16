using Clases_auxiliares;
using Contexto_de_datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        _20211CTPContext context = new _20211CTPContext();

        public List<Usuario> ObtenerUsuarios(UsuarioFiltro usuarioFiltro = null)
        {
            List<Usuario> listaUsuarios = context.Usuarios.ToList();

            if (usuarioFiltro != null)
            {
                if (usuarioFiltro.Nombre != "")
                {
                    listaUsuarios = listaUsuarios.Where(l => l.Nombre == (usuarioFiltro.Nombre)).ToList();
                }
                if (usuarioFiltro.Email != "")
                {
                    listaUsuarios = listaUsuarios.Where(l => l.Email == (usuarioFiltro.Email)).ToList();
                }
                if (usuarioFiltro.Eliminado)
                {
                    listaUsuarios = listaUsuarios.Where(l => l.BorradoPor == null).ToList();
                }

            }            


           listaUsuarios = listaUsuarios.OrderBy(u => u.Nombre).ToList();

            return listaUsuarios;
        }
    }
}
