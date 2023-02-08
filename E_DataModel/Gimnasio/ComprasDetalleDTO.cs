using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.Gimnasio
{		
	public class ComprasDetalleDTO : AuditoriaDTO
	{
        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
        public int CodigoIngreso { get; set; }

        public int CantTotal { get; set; }

       
        public int TamanioPagina { get; set; }

		
		public int CodigoDetalleIngreso { get; set; }
	
		
		public int CodigoProducto { get; set; }
	
		
		public string Descripcion { get; set; }

        
        public string NroDocumento { get; set; }

        
        public decimal Importe { get; set; }

        
        public int CantidadIngreso { get; set; }

        
        public int CantidadSalida { get; set; }

		
        public decimal PrecioCompra { get; set; }

        
        public decimal PrecioVenta { get; set; }

        
        public decimal GananciaUnitaria { get; set; }

        
        public decimal GananciaTotal { get; set; }

        
        public decimal VentaActual { get; set; }

        
        public DateTime FechaCompra { get; set; }

        
        public DateTime FechaInicio { get; set; }

        
        public int CodigoCategoria { get; set; }

        
        public DateTime FechaFin { get; set; }

        
        public DateTime Param_FechaInicio { get; set; }

        
        public DateTime Param_FechaFin { get; set; }

        
        public int Param_Flag { get; set; }
	
        
        public string Xml { get; set; }

        
        public Common.Operation Operation { get; set; } 

	}
	
	
	public class ReqComprasDetalleDTO : Request
	{
		
        public List<ComprasDetalleDTO> List { get; set; }
	}
	
	
    public class ReqFilterComprasDetalleDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ComprasDetalleDTO Item { get; set; }
        
        public Common.filterCaseComprasDetalle FilterCase { get; set; }
    }
	
	
    public class RespComprasDetalleDTO : Response
    {
      
    }
	
	
    public class RespItemComprasDetalleDTO : Response
    {
        
        public ComprasDetalleDTO Item { get; set; } 
    }

    
    public class RespListComprasDetalleDTO : Response
    {
        
        public List<ComprasDetalleDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	