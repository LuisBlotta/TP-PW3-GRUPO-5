using Clases_auxiliares;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        public List<Usuario> ObtenerUsuarios(UsuarioFiltro usuarioFiltro = null)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            for (int i = 0; i < 80; i++)
            {
                Usuario miUsuario1 = new Usuario();
                miUsuario1.Nombre = "Xuan" + i;
                miUsuario1.Email = "Xuan_perez@gmail.com";
                miUsuario1.Apellido = "Perez" + i;

                listaUsuarios.Add(miUsuario1);

                Usuario miUsuario2 = new Usuario();
                miUsuario2.Nombre = "Luis" + i;
                miUsuario2.Email = "luis_alvarez@gmail.com";
                miUsuario2.Apellido = "Alvarez" + i;

                listaUsuarios.Add(miUsuario2);

                Usuario miUsuario3 = new Usuario();
                miUsuario3.Nombre = "Roque" + i;
                miUsuario3.Email = "roque_perez@gmail.com";
                miUsuario3.Apellido = "Perez" + i;
                miUsuario3.FechaBorrado = DateTime.Now.AddHours(+2);

                listaUsuarios.Add(miUsuario3);
            }

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
                    listaUsuarios = listaUsuarios.Where(l => l.FechaBorrado < DateTime.Now).ToList();
                }

            }


            listaUsuarios = listaUsuarios.OrderBy(u => u.Nombre).ToList();

            return listaUsuarios;
        }
    }
}
