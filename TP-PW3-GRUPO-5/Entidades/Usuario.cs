using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario : Auditable
    {
        public int IdUsuario { get; set; }
        public bool EsAdmin { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaUltLogin { get; set; }

        public Usuario()
        {

        }
        public Usuario(DateTime fechacreacion,
                        DateTime fechamodificacion,
                        DateTime fechaborrado,
                        Usuario creadopor,
                        Usuario modificadopor,
                        Usuario borradopor,
                        int idUsuario,
                        bool esAdmin,
                        string email,
                        string password,
                        string nombre,
                        string apellido,
                        DateTime fechaNacimiento,
                        DateTime fechaUltLogin) : base(fechacreacion, fechamodificacion, fechaborrado, creadopor, modificadopor, borradopor)
        {
            IdUsuario = idUsuario;
            EsAdmin = esAdmin;
            Email = email;
            Password = password;
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            FechaUltLogin = fechaUltLogin;
        }
    }
}
