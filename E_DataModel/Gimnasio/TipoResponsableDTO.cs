
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class TipoResponsableDTO : AuditoriaDTO
	{
	
		
		public int CodigoTipoResponsable { get; set; }

        
		public string NombreResponsable { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqTipoResponsableDTO : Request
	{
		
        public List<TipoResponsableDTO> List { get; set; }
	}
	
	
    public class ReqFilterTipoResponsableDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public TipoResponsableDTO Item { get; set; }
        
        public Common.filterCaseTipoResponsable FilterCase { get; set; }
    }
	
	
    public class RespTipoResponsableDTO : Response
    {
      
    }
	
	
    public class RespItemTipoResponsableDTO : Response
    {
        
        public TipoResponsableDTO Item { get; set; } 
    }

    
    public class RespListTipoResponsableDTO : Response
    {
        
        public List<TipoResponsableDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	