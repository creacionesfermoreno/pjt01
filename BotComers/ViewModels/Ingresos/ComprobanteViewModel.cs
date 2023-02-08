using E_DataModel;
using System;
using System.Collections.Generic;

namespace BotComers.ViewModels.Ingresos
{
    public class ComprobanteViewModel
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
        public int? CodigoEstadoEntrega { get; set; }
        public int? CodigoComprobante { get; set; }
        public int CodigoTipoComprobante { get; set; }
        public int TipoMoneda { get; set; }
        public int CodigoAlmacen { get; set; }
        public string Correlativo { get; set; }
        public int? CodigoCliente { get; set; }

        public string NroIdentificacion { get; set; }
        public string NombresCliente { get; set; }
        public string Celular { get; set; }
        public int CodigoVendedor { get; set; }
        public DateTime FechaEmision { get; set; }
        public string DesFechaEmision { get; set; }
        public int CodigoPlazoPago { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string FechaEmision_Texto { get; set; }
        public string DesFechaVencimiento { get; set; }
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
        public string DesEstadoEntrega { get; set; }
        public string CodigoDireccion { get; set; }
        public string CodigoCupon { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UrlPDF { get; set; }
        public string Accion { get; set; }
        public DateTime? b_FechaEmisionInicio { get; set; }
        public DateTime? b_FechaEmisionFin { get; set; }
        public int PageNumber { get; set; }
        public List<ComprobanteDetalleDTO> listaDetalle { get; set; }

        public List<ComprobantePagoDTO> listaDetallePago { get; set; }
    }
}