
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class AyudaPreguntasFrecuentesDTO : AuditoriaDTO
	{
	
		
		public int Codigo { get; set; }
	
		
		public string Descripcion { get; set; }
	
		
		public string LinckRespuesta { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqAyudaPreguntasFrecuentesDTO : Request
	{
		
        public List<AyudaPreguntasFrecuentesDTO> List { get; set; }
	}
	
	
    public class ReqFilterAyudaPreguntasFrecuentesDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public AyudaPreguntasFrecuentesDTO Item { get; set; }
        
        public Common.filterCaseAyudaPreguntasFrecuentes FilterCase { get; set; }
    }
	
	
    public class RespAyudaPreguntasFrecuentesDTO : Response
    {
      
    }
	
	
    public class RespItemAyudaPreguntasFrecuentesDTO : Response
    {
        
        public AyudaPreguntasFrecuentesDTO Item { get; set; } 
    }

    
    public class RespListAyudaPreguntasFrecuentesDTO : Response
    {
        
        public List<AyudaPreguntasFrecuentesDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	