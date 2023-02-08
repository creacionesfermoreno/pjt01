
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{

	public class ComprasSuplementosDTO : AuditoriaDTO
	{

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

		public int CodigoCompraSuplemento { get; set; }
	
		
		public int CodigoSuplemento { get; set; }

        
		public int CodigoCatSuplementos { get; set; }

        
		public int TamanioPagina { get; set; }

        
		public DateTime FechaInicio { get; set; }

        
		public DateTime FechaFin { get; set; }

        
		public int CantTotal { get; set; }
	
		
		public string Descripcion { get; set; }
	
		
		public decimal Importe { get; set; }
	
		
		public int CantidadIngreso { get; set; }
	
		
		public int CantidadSalida { get; set; }

        
        public int Param_Flag { get; set; }

        
		public decimal PrecioCompra { get; set; }
	
		
		public decimal PrecioVenta { get; set; }
	
		
		public decimal GananciaUnitaria { get; set; }
	
		
		public decimal GananciaTotal { get; set; }

	    
		public decimal TotalNeto { get; set; }

        
		public string NroDocumento { get; set; }
	
		
		public DateTime FechaCompra { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }

        
        public List<ComprasSuplementosDTO> ListaDetalleCSuplementosDTO { get; set; }
    }
	
	
	public class ReqComprasSuplementosDTO : Request
	{
		
        public List<ComprasSuplementosDTO> List { get; set; }
	}
	
	
    public class ReqFilterComprasSuplementosDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ComprasSuplementosDTO Item { get; set; }
        
        public Common.filterCaseComprasSuplementos FilterCase { get; set; }
    }
	
	
    public class RespComprasSuplementosDTO : Response
    {
      
    }
	
	
    public class RespItemComprasSuplementosDTO : Response
    {
        
        public ComprasSuplementosDTO Item { get; set; } 
    }

    
    public class RespListComprasSuplementosDTO : Response
    {
        
        public List<ComprasSuplementosDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	