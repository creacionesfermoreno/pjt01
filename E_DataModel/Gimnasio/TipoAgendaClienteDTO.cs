using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class TipoAgendaClienteDTO : AuditoriaDTO
	{
		
		public int Codigo { get; set; }
	
		
		public string Descripcion { get; set; }

        
        public string Color { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqTipoAgendaClienteDTO : Request
	{
		
        public List<TipoAgendaClienteDTO> List { get; set; }
	}
	
	
    public class ReqFilterTipoAgendaClienteDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public TipoAgendaClienteDTO Item { get; set; }
        
        public Common.filterCaseTipoAgendaCliente FilterCase { get; set; }
    }
	
	
    public class RespTipoAgendaClienteDTO : Response
    {
      
    }
	
	
    public class RespItemTipoAgendaClienteDTO : Response
    {
        
        public TipoAgendaClienteDTO Item { get; set; } 
    }

    
    public class RespListTipoAgendaClienteDTO : Response
    {
        
        public List<TipoAgendaClienteDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	