using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto_de_datos.Models
{
    [ModelMetadataType(typeof(EstadoPedidoModelMetadata))]
    public partial class EstadoPedido
    {

    }
    public class EstadoPedidoModelMetadata
    {
        [StringLength(50, ErrorMessage = "Excede la cantidad válida de caracteres")]

        public string Descripcion { get; set; }
    }
}
