using E_BusinessLayer;
using E_BusinessLayer.Gimnasio;
using E_DataModel;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BotComers.ExportarExcel.Appsfit
{
    /// <summary>
    /// Descripción breve de ExportarExcelFacturacionDetalle
    /// </summary>
    public class ExportarExcelFacturacionDetalle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Titulo = context.Request.Params["NombreComercial"].ToString();
            string Ruc = context.Request.Params["RUC"].ToString();
            string[] TipoArrayList = context.Request.Params["Tipo"].ToString().Split('|');

            var request = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<VentasDetalleDTO>(context.Request.Params["request"]);
            var listaResumen = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<ExportarResumenFacturacionExcelDTO>>(context.Request.Params["lista"]);
            var listaResumenCaja = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<ExportarResumenFacturacionExcelDTO>>(context.Request.Params["listaCaja"]);

            List<VentasDetalleDTO> lista = new List<VentasDetalleDTO>();
            
            List<GastosDTO> ListaFastoCaja = new List<GastosDTO>();
            List<ComprobanteDetalleDTO> lista_ComprobanteDetalleDTO = new List<ComprobanteDetalleDTO>();


            StringBuilder datarow = new StringBuilder();
            datarow.Append("<!DOCTYPE html>");
            datarow.Append("<html>");
            datarow.Append(" <head>");
            datarow.Append("    <meta name='viewport' content='width=device-width' />");
            datarow.Append("        <title>Export</title>");
            datarow.Append(" </head>");
            datarow.Append(" <body>");


            for (int i = 0; i < TipoArrayList.Length; i++)
            {
                string Tipo = TipoArrayList[i];
                if (Tipo == "0")
                {
                    if (listaResumen != null)
                    {
                        datarow.Append("  <table class='table'>");
                        datarow.Append("      <thead>");
                        datarow.Append("          <tr>");
                        datarow.Append("              <td colspan='8' style='border: 1px; border-color: Black; border-style: solid; height:33px; text-align:center; font-size: 25px; color: #000000; font-family:Calibri; font-weight: bold;'>" + Titulo + "</td>");
                        datarow.Append("          </tr>");
                        datarow.Append("          <tr>");
                        datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'></th>");
                        datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TOTAL</th>");
                        datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>EFECTIVO</th>");
                        datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>T. DEBITO</th>");
                        datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>T. CREDITO</th>");
                        datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DEPOSITO</th>");
                        datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>WEB</th>");
                        datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DEUDA</th>");
                        datarow.Append("          </tr>");
                        datarow.Append("      </thead>");
                        datarow.Append("      <tbody>");
                        foreach (var item in listaResumen)
                        {
                            datarow.Append("      <tr>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.DescripcionCategoria + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Total) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format:'0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Efectivo) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.TarjetaDebito) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.TarjetaCredito) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Deposito) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Web) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: #ff0200; font-size: 12px; mso-number-format: '\\@';\">" + item.Deuda + "</td>");
                            datarow.Append("      </tr>");
                        }
                        datarow.Append("      </tbody>");
                        var total = listaResumen.Sum(x => Convert.ToDecimal(x.Total));
                        var efectivo = listaResumen.Sum(x => Convert.ToDecimal(x.Efectivo));
                        var tDebito = listaResumen.Sum(x => Convert.ToDecimal(x.TarjetaDebito));
                        var tCredito = listaResumen.Sum(x => Convert.ToDecimal(x.TarjetaCredito));
                        var tDeposito = listaResumen.Sum(x => Convert.ToDecimal(x.Deposito));
                        var tWeb = listaResumen.Sum(x => Convert.ToDecimal(x.Web));

                        datarow.Append("  </tfoot>");
                        datarow.Append("    <tr>");
                        datarow.Append("         <td style=\"border: 1px; border-color: Black;background-color: #53c2eca6; border-style: solid; width:150px; text-align:center; color: Black; font-size: 13px;font-weight: bold; mso-number-format:'\\@';\">TOTAL</td>");
                        datarow.Append("         <td style=\"border: 1px; border-color: Black;background-color: #53c2eca6; border-style: solid; width:80px; text-align:left; color: Black; font-size: 13px;font-weight: bold; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", total) + "</td>");
                        datarow.Append("         <td style=\"border: 1px; border-color: Black;background-color: #53c2eca6; border-style: solid; width:80px; text-align:left; color: Black; font-size: 13px;font-weight: bold; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", efectivo) + "</td>");
                        datarow.Append("         <td style=\"border: 1px; border-color: Black;background-color: #53c2eca6; border-style: solid; width:80px; text-align:left; color: Black; font-size: 13px;font-weight: bold; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", tDebito) + "</td>");
                        datarow.Append("         <td style=\"border: 1px; border-color: Black;background-color: #53c2eca6; border-style: solid; width:80px; text-align:left; color: Black; font-size: 13px;font-weight: bold; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", tCredito) + "</td>");
                        datarow.Append("         <td style=\"border: 1px; border-color: Black;background-color: #53c2eca6; border-style: solid; width:80px; text-align:left; color: Black; font-size: 13px;font-weight: bold; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", tDeposito) + "</td>");
                        datarow.Append("         <td style=\"border: 1px; border-color: Black;background-color: #53c2eca6; border-style: solid; width:80px; text-align:left; color: Black; font-size: 13px;font-weight: bold; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", tWeb) + "</td>");
                        datarow.Append("         <td style=\"border: 1px; border-color: Black;background-color: #53c2eca6; border-style: solid; width:80px; text-align:left; color: #ff0200;   font-size: 13px;font-weight: bold; mso-number-format: '\\@';\"></td>");
                        datarow.Append("    </tr>");
                        datarow.Append("  </tfoot>");

                        datarow.Append("      <tbody>");
                        foreach (var itemCaja in listaResumenCaja)
                        {
                            datarow.Append("      <tr>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:center; color: Black;font-weight: bold; font-size: 12px; mso-number-format:'\\@';\">" + itemCaja.DescripcionCategoria + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black;font-weight: bold; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", itemCaja.Total) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format:'0.00';\"></td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\"></td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\"></td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\"></td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\"></td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: #ff0200; font-size: 12px; mso-number-format: '\\@';\"></td>");
                            datarow.Append("      </tr>");
                        }
                        datarow.Append("      </tbody>");

                        datarow.Append("  </table>");
                    }
                }
                else if (Tipo == "2")
                {
                    lista = uspReporteVentasMembresiasRangoFechas_Paginacion(request.CodigoUnidadNegocio, request.FechaInicio, request.FechaFin, request.Vendedor, request.CodigoSede, request.Turno, request.FormaPago, request.TipoIngresoMembresia, request.TipoCliente, request.Counter, request.AsesorComercial, request.CodigoTiempoPaquete, 1);
                    #region Membresias
                    datarow.Append("  <table class='table'>");
                    datarow.Append("      <thead>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <td colspan='13' style='border: 1px; border-color: Black; border-style: solid; height:33px; text-align:center; font-size: 25px; color: #000000; font-family:Calibri; font-weight: bold;'>DETALLE VENTA MEMBRESIAS</td>");
                    datarow.Append("          </tr>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>COD</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DNI/RUC</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>CLIENTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>F. VENTA</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIEMPO PLAN</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCRIPCION</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>PRECIO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>IMPORTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DEBE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>FORMA PAGO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>EMPRESA</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>NUMERO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>RESPONSABLE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>VENDEDOR</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>NRO CONTRATO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>COMENTARIO</th>");

                    datarow.Append("          </tr>");
                    datarow.Append("      </thead>");
                    datarow.Append("      <tbody>");
                    if (lista != null)
                        foreach (var item in lista)
                        {
                            datarow.Append("      <tr>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:60px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.TipoIngresoMembresia + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.CodigoCliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:50px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.DNI_RUC + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Cliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy hh:mm:ss';\">" + item.FechaVenta + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.DesTiempoPaquete + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Descripcion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Precio) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Importe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Debe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.DescFormaPago + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.SubTipoDocumento + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.TipoComprobante + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.NroComprobante + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.UsuarioCreacion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.AsesorComercial + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.NroContrato + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.Comentario + "</td>");
                            datarow.Append("      </tr>");
                        }
                    datarow.Append("      </tbody>");
                    datarow.Append("  </table>");
                    #endregion

                    lista = uspReporteVentasMembresiasRangoFechas_preciocero_Paginacion(request.CodigoUnidadNegocio, request.FechaInicio, request.FechaFin, request.Vendedor, request.CodigoSede, request.Turno, request.FormaPago, request.TipoIngresoMembresia, request.TipoCliente, request.Counter, request.AsesorComercial, request.CodigoTiempoPaquete, 1);
                    
                    #region Membresias cero
                    datarow.Append("  <table class='table'>");
                    datarow.Append("      <thead>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <td colspan='11' style='border: 1px; border-color: Black; border-style: solid; height:33px; text-align:center; font-size: 25px; color: #000000; font-family:Calibri; font-weight: bold;'>DETALLE VENTA MEMBRESIAS PRECIO CERO</td>");
                    datarow.Append("          </tr>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>COD</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DNI/RUC</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>CLIENTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>F. INSCRIPCIÓN</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIEMPO PLAN</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCRIPCION</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>F. INICIO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>F. FIN</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>PRECIO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>RESPONSABLE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>VENDEDOR</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>NRO CONTRATO</th>");

                    datarow.Append("          </tr>");
                    datarow.Append("      </thead>");
                    datarow.Append("      <tbody>");
                    if (lista != null)
                        foreach (var item in lista)
                        {
                            datarow.Append("      <tr>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:60px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.TipoIngresoMembresia + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.CodigoCliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:50px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.DNI_RUC + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Cliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy hh:mm:ss';\">" + item.FechaCreacion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.DesTiempoPaquete + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Descripcion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.FechaInicio + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.FechaFin + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Precio) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.Counter + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.AsesorComercial + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.NroContrato + "</td>");
                            datarow.Append("      </tr>");
                        }
                    datarow.Append("      </tbody>");
                    datarow.Append("  </table>");
                    #endregion
                }
                else if (Tipo == "4")
                {
                    lista = uspReporteVentasServiciosRangoFechas_Paginacion(request.CodigoUnidadNegocio, request.FechaInicio, request.FechaFin, request.Vendedor, request.CodigoSede, request.Turno, request.FormaPago, request.TipoIngresoMembresia, request.TipoCliente, request.Counter, request.AsesorComercial, request.CodigoTiempoPaquete, 1);
                    #region Servicios
                    datarow.Append("  <table class='table'>");
                    datarow.Append("      <thead>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <td colspan='13' style='border: 1px; border-color: Black; border-style: solid; height:33px; text-align:center; font-size: 25px; color: #000000; font-family:Calibri; font-weight: bold;'>DETALLE VENTA DIARIO</td>");
                    datarow.Append("          </tr>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>COD</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DNI/RUC</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>CLIENTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>F. VENTA</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCRIPCION</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>PRECIO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>IMPORTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DEBE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>FORMA PAGO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>EMPRESA</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>NUMERO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>RESPONSABLE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>VENDEDOR</th>");
                    datarow.Append("          </tr>");
                    datarow.Append("      </thead>");
                    datarow.Append("      <tbody>");
                    if (lista != null)
                        foreach (var item in lista)
                        {
                            datarow.Append("      <tr>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.CodigoCliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:50px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.DNI_RUC + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Cliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy hh:mm:ss';\">" + item.FechaVenta + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Descripcion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Precio) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Importe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Debe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.DescFormaPago + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.SubTipoDocumento + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.TipoComprobante + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.NroComprobante + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.UsuarioCreacion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.AsesorComercial + "</td>");
                            datarow.Append("      </tr>");
                        }
                    datarow.Append("      </tbody>");
                    datarow.Append("  </table>");
                    #endregion
                }
                else if (Tipo == "1")
                {
                    // TIPO 1 = SUPLEMENTOS, 2 = JUGUERIA, 3 = ROPA FITNESS, 4 = ACCESORIOS
                    lista_ComprobanteDetalleDTO = CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion(request.CodigoUnidadNegocio, request.CodigoSede, request.FechaInicio, request.FechaFin, request.Counter, request.Vendedor, request.Turno, request.FormaPago, 2);
                    #region Servicios
                    datarow.Append("  <table class='table'>");
                    datarow.Append("      <thead>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <td colspan='13' style='border: 1px; border-color: Black; border-style: solid; height:33px; text-align:center; font-size: 25px; color: #000000; font-family:Calibri; font-weight: bold;'>DETALLE VENTA CAFETERIA</td>");
                    datarow.Append("          </tr>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>COD</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>CLIENTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>F. VENTA</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCRIPCION</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>CANTIDAD</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>PRECIO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>IMPORTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DEBE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>FORMA PAGO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>NUMERO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>RESPONSABLE</th>");
                    datarow.Append("          </tr>");
                    datarow.Append("      </thead>");
                    datarow.Append("      <tbody>");
                    if (lista_ComprobanteDetalleDTO != null)
                        foreach (var item in lista_ComprobanteDetalleDTO)
                        {
                            datarow.Append("      <tr>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.CodigoCliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.NombresCliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy hh:mm:ss';\">" + item.FechaCreacion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Descripcion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:50px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.Cantidad + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Precio) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Importe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Debe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.DesFormaPago + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.DesSubTipoDocumento + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.Correlativo + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.UsuarioCreacion + "</td>");
                            datarow.Append("      </tr>");
                        }
                    datarow.Append("      </tbody>");
                    datarow.Append("  </table>");
                    #endregion
                }
                else if (Tipo == "6")
                {
                    // TIPO 1 = SUPLEMENTOS, 2 = JUGUERIA, 3 = ROPA FITNESS, 4 = ACCESORIOS
                    lista_ComprobanteDetalleDTO = CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion(request.CodigoUnidadNegocio, request.CodigoSede, request.FechaInicio, request.FechaFin, request.Counter, request.Vendedor, request.Turno, request.FormaPago, 1);
                    #region Servicios
                    datarow.Append("  <table class='table'>");
                    datarow.Append("      <thead>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <td colspan='13' style='border: 1px; border-color: Black; border-style: solid; height:33px; text-align:center; font-size: 25px; color: #000000; font-family:Calibri; font-weight: bold;'>DETALLE VENTA SUPLEMENTO</td>");
                    datarow.Append("          </tr>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>COD</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>CLIENTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>F. VENTA</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCRIPCION</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>CANTIDAD</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>PRECIO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>IMPORTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DEBE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>FORMA PAGO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>NUMERO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>RESPONSABLE</th>");
                    datarow.Append("          </tr>");
                    datarow.Append("      </thead>");
                    datarow.Append("      <tbody>");
                    if (lista_ComprobanteDetalleDTO != null)
                        foreach (var item in lista_ComprobanteDetalleDTO)
                        {
                            datarow.Append("      <tr>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.CodigoCliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.NombresCliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy hh:mm:ss';\">" + item.FechaCreacion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Descripcion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:50px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.Cantidad + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Precio) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Importe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Debe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.DesFormaPago + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.DesSubTipoDocumento + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.Correlativo + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.UsuarioCreacion + "</td>");
                            datarow.Append("      </tr>");
                        }
                    datarow.Append("      </tbody>");
                    datarow.Append("  </table>");
                    #endregion
                }
                else if (Tipo == "7")
                {
                    lista = uspReporteVentasNutricionRangoFechas_Paginacion(request.CodigoUnidadNegocio, request.FechaInicio, request.FechaFin, request.Vendedor, request.CodigoSede, request.Turno, request.FormaPago, request.TipoIngresoMembresia, request.TipoCliente, request.Counter, request.AsesorComercial, request.CodigoTiempoPaquete, 1);
                    #region Servicios
                    datarow.Append("  <table class='table'>");
                    datarow.Append("      <thead>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <td colspan='13' style='border: 1px; border-color: Black; border-style: solid; height:33px; text-align:center; font-size: 25px; color: #000000; font-family:Calibri; font-weight: bold;'>DETALLE VENTA NUTRICION</td>");
                    datarow.Append("          </tr>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>COD</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>CLIENTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>F. VENTA</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIEMPO PLAN</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCRIPCION</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>PRECIO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>IMPORTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DEBE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>FORMA PAGO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>EMPRESA</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>NUMERO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>RESPONSABLE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>VENDEDOR</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>NRO CONTRATO</th>");
                    datarow.Append("          </tr>");
                    datarow.Append("      </thead>");
                    datarow.Append("      <tbody>");
                    if (lista != null)
                        foreach (var item in lista)
                        {
                            datarow.Append("      <tr>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:60px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.TipoIngresoMembresia + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.CodigoCliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Cliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy hh:mm:ss';\">" + item.FechaVenta + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.DesTiempoPaquete + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Descripcion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Precio) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Importe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Debe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.DescFormaPago + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.SubTipoDocumento + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.TipoComprobante + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.NroComprobante + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.UsuarioCreacion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.AsesorComercial + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.NroContrato + "</td>");

                            datarow.Append("      </tr>");
                        }
                    datarow.Append("      </tbody>");
                    datarow.Append("  </table>");
                    #endregion
                }
                else if (Tipo == "8")
                {
                    lista = uspReporteVentasPersonalizadoRangoFechas_Paginacion(request.CodigoUnidadNegocio, request.FechaInicio, request.FechaFin, request.Vendedor, request.CodigoSede, request.Turno, request.FormaPago, request.TipoIngresoMembresia, request.TipoCliente, request.Counter, request.AsesorComercial, request.CodigoTiempoPaquete, 1);
                    #region Servicios
                    datarow.Append("  <table class='table'>");
                    datarow.Append("      <thead>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <td colspan='13' style='border: 1px; border-color: Black; border-style: solid; height:33px; text-align:center; font-size: 25px; color: #000000; font-family:Calibri; font-weight: bold;'>DETALLE VENTA PERSONALIZADO</td>");
                    datarow.Append("          </tr>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>COD</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>CLIENTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>F. VENTA</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIEMPO PLAN</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCRIPCION</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>PRECIO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>IMPORTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DEBE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>FORMA PAGO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>EMPRESA</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>NUMERO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>RESPONSABLE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>VENDEDOR</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>NRO CONTRATO</th>");
                    datarow.Append("          </tr>");
                    datarow.Append("      </thead>");
                    datarow.Append("      <tbody>");
                    if (lista != null)
                        foreach (var item in lista)
                        {
                            datarow.Append("      <tr>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:60px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.TipoIngresoMembresia + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.CodigoCliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Cliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy hh:mm:ss';\">" + item.FechaVenta + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.DesTiempoPaquete + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Descripcion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Precio) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Importe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Debe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.DescFormaPago + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.SubTipoDocumento + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.TipoComprobante + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.NroComprobante + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.UsuarioCreacion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.AsesorComercial + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.NroContrato + "</td>");
                            datarow.Append("      </tr>");
                        }
                    datarow.Append("      </tbody>");
                    datarow.Append("  </table>");
                    #endregion
                }
                else if (Tipo == "9")
                {
                    // TIPO 1 = SUPLEMENTOS, 2 = JUGUERIA, 3 = ROPA FITNESS, 4 = ACCESORIOS
                    lista_ComprobanteDetalleDTO = CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion(request.CodigoUnidadNegocio, request.CodigoSede, request.FechaInicio, request.FechaFin, request.Counter, request.Vendedor, request.Turno, request.FormaPago, 3);
                    #region Servicios
                    datarow.Append("  <table class='table'>");
                    datarow.Append("      <thead>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <td colspan='13' style='border: 1px; border-color: Black; border-style: solid; height:33px; text-align:center; font-size: 25px; color: #000000; font-family:Calibri; font-weight: bold;'>DETALLE VENTA ROPA</td>");
                    datarow.Append("          </tr>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>COD</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>CLIENTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>F. VENTA</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCRIPCION</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>CANTIDAD</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>PRECIO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>IMPORTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DEBE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>FORMA PAGO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>NUMERO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>RESPONSABLE</th>");
                    datarow.Append("          </tr>");
                    datarow.Append("      </thead>");
                    datarow.Append("      <tbody>");
                    if (lista_ComprobanteDetalleDTO != null)
                        foreach (var item in lista_ComprobanteDetalleDTO)
                        {
                            datarow.Append("      <tr>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.CodigoCliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.NombresCliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy hh:mm:ss';\">" + item.FechaCreacion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Descripcion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:50px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.Cantidad + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Precio) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Importe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Debe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.DesFormaPago + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.DesSubTipoDocumento + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.Correlativo + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.UsuarioCreacion + "</td>");
                            datarow.Append("      </tr>");
                        }
                    datarow.Append("      </tbody>");
                    datarow.Append("  </table>");
                    #endregion
                }
                else if (Tipo == "11")
                {
                    // TIPO 1 = SUPLEMENTOS, 2 = JUGUERIA, 3 = ROPA FITNESS, 4 = ACCESORIOS
                    lista_ComprobanteDetalleDTO = CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion(request.CodigoUnidadNegocio, request.CodigoSede, request.FechaInicio, request.FechaFin, request.Counter, request.Vendedor, request.Turno, request.FormaPago, 4);
                    #region Servicios
                    datarow.Append("  <table class='table'>");
                    datarow.Append("      <thead>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <td colspan='13' style='border: 1px; border-color: Black; border-style: solid; height:33px; text-align:center; font-size: 25px; color: #000000; font-family:Calibri; font-weight: bold;'>DETALLE VENTA ACCESORIOS</td>");
                    datarow.Append("          </tr>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>COD</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>CLIENTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>F. VENTA</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCRIPCION</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>CANTIDAD</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>PRECIO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>IMPORTE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DEBE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>FORMA PAGO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>NUMERO COMP.</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>RESPONSABLE</th>");
                    datarow.Append("          </tr>");
                    datarow.Append("      </thead>");
                    datarow.Append("      <tbody>");
                    if (lista_ComprobanteDetalleDTO != null)
                        foreach (var item in lista_ComprobanteDetalleDTO)
                        {
                            datarow.Append("      <tr>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.CodigoCliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.NombresCliente + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy hh:mm:ss';\">" + item.FechaCreacion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:350px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Descripcion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:50px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.Cantidad + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Precio) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Importe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Debe) + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.DesFormaPago + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.DesSubTipoDocumento + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.Correlativo + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.UsuarioCreacion + "</td>");
                            datarow.Append("      </tr>");
                        }
                    datarow.Append("      </tbody>");
                    datarow.Append("  </table>");
                    #endregion
                }
                else if (Tipo == "10")
                {
                    //int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int PageNumber
                    ListaFastoCaja = uspReporteEgresoRangoFechas_Paginacion(request.CodigoUnidadNegocio, request.FechaInicio, request.FechaFin, request.Vendedor, request.CodigoSede, request.Turno, 1);
                    #region Gastos Caja
                    datarow.Append("  <table class='table'>");
                    datarow.Append("      <thead>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <td colspan='13' style='border: 1px; border-color: Black; border-style: solid; height:33px; text-align:center; font-size: 25px; color: #000000; font-family:Calibri; font-weight: bold;'>Gastos de Caja</td>");
                    datarow.Append("          </tr>");
                    datarow.Append("          <tr>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCRIPCION</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>RESPONSABLE</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO DE GASTO</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>FECHA Y HORA</th>");
                    datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 13px; background-color: #ff0200; color: #fff; font-weight: bold;'>MONTO</th>");
                    datarow.Append("          </tr>");
                    datarow.Append("      </thead>");
                    datarow.Append("      <tbody>");
                    if (ListaFastoCaja != null)
                        foreach (var item in ListaFastoCaja)
                        {
                            datarow.Append("      <tr>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.Descripcion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.UsuarioCreacion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:40px; text-align:center; color: Black; font-size: 12px; mso-number-format:'\\@';\">" + item.TipoEgresoDescripcion + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy hh:mm:ss';\">" + item.FechaHora + "</td>");
                            datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: '0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.MontoEgreso) + "</td>");
                            datarow.Append("      </tr>");
                        }
                    datarow.Append("      </tbody>");
                    datarow.Append("  </table>");
                    #endregion
                }


            }


            datarow.Append(" </body>");
            datarow.Append("</html>");
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=ExportarDatosFacturacion.xls");
            context.Response.Write(datarow.ToString());
            context.Response.Charset = "";
            context.Response.End();

        }


        public List<VentasDetalleDTO> uspReporteVentasMembresiasRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 2;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial == "Vendedores" ? "" : AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;
           

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasMembresuasRangoFechas_PaginacionExcel,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 9000
                }
            };
            RespListVentasDetalleDTO oResp = null;
            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public List<VentasDetalleDTO> uspReporteVentasMembresiasRangoFechas_preciocero_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 2;
            oVentasDetalleDTO.Turno = Turno;            
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial == "Vendedores" ? "" : AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasMembresiasRangoFechasPrecioCero_Paginacion,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 9000
                }
            };
            RespListVentasDetalleDTO oResp = null;
            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }



        public List<VentasDetalleDTO> uspReporteVentasServiciosRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 4;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasServiciosRangoFechas_PaginacionExcel,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 5000
                }
            };

            RespListVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;


        }

        // TIPO 1 = SUPLEMENTOS, 2 = JUGUERIA, 3 = ROPA FITNESS, 4 = ACCESORIOS
        public List<ComprobanteDetalleDTO> CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion(int CodigoUnidadNegocio, int CodigoSede, DateTime request_FechaInicio, DateTime request_Fin, string request_Vendedor, string request_Counter, int request_Turno, int request_FormaPago, int request_Tipo)
        {

            List<ComprobanteDetalleDTO> lista = new List<ComprobanteDetalleDTO>();

            ReqFilterComprobanteDetalleDTO oReq = new ReqFilterComprobanteDetalleDTO()
            {
                Item = new ComprobanteDetalleDTO()
                {
                    CodigoUnidadNegocio = CodigoUnidadNegocio,
                    CodigoSede = CodigoSede,
                    request_FechaInicio = request_FechaInicio,
                    request_Fin = request_Fin,
                    request_Vendedor = request_Vendedor,
                    request_Counter = request_Counter,
                    request_Tipo = request_Tipo,
                    request_Turno = request_Turno,
                    request_FormaPago = request_FormaPago

                },
                FilterCase = filterCaseComprobanteDetalle.CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 99999999
                }
            };

            RespListComprobanteDetalleDTO oResp = null;

            using (ComprobanteDetalleLogic oComprobanteDetalleLogic = new ComprobanteDetalleLogic())
            {
                oResp = oComprobanteDetalleLogic.ComprobanteDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public List<VentasDetalleDTO> uspReporteVentasNutricionRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 7;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasNutricionRangoFechas_PaginacionExcel,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 9000
                }
            };

            RespListVentasDetalleDTO oResp = null;
            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public List<VentasDetalleDTO> uspReporteVentasPersonalizadoRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 8;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasPersonalizadoRangoFechas_PaginacionExcel,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 900
                }
            };

            RespListVentasDetalleDTO oResp = null;
            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public static List<GastosDTO> uspReporteEgresoRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int PageNumber)
        {
            List<GastosDTO> lista = null;
            GastosDTO oGastosDTO = new GastosDTO();
            oGastosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oGastosDTO.CodigoSede = CodigoSede;
            oGastosDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oGastosDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oGastosDTO.Responsable = Vendedor;
            oGastosDTO.CodigoSede = CodigoSede;
            oGastosDTO.TipoEgreso = 2;
            oGastosDTO.Turno = Turno;
            oGastosDTO.Tipo = 2;

            ReqFilterGastosDTO oReq = new ReqFilterGastosDTO()
            {
                FilterCase = filterCaseGastos.uspReporteEgresoRangoFechas_PaginacionExcel,
                User = "Admin",
                Item = oGastosDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListGastosDTO oResp = null;

            using (GastosLogic oEgresosLogic = new GastosLogic())
            {
                oResp = oEgresosLogic.GastosGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;

        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}