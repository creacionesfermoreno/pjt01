
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.CentroEntrenamiento
{
    public class gimnasio_crm_1_embudosventaplantillaDTO : AuditoriaDTO
    {
        public string CodigoEmbudoVenta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public string DesEstado { get; set; }
        public Common.Operation Operation { get; set; }
    }


    public class Reqgimnasio_crm_1_embudosventaplantillaDTO : Request
    {
        public List<gimnasio_crm_1_embudosventaplantillaDTO> List { get; set; }
    }

    public class ReqFiltergimnasio_crm_1_embudosventaplantillaDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public gimnasio_crm_1_embudosventaplantillaDTO Item { get; set; }
        public Common.filterCasegimnasio_crm_1_embudosventaplantilla FilterCase { get; set; }
    }

    public class Respgimnasio_crm_1_embudosventaplantillaDTO : Response
    {

    }

    public class RespItemgimnasio_crm_1_embudosventaplantillaDTO : Response
    {
        public gimnasio_crm_1_embudosventaplantillaDTO Item { get; set; }
    }

    public class RespListgimnasio_crm_1_embudosventaplantillaDTO : Response
    {
        public List<gimnasio_crm_1_embudosventaplantillaDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }


}
