
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class TipoIngresoDTO : AuditoriaDTO
	{
	
		
		public int Codigo { get; set; }
	
		
		public string Descripcion { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqTipoIngresoDTO : Request
	{
		
        public List<TipoIngresoDTO> List { get; set; }
	}
	
	
    public class ReqFilterTipoIngresoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public TipoIngresoDTO Item { get; set; }
        
        public Common.filterCaseTipoIngreso FilterCase { get; set; }
    }
	
	
    public class RespTipoIngresoDTO : Response
    {
      
    }
	
	
    public class RespItemTipoIngresoDTO : Response
    {
        
        public TipoIngresoDTO Item { get; set; } 
    }

    
    public class RespListTipoIngresoDTO : Response
    {
        
        public List<TipoIngresoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	