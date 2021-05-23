using Clases_auxiliares;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class ClienteController : Controller
    {
        IClienteServicio clienteServicio;

        public ClienteController()
        {
            clienteServicio = new ClienteServicio();    
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
        public IActionResult NuevoCliente(Cliente cliente)
        {

            return View();
        }
        public IActionResult DetalleCliente(string accion)
        {
            Cliente micliente1 = new Cliente();
            micliente1.Nombre = "Jose Antonio";
            micliente1.Numero = 912;
            micliente1.Telefono = "1111-2222";
            micliente1.Email = "pepe@gmail.com";
            micliente1.Direccion = "av rivadavia 123";
            micliente1.Cuit = "123456";

            ViewData["accion"] = accion;
            return View(micliente1);
        }

        [HttpPost]
        public IActionResult EditarCliente(Cliente cliente)
        {

            return View();
        }


        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] ClienteFiltro clienteFiltro)
        {

            var resultado = JsonSerializer.Serialize(clienteServicio.ObtenerClientes(clienteFiltro));

            return Content(resultado);

        }
    }
}
