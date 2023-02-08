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
    /// Descripción breve de ExportaPostVentaCumpleanos
    /// </summary>
    public class ExportaPostVentaCumpleanos : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Titulo = "Listado de cumplea&ntilde;eros.";

            int CodigoUnidadNegocio = Convert.ToInt32(context.Request.Params["CodigoUnidadNegocio"].ToString());
            int CodigoSede = Convert.ToInt32(context.Request.Params["CodigoSede"].ToString());
            int flag = Convert.ToInt32(context.Request.Params["flag"].ToString());

            List<ClientesDTO> lista = uspNotificacionCumpleaniosSocios_Paginacion(CodigoUnidadNegocio, CodigoSede, flag);

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
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>F.NACIMIENTO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>EDAD</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>CELULAR</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>DIRECCION</th>");

            datarow.Append("          </tr>");
            datarow.Append("      </thead>");
            datarow.Append("      <tbody>");
            if (lista != null)
                foreach (var item in lista)
                {
                    datarow.Append("      <tr>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.CodigoSocio + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:180px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.NombreCompleto + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.DesFechaNacimiento + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Edad + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:300px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Celular + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Direccion + "</td>");
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


        public List<ClientesDTO> uspNotificacionCumpleaniosSocios_Paginacion(int CodigoUnidadNegocio, int CodigoSede, int flag)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodigoSede;
            oClientesDTO.flag = flag;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspNotificacionCumpleaniosSocios_Paginacion,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 100000
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
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