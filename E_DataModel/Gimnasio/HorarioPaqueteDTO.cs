
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class HorarioPaqueteDTO : AuditoriaDTO
	{

		
		public int CodigoHP { get; set; }
	
		
		public int CodigoPaquete { get; set; }
	
		
		public int Dia { get; set; }

        
        public string desDia { get; set; }

        
        public int Estado { get; set; }

        
        public string Check { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }

        
        public List<HorarioPaqueteDTO> ListaDetalle_H { get; set; }
	}
	
	
	public class ReqHorarioPaqueteDTO : Request
	{
		
        public List<HorarioPaqueteDTO> List { get; set; }
	}
	
	
    public class ReqFilterHorarioPaqueteDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public HorarioPaqueteDTO Item { get; set; }
        
        public Common.filterCaseHorarioPaquete FilterCase { get; set; }
    }
	
	
    public class RespHorarioPaqueteDTO : Response
    {
      
    }
	
	
    public class RespItemHorarioPaqueteDTO : Response
    {
        
        public HorarioPaqueteDTO Item { get; set; } 
    }

    
    public class RespListHorarioPaqueteDTO : Response
    {
        
        public List<HorarioPaqueteDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	