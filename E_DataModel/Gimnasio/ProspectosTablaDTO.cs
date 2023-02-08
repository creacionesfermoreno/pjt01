using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class ProspectosTablaDTO : AuditoriaDTO
	{


        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
        public int CodigoObjetivo { get; set; }

        
        public int CodigoComoConocioGym { get; set; }

        
        public int Codigo { get; set; }

		
		public int CodigoProspecto { get; set; }

        
		public int CodigoDatoDetalle { get; set; }
	    
        
        public string TipoDatoDetalle { get; set; }
	    
        public int CodigoSocio { get; set; }
        
        public int Flag { get; set; }
	
		
		public int CodigoSP { get; set; }

        
        public string DescripcionSP { get; set; }
	
		
		public int CodigoAE { get; set; }

        
        public int CodigoEI { get; set; }

        
        public string DescripcionAE { get; set; }
	    
		
		public int CodigoCCG { get; set; }

        
        public string DescripcionCCG { get; set; }
	
		
		public string Nombres { get; set; }

	    
        public DateTime FiltroFechaInicio { get; set; }

        
        public DateTime FiltroFechaFin { get; set; }
	
		
		public string Apellidos { get; set; }

        
        public int CantTotal { get; set; }

        
        public string NombreCompleto { get; set; }

        
        public string desOrigen { get; set; }
	
		
		public string Telefono { get; set; }
	
		public string Celular { get; set; }
        public string EstadoCelular { get; set; }
		
		public string Genero { get; set; }
	
		
		public string Facebook { get; set; }

        
        public string Correo { get; set; }

        
        public int TipoCliente { get; set; }

        
        public int TamanioPagina { get; set; }

        
        public string Observacion { get; set; }

        
        public string Ocupacion { get; set; }


        
        public int TipoConversion { get; set; }

        
        public int CodigoTiempo{ get; set; }

        
        public decimal Precio { get; set; }

        
		public int CodigoTipoPaquete { get; set; }

        
        public string DescripcionTipoPaquete { get; set; }
	
		
		public int CodigoPaquete { get; set; }

        
        public DateTime FechaNacimiento { get; set; }
        
        public string desFechaNacimiento { get; set; }

        
        public int Hijos { get; set; }

        
        public int CantHijos { get; set; }

        
        public string DNI { get; set; }

        
        public int ObjetivosClientes { get; set; }

        
        public int ComoSeEntero { get; set; }

        
        public string DescripcionPaquete { get; set; }
	
		
		public string Vendedor { get; set; }

        
        public int CodigoOrigen { get; set; }

        
        public string FechaCreacionDesc { get; set; }
	
		
		public bool Estado { get; set; }

        
        public int CantidadTotal { get; set; }

        
        public string Invitado_Observacion { get; set; }

        
        public int Invitado_NroDias { get; set; }

        
        public int Invitado_NroDiasActual { get; set; }

        
        public DateTime Invitado_FechaInicio { get; set; }

        
        public DateTime Invitado_FechaFin { get; set; }

        
        public int Invitado_CodigoInvitadoPor { get; set; }
	
		
        public Common.Operation Operation { get; set; }

        public string ColorOrigen { get; set; }

        public List<EncuestaNuevo1DTO> ListaDetalle_E { get; set; }

    }
	
	
	public class ReqProspectosDTO : Request
	{
		
        public List<ProspectosTablaDTO> List { get; set; }
	}
	
	
    public class ReqFilterProspectosDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ProspectosTablaDTO Item { get; set; }
        
        public Common.filterCaseTablaProspectos FilterCase { get; set; }
    }
	
	
    public class RespProspectosDTO : Response
    {
      
    }
	
	
    public class RespItemProspectosDTO : Response
    {
        
        public ProspectosTablaDTO Item { get; set; } 
    }

    
    public class RespListProspectosDTO : Response
    {
        
        public List<ProspectosTablaDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	