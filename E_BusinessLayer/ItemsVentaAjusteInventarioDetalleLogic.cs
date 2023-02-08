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
    public class ItemsVentaAjusteInventarioDetalleLogic : IDisposable
    {
        ItemsVentaAjusteInventarioDetalleData oItemsVentaAjusteInventarioDetalleData = null;
        public ItemsVentaAjusteInventarioDetalleLogic()
        {
            oItemsVentaAjusteInventarioDetalleData = new ItemsVentaAjusteInventarioDetalleData();
        }

        public RespListItemsVentaAjusteInventarioDetalleDTO ItemsVentaAjusteInventarioDetalleGetList(ReqFilterItemsVentaAjusteInventarioDetalleDTO oReqFilter)
        {

            RespListItemsVentaAjusteInventarioDetalleDTO oRespList = new RespListItemsVentaAjusteInventarioDetalleDTO();

            oRespList.List = new List<ItemsVentaAjusteInventarioDetalleDTO>();
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

                    List<ItemsVentaAjusteInventarioDetalleDTO> DTOList = new List<ItemsVentaAjusteInventarioDetalleDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseItemsVentaAjusteInventarioDetalle.ecommerce_uspListarItemsVentaAjusteInventarioDetalle:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            DTOList = oItemsVentaAjusteInventarioDetalleData.ecommerce_uspListarItemsVentaAjusteInventarioDetalle(oReqFilter.Item);
                            break;
                        default:
                            {
                                // CategoriaDTOList = oItemsVentaData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
                            }
                            break;
                    }

                    oRespList.List = DTOList;
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
