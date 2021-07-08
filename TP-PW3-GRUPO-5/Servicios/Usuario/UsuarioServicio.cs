using Clases_auxiliares;
using Contexto_de_datos.Models;
using Microsoft.AspNetCore.Http;
using Servicios.Session;
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
        private ISessionManager sessionManager;


        public UsuarioServicio(_20211CTPContext ctx, IHttpContextAccessor _httpContextAccessor)
        {
            context = ctx;
            sessionManager = new SessionManager(_httpContextAccessor);

        }

        public void Alta(Usuario usuario)
        {
            usuario.CreadoPor = sessionManager.ObtenerIDUsuarioLogueado();
            usuario.FechaCreacion = DateTime.Now;
            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }

        public void Baja(int id)
        {
            Usuario usuario = ObtenerPorId(id);
            usuario.FechaBorrado = DateTime.Now;
            usuario.BorradoPor = sessionManager.ObtenerIDUsuarioLogueado();
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
            usuarioBD.ModificadoPor = sessionManager.ObtenerIDUsuarioLogueado();
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

        public bool ValidarEmail(string email)
        {
            Usuario user = ObtenerPorEmail(email);
            if (user == null)
            {
                return true;
            }
            return user.BorradoPor == null ? false : true;
        }
    }
}
