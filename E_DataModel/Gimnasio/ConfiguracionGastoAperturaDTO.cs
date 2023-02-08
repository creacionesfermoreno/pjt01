
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class ConfiguracionGastoAperturaDTO : AuditoriaDTO
	{
        public int ActivarCaja { get; set; }
	
		
		public int CodigoTipoGastoCorporativo { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqConfiguracionGastoAperturaDTO : Request
	{
		
        public List<ConfiguracionGastoAperturaDTO> List { get; set; }
	}
	
	
    public class ReqFilterConfiguracionGastoAperturaDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ConfiguracionGastoAperturaDTO Item { get; set; }
        
        public Common.filterCaseConfiguracionGastoApertura FilterCase { get; set; }
    }
	
	
    public class RespConfiguracionGastoAperturaDTO : Response
    {
      
    }
	
	
    public class RespItemConfiguracionGastoAperturaDTO : Response
    {
        
        public ConfiguracionGastoAperturaDTO Item { get; set; } 
    }

    
    public class RespListConfiguracionGastoAperturaDTO : Response
    {
        
        public List<ConfiguracionGastoAperturaDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	