using BotComers.ViewModels.Ingresos;
using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.Ingresos
{
    public class ComprobantePagoRepository : IDisposable
    {
        public List<ComprobantePagoDTO> CentroEntrenamiento_uspTotalPagosProductosRangoFechas(ComprobantePagoDTO request)
        {
            List<ComprobantePagoDTO> lista = new List<ComprobantePagoDTO>();

            ReqFilterComprobantePagoDTO oReq = new ReqFilterComprobantePagoDTO()
            {
                Item = new ComprobantePagoDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    request_FechaInicio = request.request_FechaInicio,
                    request_Fin = request.request_Fin,
                    request_Vendedor = request.request_Vendedor,
                    request_Counter = request.request_Counter,
                    request_Tipo = request.request_Tipo,
                    request_Turno = request.request_Turno

                },
                FilterCase = filterCaseComprobantePago.CentroEntrenamiento_uspTotalPagosProductosRangoFechas,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListComprobantePagoDTO oResp = null;

            using (ComprobantePagoLogic oComprobantePagoLogic = new ComprobantePagoLogic())
            {
                oResp = oComprobantePagoLogic.ComprobantePagoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public string ecommerce_uspRegistrarComprobantePago(ComprobanteViewModel oItem)
        {
            string mensaje = string.Empty;

            List<ComprobantePagoDTO> list = new List<ComprobantePagoDTO>();
            foreach (ComprobantePagoDTO item in oItem.listaDetallePago)
            {
                list.Add(new ComprobantePagoDTO()
                {
                    CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                    CodigoSede = oItem.CodigoSede,
                    CodigoComprobantePago = item.CodigoComprobantePago,
                    CodigoComprobante = item.CodigoComprobante,
                    CodigoCuentaBancaria = item.CodigoCuentaBancaria,
                    CodigoMetodoPago = item.CodigoMetodoPago,
                    TipoMoneda = item.TipoMoneda,
                    Monto = item.Monto,
                    Nota = item.Nota == null ? string.Empty : item.Nota,
                    Estado = item.Estado,
                    UsuarioCreacion = oItem.UsuarioCreacion,
                    Operation = item.Accion == "N" ? Operation.Create : Operation.Update
                });
            }

            ReqComprobantePagoDTO oReq = new ReqComprobantePagoDTO()
            {
                List = list,
                User = "admin"
            };
            RespComprobantePagoDTO oResp = null;
            using (ComprobantePagoLogic oComprobantePagoLogic = new ComprobantePagoLogic())
            {
                oResp = oComprobantePagoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }

            return mensaje;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}