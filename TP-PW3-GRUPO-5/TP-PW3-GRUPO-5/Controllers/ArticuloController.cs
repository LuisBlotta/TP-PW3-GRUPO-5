using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class ArticuloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Mostrar()
        {
            Articulo miArticulo = new Articulo();
            miArticulo.IdArticulo = 1;
            miArticulo.Codigo = 123;
            return View(miArticulo);
        }
    }
}
