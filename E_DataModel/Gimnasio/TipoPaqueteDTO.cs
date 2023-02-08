
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	
	public class TipoPaqueteDTO : AuditoriaDTO
	{
	
		
		public int Codigo { get; set; }
			
		public string Descripcion { get; set; }

		
		public string Detalle { get; set; }
	
		
		public int Estado { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqTipoPaqueteDTO : Request
	{
		
        public List<TipoPaqueteDTO> List { get; set; }
	}
	
	
    public class ReqFilterTipoPaqueteDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public TipoPaqueteDTO Item { get; set; }
        
        public Common.filterCaseTipoPaquete FilterCase { get; set; }
    }
	
	
    public class RespTipoPaqueteDTO : Response
    {
      
    }
	
	
    public class RespItemTipoPaqueteDTO : Response
    {
        
        public TipoPaqueteDTO Item { get; set; } 
    }

    
    public class RespListTipoPaqueteDTO : Response
    {
        
        public List<TipoPaqueteDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	