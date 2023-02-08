
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class ConfiguracionTraspasoDTO : AuditoriaDTO
	{
		public int Estado { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqConfiguracionTraspasoDTO : Request
	{
		
        public List<ConfiguracionTraspasoDTO> List { get; set; }
	}
	
	
    public class ReqFilterConfiguracionTraspasoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ConfiguracionTraspasoDTO Item { get; set; }
        
        public Common.filterCaseConfiguracionTraspaso FilterCase { get; set; }
    }
	
	
    public class RespConfiguracionTraspasoDTO : Response
    {
      
    }
	
	
    public class RespItemConfiguracionTraspasoDTO : Response
    {
        
        public ConfiguracionTraspasoDTO Item { get; set; } 
    }

    
    public class RespListConfiguracionTraspasoDTO : Response
    {
        
        public List<ConfiguracionTraspasoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	