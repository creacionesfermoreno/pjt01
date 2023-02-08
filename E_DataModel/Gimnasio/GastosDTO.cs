
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class GastosDTO : AuditoriaDTO
	{
        
        public int Codigo { get; set; }

        
        public int CodigoEgreso { get; set; }

        
        public int CodigoEncargado { get; set; }

        public int Tipo { get; set; }

       
        public int CodigoAbrirCaja { get; set; }
	    
        
        public int Importe { get; set; }

		
		public DateTime FechaHora { get; set; }
        
        public DateTime FechaActual { get; set; }

        
        public int TipoMoneda { get; set; }

		
		public int TipoEgreso { get; set; }

        
        public string TipoEgresoDescripcion { get; set; }

        
        public string DescTipoMoneda { get; set; }

        
        public string NumeroRecibo { get; set; }

        
        public string Responsable { get; set; }
        
		
		public string Descripcion { get; set; }

        
        public int TipoResponsable { get; set; }

		
		public decimal MontoEgreso { get; set; }
	
		
		public bool FirmaAutorizacion { get; set; }

        
        public string FirmaAutorizacionDesc { get; set; }

        
        public int CantidadRegistros { get; set; }

        
        public decimal TotalGasto { get; set; }

        
        public int Turno { get; set; }

        
        public DateTime FechaInicio { get; set; }

        
        public DateTime FechaFin { get; set; }

        //---------
        
        public string RUCProveedor { get; set; }

        
        public string RZProveedor { get; set; }

        
        public decimal SubTotal { get; set; }

        
        public decimal Igv { get; set; }

        
        public decimal OtrosTributos { get; set; }

        
        public int CodigoMedioPago { get; set; }

        
        public string DesMedioPago { get; set; }

        
        public string NroOperacion { get; set; }

        
        public string Observaciones { get; set; }

        
        public int TamanioPagina { get; set; }

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqGastosDTO : Request
	{
		
        public List<GastosDTO> List { get; set; }
	}
	
	
    public class ReqFilterGastosDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public GastosDTO Item { get; set; }
        
        public Common.filterCaseGastos FilterCase { get; set; }
    }
	
	
    public class RespGastosDTO : Response
    {
      
    }
	
	
    public class RespItemGastosDTO : Response
    {
        
        public GastosDTO Item { get; set; } 
    }

    
    public class RespListGastosDTO : Response
    {
        
        public List<GastosDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	