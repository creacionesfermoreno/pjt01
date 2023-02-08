
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	public class ContratoMensajeDTO : AuditoriaDTO
	{
        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

		
		public int Codigo { get; set; }
	
		
		public int CodigoMenbresia { get; set; }

        public int CodigoSocio { get; set; }
	
		
		public string Ocurrencia { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqContratoMensajeDTO : Request
	{
		
        public List<ContratoMensajeDTO> List { get; set; }
	}
	
	
    public class ReqFilterContratoMensajeDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ContratoMensajeDTO Item { get; set; }
        
        public Common.filterCaseContratoMensaje FilterCase { get; set; }
    }
	
	
    public class RespContratoMensajeDTO : Response
    {
      
    }
	
	
    public class RespItemContratoMensajeDTO : Response
    {
        
        public ContratoMensajeDTO Item { get; set; } 
    }

    
    public class RespListContratoMensajeDTO : Response
    {
        
        public List<ContratoMensajeDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	