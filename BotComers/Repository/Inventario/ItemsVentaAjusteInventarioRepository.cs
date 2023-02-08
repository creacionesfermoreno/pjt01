using BotComers.ViewModels.Inventario;
using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.Inventario
{
    public class ItemsVentaAjusteInventarioRepository : IDisposable
    {
        public List<ItemsVentaAjusteInventarioViewModel> ecommerce_uspListarItemsVentaAjusteInventario_Paginacion(ItemsVentaAjusteInventarioViewModel oItem)
        {
            List<ItemsVentaAjusteInventarioViewModel> lista = null;

            ReqFilterItemsVentaAjusteInventarioDTO oReq = new ReqFilterItemsVentaAjusteInventarioDTO()
            {
                Item = new ItemsVentaAjusteInventarioDTO()
                {
                    CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                    CodigoSede = oItem.CodigoSede,
                    CodigoItemsVentaAjusteInventario = oItem.CodigoItemsVentaAjusteInventario,
                    DesAlmacen = oItem.DesAlmacen,
                    FechaAjuste = oItem.FechaAjuste,
                    b_FechaAjusteInicio = oItem.b_FechaAjusteInicio,
                    b_FechaAjusteFin = oItem.b_FechaAjusteFin,
                    Observaciones = oItem.Observaciones
                },
                FilterCase = filterCaseItemsVentaAjusteInventario.ecommerce_uspListarItemsVentaAjusteInventario_Paginacion,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(oItem.PageNumber),
                    PageRecords = 0
                }
            };

            RespListItemsVentaAjusteInventarioDTO oResp = null;

            using (ItemsVentaAjusteInventarioLogic oLogic = new ItemsVentaAjusteInventarioLogic())
            {
                oResp = oLogic.ItemsVentaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ItemsVentaAjusteInventarioViewModel>();
                foreach (ItemsVentaAjusteInventarioDTO item in oResp.List)
                {
                    lista.Add(new ItemsVentaAjusteInventarioViewModel()
                    {
                        CodigoItemsVentaAjusteInventario = item.CodigoItemsVentaAjusteInventario,
                        DesFechaAjuste = item.FechaAjuste.ToString("dd/MM/yyyy hh:mm:ss"),
                        CodigoAlmacen = item.CodigoAlmacen,
                        DesAlmacen = item.DesAlmacen,
                        TotalAjuste = item.TotalAjuste,
                        Observaciones = item.Observaciones,
                        UsuarioCreacion = item.UsuarioCreacion
                    });
                }
            }

            return lista;
        }

        public string ecommerce_uspRegistrarItemsVenta(ItemsVentaAjusteInventarioViewModel oItem)
        {
            string mensaje = string.Empty;

            List<ItemsVentaAjusteInventarioDTO> list = new List<ItemsVentaAjusteInventarioDTO>();

            list.Add(new ItemsVentaAjusteInventarioDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoItemsVentaAjusteInventario = oItem.CodigoItemsVentaAjusteInventario,
                CodigoAlmacen = oItem.CodigoAlmacen,
                FechaAjuste = oItem.FechaAjuste,
                Observaciones = oItem.Observaciones == null ? string.Empty : oItem.Observaciones,
                TotalAjuste = oItem.TotalAjuste,
                Estado = oItem.Estado,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = oItem.Accion == "N" ? Operation.Create : Operation.Update,
                listaDetalle = oItem.listaDetalle
            });

            int CodigoUnidadNegocio = list[0].CodigoUnidadNegocio;
            int CodigoSede = list[0].CodigoSede;
            string UsuarioCreacion = list[0].UsuarioCreacion;

            list[0].listaDetalle = new List<ItemsVentaAjusteInventarioDetalleDTO>();
            for (int i = 0; i < oItem.listaDetalle.Count; i++)
            {
                list[0].listaDetalle.Add(new ItemsVentaAjusteInventarioDetalleDTO()
                {
                    CodigoUnidadNegocio = CodigoUnidadNegocio,
                    CodigoSede = CodigoSede,
                    CodigoItemsVentaAjusteInventario = oItem.listaDetalle[i].CodigoItemsVentaAjusteInventario,
                    CodigoItemsVentaAjusteInventarioDetalle = oItem.listaDetalle[i].CodigoItemsVentaAjusteInventarioDetalle,
                    CodigoItemVenta = oItem.listaDetalle[i].CodigoItemVenta,
                    CantidadActual = oItem.listaDetalle[i].CantidadActual,
                    CodigoTipoAjuste = oItem.listaDetalle[i].CodigoTipoAjuste,
                    CantidadAjuste = oItem.listaDetalle[i].CantidadAjuste,
                    CantidadFinal = oItem.listaDetalle[i].CantidadFinal,
                    CostoUnidad = oItem.listaDetalle[i].CostoUnidad,
                    TotalAjuste = oItem.listaDetalle[i].TotalAjuste,
                    Estado = 1,
                    UsuarioCreacion = UsuarioCreacion
                });
            }


            ReqItemsVentaAjusteInventarioDTO oReq = new ReqItemsVentaAjusteInventarioDTO()
            {
                List = list,
                User = "admin"
            };
            RespItemsVentaAjusteInventarioDTO oResp = null;
            using (ItemsVentaAjusteInventarioLogic oItemsVentaAjusteInventarioLogic = new ItemsVentaAjusteInventarioLogic())
            {
                oResp = oItemsVentaAjusteInventarioLogic.ExecuteTransac(oReq);
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