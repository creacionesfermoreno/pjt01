using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
		
	public class ComprasDTO : AuditoriaDTO
	{
        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
		public int CodigoIngreso { get; set; }

        
        public int CodigoProveedor { get; set; }
        
        
        public int CodigoTipoDocumento { get; set; }

		public string NroDocumento { get; set; }
	    
		
		public DateTime FechaIngreso { get; set; }

        
        public decimal Percepcion { get; set; }

        
        public DateTime FechaInicio { get; set; }

        
        public DateTime FechaFin { get; set; }

		
		public decimal TotalNeto { get; set; }
	    
		
        public Common.Operation Operation { get; set; } 
        
        
        public string Xml { get; set; }

        
        public List<ComprasDetalleDTO> ListaComprasDetalleDTO { get; set; }       

	}
	
	
	public class ReqComprasDTO : Request
	{
		
        public List<ComprasDTO> List { get; set; }
	}
	
	
    public class ReqFilterComprasDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ComprasDTO Item { get; set; }
        
        public Common.filterCaseCompras FilterCase { get; set; }
    }
	
	
    public class RespComprasDTO : Response
    {
      
    }
	
	
    public class RespItemComprasDTO : Response
    {
        
        public ComprasDTO Item { get; set; } 
    }

    
    public class RespListComprasDTO : Response
    {
        
        public List<ComprasDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	