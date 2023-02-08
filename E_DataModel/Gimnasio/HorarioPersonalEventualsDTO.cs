
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class HorarioPersonalEventualsDTO : AuditoriaDTO
	{

		public int CodigoHorario { get; set; }
	
		
		public int CodigoPersonal { get; set; }
	
		
		public int CodigoEspecialidad { get; set; }
	
		
		public string Disciplina { get; set; }
	
		
		public decimal PagoXhora { get; set; }
	
		
		public int Dia { get; set; }
	
		
		public DateTime HoraInicio { get; set; }

        
        public string desHoraInicio { get; set; }

        
        public string desHoraFin { get; set; }

        
		public DateTime HoraFin { get; set; }
	
		
		public int NroCupos { get; set; }

        
        public string NombreCompleto { get; set; }

        
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqHorarioPersonalEventualsDTO : Request
	{
		
        public List<HorarioPersonalEventualsDTO> List { get; set; }
	}
	
	
    public class ReqFilterHorarioPersonalEventualsDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public HorarioPersonalEventualsDTO Item { get; set; }
        
        public Common.filterCaseHorarioPersonalEventuals FilterCase { get; set; }
    }
	
	
    public class RespHorarioPersonalEventualsDTO : Response
    {
      
    }
	
	
    public class RespItemHorarioPersonalEventualsDTO : Response
    {
        
        public HorarioPersonalEventualsDTO Item { get; set; } 
    }

    
    public class RespListHorarioPersonalEventualsDTO : Response
    {
        
        public List<HorarioPersonalEventualsDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	