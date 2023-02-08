
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class InvitadosDTO : AuditoriaDTO
	{

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
		public int CodigoInvitado { get; set; }

        public int CodigoSubProcedencia { get; set; }
        public int CodigoObjetivo { get; set; }
        public int CodigoOrigen { get; set; }
	
		
		public string Nombres { get; set; }

        
        public int Filtro { get; set; }

        
        public DateTime FiltroFechaInicio { get; set; }

        
        public DateTime FiltroFechaFin { get; set; }
	
		
		public string Apellidos { get; set; }

        
        public string flagCumpleanios { get; set; }

        
        public string NombreCompleto { get; set; }
	
		
		public string DNI { get; set; }
	
		
		public string Telefono { get; set; }

        public string EstadoCelular { get; set; }
        public string Celular { get; set; }
	
		
		public string Correo { get; set; }
	
		
		public DateTime FechaNacimiento { get; set; }
	
		
		public string ImagenUrl { get; set; }
	
		
		public bool Estado { get; set; }
	
		
		public string Genero { get; set; }
	
		
		public string Facebook { get; set; }
	
		
		public string HuellaStr { get; set; }
	
		
		public string InvitadoPor { get; set; }

    	
        public int CantTotal { get; set; }

        
        public int Codigo_InvitadoPor { get; set; }

        
        public int TamanioPagina { get; set; }

		
		public string Direccion { get; set; }
	
		
		public string Distrito { get; set; }
	
		
		public string Ocupacion { get; set; }
	
		
		public int TipoCliente { get; set; }
	
		public string Ubicaciones { get; set; }
	
		
		public string Vendedor { get; set; }

        
        public int CodigoTiempo { get; set; }

        
        public decimal Precio { get; set; }

        
        public string desSede { get; set; }

  	   
        public string Hoy { get; set; }

		
		public int TipoDocumento { get; set; }
	
		
		public string Observacion { get; set; }

        
        public string EstadoDiasAsistidos { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }

        
        public int CantidadTotalFilas { get; set; }

        
        public int Edad { get; set; }

        
        public string DesTipoCliente { get; set; }

        
        public string DesFechaNacimiento { get; set; }

        
        public string DesInvitadoPor { get; set; }

        
        public int NroDias { get; set; }

        
        public int NroDiasActual { get; set; }

        
        public DateTime FechaInicio { get; set; }

        
        public DateTime FechaFin { get; set; }

        
        public int CodigoPaquete { get; set; }

        
        public string DescFechaInicio { get; set; }

        
        public string DescFechaFin { get; set; }
	}
	
	
	public class ReqInvitadosDTO : Request
	{
		
        public List<InvitadosDTO> List { get; set; }
	}
	
	
    public class ReqFilterInvitadosDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public InvitadosDTO Item { get; set; }
        
        public Common.filterCaseInvitados FilterCase { get; set; }
    }
	
	
    public class RespInvitadosDTO : Response
    {
      
    }
	
	
    public class RespItemInvitadosDTO : Response
    {
        
        public InvitadosDTO Item { get; set; } 
    }

    
    public class RespListInvitadosDTO : Response
    {
        
        public List<InvitadosDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	