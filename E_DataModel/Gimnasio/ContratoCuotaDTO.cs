using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class ContratoCuotaDTO : AuditoriaDTO
	{
        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }
        
        
        public int CodigoCuota { get; set; }
        
		public int CodigoMembresia { get; set; }
	
		
		public DateTime Fecha { get; set; }

        
        public string DescFecha { get; set; }

        
        public decimal Monto { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqContratoCuotaDTO : Request
	{
		
        public List<ContratoCuotaDTO> List { get; set; }
	}
	
	
    public class ReqFilterContratoCuotaDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ContratoCuotaDTO Item { get; set; }
        
        public Common.filterCaseContratoCuota FilterCase { get; set; }
    }
	
	
    public class RespContratoCuotaDTO : Response
    {
      
    }
	
	
    public class RespItemContratoCuotaDTO : Response
    {
        
        public ContratoCuotaDTO Item { get; set; } 
    }

    
    public class RespListContratoCuotaDTO : Response
    {
        
        public List<ContratoCuotaDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	