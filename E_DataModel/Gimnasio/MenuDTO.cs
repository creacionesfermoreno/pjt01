using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class MenuDTO : AuditoriaDTO
	{
		
		public int CodigoMenu { get; set; }
	    
		
		public int CodigoMenuSuperior { get; set; }
	    
		
		public string Descripcion { get; set; }
	    
		
		public string Url { get; set; }
	    
		
		public string Tipo { get; set; }
	    
		
		public int Orden { get; set; }
	    
		
		public bool Estado { get; set; }
	    
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqMenuDTO : Request
	{
		
        public List<MenuDTO> List { get; set; }
	}
	
	
    public class ReqFilterMenuDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public MenuDTO Item { get; set; }
        
        public Common.filterCaseMenu FilterCase { get; set; }
    }
	
	
    public class RespMenuDTO : Response
    {
      
    }
	
	
    public class RespItemMenuDTO : Response
    {
        
        public MenuDTO Item { get; set; } 
    }

    
    public class RespListMenuDTO : Response
    {
        
        public List<MenuDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	