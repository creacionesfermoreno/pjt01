
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class SuplementosDTO : AuditoriaDTO
	{
        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }
        
		public int CodigoSuplemento { get; set; }
	
		
		public int CodigoCategoria { get; set; }

        
		public int TamanioPagina { get; set; }
	
		
		public string Descripcion { get; set; }

        
		public string Busqueda { get; set; }
	
		
		public string Detalle { get; set; }
	
		
		public string CodigoBarras { get; set; }
	
		
		public string imagenUrl { get; set; }
	
		
		public bool Estado { get; set; }
	
		
		public decimal PrecioCompra { get; set; }
	
		
		public decimal PrecioVenta { get; set; }

		
		public int flag { get; set; }
	
		
		public int Cantidad { get; set; }

        
		public int CantTotal { get; set; }
	
		
		public int CantidadMinima { get; set; }

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqSuplementosDTO : Request
	{
		
        public List<SuplementosDTO> List { get; set; }
	}
	
	
    public class ReqFilterSuplementosDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public SuplementosDTO Item { get; set; }
        
        public Common.filterCaseSuplementos FilterCase { get; set; }
    }
	
	
    public class RespSuplementosDTO : Response
    {
      
    }
	
	
    public class RespItemSuplementosDTO : Response
    {
        
        public SuplementosDTO Item { get; set; } 
    }

    
    public class RespListSuplementosDTO : Response
    {
        
        public List<SuplementosDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	