
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.CentroEntrenamiento
{
    public class gimnasio_crm_2_etapasplantillaDTO: AuditoriaDTO
    {
        public string CodigoEmbudoVenta { get; set; }
        public string CodigoEtapa { get; set; }
        public string NombreEtapa { get; set; }
        public int OrdenEtapa { get; set; }
        public int ProbabilidadNegocio { get; set; }
        public bool NegocioEstancandose { get; set; }
        public int DiasAvisoInactividad { get; set; }
        public bool Estado { get; set; }
        public string DesEstado { get; set; }
        public Common.Operation Operation { get; set; }
    }


    public class Reqgimnasio_crm_2_etapasplantillaDTO : Request
    {
        public List<gimnasio_crm_2_etapasplantillaDTO> List { get; set; }
    }

    public class ReqFiltergimnasio_crm_2_etapasplantillaDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public gimnasio_crm_2_etapasplantillaDTO Item { get; set; }
        public Common.filterCasegimnasio_crm_2_etapasplantilla FilterCase { get; set; }
    }

    public class Respgimnasio_crm_2_etapasplantillaDTO : Response
    {

    }

    public class RespItemgimnasio_crm_2_etapasplantillaDTO : Response
    {
        public gimnasio_crm_2_etapasplantillaDTO Item { get; set; }
    }

    public class RespListgimnasio_crm_2_etapasplantillaDTO : Response
    {
        public List<gimnasio_crm_2_etapasplantillaDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }


}
