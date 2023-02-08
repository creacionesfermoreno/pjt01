
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class AsistenciaInvitadosDTO : AuditoriaDTO
	{
      
		public int CodigoAsistenciaI { get; set; }
	
		
		public int CodigoInvitado { get; set; }

        
        public int fila { get; set; }

       
        public int CantTotal { get; set; }

       
        public int TamanioPagina { get; set; }
	
		
		public DateTime FechaIngreso { get; set; }

		
        public string NombreCompleto { get; set; }

	   
        public DateTime FechaFinalizo { get; set; }

       
       public DateTime HoraInicioAsistencia { get; set; }

       
       public DateTime HoraFinAsistencia { get; set; }

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqAsistenciaInvitadosDTO : Request
	{
		
        public List<AsistenciaInvitadosDTO> List { get; set; }
	}
	
	
    public class ReqFilterAsistenciaInvitadosDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public AsistenciaInvitadosDTO Item { get; set; }
        
        public Common.filterCaseAsistenciaInvitados FilterCase { get; set; }
    }
	
	
    public class RespAsistenciaInvitadosDTO : Response
    {
      
    }
	
	
    public class RespItemAsistenciaInvitadosDTO : Response
    {
        
        public AsistenciaInvitadosDTO Item { get; set; } 
    }

    
    public class RespListAsistenciaInvitadosDTO : Response
    {
        
        public List<AsistenciaInvitadosDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	