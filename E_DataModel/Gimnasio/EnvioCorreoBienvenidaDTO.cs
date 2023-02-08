
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class EnvioCorreoBienvenidaDTO : AuditoriaDTO
	{
	
		
		public int Codigo { get; set; }
	
		
		public string CorreoSaliente { get; set; }
	
		
		public string Contrasenia { get; set; }


        
        public string CorreoSaliente2 { get; set; }

        
        public string Contrasenia2 { get; set; }
	
		
		public string Titulo { get; set; }
	
		
		public string Asunto { get; set; }
	
		
		public string Mensaje { get; set; }

        
        public string MensajeCompleto { get; set; }
	
		public int CodigoSocio { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqEnvioCorreoBienvenidaDTO : Request
	{
		
        public List<EnvioCorreoBienvenidaDTO> List { get; set; }
	}
	
	
    public class ReqFilterEnvioCorreoBienvenidaDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public EnvioCorreoBienvenidaDTO Item { get; set; }
        
        public Common.filterCaseEnvioCorreoBienvenida FilterCase { get; set; }
    }
	
	
    public class RespEnvioCorreoBienvenidaDTO : Response
    {
      
    }
	
	
    public class RespItemEnvioCorreoBienvenidaDTO : Response
    {
        
        public EnvioCorreoBienvenidaDTO Item { get; set; } 
    }

    
    public class RespListEnvioCorreoBienvenidaDTO : Response
    {
        
        public List<EnvioCorreoBienvenidaDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	