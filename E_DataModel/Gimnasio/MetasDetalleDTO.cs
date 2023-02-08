
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class MetasDetalleDTO : AuditoriaDTO
	{
	
		
		public int CodigoEntidadNegocio { get; set; }
	
	
		public int CodigoMeta { get; set; }
	
		
		public int CodigoVendedor { get; set; }	
		
		public decimal Meta { get; set; }
		public int CantidadMetaPlan { get; set; }
        
        public decimal MetaSemanal { get; set; }

        
		public DateTime FechaInicio { get; set; }
	
		
		public DateTime FechaFin { get; set; }

        
        public string NombreCompleto { get; set; }

		
		public int Codigo { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqMetasDetalleDTO : Request
	{
		
        public List<MetasDetalleDTO> List { get; set; }
	}
	
	
    public class ReqFilterMetasDetalleDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public MetasDetalleDTO Item { get; set; }
        
        public Common.filterCaseMetasDetalle FilterCase { get; set; }
    }
	
	
    public class RespMetasDetalleDTO : Response
    {
      
    }
	
	
    public class RespItemMetasDetalleDTO : Response
    {
        
        public MetasDetalleDTO Item { get; set; } 
    }

    
    public class RespListMetasDetalleDTO : Response
    {
        
        public List<MetasDetalleDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	