using Clases_auxiliares;
//using Entidades;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Contexto_de_datos.Models;
using System.Text.Json.Serialization;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteServicio clienteServicio;
        private _20211CTPContext context;

        public ClienteController(_20211CTPContext ctx)
        {
            context = ctx;
            clienteServicio = new ClienteServicio(context);    
        }
        public IActionResult Index()
        {
            ViewData["selectClientes"] = clienteServicio.ObtenerSelectClientes();
            return View(clienteServicio.ObtenerClientes());
        }

        public IActionResult NuevoCliente()
        {

            return View();
        }

        [HttpPost]
        public IActionResult NuevoCliente(Cliente cliente, string submit)
        {
            if (ModelState.IsValid) 
            {
                clienteServicio.Alta(cliente);

                if (submit == "Guardar")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["mensaje"] = $"El cliente {cliente.Nombre} fue creado con éxito.";
                    return RedirectToAction(nameof(NuevoCliente));
                }
            }

            return View(cliente);
        }
        public IActionResult DetalleCliente(string accion, int id)
        {
            Cliente miCliente = clienteServicio.ObtenerPorId(id);
            ViewData["accion"] = accion;
            return View(miCliente);
        }

        [HttpPost]
        public IActionResult EditarCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                clienteServicio.Modificar(cliente);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect($"/cliente/detallecliente/editar/{cliente.IdCliente}");
            }
        }

        public IActionResult EliminarCliente(int id)
        {
            clienteServicio.Baja(id);
            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] ClienteFiltro clienteFiltro)
        {
            List<Cliente> clientes = clienteServicio.ObtenerClientes(clienteFiltro);
            var resultado = JsonSerializer.Serialize(clientes);
            return Content(resultado);

        }

    }
}
