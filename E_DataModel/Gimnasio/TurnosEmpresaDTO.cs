
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class TurnosEmpresaDTO : AuditoriaDTO
	{

        
        public int Codigo { get; set; }
        
		public string Descripcion { get; set; }
	
		
		public DateTime HoraInicio { get; set; }
	
		
		public DateTime HoraFin { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqTurnosEmpresaDTO : Request
	{
		
        public List<TurnosEmpresaDTO> List { get; set; }
	}
	
	
    public class ReqFilterTurnosEmpresaDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public TurnosEmpresaDTO Item { get; set; }
        
        public Common.filterCaseTurnosEmpresa FilterCase { get; set; }
    }
	
	
    public class RespTurnosEmpresaDTO : Response
    {
      
    }
	
	
    public class RespItemTurnosEmpresaDTO : Response
    {
        
        public TurnosEmpresaDTO Item { get; set; } 
    }

    
    public class RespListTurnosEmpresaDTO : Response
    {
        
        public List<TurnosEmpresaDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	