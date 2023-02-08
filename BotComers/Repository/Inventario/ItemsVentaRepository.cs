using BotComers.ViewModels;
using BotComers.ViewModels.Inventario;
using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.Inventario
{
    public class ItemsVentaRepository : IDisposable
    {

        public List<ItemsVentaViewModel> ecommerce_uspBuscadorItemsVentaInventariable(ItemsVentaViewModel request)
        {
            List<ItemsVentaViewModel> lista = new List<ItemsVentaViewModel>();

            ReqFilterItemsVentaDTO oReq = new ReqFilterItemsVentaDTO()
            {
                Item = new ItemsVentaDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoAlmacen = request.CodigoAlmacen,
                    Nombre = request.Nombre
                },
                FilterCase = filterCaseItemsVenta.ecommerce_uspBuscadorItemsVentaInventariable,
                User = "Admin",
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
                        CodigoItemVenta = item.CodigoItemVenta,
                        Nombre = item.Nombre,
                        PrecioVenta = item.PrecioVenta,
                        PrecioTotal = item.PrecioTotal,
                        Referencia = item.Referencia,
                        Descripcion = item.Descripcion,
                        CodigoTipoImpuesto = item.CodigoTipoImpuesto,
                        d_CantidadActual = item.d_CantidadActual,
                        d_CostoUnidad = item.d_CostoUnidad,
                        UrlImagen = item.UrlImagen,
                        CodigoImagen = item.CodigoImagen
                    });
                }
            }
            return lista;
        }

        public List<ItemsVentaViewModel> ecommerce_uspListarItemsVenta_Paginacion(ItemsVentaViewModel request, int PageNumber)
        {
            List<ItemsVentaViewModel> lista = new List<ItemsVentaViewModel>();

            ReqFilterItemsVentaDTO oReq = new ReqFilterItemsVentaDTO()
            {
                Item = new ItemsVentaDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    Nombre = request.Nombre,
                    UsuarioCreacion = request.UsuarioCreacion
                },
                FilterCase = filterCaseItemsVenta.ecommerce_uspListarItemsVenta_Paginacion,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
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
                        CodigoItemVenta = item.CodigoItemVenta,
                        Nombre = item.Nombre,
                        PrecioVenta = item.PrecioVenta,
                        PrecioTotal = item.PrecioTotal,
                        Referencia = item.Referencia,
                        Descripcion = item.Descripcion,
                        UrlImagen = item.UrlImagen
                    });
                }
            }
            return lista;
        }

        public List<ItemsVentaViewModel> ecommerce_uspListarValorInventario_Paginaciones(ItemsVentaViewModel request, int PageNumber)
        {
            List<ItemsVentaViewModel> lista = new List<ItemsVentaViewModel>();

            ReqFilterItemsVentaDTO oReq = new ReqFilterItemsVentaDTO()
            {
                Item = new ItemsVentaDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoAlmacen = request.CodigoAlmacen,
                    Nombre = request.Nombre,
                    UsuarioCreacion = request.UsuarioCreacion
                },
                FilterCase = filterCaseItemsVenta.ecommerce_uspListarValorInventario_Paginaciones,
                User = "appsift",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
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
                        PrecioVenta = item.PrecioVenta,
                        d_CantidadActual = item.d_CantidadActual,
                        d_CostoPromedio = item.d_CostoPromedio,
                        d_CostoTotal = item.d_CostoTotal,
                        CodigoUnidadMedida = item.CodigoUnidadMedida,
                        Estado = item.Estado,
                        UrlImagen = item.UrlImagen
                    });
                }
            }
            return lista;
        }

        public List<ItemsVentaViewModel> ecommerce_uspListarItemsVenta_PorCategoriaPaginacion(ItemsVentaViewModel request, int PageNumber)
        {
            List<ItemsVentaViewModel> lista = new List<ItemsVentaViewModel>();

            ReqFilterItemsVentaDTO oReq = new ReqFilterItemsVentaDTO()
            {
                Item = new ItemsVentaDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoImagen = request.CodigoImagen
                },
                FilterCase = filterCaseItemsVenta.ecommerce_uspListarItemsVenta_PorCategoriaPaginacion,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
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
                        CodigoItemVenta = item.CodigoItemVenta,
                        Nombre = item.Nombre,
                        PrecioVenta = item.PrecioVenta,
                        UrlImagen = item.UrlImagen,
                        CodigoImagen = item.CodigoImagen,
                        Referencia = item.Referencia,
                        Descripcion = item.Descripcion,
                        Estado = item.Estado
                    });
                }
            }
            return lista;
        }

        public List<ItemsVentaViewModel> ecommerce_uspListarValorInventario_PuntoVenta(ItemsVentaViewModel request)
        {
            List<ItemsVentaViewModel> lista = new List<ItemsVentaViewModel>();

            ReqFilterItemsVentaDTO oReq = new ReqFilterItemsVentaDTO()
            {
                Item = new ItemsVentaDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoAlmacen = request.CodigoAlmacen,
                    UsuarioCreacion = request.UsuarioCreacion,
                    Nombre = request.Nombre
                },
                FilterCase = filterCaseItemsVenta.ecommerce_uspListarValorInventario_PuntoVenta,
                User = "Admin",
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
                        UrlImagen = item.UrlImagen,
                        PrecioVenta = item.PrecioVenta,
                        Referencia = item.Referencia,
                        Descripcion = item.Descripcion,
                        d_CantidadActual = item.d_CantidadActual,
                        CodigoUnidadMedida = item.CodigoUnidadMedida,
                        Estado = item.Estado,
                        VisualizarTiendaVirtual = item.VisualizarTiendaVirtual,
                        CodigoImagen = item.CodigoImagen
                    });
                }
            }
            return lista;
        }

        public ItemsVentaViewModel ecommerce_uspBuscarItemsVentas(int CodigoUnidadNegocio, int CodigoSede, int CodigoItemVenta)
        {
            ItemsVentaViewModel oItemViewModel = null;

            ItemsVentaDTO oItemsVentaDTO = new ItemsVentaDTO();
            oItemsVentaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oItemsVentaDTO.CodigoSede = CodigoSede;
            oItemsVentaDTO.CodigoItemVenta = CodigoItemVenta;

            ReqFilterItemsVentaDTO oReq = new ReqFilterItemsVentaDTO()
            {
                FilterCase = filterCaseItemsVenta.ecommerce_uspBuscarItemsVentas,
                Item = oItemsVentaDTO,
                User = "admin"
            };
            RespItemItemsVentaDTO oResp = null;
            using (ItemsVentaLogic oItemsVentaLogic = new ItemsVentaLogic())
            {
                oResp = oItemsVentaLogic.ItemsVentaGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new ItemsVentaViewModel();

                oItemViewModel.CodigoUnidadNegocio = oResp.Item.CodigoUnidadNegocio;
                oItemViewModel.CodigoSede = oResp.Item.CodigoSede;
                oItemViewModel.CodigoItemVenta = oResp.Item.CodigoItemVenta;
                oItemViewModel.Nombre = oResp.Item.Nombre;
                oItemViewModel.PrecioVenta = oResp.Item.PrecioVenta;
                oItemViewModel.PrecioTotal = oResp.Item.PrecioTotal;
                oItemViewModel.CodigoTipoImpuesto = oResp.Item.CodigoTipoImpuesto;
                oItemViewModel.CodigoUnidadMedida = oResp.Item.CodigoUnidadMedida;
                oItemViewModel.CodigoTipoItem = oResp.Item.CodigoTipoItem;
                oItemViewModel.CodigoAlmacen = oResp.Item.CodigoAlmacen;
                oItemViewModel.ItemInventariable = oResp.Item.ItemInventariable;
                oItemViewModel.Referencia = oResp.Item.Referencia;
                oItemViewModel.Descripcion = oResp.Item.Descripcion;
                oItemViewModel.CodigoCategoriaItem = oResp.Item.CodigoCategoriaItem;
                oItemViewModel.CodigoProductoSUNAT = oResp.Item.CodigoProductoSUNAT;
                oItemViewModel.CodigoCuentaContable = oResp.Item.CodigoCuentaContable;
                oItemViewModel.UrlImagen = oResp.Item.UrlImagen == string.Empty ? "../Content/app/img/img0.jpg" : oResp.Item.UrlImagen;
                oItemViewModel.Estado = oResp.Item.Estado;
                oItemViewModel.VisualizarTiendaVirtual = oResp.Item.VisualizarTiendaVirtual;
                oItemViewModel.UsuarioCreacion = oResp.Item.UsuarioCreacion;
                oItemViewModel.CodigoImagen = oResp.Item.CodigoImagen;

                AlmacenesRepository almacenesRepository = new AlmacenesRepository();
                List<AlmacenesViewModel> listaAlmacenes = new List<AlmacenesViewModel>();
                listaAlmacenes = almacenesRepository.ecommerce_uspListarAlmacenes(CodigoUnidadNegocio, CodigoSede, 1);
                oItemViewModel.ListAlmacenes = new List<System.Web.Mvc.SelectListItem>();
                foreach (AlmacenesViewModel item in listaAlmacenes)
                {
                    oItemViewModel.ListAlmacenes.Add(new System.Web.Mvc.SelectListItem()
                    {
                        Value = item.CodigoAlmacen.ToString(),
                        Text = item.Descripcion
                    });
                }

                CategoriasRepository categoriasRepository = new CategoriasRepository();
                List<CategoriasProductosViewModel> listaCategorias = new List<CategoriasProductosViewModel>();
                listaCategorias = categoriasRepository.ecommerce_uspListarCategorias(CodigoUnidadNegocio, CodigoSede, 0);
                oItemViewModel.ListCategoriasDTO = new List<CategoriasDTO>();
                foreach (CategoriasProductosViewModel item in listaCategorias)
                {
                    oItemViewModel.ListCategoriasDTO.Add(new CategoriasDTO()
                    {
                        CodigoMenuSuperior = item.CodigoMenuSuperior,
                        CodigoMenu = item.CodigoMenu,
                        Descripcion = item.Descripcion
                    });
                }

                oItemViewModel.lista_ItemsVentaInventarioDTO = new List<ItemsVentaInventarioDTO>();
                foreach (ItemsVentaInventarioDTO item in oResp.Item.lista_ItemsVentaInventarioDTO)
                {
                    oItemViewModel.lista_ItemsVentaInventarioDTO.Add(new ItemsVentaInventarioDTO()
                    {
                        CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                        CodigoSede = item.CodigoSede,
                        CodigoItemVenta = item.CodigoItemVenta,
                        CodigoItemsVentaInventario = item.CodigoItemsVentaInventario,
                        CostoUnidad = item.CostoUnidad,
                        CodigoAlmacen = item.CodigoAlmacen,
                        DesAlmacen = item.DesAlmacen,
                        CantidadInicial = item.CantidadInicial,
                        CantidadMinima = item.CantidadMinima,
                        CantidadMaxima = item.CantidadMaxima,
                        UsuarioCreacion = item.UsuarioCreacion,
                        DescFechaCreacion = item.DescFechaCreacion
                    });
                }


            }

            return oItemViewModel;
        }


        public ItemsVentaViewModel ecommerce_uspBuscarItemsVentasTienda(ItemsVentaViewModel request)
        {
            ItemsVentaViewModel oItemViewModel = null;

            ItemsVentaDTO oItemsVentaDTO = new ItemsVentaDTO();
            oItemsVentaDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oItemsVentaDTO.CodigoSede = request.CodigoSede;
            oItemsVentaDTO.CodigoImagen = request.CodigoImagen;

            ReqFilterItemsVentaDTO oReq = new ReqFilterItemsVentaDTO()
            {
                FilterCase = filterCaseItemsVenta.ecommerce_uspBuscarItemsVentasTienda,
                Item = oItemsVentaDTO,
                User = "admin"
            };
            RespItemItemsVentaDTO oResp = null;
            using (ItemsVentaLogic oItemsVentaLogic = new ItemsVentaLogic())
            {
                oResp = oItemsVentaLogic.ItemsVentaGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new ItemsVentaViewModel();

                oItemViewModel.CodigoUnidadNegocio = oResp.Item.CodigoUnidadNegocio;
                oItemViewModel.CodigoSede = oResp.Item.CodigoSede;
                oItemViewModel.CodigoItemVenta = oResp.Item.CodigoItemVenta;
                oItemViewModel.Nombre = oResp.Item.Nombre;
                oItemViewModel.PrecioVenta = oResp.Item.PrecioVenta;
                oItemViewModel.PrecioTotal = oResp.Item.PrecioTotal;

                oItemViewModel.ItemInventariable = oResp.Item.ItemInventariable;
                oItemViewModel.Referencia = oResp.Item.Referencia;
                oItemViewModel.Descripcion = oResp.Item.Descripcion;
                oItemViewModel.CodigoCategoriaItem = oResp.Item.CodigoCategoriaItem;

                oItemViewModel.UrlImagen = oResp.Item.UrlImagen;
                oItemViewModel.CodigoImagen = oResp.Item.CodigoImagen;
                oItemViewModel.d_CantidadActual = oResp.Item.d_CantidadActual;

            }

            return oItemViewModel;
        }


        public int ecommerce_uspRegistrarItemsVenta(ItemsVentaViewModel oItem)
        {
            int mensaje = 0;

            List<ItemsVentaDTO> list = new List<ItemsVentaDTO>();

            list.Add(new ItemsVentaDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoItemVenta = oItem.CodigoItemVenta,
                Nombre = oItem.Nombre,
                PrecioVenta = oItem.PrecioVenta,
                PrecioTotal = oItem.PrecioTotal,
                CodigoTipoImpuesto = oItem.CodigoTipoImpuesto,
                CodigoUnidadMedida = oItem.CodigoUnidadMedida,
                CodigoTipoItem = oItem.CodigoTipoItem,
                CodigoAlmacen = oItem.CodigoAlmacen,
                ItemInventariable = oItem.ItemInventariable,
                Referencia = oItem.Referencia == null ? string.Empty : oItem.Referencia,
                Descripcion = oItem.Descripcion == null ? string.Empty : oItem.Descripcion,
                CodigoCategoriaItem = oItem.CodigoCategoriaItem,
                CodigoProductoSUNAT = oItem.CodigoProductoSUNAT == null ? string.Empty : oItem.CodigoProductoSUNAT,
                CodigoCuentaContable = oItem.CodigoCuentaContable,
                UrlImagen = oItem.UrlImagen == null ? string.Empty : oItem.UrlImagen,
                Estado = oItem.Estado,
                VisualizarTiendaVirtual = oItem.VisualizarTiendaVirtual,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = oItem.Accion == "N" ? Operation.Create : Operation.Update,

            });

            if (list[0].Operation == Operation.Create && oItem.CodigoTipoItem == 1 && oItem.ItemInventariable == 1)
            {
                int CodigoUnidadNegocio = list[0].CodigoUnidadNegocio;
                int CodigoSede = list[0].CodigoSede;
                int CodigoUnidadMedida = list[0].CodigoUnidadMedida;
                Decimal CostoUnidad = oItem.lista_ItemsVentaInventarioDTO[0].CostoUnidad;
                string UsuarioCreacion = list[0].UsuarioCreacion;

                list[0].lista_ItemsVentaInventarioDTO = new List<ItemsVentaInventarioDTO>();
                for (int i = 0; i < oItem.lista_ItemsVentaInventarioDTO.Count; i++)
                {
                    list[0].lista_ItemsVentaInventarioDTO.Add(new ItemsVentaInventarioDTO()
                    {
                        CodigoItemsVentaInventario = 0,
                        CodigoUnidadNegocio = CodigoUnidadNegocio,
                        CodigoSede = CodigoSede,
                        CodigoItemVenta = 0,
                        CodigoUnidadMedida = CodigoUnidadMedida,
                        CostoUnidad = CostoUnidad,
                        CodigoAlmacen = oItem.lista_ItemsVentaInventarioDTO[i].CodigoAlmacen,
                        CantidadInicial = oItem.lista_ItemsVentaInventarioDTO[i].CantidadInicial,
                        CantidadMinima = oItem.lista_ItemsVentaInventarioDTO[i].CantidadMinima,
                        CantidadMaxima = oItem.lista_ItemsVentaInventarioDTO[i].CantidadMaxima,
                        UsuarioCreacion = UsuarioCreacion,
                        Estado = 1
                    });
                }

            }
            else if (list[0].Operation == Operation.Update && oItem.CodigoTipoItem == 1 && oItem.ItemInventariable == 1)
            {
                int CodigoUnidadNegocio = list[0].CodigoUnidadNegocio;
                int CodigoSede = list[0].CodigoSede;
                int CodigoItemVenta = list[0].CodigoItemVenta;
                int CodigoUnidadMedida = list[0].CodigoUnidadMedida;
                Decimal CostoUnidad = oItem.lista_ItemsVentaInventarioDTO[0].CostoUnidad;
                string UsuarioCreacion = list[0].UsuarioCreacion;

                list[0].lista_ItemsVentaInventarioDTO = new List<ItemsVentaInventarioDTO>();
                for (int i = 0; i < oItem.lista_ItemsVentaInventarioDTO.Count; i++)
                {
                    list[0].lista_ItemsVentaInventarioDTO.Add(new ItemsVentaInventarioDTO()
                    {
                        CodigoUnidadNegocio = CodigoUnidadNegocio,
                        CodigoSede = CodigoSede,
                        CodigoItemVenta = CodigoItemVenta,
                        CodigoItemsVentaInventario = oItem.lista_ItemsVentaInventarioDTO[i].CodigoItemsVentaInventario,
                        CodigoUnidadMedida = CodigoUnidadMedida,
                        CostoUnidad = CostoUnidad,
                        CodigoAlmacen = oItem.lista_ItemsVentaInventarioDTO[i].CodigoAlmacen,
                        CantidadInicial = oItem.lista_ItemsVentaInventarioDTO[i].CantidadInicial,
                        CantidadMinima = oItem.lista_ItemsVentaInventarioDTO[i].CantidadMinima,
                        CantidadMaxima = oItem.lista_ItemsVentaInventarioDTO[i].CantidadMaxima,
                        UsuarioCreacion = UsuarioCreacion,
                        Estado = 1
                    });
                }

            }

            ReqItemsVentaDTO oReq = new ReqItemsVentaDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespItemsVentaDTO oResp = null;
            using (ItemsVentaLogic oItemsVentaLogic = new ItemsVentaLogic())
            {
                oResp = oItemsVentaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;

            }

            return mensaje;
        }


        public ItemsVentaViewModel ecommerce_uspBuscarItemsVentasParaGuardarImagen(int CodigoUnidadNegocio, int CodigoSede, int CodigoItemVenta)
        {
            ItemsVentaViewModel oItemViewModel = null;

            ItemsVentaDTO oItemsVentaDTO = new ItemsVentaDTO();
            oItemsVentaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oItemsVentaDTO.CodigoSede = CodigoSede;
            oItemsVentaDTO.CodigoItemVenta = CodigoItemVenta;

            ReqFilterItemsVentaDTO oReq = new ReqFilterItemsVentaDTO()
            {
                FilterCase = filterCaseItemsVenta.ecommerce_uspBuscarItemsVentasParaGuardarFoto,
                Item = oItemsVentaDTO,
                User = "appsfit"
            };
            RespItemItemsVentaDTO oResp = null;
            using (ItemsVentaLogic oItemsVentaLogic = new ItemsVentaLogic())
            {
                oResp = oItemsVentaLogic.ItemsVentaGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new ItemsVentaViewModel();

                oItemViewModel.CodigoUnidadNegocio = oResp.Item.CodigoUnidadNegocio;
                oItemViewModel.CodigoSede = oResp.Item.CodigoSede;
                oItemViewModel.CodigoItemVenta = oResp.Item.CodigoItemVenta;
                oItemViewModel.UrlImagen = oResp.Item.UrlImagen;
                oItemViewModel.CodigoImagen = oResp.Item.CodigoImagen;
            }

            return oItemViewModel;
        }


        public string ecommerce_uspActualizarItemsVentaFoto(ItemsVentaViewModel oItem)
        {
            string mensaje = string.Empty;

            List<ItemsVentaDTO> list = new List<ItemsVentaDTO>();

            list.Add(new ItemsVentaDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoItemVenta = oItem.CodigoItemVenta,
                CodigoImagen = oItem.CodigoImagen,
                UrlImagen = oItem.UrlImagen,
                Operation = Operation.UpdateFoto
            });

            ReqItemsVentaDTO oReq = new ReqItemsVentaDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespItemsVentaDTO oResp = null;
            using (ItemsVentaLogic oItemsVentaLogic = new ItemsVentaLogic())
            {
                oResp = oItemsVentaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }

            return mensaje;
        }


        public string ecommerce_uspEliminarItemsVenta(ItemsVentaViewModel oItem)
        {
            string mensaje = string.Empty;

            List<ItemsVentaDTO> list = new List<ItemsVentaDTO>();

            list.Add(new ItemsVentaDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoItemVenta = oItem.CodigoItemVenta,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = Operation.Delete,
            });

            ReqItemsVentaDTO oReq = new ReqItemsVentaDTO()
            {
                List = list,
                User = "admin"
            };
            RespItemsVentaDTO oResp = null;
            using (ItemsVentaLogic oItemsVentaLogic = new ItemsVentaLogic())
            {
                oResp = oItemsVentaLogic.ExecuteTransac(oReq);
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