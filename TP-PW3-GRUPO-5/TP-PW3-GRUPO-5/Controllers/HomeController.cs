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
using Servicios.Session;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class HomeController : Controller
    {
        private IUsuarioServicio usuarioServicio;
        private ILoginServicio loginServicio;
        private _20211CTPContext context;
        private ISessionManager sessionManager;

        public HomeController(_20211CTPContext ctx,IHttpContextAccessor _httpContextAccessor)
        {
            context = ctx;
            usuarioServicio = new UsuarioServicio(context, _httpContextAccessor);
            loginServicio = new LoginServicio(context,_httpContextAccessor);
            sessionManager = new SessionManager(_httpContextAccessor);
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
            if (loginServicio.ValidarLogin(Email,Password))
            {
                TempData["nombre"] = sessionManager.ObtenerUsuarioLogueado().Nombre;
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
