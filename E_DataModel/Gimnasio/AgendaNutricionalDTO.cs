
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	//Archivo     : AgendaNutricionalDTO.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 22/08/2017
	//Descripcion : Entidad de negocio
	
	
	public class AgendaNutricionalDTO : AuditoriaDTO
	{
		public int Codigo { get; set; }
	
		
		public int CodigoSocio { get; set; }
	
		
		public DateTime HoraInicio { get; set; }

        
        public string Buscador { get; set; }

        
		public string Asunto { get; set; }
	
		
        public int Estado { get; set; }

        
        public DateTime FechaInicio_Filtro { get; set; }

        
        public DateTime FechaFin_Filtro { get; set; }

        
        public string strFechaCreacion { get; set; }

        
        public string AsuntoCompleto { get; set; }
        
        public int CantidadRegistros { get; set; }
        
        public int Seguimiento_Cliente { get; set; }

         
        public int TipoActividad { get; set; }

        
        public int TamanioPagina { get; set; }

        
        public string Nombres { get; set; }

        
        public string Apellidos { get; set; }

        
        public string Celular { get; set; }

        
        public string EstadoCelular { get; set; }

        
        public string Correo { get; set; }

        
        public string Genero { get; set; }

        
        public string DesTipoActividad { get; set; }
        public string UrlIconoTipoActividad { get; set; }

        
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqAgendaNutricionalDTO : Request
	{
		
        public List<AgendaNutricionalDTO> List { get; set; }
	}
	
	
    public class ReqFilterAgendaNutricionalDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public AgendaNutricionalDTO Item { get; set; }
        
        public Common.filterCaseAgendaNutricional FilterCase { get; set; }
    }
	
	
    public class RespAgendaNutricionalDTO : Response
    {
      
    }
	
	
    public class RespItemAgendaNutricionalDTO : Response
    {
        
        public AgendaNutricionalDTO Item { get; set; } 
    }

    
    public class RespListAgendaNutricionalDTO : Response
    {
        
        public List<AgendaNutricionalDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	