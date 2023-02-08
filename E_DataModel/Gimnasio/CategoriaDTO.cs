using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class CategoriaDTO : AuditoriaDTO
	{
        
        public int CodigoCategoria { get; set; }

        
		public string Descripcion { get; set; }
	
		
		public bool Estado { get; set; }

        
        public string DescripcionEstado { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqCategoriaDTO : Request
	{
		
        public List<CategoriaDTO> List { get; set; }
	}
	
	
    public class ReqFilterCategoriaDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public CategoriaDTO Item { get; set; }
        
        public Common.filterCaseCategoria FilterCase { get; set; }
    }
	
	
    public class RespCategoriaDTO : Response
    {
      
    }
	
	
    public class RespItemCategoriaDTO : Response
    {
        
        public CategoriaDTO Item { get; set; } 
    }

    
    public class RespListCategoriaDTO : Response
    {
        
        public List<CategoriaDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	