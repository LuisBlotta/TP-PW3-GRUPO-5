using Clases_auxiliares;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View(ObtenerClientes());
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

            var resultado = JsonSerializer.Serialize(ObtenerClientes(clienteFiltro));

            return Content(resultado);

        }

        public List<Cliente> ObtenerClientes(ClienteFiltro clienteFiltro = null)
        {

            List<Cliente> listaClientes = new List<Cliente>();

            for (int i = 0; i < 80; i++)
            {
                Cliente micliente1 = new Cliente();
                micliente1.Nombre = "Pepe" + i;
                micliente1.Numero = i;
                micliente1.Telefono = "1111-2222";


                listaClientes.Add(micliente1);

                Cliente micliente2 = new Cliente();
                micliente2.Nombre = "Juanito" + i;
                micliente2.Numero = i;
                micliente2.Telefono = "1111-2222";



                listaClientes.Add(micliente2);

                Cliente micliente3 = new Cliente();
                micliente3.Nombre = "Manolo" + i;
                micliente3.Numero = i;
                micliente3.Telefono = "1111-2222";
                micliente3.FechaBorrado = DateTime.Now.AddHours(+2);



                listaClientes.Add(micliente3);


            }

            if (clienteFiltro != null)
            {
                if (clienteFiltro.Nombre != "")
                {
                    listaClientes = listaClientes.Where(l => l.Nombre == (clienteFiltro.Nombre)).ToList();
                }
                if (clienteFiltro.Numero != null)
                {
                    listaClientes = listaClientes.Where(l => l.Numero == (clienteFiltro.Numero)).ToList();
                }
                if (clienteFiltro.Eliminado)
                {
                    listaClientes = listaClientes.Where(l => l.FechaBorrado < DateTime.Now).ToList();
                }

            }


            listaClientes = listaClientes.OrderBy(u => u.Nombre).ToList();

            return listaClientes;

        }
    }
}
