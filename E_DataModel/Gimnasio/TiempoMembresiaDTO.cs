
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class TiempoMembresiaDTO : AuditoriaDTO
	{
	
		
		public int CodigoTiempo { get; set; }
	
		
		public string Descripcion { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }

        
        public string Valor { get; set; }

        
        public int ValorDias { get; set; }
    }
	
	
	public class ReqTiempoMembresiaDTO : Request
	{
		
        public List<TiempoMembresiaDTO> List { get; set; }
	}
	
	
    public class ReqFilterTiempoMembresiaDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public TiempoMembresiaDTO Item { get; set; }
        
        public Common.filterCaseTiempoMembresia FilterCase { get; set; }
    }
	
	
    public class RespTiempoMembresiaDTO : Response
    {
      
    }
	
	
    public class RespItemTiempoMembresiaDTO : Response
    {
        
        public TiempoMembresiaDTO Item { get; set; } 
    }

    
    public class RespListTiempoMembresiaDTO : Response
    {
        
        public List<TiempoMembresiaDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	