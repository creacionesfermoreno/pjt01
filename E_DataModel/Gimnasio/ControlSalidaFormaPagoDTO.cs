
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class ControlSalidaFormaPagoDTO : AuditoriaDTO
    {   
       
		
		public int Codigo { get; set; }
	
		
		public int CodigoIngreso { get; set; }
	
		
		public int TipoMoneda { get; set; }
	
		
		public decimal Monto { get; set; }
	
		
		public decimal TipoCambio { get; set; }
	
		
		public int FormaPago { get; set; }
	
		
		public int SubFormaPago { get; set; }
	
		
		public string NroBoucher { get; set; }
	
		
		public string UrlBoucher { get; set; }

        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqControlSalidaFormaPagoDTO : Request
	{
		
        public List<ControlSalidaFormaPagoDTO> List { get; set; }
	}
	
	
    public class ReqFilterControlSalidaFormaPagoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ControlSalidaFormaPagoDTO Item { get; set; }
        
        public Common.filterCaseControlSalidaFormaPago FilterCase { get; set; }
    }
	
	
    public class RespControlSalidaFormaPagoDTO : Response
    {
      
    }
	
	
    public class RespItemControlSalidaFormaPagoDTO : Response
    {
        
        public ControlSalidaFormaPagoDTO Item { get; set; } 
    }

    
    public class RespListControlSalidaFormaPagoDTO : Response
    {
        
        public List<ControlSalidaFormaPagoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	