using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;


namespace TP_PW3_GRUPO_5.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {

            return View(ObtenerUsuarios());
        }
  

        public List<Usuario> ObtenerUsuarios()
        {
             List<Usuario> listaUsuarios = new List<Usuario>();

            Usuario miUsuario1 = new Usuario();
            miUsuario1.Nombre = "Xuan";
            miUsuario1.Email = "Xuan_perez@gmail.com";
            miUsuario1.Apellido = "Perez";

            listaUsuarios.Add(miUsuario1);

            Usuario miUsuario2 = new Usuario();
            miUsuario2.Nombre = "Luis";
            miUsuario2.Email = "luis_alvarez@gmail.com";
            miUsuario2.Apellido = "Alvarez";

            listaUsuarios.Add(miUsuario2);

            Usuario miUsuario3 = new Usuario();
            miUsuario3.Nombre = "Roque";
            miUsuario3.Email = "roque_perez@gmail.com";
            miUsuario3.Apellido = "Perez";
            miUsuario3.FechaBorrado = DateTime.Now;

            listaUsuarios.Add(miUsuario3);
            
            listaUsuarios = listaUsuarios.OrderBy(u => u.Nombre).ToList();

            return listaUsuarios;
            
        }

}
}
