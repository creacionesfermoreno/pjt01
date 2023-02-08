using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.Gimnasio
{

    public class VentasDTO : AuditoriaDTO
    {


        public int TK_ID { get; set; }

        public string TK_Latitude { get; set; }


        public string TK_Longitude { get; set; }


        public int CodigoIngreso { get; set; }


        public int ValidadorSerie { get; set; }


        public int CodigoSocio { get; set; }

        public string RazonSocial_Sr { get; set; }


        public string RUC_DNI { get; set; }


        public string Direccion { get; set; }


        public string AsesorComercial { get; set; }


        public DateTime FechaVenta { get; set; }


        public string DescHoraVenta { get; set; }
        public string DescFechaVenta { get; set; }

        public int CodigoSubTipoDocumento { get; set; }


        public int CodigoTipoComprobante { get; set; }


        public string NroComprobante { get; set; }


        public string NombreComprobante { get; set; }


        public string CorreoEmpresa { get; set; }


        public string NroTarjeta { get; set; }


        public decimal SubTotal { get; set; }


        public decimal IGV { get; set; }


        public decimal TotalNeto { get; set; }


        public decimal Debe { get; set; }


        public decimal TotalDolares { get; set; }


        public decimal TotalAporte { get; set; }


        public int TipoMoneda { get; set; }


        public int FormaPago { get; set; }


        public int SubFormaPago { get; set; }


        public string DescFormaPago { get; set; }


        public decimal tipoCambio { get; set; }



        public string User { get; set; }


        public bool Estado { get; set; }


        public string DesEstado { get; set; }


        public string Comentario { get; set; }


        public string Hora { get; set; }


        public int EstadoImpresora { get; set; }
        public bool GenerarSerie { get; set; }
        public bool TieneFacturacionElectronica { get; set; }
        public string RucContribuyente { get; set; }
        public string RazonSocialContribuyente { get; set; }
        public string flagEstado { get; set; }
        public string NroBoucher { get; set; }


        ////////////////////////////////
        //////// PARA IMPRIMIR /////////
        ////////////////////////////////

        public decimal CostoPlan { get; set; }

        public int Cantidad { get; set; }


        public int Dia { get; set; }


        public int Mes { get; set; }


        public string HoraDia { get; set; }

        public string MesDescripcion { get; set; }

        public string TipoComprobante { get; set; }

        public string Correlativo_Comprobante { get; set; }

        public int Anio { get; set; }
        public decimal PrecioUnitario { get; set; }

        public string DescripcionProducto { get; set; }

        public decimal ImporteProducto { get; set; }

        public int CodigoVenta { get; set; }

        public string NombreEmpresa { get; set; }

        public string Frase { get; set; }


        public string RucEmpresa { get; set; }


        public string TelefonoEmpresa { get; set; }

        public string CelularEmpresa { get; set; }


        public string DistritoCliente { get; set; }



        public string NombreCliente { get; set; }


        public string TelefonoCliente { get; set; }



        public string DireccionEmpresa { get; set; }


        public string DireccionCliente { get; set; }


        public int Tipo_Conf_Comprobante { get; set; }


        public string DireccionDistritoCliente { get; set; }


        public string DistritoEmpresa { get; set; }


        public string Distrito { get; set; }


        public DateTime FechaInicio { get; set; }


        public DateTime FechaFin { get; set; }

        public Common.Operation Operation { get; set; }


        public List<VentasDetalleDTO> ListaDetalle { get; set; }


        public List<ControlSalidaFormaPagoDTO> ListaFormaPago { get; set; }


        public List<ContratoCuotaDTO> ListaCuotas { get; set; }


        public int TamanioPagina { get; set; }

        public string Xml { get; set; }

        public int CodigoPaquete { get; set; }

        //Facturacion Electronica
        public string SerieComprobante { get; set; }
        public int CodigoTipoDocumentoSocio { get; set; }
        public string RazonSocialEmpresa { get; set; }
        public string DireccionFiscalEmpresa { get; set; }

    }

    public class ReqVentasDTO : Request
    {

        public List<VentasDTO> List { get; set; }
    }

    public class ReqFilterVentasDTO : Request
    {

        public Common.Paging Paging { get; set; }

        public VentasDTO Item { get; set; }

        public Common.filterCaseVentas FilterCase { get; set; }
    }


    public class RespVentasDTO : Response
    {

    }


    public class RespItemVentasDTO : Response
    {

        public VentasDTO Item { get; set; }
    }


    public class RespListVentasDTO : Response
    {

        public List<VentasDTO> List { get; set; }

        public Common.Paging Paging { get; set; }
    }
}
