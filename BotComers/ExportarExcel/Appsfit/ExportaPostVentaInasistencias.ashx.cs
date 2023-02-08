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
    /// Descripción breve de ExportaPostVentaInasistencias
    /// </summary>
    public class ExportaPostVentaInasistencias : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Titulo = "Listado de inasistencias de socios.";

            int CodigoUnidadNegocio = Convert.ToInt32(context.Request.Params["CodigoUnidadNegocio"].ToString());
            int CodigoSede = Convert.ToInt32(context.Request.Params["CodigoSede"].ToString());

            string dias = context.Request.Params["dias"].ToString();
            string vendedor = context.Request.Params["vendedor"].ToString();
            string buscador = context.Request.Params["buscador"].ToString();

            List<AsistenciaDTO> lista = uspListar_Socios_Inasistencias(CodigoUnidadNegocio, CodigoSede, dias, vendedor, buscador);

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
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>CELULAR</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>PLAN</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>FECHA INICIO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>FECHA FIN</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>VENDEDOR</th>");
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
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Celular + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:300px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.desTiempoPaquete + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.FechaInicio + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.FechaFin + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:180px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Vendedor + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.strFechaIngreso + "</td>");
                    datarow.Append("      </tr>");
                }
            datarow.Append("      </tbody>");
            datarow.Append("  </table>");
            datarow.Append(" </body>");
            datarow.Append("</html>");
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=InformeInasistenciaSocios.xls");
            context.Response.Write(datarow.ToString());
            context.Response.Charset = "";
            context.Response.End();

        }

        public List<AsistenciaDTO> uspListar_Socios_Inasistencias(int CodigoUnidadNegocio, int CodigoSede, string NumeroDiasAtras, string Vendedor, string Buscador)
        {
            List<AsistenciaDTO> lista = new List<AsistenciaDTO>();
            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            int DiasAtras;
            if (NumeroDiasAtras == "")
            {
                DiasAtras = 1000;
            }
            else
            {
                try
                {
                    DiasAtras = Convert.ToInt32(NumeroDiasAtras);
                }
                catch
                {
                    DiasAtras = 1000;
                }
            }

            if (Vendedor == "")
            {
                Vendedor = "Todos";
            }
            oAsistenciaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oAsistenciaDTO.CodigoSede = CodigoSede;
            oAsistenciaDTO.DiasAtras = DiasAtras;
            oAsistenciaDTO.Vendedor = Vendedor;
            oAsistenciaDTO.Nombres = Buscador;
            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                Item = oAsistenciaDTO,
                User = "appsfit",
                FilterCase = filterCaseAsistencia.uspListar_Socios_Inasistencias_Paginacion,
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


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}