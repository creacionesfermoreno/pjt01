
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	//Archivo     : HistorialCongelamientoDTO.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 10/11/2015
	//Descripcion : Entidad de negocio
	
	
	public class HistorialCongelamientoDTO : AuditoriaDTO
	{

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
        public string NombreSocio { get; set; }

        
        public string NombrePaquete { get; set; }

		
		public int Codigo { get; set; }

		
		public int CodigoMembresia { get; set; }
	
		
		public int CodigoSocio { get; set; }
	
		
		public DateTime FechaInicio { get; set; }
	
		
		public DateTime FechaFin { get; set; }
	
		
		public int NroDias { get; set; }
	
        
        public string NroSolicitud { get; set; }

        
        public string Motivo { get; set; }

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqHistorialCongelamientoDTO : Request
	{
		
        public List<HistorialCongelamientoDTO> List { get; set; }
	}
	
	
    public class ReqFilterHistorialCongelamientoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public HistorialCongelamientoDTO Item { get; set; }
        
        public Common.filterCaseHistorialCongelamiento FilterCase { get; set; }
    }
	
	
    public class RespHistorialCongelamientoDTO : Response
    {
      
    }
	
	
    public class RespItemHistorialCongelamientoDTO : Response
    {
        
        public HistorialCongelamientoDTO Item { get; set; } 
    }

    
    public class RespListHistorialCongelamientoDTO : Response
    {
        
        public List<HistorialCongelamientoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	