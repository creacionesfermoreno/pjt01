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
    public class TipoComprobanteLogic : IDisposable
    {
        TipoComprobanteData oTipoComprobanteData = null;
        public TipoComprobanteLogic()
        {
            oTipoComprobanteData = new TipoComprobanteData();
        }

        public RespListTipoComprobanteDTO TipoComprobanteGetList(ReqFilterTipoComprobanteDTO oReqFilter)
        {

            RespListTipoComprobanteDTO oRespList = new RespListTipoComprobanteDTO();

            oRespList.List = new List<TipoComprobanteDTO>();
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

                    List<TipoComprobanteDTO> CategoriaDTOList = new List<TipoComprobanteDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseTipoComprobante.ecommerce_uspListarTipoComprobante:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oTipoComprobanteData.ecommerce_uspListarTipoComprobante(oReqFilter.Item);
                            break;
                        default:
                            {
                                // CategoriaDTOList = oTipoComprobanteData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
