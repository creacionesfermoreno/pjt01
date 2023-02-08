
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class EnvioMensajeGPDTO : AuditoriaDTO
	{
	
		
		public int Codigo { get; set; }
	
		
        public int Perfil { get; set; }

        
        public int Tipo { get; set; }

        
        public int Usuario { get; set; }
	
		
		public int CodigoPerfilE { get; set; }
	
		
		public int CodigoPerfilR { get; set; }
	
		
		public int CodigoUsuarioE { get; set; }
	
		
		public int CodigoUsuarioR { get; set; }

        
        public string desFecha { get; set; }
	
		
		public string Mensaje { get; set; }

        
        public string desNomRec { get; set; }
	
		
		public bool Visto { get; set; }

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqEnvioMensajeGPDTO : Request
	{
		
        public List<EnvioMensajeGPDTO> List { get; set; }
	}
	
	
    public class ReqFilterEnvioMensajeGPDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public EnvioMensajeGPDTO Item { get; set; }
        
        public Common.filterCaseEnvioMensajeGP FilterCase { get; set; }
    }
	
	
    public class RespEnvioMensajeGPDTO : Response
    {
      
    }
	
	
    public class RespItemEnvioMensajeGPDTO : Response
    {
        
        public EnvioMensajeGPDTO Item { get; set; } 
    }

    
    public class RespListEnvioMensajeGPDTO : Response
    {
        
        public List<EnvioMensajeGPDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	