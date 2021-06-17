using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Contexto_de_datos.Models
{
    [ModelMetadataType(typeof(ArticuloModelMetadata))]
    public partial class Articulo
    {

    }
    
    public class ArticuloModelMetadata
    {
        [Required(ErrorMessage = "Debe ingresar un código de artículo")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Debe ingresar una descripción del artículo")]
        public string Descripcion { get; set; }
    }
}
