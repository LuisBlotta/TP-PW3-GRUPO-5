using Contexto_de_datos.Models;
using Clases_auxiliares;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP_PW3_GRUPO_5.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System;
using Servicios;
using Servicios.Login;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class HomeController : Controller
    {
        private IUsuarioServicio usuarioServicio;
        private ILoginServicio loginServicio;
        private _20211CTPContext context;
        

        public HomeController(_20211CTPContext ctx)
        {
            context = ctx;
            usuarioServicio = new UsuarioServicio(context);
            loginServicio = new LoginServicio(context);
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
            if (loginServicio.ValidarLogin(Email,Password,HttpContext.Session))
            {
                TempData["nombre"] = JsonSerializer.Deserialize<UsuarioSesion>(HttpContext.Session.GetString("User")).Nombre;
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
