using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class TipoDocumentoDTO : AuditoriaDTO
	{
	
		public int codigo { get; set; }
	
		
		public string Descripcion { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqTipoDocumentoDTO : Request
	{
		
        public List<TipoDocumentoDTO> List { get; set; }
	}
	
	
    public class ReqFilterTipoDocumentoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public TipoDocumentoDTO Item { get; set; }
        
        public Common.filterCaseTipoDocumento FilterCase { get; set; }
    }
	
	
    public class RespTipoDocumentoDTO : Response
    {
      
    }
	
	
    public class RespItemTipoDocumentoDTO : Response
    {
        
        public TipoDocumentoDTO Item { get; set; } 
    }

    
    public class RespListTipoDocumentoDTO : Response
    {
        
        public List<TipoDocumentoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	