using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class ComprobanteDTO : AuditoriaDTO
    {       
        public int? CodigoComprobante   { get; set; }      
        public int CodigoTipoComprobante { get; set; }
        public int TipoMoneda  { get; set; }
        public int CodigoAlmacen { get; set; }
        public string NroSerie { get; set; }
        public string Correlativo { get; set; }
        public int? CodigoCliente { get; set; }
        public int CodigoTipoDocumentoSocio { get; set; }
        public string RucEmpresa { get; set; }
        public string RUC_DNI { get; set; }
        public string RazonSocial_Sr { get; set; }
        public string Direccion { get; set; }
        public string NombresCliente { get; set; }
        public string NroIdentificacion { get; set; }
        public string Celular { get; set; }

        public int CodigoVendedor { get; set; }
        public DateTime FechaEmision { get; set; }
        public string FechaEmision_Texto { get; set; }
        public int CodigoPlazoPago{ get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string ColorFechaVencimiento { get; set; }
        public string TerminosCondiciones { get; set; }
        public string Notas { get; set; }
        public string Comentarios { get; set; }
        public Decimal SubTotal { get; set; }
        public Decimal Descuento { get; set; }
        public Decimal SubTotal2 { get; set; }
        public Decimal Envio { get; set; }
        public Decimal SubTotal3 { get; set; }
        public Decimal IGV { get; set; }
        public Decimal Total { get; set; }    
        public Decimal TotalCobrado { get; set; }
        public Decimal TotalPorCobrar { get; set; }    
        public int? Estado { get; set; }
        public string DesEstado { get; set; }
        public string ColorEstado { get; set; }
        public string CodigoDireccion { get; set; }
        public string CodigoCupon { get; set; }
        public int? CodigoEstadoEntrega { get; set; }
        public string DesEstadoEntrega { get; set; }
        public List<ComprobanteDetalleDTO> listaDetalle { get; set; }
        public List<ComprobantePagoDTO> listaDetallePago { get; set; }
        public Common.Operation Operation { get; set; }

        public DateTime? b_FechaEmisionInicio { get; set; }
        public DateTime? b_FechaEmisionFin { get; set; }

        public string UrlPDF { get; set; }

    }


    public class ReqComprobanteDTO : Request //Peticion de un CRUD
    {
        public List<ComprobanteDTO> List { get; set; }
    }

    public class ReqFilterComprobanteDTO : Request //Peticion de un List o Items
    {
        public Common.Paging Paging { get; set; }
        public ComprobanteDTO Item { get; set; }
        public Common.filterCaseComprobante FilterCase { get; set; }
    }

    public class RespComprobanteDTO : Response //respuesta de un CRUD
    {

    }

    public class RespItemComprobanteDTO : Response
    {
        public ComprobanteDTO Item { get; set; }
    } //respuesta de un ITEM

    public class RespListComprobanteDTO : Response
    {
        public List<ComprobanteDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    } //respuesta de un LIST

}
