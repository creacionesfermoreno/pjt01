using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel.Configuracion
{
    public class AspNetUsersDTO : AuditoriaDTO
    {
     
       public int  UserType            { get; set; }
       public string  Id                  { get; set; }
       public string  FullName            { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Photo               { get; set; }
       public string UserName            { get; set; }
       public string PasswordHash        { get; set; }
       public string DefaultKey          { get; set; }
       public string Identificacion { get; set; }
        
       public string Email               { get; set; }
       public bool  EmailConfirmed      { get; set; }
       public string PhoneNumber         { get; set; }
       public bool  PhoneNumberConfirmed{ get; set; }
       public string SecurityStamp       { get; set; }
       public int Estate { get; set; }

        public int CodigoCargo { get; set; }
        public string DesCargo { get; set; }
        public int LoginValidation { get; set; }

        public int IdValidation { get; set; }
        public string MensajeValidation { get; set; }

        public Common.Operation Operation { get; set; }

        public string PasswordHashActual { get; set; }
        public string PasswordHashNueva { get; set; }

    }



    public class ReqAspNetUsersDTO : Request
    {
        public List<AspNetUsersDTO> List { get; set; }
    }

    public class ReqFilterAspNetUsersDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public AspNetUsersDTO Item { get; set; }
        public Common.filterCaseAspNetUsers FilterCase { get; set; }
    }

    public class RespAspNetUsersDTO : Response
    {

    }

    public class RespItemAspNetUsersDTO : Response
    {
        public AspNetUsersDTO Item { get; set; }
    }

    public class RespListAspNetUsersDTO : Response
    {
        public List<AspNetUsersDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }

}
