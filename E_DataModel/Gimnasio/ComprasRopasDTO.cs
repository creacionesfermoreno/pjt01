
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{	
	public class ComprasRopasDTO : AuditoriaDTO
	{

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
        public int TamanioPagina { get; set; }

        
        public decimal TotalNeto { get; set; }

        
        public string NroDocumento { get; set; }

        
		public int CodigoCompraRopa { get; set; }
	
		
		public int CodigoProducto { get; set; }
	
		
		public string Descripcion { get; set; }

        
        public int CantTotal { get; set; }

        
        public DateTime FechaInicio { get; set; }

        
        public DateTime FechaFin { get; set; }

        
		public decimal Importe { get; set; }
	
		
		public int CantidadIngreso { get; set; }
	
		
		public int CantidadSalida { get; set; }

        
        public int Param_Flag { get; set; }

        
		public decimal PrecioCompra { get; set; }
	
		
		public decimal PrecioVenta { get; set; }
	
		
		public decimal GananciaUnitaria { get; set; }
	
		
		public decimal GananciaTotal { get; set; }
	
		
		public DateTime FechaCompra { get; set; }
	
		
		public int Estado { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }

        
        public List<ComprasRopasDTO> ListaDetalleCRopasDTO { get; set; }
    }
	
	
	public class ReqComprasRopasDTO : Request
	{
		
        public List<ComprasRopasDTO> List { get; set; }
	}
	
	
    public class ReqFilterComprasRopasDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ComprasRopasDTO Item { get; set; }
        
        public Common.filterCaseComprasRopas FilterCase { get; set; }
    }
	
	
    public class RespComprasRopasDTO : Response
    {
      
    }
	
	
    public class RespItemComprasRopasDTO : Response
    {
        
        public ComprasRopasDTO Item { get; set; } 
    }

    
    public class RespListComprasRopasDTO : Response
    {
        
        public List<ComprasRopasDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	