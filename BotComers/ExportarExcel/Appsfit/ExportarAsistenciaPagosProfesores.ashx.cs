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
    /// Descripción breve de ExportarAsistenciaPagosProfesores
    /// </summary>
    public class ExportarAsistenciaPagosProfesores : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Titulo = "Listado asistencia y pagos de profesores.";

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

            List<PersonalFitnessAsistenciaDTO> lista = ListarInformeProfesores(request, 1, 10000);

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
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:left; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>NOMBRE COMPLETO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:left; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>DISCIPLINA</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>COSTO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>INICIO CLASE</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>INGRESO ASISTENCIA</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>SALIDA ASISTENCIA</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>TARDANZA</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCUENTO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>PAGO TOTAL</th>");

            datarow.Append("          </tr>");
            datarow.Append("      </thead>");
            datarow.Append("      <tbody>");
            if (lista != null)
                foreach (var item in lista)
                {
                    datarow.Append("      <tr>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.NumeroDocumento + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:220px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.NombreProfesionalFitness + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.Disciplina + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:center; color: Black; font-size: 12px; mso-number-format:'0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.CostoPorClase) + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:center; color: Black; font-size: 12px; mso-number-format: 'HH:mm:ss';\">" + Convert.ToDateTime(item.FechaHoraInicioClase).ToString("HH:mm:ss tt") + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format:'dd/MM/yyyy HH:mm:ss';\">" + item.FechaHoraMarcacionIngreso_texto + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format:'dd/MM/yyyy HH:mm:ss';\">" + item.FechaHoraMarcacionSalida_texto + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.Tardanza + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:center; color: Black; font-size: 12px; mso-number-format:'0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.DescuentoPorTardanza) + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:center; color: Black; font-size: 12px; mso-number-format:'0.00';\">" + string.Format(new System.Globalization.CultureInfo("en-US"), "{0:n}", item.TotalPago) + "</td>");

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

        public List<PersonalFitnessAsistenciaDTO> ListarInformeProfesores(PersonalAsistenciaDTO request, int PageNumber, int PageRecords)
        {
            request.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            request.CodigoSede = request.CodigoSede;
            List<PersonalAsistenciaDTO> list = new List<PersonalAsistenciaDTO>();
            list.Add(request);
            ReqFilterPersonalAsistenciaDTO oReq = new ReqFilterPersonalAsistenciaDTO()
            {
                Item = request,
                User = "appsfit",
                FilterCase = filterCasePersonalAsistencia.AsistenciaProfesores,
                Paging = new Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = Convert.ToUInt32(PageRecords)
                }
            };

            RespListPersonalAsistenciaDTO oResp = null;
            using (PersonalAsistenciaLogic oPersonalAsistenciaLogic = new PersonalAsistenciaLogic())
            {
                oResp = oPersonalAsistenciaLogic.PersonalAsistenciaGetList(oReq);
            }
            if (oResp.Success)
            {
                //Codigo = oResp.MessageList[0].Codigo;
            }
            return oResp.ListProfesores;
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