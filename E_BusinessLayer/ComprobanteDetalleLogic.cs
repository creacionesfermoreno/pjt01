using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer;
using E_DataModel;
using E_DataModel.Common;

namespace E_BusinessLayer
{
    public class ComprobanteDetalleLogic : IDisposable
    {
        ComprobanteDetalleData oComprobanteDetalleData = null;
        public ComprobanteDetalleLogic()
        {
            oComprobanteDetalleData = new ComprobanteDetalleData();
        }

        public RespListComprobanteDetalleDTO ComprobanteDetalleGetList(ReqFilterComprobanteDetalleDTO oReqFilter)
        {
            RespListComprobanteDetalleDTO oRespList = new RespListComprobanteDetalleDTO();

            oRespList.List = new List<ComprobanteDetalleDTO>();
            oRespList.User = oReqFilter.User;
            oRespList.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilter.User))
            {
                oRespList.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Categoria no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilter.Paging == null)
            {
                oRespList.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }


            if (oRespList.MessageList.Count == 0)
            {
                try
                {

                    if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                    {
                        oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<ComprobanteDetalleDTO> CategoriaDTOList = new List<ComprobanteDetalleDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseComprobanteDetalle.CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oComprobanteDetalleData.CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion(oReqFilter.Item, oReqFilter.Paging);
                            break;
                        case filterCaseComprobanteDetalle.CentroEntrenamiento_uspListarDeudasCliente:                           
                            CategoriaDTOList = oComprobanteDetalleData.CentroEntrenamiento_uspListarDeudasCliente(oReqFilter.Item);
                            break;
                        case filterCaseComprobanteDetalle.CentroEntrenamiento_uspListarComprobanteDetalleParaAnular:                           
                            CategoriaDTOList = oComprobanteDetalleData.CentroEntrenamiento_uspListarComprobanteDetalleParaAnular(oReqFilter.Item);
                            break;
                        default:
                            {
                                // CategoriaDTOList = oComprobanteDetalleData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
                            }
                            break;
                    }

                    oRespList.List = CategoriaDTOList;
                    oRespList.Success = true;

                }
                catch (Exception ex)
                {
                    oRespList.Success = false;
                    oRespList.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespList;

        }

        public RespItemComprobanteDetalleDTO ComprobanteDetalleGetItem(ReqFilterComprobanteDetalleDTO oReqFilter)
        {
            RespItemComprobanteDetalleDTO oRespItem = new RespItemComprobanteDetalleDTO();

            oRespItem.Success = false;
            oRespItem.Item = null;
            oRespItem.User = oReqFilter.User;
            oRespItem.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilter.User))
            {
                oRespItem.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Socios no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItem.MessageList.Count == 0)
            {
                ComprobanteDetalleDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseComprobanteDetalle.CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_NumeroRegistros:
                            {
                                oItem = new ComprobanteDetalleDTO();
                                oItem = oComprobanteDetalleData.CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_NumeroRegistros(oReqFilter.Item);
                            }
                            break;                     
                    }

                    oRespItem.Item = new ComprobanteDetalleDTO();
                    oRespItem.Item = oItem;
                    oRespItem.Success = true;
                    oRespItem.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItem.Success = false;
                    oRespItem.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItem;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
