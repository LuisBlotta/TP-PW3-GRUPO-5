using System.Collections.Generic;

namespace Clases_auxiliares
{
    public class PedidoNuevoFiltro
    {
        public int IdCliente { get; set; }
        public List<ArticuloCantidad> Articulos { get; set; }
        public string Accion { get; set; }
        public string Comentarios { get; set; }
    }
}
