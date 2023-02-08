using E_DataModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace BotComers.ExportarExcel.Appsfit
{
    /// <summary>
    /// Descripción breve de ExportarPostVentaSegmentacionActivos
    /// </summary>
    public class ExportarPostVentaSegmentacionActivos : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Titulo = "Listado de Socios Activos";

            int CodigoUnidadNegocio = Convert.ToInt32(context.Request.Params["CodigoUnidadNegocio"].ToString());

            int PageNumber = 1;
            DateTime FechaInicio = Convert.ToDateTime(context.Request.Params["FechaInicio"].ToString());
            DateTime FechaFin = Convert.ToDateTime(context.Request.Params["FechaFin"].ToString());

            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<E_DataModel.Gimnasio.ClientesDTO> lista = null;
            E_DataModel.Gimnasio.ClientesDTO oClientesDTO = new E_DataModel.Gimnasio.ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Convert.ToInt32(context.Request.Params["CodigoSede"].ToString());// CodigoSede;
            oClientesDTO.CodTiempoPaquete = Convert.ToInt32(context.Request.Params["CodigoTiempoMenbresia"].ToString());// CodigoTiempoMenbresia;
            oClientesDTO.Genero = context.Request.Params["Genero"].ToString(); //Genero;
            oClientesDTO.EdadRango1 = Convert.ToInt32(context.Request.Params["EdadRango1"].ToString());//EdadRango1;
            oClientesDTO.EdadRango2 = Convert.ToInt32(context.Request.Params["EdadRango2"].ToString());//EdadRango2;
            oClientesDTO.EstadoDeuda = Convert.ToInt32(context.Request.Params["EstadoDeuda"].ToString());//EstadoDeuda;
            oClientesDTO.EstadoAsistencia = context.Request.Params["EstadoAsistencia"].ToString();// EstadoAsistencia;
            oClientesDTO.Ubicaciones = context.Request.Params["Ubicaciones"].ToString();//

            oClientesDTO.AsesorComercial = context.Request.Params["AsesorComercial"].ToString();// AsesorComercial;
            oClientesDTO.Nombre = context.Request.Params["Nombre"].ToString();//Nombre;
            oClientesDTO.Apellidos = context.Request.Params["Apellidos"].ToString();// Apellidos;
            oClientesDTO.CodigoS = Convert.ToInt32(context.Request.Params["CodigoS"].ToString());// CodigoS;
            oClientesDTO.DNI = context.Request.Params["DNI"].ToString();// DNI;
            oClientesDTO.Telefono = context.Request.Params["Telefono"].ToString();// Telefono;
            oClientesDTO.Celular = context.Request.Params["Celular"].ToString();// Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            E_DataModel.Gimnasio.ReqFilterClientesDTO oReq = new E_DataModel.Gimnasio.ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesActivos_ExportarExcel,
                Item = oClientesDTO,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 4000,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };

            E_DataModel.Gimnasio.RespListClientesDTO oResp = null;

            using (E_BusinessLayer.Gimnasio.ClientesLogic oSociosLogic = new E_BusinessLayer.Gimnasio.ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<E_DataModel.Gimnasio.ClientesDTO>();
                lista = oResp.List;
            }

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
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>APELLIDOS</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>CORREO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>DNI</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>CELULAR</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>F NACIMIENTO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:50px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>DIRECCION</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>PAQUETE</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>COSTO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>FECHA INICIO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>FECHA FIN</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>VENDEDOR</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>REPARTIDO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>ASISTENCIAS</th>");
            //datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>ULTIMA ASISTENCIA</th>");
            datarow.Append("          </tr>");
            datarow.Append("      </thead>");
            datarow.Append("      <tbody>");
            if (lista != null)
                foreach (var item in lista)
                {
                    datarow.Append("      <tr>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\">" + item.CodigoSocio + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Nombres + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Apellidos + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Correo + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:70px; text-align:left; color: Black; font-size: 12px; mso-number-format: '\\@';\">" + item.DNI + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Celular + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + Convert.ToDateTime(item.FechaNacimiento).ToString("dd/MM/yyyy") + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:90px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Direccion + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:300px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.desTiempoPaquete + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Costo + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.DesFechaInicio + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.DesFechaFin + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Vendedor + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.VendedorRepartido + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.NroIngresoActual + "</td>");
                    //datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'dd/MM/yyyy';\">" + item.DesFechaIngreso + "</td>");
                    datarow.Append("      </tr>");
                }
            datarow.Append("      </tbody>");
            datarow.Append("  </table>");
            datarow.Append(" </body>");
            datarow.Append("</html>");
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=InformeSociosActivos.xls");
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
    }
}