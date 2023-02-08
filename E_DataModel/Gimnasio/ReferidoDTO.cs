
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class ReferidoDTO : AuditoriaDTO
	{

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
		public int CodigoReferido { get; set; }
		public int CodigoSubProcedencia { get; set; }
		public int CodigoObjetivo { get; set; }
		public string NombreCompleto { get; set; }
		public string Nombres { get; set; }
	
		public string Apellidos { get; set; }
	
		
		public string DNI { get; set; }

        
        public int CantTotal { get; set; }
	
		
		public string Telefono { get; set; }
	
		public string EstadoCelular { get; set; }

		public string Celular { get; set; }
	
		public string Correo { get; set; }
	
		
		public DateTime FechaNacimiento { get; set; }

        
        public string DesFechaNacimiento { get; set; }
	
		
		public string ImagenUrl { get; set; }
	
		
		public bool Estado { get; set; }
	
		
		public string Genero { get; set; }
	
		
		public string Facebook { get; set; }
	
		
		public string HuellaStr { get; set; }
	
		
		public string ReferidoPor { get; set; }

        
		public int CodigoReferidoPor { get; set; }
	
		
		public string Direccion { get; set; }
	
		
		public string Distrito { get; set; }
	
		
		public string Ocupacion { get; set; }
	
		
		public int TipoCliente { get; set; }
			
		public string Ubicaciones { get; set; }
	
		
		public string Vendedor { get; set; }

	    
        public int TamanioPagina { get; set; }

		
		public int TipoDocumento { get; set; }

        
        public DateTime FiltroFechaInicio { get; set; }

        
        public DateTime FiltroFechaFin { get; set; }

        
        public int CodigoPaquete { get; set; }

        
        public int Hijos { get; set; }

        
        public int CantHijos { get; set; }

       
        public int CodigoTiempo { get; set; }

        
        public decimal Precio { get; set; }

        
        public string Referido_Observacion { get; set; }

        
        public int Referido_NroDias { get; set; }

        
        public int Referido_NroDiasActual { get; set; }

        
        public DateTime Referido_FechaInicio { get; set; }

        
        public DateTime Referido_FechaFin { get; set; }

        
        public int Referido_CodigoReferidoPor { get; set; }

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqReferidoDTO : Request
	{
		
        public List<ReferidoDTO> List { get; set; }
	}
	
	
    public class ReqFilterReferidoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ReferidoDTO Item { get; set; }
        
        public Common.filterCaseReferido FilterCase { get; set; }
    }
	
	
    public class RespReferidoDTO : Response
    {
      
    }
	
	
    public class RespItemReferidoDTO : Response
    {
        
        public ReferidoDTO Item { get; set; } 
    }

    
    public class RespListReferidoDTO : Response
    {
        
        public List<ReferidoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	