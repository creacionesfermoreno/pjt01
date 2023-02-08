using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class TipoEgresoDTO : AuditoriaDTO
	{
	
		
		public int Codigo { get; set; }
	
		
		public string Descripcion { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqTipoEgresoDTO : Request
	{
		
        public List<TipoEgresoDTO> List { get; set; }
	}
	
	
    public class ReqFilterTipoEgresoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public TipoEgresoDTO Item { get; set; }
        
        public Common.filterCaseTipoEgreso FilterCase { get; set; }
    }
	
	
    public class RespTipoEgresoDTO : Response
    {
      
    }
	
	
    public class RespItemTipoEgresoDTO : Response
    {
        
        public TipoEgresoDTO Item { get; set; } 
    }

    
    public class RespListTipoEgresoDTO : Response
    {
        
        public List<TipoEgresoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	