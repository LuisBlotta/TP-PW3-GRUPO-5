using System;
using System.Collections.Generic;

#nullable disable

namespace Contexto_de_datos.Models
{
    public partial class PedidoArticulo
    {
        public int IdPedido { get; set; }
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; }
        public virtual Pedido IdPedidoNavigation { get; set; }
    }
}
