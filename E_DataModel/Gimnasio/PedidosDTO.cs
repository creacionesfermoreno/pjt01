
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.Gimnasio
{
       
    public class PedidosDTO : AuditoriaDTO
    {      
        
        public int Codigo { get; set; }
        
        public int TipoCodigoLlavePersona { get; set; }

        
        public string NroComprobante { get; set; }        
	    
        
        public int CodigoDetalle { get; set; }

        
        public int CodigoSocio { get; set; }
        
        
        public string HoraDesc { get; set; }

        
        public string Hora { get; set; }

        
        public string NombreSocio { get; set; }

        
        public int CodigoProducto { get; set; }

        
        public int Tipo { get; set; }
        
        
        public int EstadoActualizar { get; set; }
        
        
        public int EstadoActual { get; set; }

        
        public string Descripcion { get; set; }

        
        public string FechaDesc { get; set; }

        
        public int CodigoObservacion { get; set; }

        
        public int Cantidad { get; set; }

        
        public int Estado { get; set; }

        
        public string Imprimir { get; set; }

        
        public decimal PrecioUnitario { get; set; }

        
        public decimal Importe { get; set; }

        
        public decimal Debe { get; set; }

        
        public List<PedidosDTO> ListaDetalle { get; set; }

        
        public Common.Operation Operation { get; set; }

        
        public string Xml { get; set; }
    }

    
    public class ReqPedidosDTO : Request
    {
        
        public List<PedidosDTO> List { get; set; }
    }

    
    public class ReqFilterPedidosDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public PedidosDTO Item { get; set; }
        
        public Common.filterCasePedidos FilterCase { get; set; }
    }

    
    public class RespPedidosDTO : Response
    {
        
    }

    
    public class RespItemPedidosDTO : Response
    {
        
        public PedidosDTO Item { get; set; }
    }

    
    public class RespListPedidosDTO : Response
    {
        
        public List<PedidosDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }


}
