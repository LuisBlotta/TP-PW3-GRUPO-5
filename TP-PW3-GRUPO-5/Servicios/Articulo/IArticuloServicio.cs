using Clases_auxiliares;
using Contexto_de_datos.Models;
using System.Collections.Generic;

namespace Servicios
{
    public interface IArticuloServicio
    {
        public List<Articulo> ObtenerArticulos(ArticuloFiltro articuloFiltro = null);
        public void Alta(Articulo articulo);
        public void Baja(int id);
        public void Modificar(Articulo articulo);
        public Articulo ObtenerPorId(int id);
    }
}