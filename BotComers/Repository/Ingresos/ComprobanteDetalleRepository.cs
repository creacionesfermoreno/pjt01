using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.Ingresos
{
    public class ComprobanteDetalleRepository : IDisposable
    {
        public List<ComprobanteDetalleDTO> CentroEntrenamiento_uspListarComprobanteDetalleParaAnular(ComprobanteDetalleDTO request)
        {
            List<ComprobanteDetalleDTO> lista = new List<ComprobanteDetalleDTO>();

            ReqFilterComprobanteDetalleDTO oReq = new ReqFilterComprobanteDetalleDTO()
            {
                Item = new ComprobanteDetalleDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoComprobante = request.CodigoComprobante
                },
                FilterCase = filterCaseComprobanteDetalle.CentroEntrenamiento_uspListarComprobanteDetalleParaAnular,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(request.PageNumber),
                    PageRecords = 0
                }
            };

            RespListComprobanteDetalleDTO oResp = null;

            using (ComprobanteDetalleLogic oComprobanteDetalleLogic = new ComprobanteDetalleLogic())
            {
                oResp = oComprobanteDetalleLogic.ComprobanteDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }


        public List<ComprobanteDetalleDTO> CentroEntrenamiento_uspListarDeudasCliente(ComprobanteDetalleDTO request)
        {
            List<ComprobanteDetalleDTO> lista = new List<ComprobanteDetalleDTO>();

            ReqFilterComprobanteDetalleDTO oReq = new ReqFilterComprobanteDetalleDTO()
            {
                Item = new ComprobanteDetalleDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    Identificacion = request.Identificacion
                },
                FilterCase = filterCaseComprobanteDetalle.CentroEntrenamiento_uspListarDeudasCliente,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(request.PageNumber),
                    PageRecords = 0
                }
            };

            RespListComprobanteDetalleDTO oResp = null;

            using (ComprobanteDetalleLogic oComprobanteDetalleLogic = new ComprobanteDetalleLogic())
            {
                oResp = oComprobanteDetalleLogic.ComprobanteDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }


        public List<ComprobanteDetalleDTO> CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion(ComprobanteDetalleDTO request)
        {

            List<ComprobanteDetalleDTO> lista = new List<ComprobanteDetalleDTO>();

            ReqFilterComprobanteDetalleDTO oReq = new ReqFilterComprobanteDetalleDTO()
            {
                Item = new ComprobanteDetalleDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    request_FechaInicio = request.request_FechaInicio,
                    request_Fin = request.request_Fin,
                    request_Vendedor = request.request_Vendedor,
                    request_Counter = request.request_Counter,
                    request_Tipo = request.request_Tipo,
                    request_Turno = request.request_Turno,
                    request_FormaPago = request.request_FormaPago

                },
                FilterCase = filterCaseComprobanteDetalle.CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(request.PageNumber),
                    PageRecords = 0
                }
            };

            RespListComprobanteDetalleDTO oResp = null;

            using (ComprobanteDetalleLogic oComprobanteDetalleLogic = new ComprobanteDetalleLogic())
            {
                oResp = oComprobanteDetalleLogic.ComprobanteDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public ComprobanteDetalleDTO CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_NumeroRegistros(ComprobanteDetalleDTO request)
        {
            ComprobanteDetalleDTO response = new ComprobanteDetalleDTO();

            ReqFilterComprobanteDetalleDTO oReq = new ReqFilterComprobanteDetalleDTO()
            {
                Item = new ComprobanteDetalleDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    request_FechaInicio = request.request_FechaInicio,
                    request_Fin = request.request_Fin,
                    request_Vendedor = request.request_Vendedor,
                    request_Counter = request.request_Counter,
                    request_Tipo = request.request_Tipo,
                    request_Turno = request.request_Turno,
                    request_FormaPago = request.request_FormaPago
                },
                FilterCase = filterCaseComprobanteDetalle.CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_NumeroRegistros,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemComprobanteDetalleDTO oResp = null;

            using (ComprobanteDetalleLogic oComprobanteDetalleLogic = new ComprobanteDetalleLogic())
            {
                oResp = oComprobanteDetalleLogic.ComprobanteDetalleGetItem(oReq);
            }

            if (oResp.Success)
            {
                response = oResp.Item;
            }
            return response;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}