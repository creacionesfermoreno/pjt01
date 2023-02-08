
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
		
	public class ProductoElaboradoDTO : AuditoriaDTO
	{
        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
        public int Tipo { get; set; }

        
        public int CodigoDetalle { get; set; }
        
		public int CodigoProducto { get; set; }
	
		
		public string Descripcion { get; set; }

        
		public string Modelo { get; set; }

        
		public string Busqueda { get; set; }

        
        public int CodigoCategoria { get; set; }

        
        public int CantTotal { get; set; }

        
        public int TamanioPagina { get; set; }

        
        public int Cantidad { get; set; }

        
        public string DescripcionCategoria { get; set; }

		
		public decimal PrecioVenta { get; set; }
      
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqProductoElaboradoDTO : Request
	{
		
        public List<ProductoElaboradoDTO> List { get; set; }
	}
	
	
    public class ReqFilterProductoElaboradoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ProductoElaboradoDTO Item { get; set; }
        
        public Common.filterCaseProductoElaborado FilterCase { get; set; }
    }
	
	
    public class RespProductoElaboradoDTO : Response
    {
      
    }
	
	
    public class RespItemProductoElaboradoDTO : Response
    {
        
        public ProductoElaboradoDTO Item { get; set; } 
    }

    
    public class RespListProductoElaboradoDTO : Response
    {
        
        public List<ProductoElaboradoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	