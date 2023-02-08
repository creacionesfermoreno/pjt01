
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class EnvioCorreoDTO : AuditoriaDTO
	{
	
		
		public int EstadoEnvio { get; set; }

        
        public DateTime FechaEnvio { get; set; }

        
        public int Codigo { get; set; }

		
		public string CorreoSaliente { get; set; }
	
		
		public string Contrasenia { get; set; }
	
		
		public string Titulo { get; set; }

        
        public string Asunto { get; set; }

        
        public string Mensaje { get; set; }
	
		
		public int DiasAntesDeEnvioMem { get; set; }
	
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqEnvioCorreoDTO : Request
	{
		
        public List<EnvioCorreoDTO> List { get; set; }
	}
	
	
    public class ReqFilterEnvioCorreoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public EnvioCorreoDTO Item { get; set; }
        
        public Common.filterCaseEnvioCorreo FilterCase { get; set; }
    }
	
	
    public class RespEnvioCorreoDTO : Response
    {
      
    }
	
	
    public class RespItemEnvioCorreoDTO : Response
    {
        
        public EnvioCorreoDTO Item { get; set; } 
    }

    
    public class RespListEnvioCorreoDTO : Response
    {
        
        public List<EnvioCorreoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	