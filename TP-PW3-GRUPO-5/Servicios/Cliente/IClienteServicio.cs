using Clases_auxiliares;
using Contexto_de_datos.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Servicios
{
    public interface IClienteServicio
    {
        public List<Cliente> ObtenerClientes (ClienteFiltro clienteFiltro = null);

        public List<string> ObtenerSelectClientes();
        public void Alta(Cliente cliente);
        public void Baja(int id);
        public void Modificar(Cliente cliente);
        public Cliente ObtenerPorId(int id);
        public List<Cliente> ObtenerClientesConPedidos();
    }
}