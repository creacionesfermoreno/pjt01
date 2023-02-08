using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
		
	public class PerfilDTO : AuditoriaDTO
	{
		
		public int CodigoPerfil { get; set; }

        
        public int TipoSistema { get; set; }
	
		
		public string Descripcion { get; set; }
	
		
		public bool Estado { get; set; }

        
        public string Check { get; set; }
	    
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqPerfilDTO : Request
	{
		
        public List<PerfilDTO> List { get; set; }

	}
	
	
    public class ReqFilterPerfilDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public PerfilDTO Item { get; set; }
        
        public Common.filterCasePerfil FilterCase { get; set; }
    }
	
	
    public class RespPerfilDTO : Response
    {
      
    }
	
	
    public class RespItemPerfilDTO : Response
    {
        
        public PerfilDTO Item { get; set; } 
    }

    
    public class RespListPerfilDTO : Response
    {
        
        public List<PerfilDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	