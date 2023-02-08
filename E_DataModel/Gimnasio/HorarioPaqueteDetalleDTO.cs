
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	public class HorarioPaqueteDetalleDTO : AuditoriaDTO
	{
	  
		
		public int CodigoHPD { get; set; }
	
		
		public int CodigoHP { get; set; }
	
		
		public DateTime HoraInicio { get; set; }
	
		
		public DateTime horaFin { get; set; }

        
        public string desHora { get; set; }

        
        public int CodigoPaquete { get; set; }

        
        public int Dia { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqHorarioPaqueteDetalleDTO : Request
	{
		
        public List<HorarioPaqueteDetalleDTO> List { get; set; }
	}
	
	
    public class ReqFilterHorarioPaqueteDetalleDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public HorarioPaqueteDetalleDTO Item { get; set; }
        
        public Common.filterCaseHorarioPaqueteDetalle FilterCase { get; set; }
    }
	
	
    public class RespHorarioPaqueteDetalleDTO : Response
    {
      
    }
	
	
    public class RespItemHorarioPaqueteDetalleDTO : Response
    {
        
        public HorarioPaqueteDetalleDTO Item { get; set; } 
    }

    
    public class RespListHorarioPaqueteDetalleDTO : Response
    {
        
        public List<HorarioPaqueteDetalleDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	