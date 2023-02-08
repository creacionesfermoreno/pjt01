
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class InteresProspectosDTO : AuditoriaDTO
	{

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
		public int CodigoInteres { get; set; }
	
		
		public string Descripcion { get; set; }

        
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqInteresProspectosDTO : Request
	{
		
        public List<InteresProspectosDTO> List { get; set; }
	}
	
	
    public class ReqFilterInteresProspectosDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public InteresProspectosDTO Item { get; set; }
        
        public Common.filterCaseInteresProspectos FilterCase { get; set; }
    }
	
	
    public class RespInteresProspectosDTO : Response
    {
      
    }
	
	
    public class RespItemInteresProspectosDTO : Response
    {
        
        public InteresProspectosDTO Item { get; set; } 
    }

    
    public class RespListInteresProspectosDTO : Response
    {
        
        public List<InteresProspectosDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	