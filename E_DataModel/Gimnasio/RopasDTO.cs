
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{	
	public class RopasDTO : AuditoriaDTO
	{

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
        public int TamanioPagina { get; set; }
	
		
		public int CodigoProducto { get; set; }
	
		
		public string Descripcion { get; set; }
	
		
		public string Detalle { get; set; }

        
		public string Busqueda { get; set; }

	    
		public int CantTotal { get; set; }

		
		public string CodigoBarras { get; set; }
	
		
		public string ImagenUrl { get; set; }
	
		
		public bool Estado { get; set; }
	
		
		public decimal PrecioCompra { get; set; }
	
		
		public decimal PrecioVenta { get; set; }

        
        public int flag { get; set; }

        
		public int Cantidad { get; set; }
	
		
		public int CantidadMinima { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqRopasDTO : Request
	{
		
        public List<RopasDTO> List { get; set; }
	}
	
	
    public class ReqFilterRopasDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public RopasDTO Item { get; set; }
        
        public Common.filterCaseRopas FilterCase { get; set; }
    }
	
	
    public class RespRopasDTO : Response
    {
      
    }
	
	
    public class RespItemRopasDTO : Response
    {
        
        public RopasDTO Item { get; set; } 
    }

    
    public class RespListRopasDTO : Response
    {
        
        public List<RopasDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	