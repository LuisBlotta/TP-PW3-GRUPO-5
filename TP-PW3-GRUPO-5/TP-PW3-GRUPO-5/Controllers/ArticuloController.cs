using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Entidades;
using Servicios;
using Clases_auxiliares;
using System.Text.Json;
using TP_PW3_GRUPO_5.Models;
using Contexto_de_datos.Models;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class ArticuloController : Controller
    {
        private IArticuloServicio articuloServicio;
        private _20211CTPContext context;          

        public ArticuloController(_20211CTPContext ctx)
        {
            context = ctx;
            articuloServicio = new ArticuloServicio(context);
        }
        public IActionResult Index()
        {
            return View(articuloServicio.ObtenerArticulos());
        }

        public IActionResult NuevoArticulo()
        {

            return View();
        }


        [HttpPost]
        public IActionResult NuevoArticulo(Articulo articulo, string submit)
        {
            if(ModelState.IsValid)
            {
                articuloServicio.Alta(articulo);

                if(submit == "Guardar")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["mensaje"] = $"El artículo {articulo.Codigo} - {articulo.Descripcion} se agregó correctamente.";
                    return RedirectToAction(nameof(NuevoArticulo));
                }
                
            }
            return View(articulo);
        }
        public IActionResult DetalleArticulo(string accion,int id)
        {
            Articulo miArticulo = articuloServicio.ObtenerPorId(id);
            ViewData["accion"] = accion;
            return View(miArticulo);
        }

        [HttpPost]
        public IActionResult EditarArticulo(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                articuloServicio.Modificar(articulo);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect($"/articulo/detallearticulo/editar/{articulo.IdArticulo}");
            }      
        }
        public IActionResult EliminarArticulo(int id)
        {
            articuloServicio.Baja(id);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] ArticuloFiltro articuloFiltro)
        {

            var resultado = JsonSerializer.Serialize(articuloServicio.ObtenerArticulos(articuloFiltro));

            return Content(resultado);

        }

    }
}
