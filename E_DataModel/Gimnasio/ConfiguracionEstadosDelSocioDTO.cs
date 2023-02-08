
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{

	public class ConfiguracionEstadosDelSocioDTO : AuditoriaDTO
	{
	
		
		public int Codigo { get; set; }
			
		public string Descripcion { get; set; }
	
		
		public string Color { get; set; }
	
		
		public bool Estado { get; set; }

        
        public string DirigidoA { get; set; }    

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqConfiguracionEstadosDelSocioDTO : Request
	{
		
        public List<ConfiguracionEstadosDelSocioDTO> List { get; set; }
	}
	
	
    public class ReqFilterConfiguracionEstadosDelSocioDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ConfiguracionEstadosDelSocioDTO Item { get; set; }
        
        public Common.filterCaseConfiguracionEstadosDelSocio FilterCase { get; set; }
    }
	
	
    public class RespConfiguracionEstadosDelSocioDTO : Response
    {
      
    }
	
	
    public class RespItemConfiguracionEstadosDelSocioDTO : Response
    {
        
        public ConfiguracionEstadosDelSocioDTO Item { get; set; } 
    }

    
    public class RespListConfiguracionEstadosDelSocioDTO : Response
    {
        
        public List<ConfiguracionEstadosDelSocioDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	