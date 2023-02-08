
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.Gimnasio
{
    
    public class AgendaPostVentaDTO : AuditoriaDTO
    {
        
        public int TK_ID { get; set; }
        
        public string TK_Latitude { get; set; }
        
        public string TK_Longitude { get; set; }
        
        public int Codigo { get; set; }
        
        public int TipoAgenda { get; set; }
        
        public string DesTipoAgenda { get; set; }
        
        public int CodigoSocio { get; set; }
        
        public int CodigoCalificacion { get; set; }
        
        public string DesCalificacion { get; set; }
        
        public string ColorCalificacion { get; set; }
        
        public string Mensaje { get; set; }
        
        public int Estado { get; set; }
        
        public DateTime FechaInicio { get; set; }
        
        public DateTime FechaFinal { get; set; }
        
        public string Nombre { get; set; }
        
        public string Apellidos { get; set; }
        
        public string Telefono { get; set; }
        
        public string Celular { get; set; }
        
        public int CantidadTotalFilas { get; set; }

        
        public string Descripcion { get; set; }

        
        public int Cantidad_Excelente { get; set; }
        
        public int Cantidad_Bueno { get; set; }
        
        public int Cantidad_Regular { get; set; }
        
        public int Cantidad_Malo { get; set; }

        
        public int CantidadRegistros { get; set; }
        
        public int TamanioPagina { get; set; }
        
        public Common.Operation Operation { get; set; }
        
        public string Xml { get; set; }
    }

    
    public class ReqAgendaPostVentaDTO : Request
    {
        
        public List<AgendaPostVentaDTO> List { get; set; }
    }

    
    public class ReqFilterAgendaPostVentaDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public AgendaPostVentaDTO Item { get; set; }
        
        public Common.filterCaseAgendaPostVenta FilterCase { get; set; }
    }

    
    public class RespAgendaPostVentaDTO : Response
    {

    }

    
    public class RespItemAgendaPostVentaDTO : Response
    {
        
        public AgendaPostVentaDTO Item { get; set; }
    }

    
    public class RespListAgendaPostVentaDTO : Response
    {
        
        public List<AgendaPostVentaDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }

}
