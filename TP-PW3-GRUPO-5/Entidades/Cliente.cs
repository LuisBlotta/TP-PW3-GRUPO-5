using System;
using System.ComponentModel.DataAnnotations;
using Clases_auxiliares;

namespace Entidades
{
    public class Cliente : Auditable
    {
        public int IdCliente { get; set; }

        [RegularExpression(@"^\d$",ErrorMessage ="Ingrese un numero entero valido")]
        public int? Numero { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre de cliente")]
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage ="Ingrese una direccion de email valida")]
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Cuit { get; set; }

        public Cliente()
        {

        }
        public Cliente(DateTime fechacreacion,
                        DateTime fechamodificacion,
                        DateTime fechaborrado,
                        Usuario creadopor,
                        Usuario modificadopor,
                        Usuario borradopor,
                        int idcliente,
                        int numero,
                        string nombre,
                        string telefono,
                        string email,
                        string direccion,
                        string cuit):base(fechacreacion, fechamodificacion, fechaborrado, creadopor, modificadopor, borradopor)
        {
            IdCliente = idcliente;
            Numero = numero;
            Nombre = nombre;
            Telefono = telefono;
            Email = email;
            Direccion = direccion;
            Cuit = cuit;
    
        }

    }
}