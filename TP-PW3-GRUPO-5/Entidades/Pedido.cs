using System;
using System.Collections.Generic;

namespace Entidades
{
    public class Pedido : Auditable
    {
        public int IdPedido { get; set; }
        public int NroPedido { get; set; }
        public List<Articulo> Articulos { get; set; }
        public Cliente Cliente { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
        public string Comentarios { get; set; }



        public Pedido()
        {

        }

       public Pedido(  DateTime fechacreacion,
                       DateTime fechamodificacion,
                       DateTime fechaborrado,
                       Usuario creadopor,
                       Usuario modificadopor,
                       Usuario borradopor,
                       int idPedido,
                       int nroPedido,
                       List<Articulo> articulos,
                       Cliente cliente,
                       EstadoPedido estadoPedido,
                       string comentarios) : base(fechacreacion, fechamodificacion, fechaborrado, creadopor, modificadopor, borradopor)
        {

            IdPedido = idPedido;
            NroPedido = nroPedido;
            Articulos = articulos;
            Cliente = cliente;
            EstadoPedido = estadoPedido;
            Comentarios = comentarios;
        }
    }
}
