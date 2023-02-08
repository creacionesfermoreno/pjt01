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
    /// Descripción breve de ExportaPostVentaAsistencias
    /// </summary>
    public class ExportaPostVentaAsistencias : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Titulo = "Listado de asistencias de socios.";

            int CodigoUnidadNegocio = Convert.ToInt32(context.Request.Params["CodigoUnidadNegocio"].ToString());
            int CodigoSede = Convert.ToInt32(context.Request.Params["CodigoSede"].ToString());
            string Buscador = string.Empty;

            string FechaInicio = context.Request.Params["FechaInicio"].ToString();
            string FechaFin = context.Request.Params["FechaFin"].ToString();

            string Fi = context.Request.Params["Fi"].ToString();
            string Ff = context.Request.Params["Ff"].ToString();

            List<AsistenciaDTO> lista = ListarAsistenciaTodosFiltro(CodigoUnidadNegocio, CodigoSede, FechaInicio, FechaFin, Fi, Ff, Buscador);

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
            datarow.Append("              <td colspan='8' style='border: 1px; border-color: Black; border-style: solid; height:33px; text-align:center; font-size: 25px; color: #000000; font-family:Calibri; font-weight: bold;'>" + Titulo + "</td>");
            datarow.Append("          </tr>");
            datarow.Append("          <tr>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>CODIGO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>NOMBRES</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>CORREO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>DNI</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>CELULAR</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>TIPO PLAN</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>ORIGEN</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>PLAN</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>FECHA INICIO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>FECHA FIN</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>ULTIMA ASISTENCIA</th>");

            datarow.Append("          </tr>");
            datarow.Append("      </thead>");
            datarow.Append("      <tbody>");
            if (lista != null)
                foreach (var item in lista)
                {
                    datarow.Append("      <tr>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.CodigoSocio + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:180px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Nombres + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Correo + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.DNI + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Celular + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.DesTipoPaquete + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.DesTipoIngreso + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:300px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.desTiempoPaquete + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.FechaInicio + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.FechaFin + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.strFechaIngreso + "</td>");
                    datarow.Append("      </tr>");
                }
            datarow.Append("      </tbody>");
            datarow.Append("  </table>");
            datarow.Append(" </body>");
            datarow.Append("</html>");
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=InformeAsistenciaSocios.xls");
            context.Response.Write(datarow.ToString());
            context.Response.Charset = "";
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        //ASISTENCIAS
        public List<AsistenciaDTO> ListarAsistenciaTodosFiltro(int CodigoUnidadNegocio, int CodigoSede, string fechaInicio, string fechaFin, string Hi, string Hf, string Buscador)
        {
            DateTime fechaConsulta;
            DateTime fechaConsultaFin;

            DateTime HoraInicio = Convert.ToDateTime(Hi);
            DateTime HoraFin = Convert.ToDateTime(Hf);
            if (fechaInicio == string.Empty)
            {
                fechaConsulta = DateTime.Now;
            }
            else
            {
                fechaConsulta = new DateTime(Convert.ToInt32(fechaInicio.Split('/')[2]), Convert.ToInt32(fechaInicio.Split('/')[1]), Convert.ToInt32(fechaInicio.Split('/')[0]), HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second);
            }

            if (fechaFin == string.Empty)
            {
                fechaConsultaFin = DateTime.Now;
            }
            else
            {
                fechaConsultaFin = new DateTime(Convert.ToInt32(fechaFin.Split('/')[2]), Convert.ToInt32(fechaFin.Split('/')[1]), Convert.ToInt32(fechaFin.Split('/')[0]), HoraFin.Hour, HoraFin.Minute, HoraFin.Second);
            }

            List<AsistenciaDTO> lista = new List<AsistenciaDTO>();

            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            oAsistenciaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oAsistenciaDTO.CodigoSede = CodigoSede;
            oAsistenciaDTO.TipoPersona = "S";
            oAsistenciaDTO.FechaIngreso = fechaConsulta;
            oAsistenciaDTO.FechaFinalizo = fechaConsultaFin;
            oAsistenciaDTO.HoraInicioAsistencia = HoraInicio;
            oAsistenciaDTO.HoraFinAsistencia = HoraFin;
            oAsistenciaDTO.Nombres = Buscador;

            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                FilterCase = filterCaseAsistencia.ListarAsistenciaTodosFiltro_Paginacion,
                Item = oAsistenciaDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 100000
                }
            };

            RespListAsistenciaDTO oResp = null;

            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.AsistenciaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

    }
}