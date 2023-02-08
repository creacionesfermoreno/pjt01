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
    /// Descripción breve de ExportaPostVentaDeudas
    /// </summary>
    public class ExportaPostVentaDeudas : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            string Titulo = "Listado de Socios con Deuda";

            int CodigoUnidadNegocio = Convert.ToInt32(context.Request.Params["CodigoUnidadNegocio"].ToString());
            int CodigoSede = Convert.ToInt32(context.Request.Params["CodigoSede"].ToString());
            string Buscador = context.Request.Params["Buscador"].ToString();
            string Vendedor = context.Request.Params["Vendedor"].ToString();
            DateTime FechaInicio = Convert.ToDateTime(context.Request.Params["FechaInicio"].ToString());
            DateTime FechaFin = Convert.ToDateTime(context.Request.Params["FechaFin"].ToString());
            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ContratoDTO> lista = uspListarMembresiasSociosAcuenta_Paginacion(CodigoUnidadNegocio, CodigoSede, Vendedor, Fi, Ff, Buscador);

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
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>COSTO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>DEBE</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>VENDEDOR</th>");
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
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.FechaInicio.ToString("dd/MM/yyy") + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.FechaFin.ToString("dd/MM/yyy") + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Costo + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Debe + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Vendedor + "</td>");
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


        public List<ContratoDTO> uspListarMembresiasSociosAcuenta_Paginacion(int CodigoUnidadNegocio, int CodigoSede, string Vendedor, DateTime fecha, DateTime fechaFin, string Buscador)
        {

            List<ContratoDTO> lista = new List<ContratoDTO>();
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = CodigoSede;
            oContratoDTO.UsuarioCreacion = Vendedor;
            oContratoDTO.FechaInicio = fecha;
            oContratoDTO.FechaFin = fechaFin;
            oContratoDTO.Nombres = Buscador;

            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.uspListarMembresiasSociosAcuenta_Paginacion,
                Item = oContratoDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 1,
                    PageRecords = 100000
                }
            };

            RespListContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;

            }
            return lista;
        }


    }
}