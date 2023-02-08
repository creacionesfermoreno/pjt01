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
    /// Descripción breve de ExportarAsistenciaPersonalAdministrativo
    /// </summary>
    public class ExportarAsistenciaPersonalAdministrativo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Titulo = "Listado de asistencia del personal fijo.";

            int CodigoUnidadNegocio = Convert.ToInt32(context.Request.Params["CodigoUnidadNegocio"].ToString());
            int CodigoSede = Convert.ToInt32(context.Request.Params["CodigoSede"].ToString());
            string NroDocumento = context.Request.Params["NroDocumento"].ToString();
            string NombreCompleto = context.Request.Params["NombreCompleto"].ToString();
            int CodigoCargo = Convert.ToInt32(context.Request.Params["CodigoCargo"]);
            DateTime FechaInicio = Convert.ToDateTime(context.Request.Params["FechaInicio"].ToString());
            DateTime FechaFin = Convert.ToDateTime(context.Request.Params["FechaFin"].ToString());

            PersonalAsistenciaDTO request = new PersonalAsistenciaDTO();
            request.CodigoUnidadNegocio = CodigoUnidadNegocio;
            request.CodigoSede = CodigoSede;
            request.NumeroDocumento = NroDocumento;
            request.NombreCompleto = NombreCompleto;
            request.CodigoCargo = CodigoCargo;
            request.FechaInicio = FechaInicio;
            request.FechaFin = FechaFin;

            List<PersonalAsistenciaDTO> lista = ListarAsistenciaPersonalAdministrativo(request, 1, 10000);

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
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>FECHA</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>HORA INGRESO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>INICIO BREAK</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>FIN BREAK</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>HORA SALIDA</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>MINUTOS TARDE</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>HORA INGRESO 2</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>INICIO BREAK 2</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>FIN BREAK 2</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>HORA SALIDA 2</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>MINUTOS TARDE 2</th>");
            datarow.Append("          </tr>");
            datarow.Append("      </thead>");
            datarow.Append("      <tbody>");
            if (lista != null)
                foreach (var item in lista)
                {
                    datarow.Append("      <tr>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.NumeroDocumento + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:220px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.NombreCompleto + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.DescripcionCargo + "</td>");
                    if (item.FechaHoraIngreso != null)
                    {
                        datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + Convert.ToDateTime(item.FechaHoraIngreso).ToString("dd/MM/yyy") + "</td>");
                        datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'HH:mm:ss';\">" + Convert.ToDateTime(item.FechaHoraIngreso).ToString("HH:mm:ss tt") + "</td>");
                    }
                    else
                    {
                        datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">00/00/00</td>");
                        datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'HH:mm:ss';\">00:00</td>");
                    }

                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'HH:mm:ss';\">" + (item.FechaHoraRefrigerioSalida != null ? Convert.ToDateTime(item.FechaHoraRefrigerioSalida).ToString("HH:mm:ss tt") : "00:00") + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'HH:mm:ss';\">" + (item.FechaHoraRefrigerioRetorno != null ? Convert.ToDateTime(item.FechaHoraRefrigerioRetorno).ToString("HH:mm:ss tt") : "00:00") + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'HH:mm:ss';\">" + (item.FechaHoraSalida != null ? Convert.ToDateTime(item.FechaHoraSalida).ToString("HH:mm:ss tt") : "00:00") + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.TardanzaMinutos + "</td>");

                    if (item.FechaHoraIngreso_TurnoTarde != null)
                    {
                        datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'HH:mm:ss';\">" + Convert.ToDateTime(item.FechaHoraIngreso_TurnoTarde).ToString("HH:mm:ss tt") + "</td>");
                    }
                    else
                    {
                        datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'HH:mm:ss';\">00:00</td>");
                    }

                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'HH:mm:ss';\">" + (item.FechaHoraRefrigerioSalida_TurnoTarde != null ? Convert.ToDateTime(item.FechaHoraRefrigerioSalida_TurnoTarde).ToString("HH:mm:ss tt") : "00:00") + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'HH:mm:ss';\">" + (item.FechaHoraRefrigerioRetorno_TurnoTarde != null ? Convert.ToDateTime(item.FechaHoraRefrigerioRetorno_TurnoTarde).ToString("HH:mm:ss tt") : "00:00") + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'HH:mm:ss';\">" + (item.FechaHoraSalida_TurnoTarde != null ? Convert.ToDateTime(item.FechaHoraSalida_TurnoTarde).ToString("HH:mm:ss tt") : "00:00") + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.TardanzaMinutos_TurnoTarde + "</td>");


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


        public List<PersonalAsistenciaDTO> ListarAsistenciaPersonalAdministrativo(PersonalAsistenciaDTO request, int PageNumber, int PageRecords)
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
                FilterCase = filterCasePersonalAsistencia.AsistenciaPersonalAdministrativo
            };

            RespListPersonalAsistenciaDTO oResp = null;
            using (PersonalAsistenciaLogic oLogic = new PersonalAsistenciaLogic())
            {
                oResp = oLogic.PersonalAsistenciaGetList(oReq);
            }
            return oResp.List;
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