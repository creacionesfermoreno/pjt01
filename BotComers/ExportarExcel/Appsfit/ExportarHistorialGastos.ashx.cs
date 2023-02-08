using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace BotComers.ExportarExcel.Appsfit
{
    /// <summary>
    /// Descripción breve de ExportarHistorialGastos
    /// </summary>
    public class ExportarHistorialGastos : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Titulo = "Listado historial de gastos.";

            int CodigoUnidadNegocio = Convert.ToInt32(context.Request.Params["CodigoUnidadNegocio"].ToString());
            int CodigoSede = Convert.ToInt32(context.Request.Params["CodigoSede"].ToString());
            DateTime FechaInicio = Convert.ToDateTime(context.Request.Params["FechaInicio"].ToString());
            DateTime FechaFin = Convert.ToDateTime(context.Request.Params["FechaFin"].ToString());
            string Vendedor = context.Request.Params["Vendedor"].ToString();
            int Turno = Convert.ToInt32(context.Request.Params["Turno"]);
            int TipoEgreso = Convert.ToInt32(context.Request.Params["TipoEgreso"]);
            int TipoDocumento = Convert.ToInt32(context.Request.Params["TipoDocumento"]);

            List<GastosDTO> lista = uspReporteEgresoRangoFechas_Exportar(CodigoUnidadNegocio, CodigoSede, FechaInicio, FechaFin, Vendedor, Turno, TipoEgreso, TipoDocumento, 1);
            StringBuilder datarow = new StringBuilder();
            datarow.Append("<!DOCTYPE html>");
            datarow.Append("<html>");
            datarow.Append(" <head>");
            datarow.Append("    <meta name='viewport' content='width=device-width' />");
            datarow.Append("        <title>Export</title>");
            datarow.Append(" </head>");
            datarow.Append(" <body>");
            datarow.Append("  <table class='table'>");
            datarow.Append("      <thead>");
            datarow.Append("          <tr>");
            datarow.Append("              <td colspan='10' style='border: 1px; border-color: Black; border-style: solid; height:33px; text-align:center; font-size: 25px; color: #000000; font-family:Calibri; font-weight: bold;'>" + Titulo + "</td>");
            datarow.Append("          </tr>");
            datarow.Append("          <tr>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO EGRESO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:left; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCRIPCION</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:left; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>OBSERVACIONES</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:left; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>PROVEEDOR</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:left; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>RUC</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:left; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO DOCUMENTO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>NRO DOCUMENTO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>RESPONSABLE</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>FECHA HORA</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>MONTO</th>");

            datarow.Append("          </tr>");
            datarow.Append("      </thead>");
            datarow.Append("      <tbody>");
            if (lista != null)
                foreach (var item in lista)
                {
                    datarow.Append("      <tr>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.TipoEgresoDescripcion + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:220px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Descripcion + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:120px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Observaciones + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.RZProveedor + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.RUCProveedor + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.DescTipoMoneda + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.NumeroRecibo + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.UsuarioCreacion + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy HH:mm:ss';\">" + Convert.ToDateTime(item.FechaHora).ToString("dd/MM/yyyy HH:mm:ss tt") + "</td>");

                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:center; color: Black; font-size: 12px; mso-number-format:'0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.MontoEgreso) + "</td>");

                    datarow.Append("      </tr>");
                }
            datarow.Append("      </tbody>");
            datarow.Append("  </table>");
            datarow.Append(" </body>");
            datarow.Append("</html>");
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=InformeAsistenciaPersonalFijo.xls");
            context.Response.Write(datarow.ToString());
            context.Response.Charset = "";
            context.Response.End();


        }

        public List<GastosDTO> uspReporteEgresoRangoFechas_Exportar(int CodigoUnidadNegocio, int CodigoSede, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int TipoEgreso, int TipoDocumento, int PageNumber)
        {
            using (Repository.Gimnasio.ModuloCajaRepository oRepository = new Repository.Gimnasio.ModuloCajaRepository())
            {
                return oRepository.uspReporteEgresoRangoFechas_Exportar(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodigoSede, Turno, TipoEgreso, TipoDocumento, PageNumber);
            }
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