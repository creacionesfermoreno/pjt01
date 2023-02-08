
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.CentroEntrenamiento
{
    public class gimnasio_crm_3_tratosprospectoDTO : AuditoriaDTO
    {       
        public string CodigoEmbudoVenta     { get; set; }
        public string DesEmbudoVenta { get; set; }      
        public string CodigoEtapa { get; set; }
        public string NombreEtapa { get; set; }
        public string CodigoTratoProspecto { get; set; }
        public string NombreTrato { get; set; }
        public int CodigoEstadoEtapa { get; set; }
        public string DesEstadoEtapa { get; set; }
        public DateTime FechaPrevistaCierre { get; set; }
        public string DesFechaPrevistaCierre { get; set; }
        public int CodigoMoneda { get; set; }
        public decimal Valor { get; set; }
        public int CodigoOrigenProspecto { get; set; }
        public string DesOrigenProspecto { get; set; }
        public string ColorOrigenProspecto { get; set; }
        public string ColorEstadoActividad { get; set; }
        public string IconoEstadoActividad { get; set; }

        public string DesObjetivo { get; set; }
        public string DesComoConocioGym { get; set; }
        public int CodigoProspecto { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Celular { get; set; }
        public string Vendedor { get; set; }
        public string Nota { get; set; }
        public Common.Operation Operation { get; set; }
    }

    public class Reqgimnasio_crm_3_tratosprospectoDTO : Request
    {
        public List<gimnasio_crm_3_tratosprospectoDTO> List { get; set; }
    }

    public class ReqFiltergimnasio_crm_3_tratosprospectoDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public gimnasio_crm_3_tratosprospectoDTO Item { get; set; }
        public Common.filterCasegimnasio_crm_3_tratosprospecto FilterCase { get; set; }
    }

    public class Respgimnasio_crm_3_tratosprospectoDTO : Response
    {

    }

    public class RespItemgimnasio_crm_3_tratosprospectoDTO : Response
    {
        public gimnasio_crm_3_tratosprospectoDTO Item { get; set; }
    }

    public class RespListgimnasio_crm_3_tratosprospectoDTO : Response
    {
        public List<gimnasio_crm_3_tratosprospectoDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }


}
