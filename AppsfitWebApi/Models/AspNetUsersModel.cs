using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppsfitWebApi.Models
{
    public class AspNetUsersModel
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
        public int UserType { get; set; }
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Photo { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordConfirmacion { get; set; }
        public string DefaultKey { get; set; }
        public string Identificacion { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }

        public int CodigoCargo { get; set; }
        public string DesCargo { get; set; }
        public int Estate { get; set; }
        public string UsuarioCreacion { get; set; }
        public int LoginValidation { get; set; }
        public int PageNumber { get; set; }
        public string DesFechaCreacion { get; set; }
        public string Accion { get; set; }

        public string PasswordHashActual { get; set; }
        public string PasswordHashNueva { get; set; }

        public string SubDominio { get; set; }
    }
}