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
        _20211CTPContext context;

        public UsuarioServicio(_20211CTPContext ctx)
        {
            context = ctx;
        }

        public void Alta(Usuario usuario)
        {
            usuario.FechaCreacion = DateTime.Now;
            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }

        public void Baja(int id)
        {
            Usuario usuarioBD = ObtenerPorId(id);
            usuarioBD.FechaBorrado = DateTime.Now;
            //FALTA BORRADO POR
            context.SaveChanges();
        }

        public void Modificar(Usuario usuario)
        {
            Usuario usuarioBD = ObtenerPorId(usuario.IdUsuario);
            usuarioBD.EsAdmin = usuario.EsAdmin;
            usuarioBD.Email = usuario.Email;
            usuarioBD.Nombre = usuario.Nombre;
            usuarioBD.Apellido = usuario.Apellido;
            usuarioBD.FechaNacimiento = usuario.FechaNacimiento;
            usuarioBD.Password = usuario.Password;
            usuarioBD.FechaModificacion = DateTime.Now;
            //FALTA AGREGAR MODIFICADO POR
            context.SaveChanges();
        }

        public Usuario ObtenerPorEmail(string email)
        {
            return context.Usuarios.FirstOrDefault(o => o.Email == email);
        }

        public Usuario ObtenerPorId(int id)
        {
            return context.Usuarios.Find(id);
        }

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
