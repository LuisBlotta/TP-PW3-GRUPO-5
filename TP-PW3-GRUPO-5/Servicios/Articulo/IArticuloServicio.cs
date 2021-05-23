using Clases_auxiliares;
using Entidades;
using System.Collections.Generic;

namespace Servicios
{
    public interface IArticuloServicio
    {
        public List<Articulo> ObtenerArticulos(ArticuloFiltro articuloFiltro = null);
    }
}