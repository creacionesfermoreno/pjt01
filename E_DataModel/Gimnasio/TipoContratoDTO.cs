
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class TipoContratoDTO : AuditoriaDTO
	{	    
		
		public int Codigo { get; set; }
	
		
        public string Descripcion { get; set; }
	
		
		public string Clausula { get; set; }
	    
        
        public string Compromiso { get; set; }

        public int CodigoSerie { get; set; }
        public string NroSerie { get; set; }
        public string NroCorrelativoActual{ get; set; }

        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqTipoContratoDTO : Request
	{
		
        public List<TipoContratoDTO> List { get; set; }
	}
	
	
    public class ReqFilterTipoContratoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public TipoContratoDTO Item { get; set; }
        
        public Common.filterCaseTipoContrato FilterCase { get; set; }
    }
	
	
    public class RespTipoContratoDTO : Response
    {
      
    }
	
	
    public class RespItemTipoContratoDTO : Response
    {
        
        public TipoContratoDTO Item { get; set; } 
    }

    
    public class RespListTipoContratoDTO : Response
    {
        
        public List<TipoContratoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	