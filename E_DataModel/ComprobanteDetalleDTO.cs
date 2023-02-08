using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class ComprobanteDetalleDTO : AuditoriaDTO
    {     
        public int CodigoComprobante { get; set; }
        public int CodigoComprobanteDetalle { get; set; }
        public int CodigoAlmacen { get; set; }
        public int CodigoMenuSuperior { get; set; }
        public int CodigoItemsVenta { get; set; }
        public string Referencia { get; set; }
        public Decimal Precio { get; set; }
        public Decimal Descuento { get; set; }
        public int CodigoTipoImpuesto { get; set; }
        public string Descripcion { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Total { get; set; }
        public int Estado { get; set; }
        public Common.Operation Operation { get; set; }
        public string CodigoImagen { get; set; }

        public int CodigoCliente { get; set; }
        public string NombresCliente { get; set; }
        public Decimal Importe { get; set; }
        public Decimal Debe { get; set; }
        public string DesFormaPago { get; set; }
        public string Correlativo { get; set; }
        public string DesSubTipoDocumento { get; set; }
        public string Identificacion { get; set; }
        public int CantidadTotal { get; set; }

        public DateTime request_FechaInicio { get; set; }
        public DateTime request_Fin { get; set; }
        public string request_Vendedor { get; set; }
        public string request_Counter { get; set; }
        public int request_Tipo { get; set; }
        public int request_Turno { get; set; }
        public int request_FormaPago { get; set; }

        public int PageNumber { get; set; }
    }



    public class ReqComprobanteDetalleDTO : Request //Peticion de un CRUD
    {
        public List<ComprobanteDetalleDTO> List { get; set; }
    }

    public class ReqFilterComprobanteDetalleDTO : Request //Peticion de un List o Items
    {
        public Common.Paging Paging { get; set; }
        public ComprobanteDetalleDTO Item { get; set; }
        public Common.filterCaseComprobanteDetalle FilterCase { get; set; }
    }

    public class RespComprobanteDetalleDTO : Response //respuesta de un CRUD
    {

    }

    public class RespItemComprobanteDetalleDTO : Response
    {
        public ComprobanteDetalleDTO Item { get; set; }
    } //respuesta de un ITEM

    public class RespListComprobanteDetalleDTO : Response
    {
        public List<ComprobanteDetalleDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    } //respuesta de un LIST

}
