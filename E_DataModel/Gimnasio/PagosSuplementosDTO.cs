
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
		
	public class PagosSuplementosDTO : AuditoriaDTO
	{

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }
        	
		public int CodigoPago { get; set; }
	
		
		public int CodigoSalida { get; set; }

        
		public int CantidadRegistros { get; set; }

        
		public decimal TotalDeuda { get; set; }

        
        public int CodigoSocio { get; set; }

        
        public string Cliente { get; set; }

        
        public string Telefono { get; set; }

        
        public int TamanioPagina { get; set; }

        
        public string Dni { get; set; }

        
        public string Distrito { get; set; }

        
        public string Direccion { get; set; }

        
        public decimal Debe { get; set; }

        
        public string Responsable { get; set; }

        
        public string Descripcion { get; set; }

        
        public DateTime FechaInicio { get; set; }

        
        public DateTime FechaFin { get; set; }

        
        public decimal TipoCambio { get; set; }

        
        public int TipoMoneda { get; set; }

        
        public decimal Total { get; set; }

        
        public decimal Saldo { get; set; }

        
		public decimal Monto { get; set; }
	
		
		public DateTime FechaPago { get; set; }
	
		
		public string NroComprobante { get; set; }
	
		
		public int FormaPago { get; set; }

        
        public int SubFormaPago { get; set; }

        
		public int Estado { get; set; }
	
		
		public string NroBoucher { get; set; }

        
		public string User { get; set; }

        
        public int CodigoTipoComprobante { get; set; }

        
        public int CodigoSubTipoComprobante { get; set; }

        
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqPagosSuplementosDTO : Request
	{
		
        public List<PagosSuplementosDTO> List { get; set; }
	}
	
	
    public class ReqFilterPagosSuplementosDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public PagosSuplementosDTO Item { get; set; }
        
        public Common.filterCasePagosSuplementos FilterCase { get; set; }
    }
	
	
    public class RespPagosSuplementosDTO : Response
    {
      
    }
	
	
    public class RespItemPagosSuplementosDTO : Response
    {
        
        public PagosSuplementosDTO Item { get; set; } 
    }

    
    public class RespListPagosSuplementosDTO : Response
    {
        
        public List<PagosSuplementosDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	