using Contexto_de_datos.Models;
using Clases_auxiliares;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System.Diagnostics;
using TP_PW3_GRUPO_5.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class HomeController : Controller
    {
        private IUsuarioServicio usuarioServicio;
        private _20211CTPContext context;
        

        public HomeController(_20211CTPContext ctx)
        {
            context = ctx;
            usuarioServicio = new UsuarioServicio(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Ingresar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ingresar(string Email, string Password, string Url)
        {
            Usuario user = usuarioServicio.ObtenerPorEmail(Email);
            if (user != null && Password == user.Password)
            {
                user.FechaUltLogin = DateTime.Now;
                context.SaveChanges();

                UsuarioSesion usuarioSesion = new UsuarioSesion() { IdUsuario = user.IdUsuario, Nombre = user.Nombre, EsAdmin = user.EsAdmin };
                HttpContext.Session.SetString("User", JsonSerializer.Serialize(usuarioSesion));
                TempData["nombre"] = usuarioSesion.Nombre;

                return Redirect(Url);
            }

            ViewData["error"] = "Email y/o contraseña inválidos";
            ViewData["email"] = Email;
            return View();
        }

        [HttpPost]
        public IActionResult Salir()
        {
            HttpContext.Session.Remove(".MyApp.Session");
            HttpContext.Session.Clear();
            TempData.Remove("nombre");

            return RedirectToAction(nameof(Index));
        }
    }
}
