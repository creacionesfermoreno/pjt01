using BotComers.ViewModels.Inventario;
using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace BotComers.ExportarExcel.Appsfit
{
    /// <summary>
    /// Descripción breve de ExportaInventarioValorInventario
    /// </summary>
    public class ExportaInventarioValorInventario : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Titulo = "Listado de Socios Inactivos";

            int CodigoUnidadNegocio = Convert.ToInt32(context.Request.Params["CodigoUnidadNegocio"].ToString());
            int CodigoSede = Convert.ToInt32(context.Request.Params["CodigoSede"].ToString());// CodigoSede;
            int CodigoAlmacen = Convert.ToInt32(context.Request.Params["CodigoAlmacen"].ToString());// CodigoAlmacen;

            List<ItemsVentaViewModel> lista = new List<ItemsVentaViewModel>();

            ReqFilterItemsVentaDTO oReq = new ReqFilterItemsVentaDTO()
            {
                Item = new ItemsVentaDTO()
                {
                    CodigoUnidadNegocio = CodigoUnidadNegocio,
                    CodigoSede = CodigoSede,
                    CodigoAlmacen = CodigoAlmacen,
                    UsuarioCreacion = "appsfit"
                },
                FilterCase = filterCaseItemsVenta.ecommerce_uspListarValorInventario_Paginaciones,
                User = "appsift",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListItemsVentaDTO oResp = null;

            using (ItemsVentaLogic oItemsVentaLogic = new ItemsVentaLogic())
            {
                oResp = oItemsVentaLogic.ItemsVentaGetList(oReq);
            }

            if (oResp.Success)
            {

                foreach (ItemsVentaDTO item in oResp.List)
                {
                    lista.Add(new ItemsVentaViewModel()
                    {
                        CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                        CodigoSede = item.CodigoSede,
                        CodigoAlmacen = item.CodigoAlmacen,
                        CodigoItemVenta = item.CodigoItemVenta,
                        Nombre = item.Nombre,
                        Referencia = item.Referencia,
                        Descripcion = item.Descripcion,
                        d_CantidadActual = item.d_CantidadActual,
                        d_CostoPromedio = item.d_CostoPromedio,
                        d_CostoTotal = item.d_CostoTotal,
                        CodigoUnidadMedida = item.CodigoUnidadMedida,
                        Estado = item.Estado,
                        UrlImagen = item.UrlImagen
                    });
                }
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
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>IMAGEN</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>NOMBRES</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>REFERENCIAS</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>DESCRIPCION</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>CANTIDAD ACTUAL</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>COSTO PROMEDIO</th>");
            datarow.Append("              <th style='border: 1px; border-color: Black; border-style: solid; height:40px; text-align:center; font-size: 12px; background-color: #ff0200; color: #fff; font-weight: bold;'>COSTO TOTAL</th>");
            datarow.Append("          </tr>");
            datarow.Append("      </thead>");
            datarow.Append("      <tbody>");
            if (lista != null)
                foreach (var item in lista)
                {
                    datarow.Append("      <tr style='height: 60px;'>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:80px; text-align:center; color: Black; font-size: 12px; mso-number-format:'General';\"><img src='" + item.UrlImagen + "' width='6%' height='6%' alt=''></td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Nombre + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Referencia + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.Descripcion + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + item.d_CantidadActual + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + decimal.Round(item.d_CostoPromedio, 2) + "</td>");
                    datarow.Append("         <td style=\"border: 1px; border-color: Black; border-style: solid; width:150px; text-align:left; color: Black; font-size: 12px; mso-number-format: 'General';\">" + decimal.Round(item.d_CostoTotal, 2) + "</td>");
                    datarow.Append("      </tr>");
                }
            datarow.Append("      </tbody>");
            datarow.Append("  </table>");
            datarow.Append(" </body>");
            datarow.Append("</html>");
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=valorinventario.xls");
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