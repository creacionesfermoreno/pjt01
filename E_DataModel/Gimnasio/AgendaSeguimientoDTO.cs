using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class AgendaSeguimientoDTO : AuditoriaDTO
	{

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
		public int TipoActividad { get; set; }

        public string desActividad { get; set; }
        public string imgTipoActividad { get; set; }

        public string DescTipoActividad { get; set; }
        
		public int Codigo { get; set; }

        
        public int CodigoPaquete { get; set; }

		
		public int CodigoSocio { get; set; }
        
        public int CantidadCitas { get; set; }
        
        public string NombreSocio { get; set; }
        
        public string Telefono { get; set; }
		
		public int Tipo { get; set; }
        
        public string DesTipo { get; set; }
        
        public string imagenActualizar { get; set; }

        
        public string desTipoAgenda { get; set; }

		
		public string Color { get; set; }

        
        public string Nombre { get; set; }

        
        public string Apellidos { get; set; }

        
        public string DescTipoAgenda { get; set; }

        
        public string Celular { get; set; }
        public string EstadoCelular { get; set; }
        
        public string DesPaquete { get; set; }

        
        public string DesTiempoPaquete { get; set; }

        
        public decimal Costo { get; set; }

        
        public string Vendedor { get; set; }

        
        public int CantCitasVendedores { get; set; }

        
        public string ColorAgenda { get; set; }

        
        public string BotonVerAgenda { get; set; }

       
        public int TamanioPagina { get; set; }

        
        public int CantTotal { get; set; }

        
        public decimal MontoTotal { get; set; }

        
        public int DiasCitaCaida { get; set; }

        
        public int EstadoAgenda { get; set; }

        
        public string DesFechaCreacion { get; set; }

        
        public string Facebook { get; set; }

        
        public string DNI { get; set; }

        
        public DateTime HoraInicioAgenda { get; set; }

        
        public string AsuntoAgenda { get; set; }


        
        public DateTime HoraFinAgenda { get; set; }

        
        public string DesTipoCliente { get; set; }

        
        public string DesFechaHoraInicio { get; set; }

        
        public string DescEstadoAgenda { get; set; }

        
        public string Buscador { get; set; }

        
        public int CodigoTipoAgenda { get; set; }

        
        public int CodTiempoPaquete { get; set; }

        
        public int TipoCliente { get; set; }

        
        public int CodSede { get; set; }

        
        public DateTime FechaDesde { get; set; }

        
        public DateTime FechaHasta { get; set; }
	
		
		public DateTime HoraInicio { get; set; }
	
		
		public DateTime HoraFin { get; set; }

        
        public string StrHoraInicio { get; set; }

        
        public string StrFechaInicio { get; set; }

		
        public string StrHoraFin { get; set; }

        
        public string Encargado { get; set; }

		
		public string Asunto { get; set; }

        
        public string AsuntoCompleto { get; set; }

        
        public int Anio { get; set; }

        
        public int Mes { get; set; }

        
        public string ImagenUrl { get; set; }
	
		
        public string fechaTexto { get; set; }
        public string fechaActividadTexto { get; set; }
        
        public string strFechaCreacion { get; set; }

        
        public int Estado { get; set; }

        
        public string DesEstado { get; set; }

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }


        public int actividad1 { get; set; }
        public int actividad2 { get; set; }
        public int actividad3 { get; set; }
        public int actividad4 { get; set; }
        public int actividad5 { get; set; }
        public int actividad6 { get; set; }
        public int actividad7 { get; set; }
        public int actividad8 { get; set; }
        public int actividad9 { get; set; }
        public int DiasFaltantesCaida { get; set; }

    }
	
	
	public class ReqAgendaSeguimientoDTO : Request
	{
		
        public List<AgendaSeguimientoDTO> List { get; set; }
	}
	
	
    public class ReqFilterAgendaSeguimientoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public AgendaSeguimientoDTO Item { get; set; }
        
        public Common.filterCaseAgendaSeguimiento FilterCase { get; set; }
    }
	
	
    public class RespAgendaSeguimientoDTO : Response
    {
      
    }
	
	
    public class RespItemAgendaSeguimientoDTO : Response
    {
        
        public AgendaSeguimientoDTO Item { get; set; } 
    }

    
    public class RespListAgendaSeguimientoDTO : Response
    {
        
        public List<AgendaSeguimientoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	