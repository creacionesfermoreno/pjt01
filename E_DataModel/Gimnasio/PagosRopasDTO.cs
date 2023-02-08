
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{    
	public class PagosRopasDTO : AuditoriaDTO
	{
        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
		public int TamanioPagina { get; set; }

        
		public DateTime FechaInicio { get; set; }

        
        public DateTime FechaFin { get; set; }

        
		public int CodigoPago { get; set; }
	
		
		public int CodigoSalida { get; set; }

        
        public int TipoMoneda { get; set; }

        
        public int CantidadRegistros { get; set; }

        
        public decimal TotalDeuda { get; set; }

        
        public decimal Total { get; set; }

        
        public decimal Saldo { get; set; }

        
		public decimal Monto { get; set; }

        
        public decimal TipoCambio { get; set; }

        
		public DateTime FechaPago { get; set; }
	
		
		public string NroComprobante { get; set; }
	
		
		public int FormaPago { get; set; }
	
		
		public int SubFormaPago { get; set; }
	
		
		public int Estado { get; set; }

	    
		public string User { get; set; }

		
		public string NroBoucher { get; set; }

        
        public int CodigoSocio { get; set; }

        
        public string Cliente { get; set; }

        
        public string Telefono { get; set; }

        
        public string Dni { get; set; }

        
        public string Distrito { get; set; }

        
        public string Direccion { get; set; }

        
        public string Descripcion { get; set; }

        
        public decimal Debe { get; set; }

        
        public string Responsable { get; set; }

        
		public int CodigoTipoComprobante { get; set; }
	
		
		public int CodigoSubTipoComprobante { get; set; }
	
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqPagosRopasDTO : Request
	{
		
        public List<PagosRopasDTO> List { get; set; }
	}
	
	
    public class ReqFilterPagosRopasDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public PagosRopasDTO Item { get; set; }
        
        public Common.filterCasePagosRopas FilterCase { get; set; }
    }
	
	
    public class RespPagosRopasDTO : Response
    {
      
    }
	
	
    public class RespItemPagosRopasDTO : Response
    {
        
        public PagosRopasDTO Item { get; set; } 
    }

    
    public class RespListPagosRopasDTO : Response
    {
        
        public List<PagosRopasDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	