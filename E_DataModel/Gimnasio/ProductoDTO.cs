using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class ProductoDTO : AuditoriaDTO
	{

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
        public int CodigoDetalle { get; set; }

    
        public int CantTotal { get; set; }

		
		public int CodigoCategoria { get; set; }

        
		public string Busqueda { get; set; }

        
        public int TamanioPagina { get; set; }

        
        public string DesCategoria { get; set; }
        
        
        public string AsesorComercial { get; set; }
	
		
		public int CodigoMarca { get; set; }

        
        public string DesMarca { get; set; }

        
        public int CodigoProducto { get; set; }

        
        public string TipoIngreso { get; set; }
	
		
		public string Descripcion { get; set; }
	    
		
		public string Detalle { get; set; }

        
        public string CodigoBarras { get; set; }

        
        public string Modelo { get; set; }

        
        public int Tipo { get; set; }

		
		public byte[] logo { get; set; }

        
        public string imagenURL { get; set; }

        
        public decimal PrecioCompra { get; set; }

        
        public decimal PrecioVenta { get; set; }

		
		public bool Estado { get; set; }

        
        public int flag { get; set; }

        
        public string DescripcionEstado { get; set; }
	    
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }

        
        public int Cantidad { get; set; }

        
        public int CantidadMinima { get; set; }

        
        public decimal CantidadIngreso { get; set; }
        
        public decimal CantidadSalida { get; set; }

        
        public decimal Importe { get; set; }
        
        public decimal ImporteIngreso { get; set; }
        
        public decimal ImporteSalida { get; set; }
        
        public int PaginaActual { get; set; }
        
        public int NumeroRegistros { get; set; }
        
        public int codigoSocio { get; set; }

        
        public int tipoProducto { get; set; }

        
        public string fechaCompra { get; set; }
        
        public DateTime FechaCompra { get; set; }
        
        public decimal inverti { get; set; }

        
        public decimal Valorizado { get; set; }

        
        public decimal Ganancia { get; set; }

        
        public string Color { get; set; }

        
        public DateTime FechaInicio { get; set; }
        
        public DateTime FechaFin { get; set; }
        
        public decimal StockActual { get; set; }
        
        public string EstadoStockActual { get; set; }
        
        public decimal GananciaUnitaria { get; set; }
        
        public decimal GananciaTotal { get; set; }
        
        public decimal CantidadRegistro { get; set; }
        
        public decimal TotalImporte { get; set; }

    }
	
	
	public class ReqProductoDTO : Request
	{
		
        public List<ProductoDTO> List { get; set; }
	}
	
	
    public class ReqFilterProductoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ProductoDTO Item { get; set; }
        
        public Common.filterCaseProducto FilterCase { get; set; }
    }
	
	
    public class RespProductoDTO : Response
    {
      
    }
	
	
    public class RespItemProductoDTO : Response
    {
        
        public ProductoDTO Item { get; set; } 
    }

    
    public class RespListProductoDTO : Response
    {
        
        public List<ProductoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	