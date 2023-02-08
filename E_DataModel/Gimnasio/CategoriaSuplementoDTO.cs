
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class CategoriaSuplementoDTO : AuditoriaDTO
	{
	
		public int CodigoCateSuplemento { get; set; }
	
		
		public string Descripcion { get; set; }
	
		
		public bool Estado { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqCategoriaSuplementoDTO : Request
	{
		
        public List<CategoriaSuplementoDTO> List { get; set; }
	}
	
	
    public class ReqFilterCategoriaSuplementoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public CategoriaSuplementoDTO Item { get; set; }
        
        public Common.filterCaseCategoriaSuplemento FilterCase { get; set; }
    }
	
	
    public class RespCategoriaSuplementoDTO : Response
    {
      
    }
	
	
    public class RespItemCategoriaSuplementoDTO : Response
    {
        
        public CategoriaSuplementoDTO Item { get; set; } 
    }

    
    public class RespListCategoriaSuplementoDTO : Response
    {
        
        public List<CategoriaSuplementoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	