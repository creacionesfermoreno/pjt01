
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{

	public class LlamadaEntranteDTO : AuditoriaDTO
	{

		public int TK_ID { get; set; }


		public string TK_Latitude { get; set; }


		public string TK_Longitude { get; set; }


		public int CodigoLlamadaE { get; set; }
	

		public int CodigoObjetivo { get; set; }
			
		public string Nombres { get; set; }
		
		public string Apellidos { get; set; }
        public string NombreCompleto { get; set; }
			
		public string DNI { get; set; }
	
		
		public string Telefono { get; set; }

        
        public int CantTotal { get; set; }
	
		
		public string Celular { get; set; }
        public string EstadoCelular { get; set; }
        
        public int TamanioPagina { get; set; }
	
		
		public string Correo { get; set; }
	
		
		public DateTime FechaNacimiento { get; set; }

       
        public string DesFechaNacimiento { get; set; }
	
		
		public string ImagenUrl { get; set; }
	
		
		public bool Estado { get; set; }
	
		
		public string Genero { get; set; }
	
		
		public string Facebook { get; set; }
	
		
		public string Direccion { get; set; }
	
		
		public string Distrito { get; set; }
	
		
		public string Ocupacion { get; set; }
	
		
		public int TipoCliente { get; set; }
	
	
		public string Ubicaciones { get; set; }
	
		
		public string Vendedor { get; set; }


		
        public DateTime FiltroFechaInicio { get; set; }

        
        public DateTime FiltroFechaFin { get; set; }
	
		
		public int TipoDocumento { get; set; }
	
		
		public int CodigoPaquete { get; set; }
	
		
		public int Hijos { get; set; }
	
		
		public int CantHijos { get; set; }

        
        public string LlamadaE_Observacion { get; set; }

        
        public int LlamadaE_NroDias { get; set; }

       
        public int CodigoTiempo { get; set; }

        
        public decimal Precio { get; set; }

        
        public int LlamadaE_NroDiasActual { get; set; }

        
        public DateTime LlamadaE_FechaInicio { get; set; }

        
        public DateTime LlamadaE_FechaFin { get; set; }

        
        public int LlamadaE_CodigoLlamadaEPor { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqLlamadaEntranteDTO : Request
	{
		
        public List<LlamadaEntranteDTO> List { get; set; }
	}
	
	
    public class ReqFilterLlamadaEntranteDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public LlamadaEntranteDTO Item { get; set; }
        
        public Common.filterCaseLlamadaEntrante FilterCase { get; set; }
    }
	
	
    public class RespLlamadaEntranteDTO : Response
    {
      
    }
	
	
    public class RespItemLlamadaEntranteDTO : Response
    {
        
        public LlamadaEntranteDTO Item { get; set; } 
    }

    
    public class RespListLlamadaEntranteDTO : Response
    {
        
        public List<LlamadaEntranteDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	