using System;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Articulo : Auditable
    {
        public int IdArticulo { get; set; }
        [Required(ErrorMessage ="Debe ingresar un código de artículo")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Debe ingresar una descripción del artículo")]
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
                        string codigo, 
                        string descripcion) : base(fechacreacion, fechamodificacion, fechaborrado, creadopor, modificadopor, borradopor)
        {

            IdArticulo = idArticulo;
            Codigo = codigo;
            Descripcion = descripcion;
        }
    }
}