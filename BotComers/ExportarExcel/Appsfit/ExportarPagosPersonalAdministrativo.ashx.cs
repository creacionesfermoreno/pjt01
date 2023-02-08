using E_BusinessLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace BotComers.ExportarExcel.Appsfit
{
    /// <summary>
    /// Descripción breve de ExportarPagosPersonalAdministrativo
    /// </summary>
    public class ExportarPagosPersonalAdministrativo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Titulo = "Listado de asistencia del personal fijo.";

            int CodigoUnidadNegocio = Convert.ToInt32(context.Request.Params["CodigoUnidadNegocio"].ToString());
            int CodigoSede = Convert.ToInt32(context.Request.Params["CodigoSede"].ToString());
            string NroDocumento = context.Request.Params["NroDocumento"].ToString();
            string NombreCompleto = context.Request.Params["NombreCompleto"].ToString();
            int CodigoDisciplina = Convert.ToInt32(context.Request.Params["CodigoDisciplina"]);
            DateTime FechaInicio = Convert.ToDateTime(context.Request.Params["FechaInicio"].ToString());
            DateTime FechaFin = Convert.ToDateTime(context.Request.Params["FechaFin"].ToString());

            PersonalAsistenciaDTO request = new PersonalAsistenciaDTO();
            request.CodigoUnidadNegocio = CodigoUnidadNegocio;
            request.CodigoSede = CodigoSede;
            request.NumeroDocumento = NroDocumento;
            request.NombreCompleto = NombreCompleto;
            request.CodigoDisciplina = CodigoDisciplina;
            request.FechaInicio = FechaInicio;
            request.FechaFin = FechaFin;

            List<PersonalAdministrativoAsistenciaResumentDTO> lista = ListarAsistenciaPersonalAdministrativoResumen(request, 1, 10000);

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
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>NRO DOCUMENTO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>NOMBRE COMPLETO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>CARGO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>DIAS LABORADOS</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>HORAS TRABAJADAS</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>MINUTOS TARDANZA</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCUENTO TARDANZA</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>SUELDO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>SUB TOTAL</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>PAGO TOTAL</th>");
            datarow.Append("          </tr>");
            datarow.Append("      </thead>");
            datarow.Append("      <tbody>");
            if (lista != null)
                foreach (var item in lista)
                {
                    datarow.Append("      <tr>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.DNI + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:220px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.NombreCompleto + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.DescripcionCargo + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.DiasTrabajados + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.HorasTrabajadasText + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.MinutosTardanza + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:center; color: Black; font-size: 12px; mso-number-format:'0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.TotalDescuento) + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:center; color: Black; font-size: 12px; mso-number-format:'0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.Sueldo) + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:center; color: Black; font-size: 12px; mso-number-format:'0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.SubTotal) + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:center; color: Black; font-size: 12px; mso-number-format:'0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.TotalNeto) + "</td>");

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

        public List<PersonalAdministrativoAsistenciaResumentDTO> ListarAsistenciaPersonalAdministrativoResumen(PersonalAsistenciaDTO request, int PageNumber, int PageRecords)
        {
            request.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            request.CodigoSede = request.CodigoSede;
            ReqFilterPersonalAsistenciaDTO oReq = new ReqFilterPersonalAsistenciaDTO()
            {
                Item = request,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = Convert.ToUInt32(PageRecords)
                },
                FilterCase = filterCasePersonalAsistencia.ListarAsistenciaPersonalAdministrativoResumen
            };

            RespListPersonalAsistenciaDTO oResp = null;
            using (PersonalAsistenciaLogic oLogic = new PersonalAsistenciaLogic())
            {
                oResp = oLogic.PersonalAsistenciaGetList(oReq);
            }
            return oResp.ListPersonalAdministrativoAsistencia;
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