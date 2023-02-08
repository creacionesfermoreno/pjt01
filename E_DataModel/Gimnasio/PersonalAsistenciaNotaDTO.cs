
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class PersonalAsistenciaNotaDTO : AuditoriaDTO
	{
        
		public string CodigoPersonalAsistencia { get; set; }	
		
		public string CodigoPersonalAsistenciaNota { get; set; }	
		
		public string Nota { get; set; }	
		
        public DateTime FechaRegistro { get; set; }
		
        public Common.Operation Operation { get; set; } 
        
        public string Xml { get; set; }
	}
	
	
	public class ReqPersonalAsistenciaNotaDTO : Request
	{
		
        public List<PersonalAsistenciaNotaDTO> List { get; set; }
	}
	
	
    public class ReqFilterPersonalAsistenciaNotaDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public PersonalAsistenciaNotaDTO Item { get; set; }
        
        public Common.filterCasePersonalAsistenciaNota FilterCase { get; set; }
    }
	
	
    public class RespPersonalAsistenciaNotaDTO : Response
    {
      
    }
	
	
    public class RespItemPersonalAsistenciaNotaDTO : Response
    {
        
        public PersonalAsistenciaNotaDTO Item { get; set; } 
    }

    
    public class RespListPersonalAsistenciaNotaDTO : Response
    {
        
        public List<PersonalAsistenciaNotaDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	