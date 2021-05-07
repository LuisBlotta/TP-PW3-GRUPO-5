using System;

namespace Entidades
{
    public class Articulo : Auditable
    {
        public int IdArticulo { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }

        public Articulo()
        {

        }

        public Articulo(DateTime fechacreacion,
                        DateTime fechamodificacion,
                        DateTime fechaborrado,
                        Usuario creadopor,
                        Usuario modificadopor,
                        Usuario borradopor,
                        int idArticulo, 
                        int codigo, 
                        string descripcion) : base(fechacreacion, fechamodificacion, fechaborrado, creadopor, modificadopor, borradopor)
        {

            IdArticulo = idArticulo;
            Codigo = codigo;
            Descripcion = descripcion;
        }
    }
}