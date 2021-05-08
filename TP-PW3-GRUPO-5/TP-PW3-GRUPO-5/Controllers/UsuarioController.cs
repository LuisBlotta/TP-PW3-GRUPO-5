using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;
using System.Web.Helpers;
using System.Text.Json;
using Clases_auxiliares;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {

            return View(ObtenerUsuarios());
        }

        public IActionResult NuevoUsuario()
        {

            return View();
        }

        [HttpPost]
        public IActionResult NuevoUsuario(Usuario usuario)
        {

            return View();
        }

        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] UsuarioFiltro usuarioFiltro)
        {

            var resultado = JsonSerializer.Serialize(ObtenerUsuarios(usuarioFiltro));

            return Content(resultado);

        }


        public List<Usuario> ObtenerUsuarios(UsuarioFiltro usuarioFiltro = null)
        {

            List<Usuario> listaUsuarios = new List<Usuario>();

            for (int i = 0; i < 40; i++)
            {
                Usuario miUsuario1 = new Usuario();
                miUsuario1.Nombre = "Xuan" +i;
                miUsuario1.Email = "Xuan_perez@gmail.com";
                miUsuario1.Apellido = "Perez" + i;

                listaUsuarios.Add(miUsuario1);
            }
            //Usuario miUsuario1 = new Usuario();
            //miUsuario1.Nombre = "Xuan";
            //miUsuario1.Email = "Xuan_perez@gmail.com";
            //miUsuario1.Apellido = "Perez";

            //listaUsuarios.Add(miUsuario1);

            //Usuario miUsuario2 = new Usuario();
            //miUsuario2.Nombre = "Luis";
            //miUsuario2.Email = "luis_alvarez@gmail.com";
            //miUsuario2.Apellido = "Alvarez";

            //listaUsuarios.Add(miUsuario2);

            Usuario miUsuario3 = new Usuario();
            miUsuario3.Nombre = "Roque";
            miUsuario3.Email = "roque_perez@gmail.com";
            miUsuario3.Apellido = "Perez";
            miUsuario3.FechaBorrado = DateTime.Now.AddHours(+2);

            listaUsuarios.Add(miUsuario3);



            if (usuarioFiltro != null)
            {
                listaUsuarios = listaUsuarios.Where(l => l.FechaBorrado > DateTime.Now).ToList();

            }


            listaUsuarios = listaUsuarios.OrderBy(u => u.Nombre).ToList();

            return listaUsuarios;

        }

    }
}
