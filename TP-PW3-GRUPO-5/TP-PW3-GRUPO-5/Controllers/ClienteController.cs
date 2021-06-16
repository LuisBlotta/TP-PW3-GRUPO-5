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

namespace TP_PW3_GRUPO_5.Controllers
{
    public class ClienteController : Controller
    {
        IClienteServicio clienteServicio;
        _20211CTPContext context;

        public ClienteController(_20211CTPContext ctx)
        {
            context = ctx;
            clienteServicio = new ClienteServicio();    
        }
        public IActionResult Index()
        {
            ViewData["selectClientes"] = clienteServicio.ObtenerSelectClientes();
            return View(context.Clientes.ToList());
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
                cliente.FechaCreacion = DateTime.Now;
                context.Clientes.Add(cliente);
                context.SaveChanges();
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

            return View();
        }
        public IActionResult DetalleCliente(string accion, int id)
        {
            Cliente miCliente = context.Clientes.Find(id);
            ViewData["accion"] = accion;
            return View(miCliente);
        }

        [HttpPost]
        public IActionResult EditarCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                Cliente clienteBD = context.Clientes.Find(cliente.IdCliente);
                clienteBD.Nombre = cliente.Nombre;
                clienteBD.Numero = cliente.Numero;
                clienteBD.Telefono = cliente.Telefono;
                clienteBD.Direccion = cliente.Direccion;
                clienteBD.Email = cliente.Email;
                clienteBD.Cuit = cliente.Cuit;
                clienteBD.FechaModificacion = DateTime.Now;
                //FALTA AGREGAR MODIFICADO POR
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect($"/cliente/detallecliente/editar/{cliente.IdCliente}");
            }
        }

        public IActionResult EliminarCliente(int id)
        {
            Cliente clienteBD = context.Clientes.Find(id);
            clienteBD.FechaBorrado = DateTime.Now;
            //FALTA BORRADO POR Y CONFIRMACION DEL BORRADO
            context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] ClienteFiltro clienteFiltro)
        {

            var resultado = JsonSerializer.Serialize(clienteServicio.ObtenerClientes(clienteFiltro));

            return Content(resultado);

        }
    }
}
