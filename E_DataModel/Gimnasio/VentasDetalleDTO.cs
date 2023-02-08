using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{

	public class VentasDetalleDTO : AuditoriaDTO
	{        
        public int codigoDetalle_Ingreso { get; set; }

		
		public int CodigoSalidaDetalle { get; set; }
	
		
		public int CodigoSalida { get; set; }

        
        public int CodigoPedido { get; set; }

        
        public int Tipo { get; set; }

		
		public int Cantidad { get; set; }

	    
        public int CodigoProducto { get; set; }

		
		public decimal PrecioUnitario { get; set; }

        
        public string Descripcion { get; set; }

        
        public decimal Precio { get; set; }

        
        public decimal TotalPagando { get; set; }

        
        public decimal Debe { get; set; }

        
        public decimal Acuenta { get; set; }

        
        public string AsesorComercial { get; set; }
        
        
        public string TipoIngresoMembre { get; set; }
	
		
		public decimal Importe { get; set; }

        
		public decimal Total { get; set; }

        
        public DateTime FechaInicio { get; set; }

        
        public DateTime FechaFin { get; set; }

        
        public string Vendedor { get; set; }

        
        public string DNI_RUC { get; set; }

        
        public string TipoComprobante { get; set; }
        
        public string SubTipoDocumento { get; set; }
        
        public int Turno { get; set; }

        
        public int FormaPago { get; set; }

        
        public string TipoIngresoMembresia { get; set; }

        
        public int TipoCliente { get; set; }

        
        public string Counter { get; set; }

        
        public string Cliente { get; set; }

        
        public string FechaVenta { get; set; }

        
        public DateTime Fecha { get; set; }

        
        public string Mes { get; set; }

        
        public string Telefono { get; set; }

        
        public string Direccion { get; set; }

        
        public string Correo { get; set; }

        
        public string Facebook { get; set; }
        
        
        public string ImgFacebook { get; set; }

        
        public string NroTarjeta { get; set; }

        
        public string DescFormaPago { get; set; }

        
        public int CodigoCliente { get; set; }

        
        public int CodigoPago { get; set; }

        
        public string NroComprobante { get; set; }

        
        public string Responsable { get; set; }

        
        public string flagEstado { get; set; }

        
        public string DesEstado { get; set; }

        
        public int CantidadRegistros { get; set; }

        
        public int CodigoTiempoPaquete { get; set; }
        
        public string DesTiempoPaquete { get; set; } 
        public string NroContrato { get; set; }

        
        public Common.Operation Operation { get; set; }

        
        public int TamanioPagina { get; set; }

        
        public string Xml { get; set; }
        public string Comentario { get; set; }
    }
	
	
	public class ReqVentasDetalleDTO : Request
	{
		
        public List<VentasDetalleDTO> List { get; set; }
	}
	
	
    public class ReqFilterVentasDetalleDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public VentasDetalleDTO Item { get; set; }
        
        public Common.filterCaseVentasDetalle FilterCase { get; set; }
    }
	
	
    public class RespVentasDetalleDTO : Response
    {
      
    }
	
	
    public class RespItemVentasDetalleDTO : Response
    {
        
        public VentasDetalleDTO Item { get; set; } 
    }

    
    public class RespListVentasDetalleDTO : Response
    {
        
        public List<VentasDetalleDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	