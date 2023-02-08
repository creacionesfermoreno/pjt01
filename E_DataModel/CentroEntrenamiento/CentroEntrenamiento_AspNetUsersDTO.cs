
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.CentroEntrenamiento
{
    public class CentroEntrenamiento_AspNetUsersDTO : AuditoriaDTO
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Email_Confirmed { get; set; }
        public string PhoneNumber { get; set; }        
        public Common.Operation Operation { get; set; }
    }

    public class ReqCentroEntrenamiento_AspNetUsersDTO : Request
    {
        public List<CentroEntrenamiento_AspNetUsersDTO> List { get; set; }
    }

    public class ReqFilterCentroEntrenamiento_AspNetUsersDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public CentroEntrenamiento_AspNetUsersDTO Item { get; set; }
        public Common.filterCaseCentroEntrenamiento_AspNetUsers FilterCase { get; set; }
    }

    public class RespCentroEntrenamiento_AspNetUsersDTO : Response
    {

    }

    public class RespItemCentroEntrenamiento_AspNetUsersDTO : Response
    {
        public CentroEntrenamiento_AspNetUsersDTO Item { get; set; }
    }

    public class RespListCentroEntrenamiento_AspNetUsersDTO : Response
    {
        public List<CentroEntrenamiento_AspNetUsersDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }


}
