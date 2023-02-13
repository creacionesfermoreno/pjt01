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
    public class CategoriasLogic : IDisposable
    {
        CategoriasData oCategoriasData = null;
        public CategoriasLogic()
        {
            oCategoriasData = new CategoriasData();
        }

        public RespListCategoriasDTO CategoriasGetList(ReqFilterCategoriasDTO oReqFilter)
        {

            RespListCategoriasDTO oRespList = new RespListCategoriasDTO();

            oRespList.List = new List<CategoriasDTO>();
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

                    List<CategoriasDTO> CategoriaDTOList = new List<CategoriasDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCategorias.ecommerce_uspListarCategorias_Edit:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oCategoriasData.ecommerce_uspListarCategorias_Edit(oReqFilter.Item);
                            break;
                        
                        case filterCaseCategorias.api_listCategories:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oCategoriasData.CategoryListApi(oReqFilter.Item);
                            break;

                        default:
                            {
                                // CategoriaDTOList = oCategoriasData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
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

        public RespItemCategoriasDTO CategoriasGetItem(ReqFilterCategoriasDTO oReqFilter)
        {
            RespItemCategoriasDTO oRespItem = new RespItemCategoriasDTO();

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
                CategoriasDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCategorias.ecommerce_uspBuscarCategorias:
                            {
                                oItem = new CategoriasDTO();
                                oItem = oCategoriasData.ecommerce_uspBuscarCategorias(oReqFilter.Item);
                            }
                            break;
                        case filterCaseCategorias.ecommerce_uspBuscarCategoriasTiendaVirutal:
                            {
                                oItem = new CategoriasDTO();
                                oItem = oCategoriasData.ecommerce_uspBuscarCategoriasTiendaVirtual(oReqFilter.Item);
                                ItemsVentaData oItemsVentaData = new ItemsVentaData();
                                ItemsVentaDTO oItemsVentaDTO = new ItemsVentaDTO();
                                oItemsVentaDTO.CodigoUnidadNegocio = oReqFilter.Item.CodigoUnidadNegocio;
                                oItemsVentaDTO.CodigoSede = oReqFilter.Item.CodigoSede;
                                oItemsVentaDTO.CodigoImagen = oReqFilter.Item.CodigoImagenPortada;

                                oReqFilter.Paging = new Paging() {
                                    All = false,
                                    PageNumber = Convert.ToUInt32(1),
                                    PageRecords = 100
                                };

                                oItem.listaItemsVenta = oItemsVentaData.ecommerce_uspListarItemsVenta_PorCategoriaPaginacion(oItemsVentaDTO, oReqFilter.Paging);

                                //agregar productos
                            }
                            break;
                        default:
                            {
                                oItem = new CategoriasDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new CategoriasDTO();
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

        public RespCategoriasDTO ExecuteTransac(ReqCategoriasDTO oReq)
        {
            RespCategoriasDTO oResp = new RespCategoriasDTO();

            oResp.MessageList = new List<Mensaje>();
            oResp.User = oReq.User;

            if (String.IsNullOrEmpty(oReq.User))
            {
                oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Emnpresa no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oResp.MessageList.Count == 0)
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (CategoriasDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oCategoriasData.ecommerce_uspRegistrarCategorias(item);
                                    break;
                                case Operation.Update:
                                    oCategoriasData.ecommerce_uspActualizarCategorias(item);
                                    break;
                                case Operation.UpdateFoto:
                                    oCategoriasData.ecommerce_uspActualizarCatalogoPortadaFoto(item);
                                    break;
                                case Operation.Delete:
                                    oCategoriasData.ecommerce_uspEliminarCategorias(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oResp.Success = true;
                        oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });

                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oResp.Success = false;
                        oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oResp;
        }
       
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
