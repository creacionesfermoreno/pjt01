
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class PagosContratoDTO : AuditoriaDTO
	{
        public int CodigoSocio { get; set; }

        public int CodigoPagoMembresia { get; set; }

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string DesPlan { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int CodigoMembresia { get; set; }

        
        public int CodigoControlSalidaFormaPago { get; set; }

		
		public decimal Monto { get; set; }

        
        public string desTarjeta { get; set; }

        
        public string nroBoucher{ get; set; }
        public string Comentario { get; set; }

        
        public string NroComprobante { get; set; }

        
        public string desComprobante { get; set; }
	
		
		public DateTime FechaPago { get; set; }

        
		public string desFechaPago { get; set; }

        
        public DateTime FechaNewPago { get; set; }

        
        public int FormaPago { get; set; }

        
        public string DesFormaPago { get; set; }

        
        public string nroTarjeta { get; set; }

        
        public int Estado { get; set; }

        public string ColorEstado { get; set; }
        public string DesEstado { get; set; }
        
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqPagosContratoDTO : Request
	{
		
        public List<PagosContratoDTO> List { get; set; }
	}
	
	
    public class ReqFilterPagosContratoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public PagosContratoDTO Item { get; set; }
        
        public Common.filterCasePagosContrato FilterCase { get; set; }
    }
	
	
    public class RespPagosContratoDTO : Response
    {
      
    }
	
	
    public class RespItemPagosContratoDTO : Response
    {
        
        public PagosContratoDTO Item { get; set; } 
    }

    
    public class RespListPagosContratoDTO : Response
    {
        
        public List<PagosContratoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	