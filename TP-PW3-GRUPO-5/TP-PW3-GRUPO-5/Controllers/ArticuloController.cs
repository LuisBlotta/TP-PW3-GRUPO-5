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
        IArticuloServicio articuloServicio;
        _20211CTPContext context;

           

        public ArticuloController(_20211CTPContext ctx)
        {
            context = ctx;
            articuloServicio = new ArticuloServicio();
        }
        public IActionResult Index()
        {
            return View(context.Articulos.ToList());
        }

        public IActionResult NuevoArticulo()
        {

            return View();
        }


        [HttpPost]
        public IActionResult NuevoArticulo(Entidades.Articulo articulo, string submit)
        {
            if(ModelState.IsValid)
            {
                Articulo miArticulo = new Articulo();

                miArticulo.Codigo = articulo.Codigo;
                miArticulo.Descripcion = articulo.Descripcion;
                
                context.Articulos.Add(miArticulo);
                context.SaveChanges();

                if(submit == "Guardar")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["mensaje"] = $"El artículo {miArticulo.Codigo} - {miArticulo.Descripcion} se agregó correctamente.";
                    return RedirectToAction(nameof(NuevoArticulo));
                }
                
            }
            return View(articulo);
        }
        public IActionResult DetalleArticulo(string accion,int id)
        {
            Entidades.Articulo miArticulo = new Entidades.Articulo();
            Articulo articuloBD = context.Articulos.Find(id);

            miArticulo.IdArticulo = articuloBD.IdArticulo;
            miArticulo.Codigo = articuloBD.Codigo;
            miArticulo.Descripcion = articuloBD.Descripcion;

            ViewData["accion"] = accion;
            return View(miArticulo);
        }

        [HttpPost]
        public IActionResult EditarArticulo(Entidades.Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                Articulo articuloBD = context.Articulos.Find(articulo.IdArticulo);
                articuloBD.Codigo = articulo.Codigo;
                articuloBD.Descripcion = articulo.Descripcion;
                articuloBD.FechaModificacion = DateTime.Now;
                //FALTA AGREGAR MODIFICADO POR
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect($"/articulo/detallearticulo/editar/{articulo.IdArticulo}");
            }      
        }
        public IActionResult EliminarArticulo(int id)
        {
            Articulo articuloBD = context.Articulos.Find(id);
            articuloBD.FechaBorrado = DateTime.Now;
            //FALTA BORRADO POR Y CONFIRMACION DEL BORRADO
            context.SaveChanges();
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
