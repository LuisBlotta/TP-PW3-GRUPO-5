using System.Collections.Generic;

namespace Clases_auxiliares
{
    public class EditarPedido
    {
        public int IdPedido { get; set; }
        public List<ArticuloCantidad> Articulos { get; set; }
        public int EstadoPedido { get; set; }
        public string Comentarios { get; set; }
    }
}
