using BotComers.ViewModels;
using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository
{
    public class CategoriasRepository : IDisposable
    {
        public List<CategoriasProductosViewModel> ecommerce_uspListarCategorias(int CodigoUnidadNegocio, int CodigoSede, int CodigoMenuSuperior)
        {
            List<CategoriasProductosViewModel> lista = null;

            ReqFilterCategoriasDTO oReq = new ReqFilterCategoriasDTO()
            {
                Item = new CategoriasDTO()
                {
                    CodigoUnidadNegocio = CodigoUnidadNegocio,
                    CodigoSede = CodigoSede,
                    CodigoMenuSuperior = CodigoMenuSuperior
                },
                FilterCase = filterCaseCategorias.ecommerce_uspListarCategorias_Edit,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCategoriasDTO oResp = null;

            using (CategoriasLogic oCategoriasLogic = new CategoriasLogic())
            {
                oResp = oCategoriasLogic.CategoriasGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CategoriasProductosViewModel>();
                foreach (CategoriasDTO item in oResp.List)
                {
                    lista.Add(new CategoriasProductosViewModel()
                    {
                        CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                        CodigoSede = item.CodigoSede,
                        CodigoMenu = item.CodigoMenu,
                        CodigoMenuSuperior = item.CodigoMenuSuperior,
                        Descripcion = item.Descripcion,
                        UrlUbicacion = item.UrlUbicacion,
                        UrlImagen = item.UrlImagen,
                        CodigoImagenPortada = item.CodigoImagenPortada,
                        Tipo = item.Tipo,
                        Orden = item.Orden,
                        Estado = item.Estado
                    });
                }
            }

            return lista;

        }

        public CategoriasProductosViewModel ecommerce_uspBuscarCategorias(int CodigoUnidadNegocio, int CodigoSede, int CodigoMenuSuperior, int CodigoMenu)
        {
            CategoriasProductosViewModel oItemViewModel = null;

            CategoriasDTO oCategoriasDTO = new CategoriasDTO();
            oCategoriasDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oCategoriasDTO.CodigoSede = CodigoSede;
            oCategoriasDTO.CodigoMenu = CodigoMenu;
            oCategoriasDTO.CodigoMenuSuperior = CodigoMenuSuperior;

            ReqFilterCategoriasDTO oReq = new ReqFilterCategoriasDTO()
            {
                FilterCase = filterCaseCategorias.ecommerce_uspBuscarCategorias,
                Item = oCategoriasDTO,
                User = "admin"
            };
            RespItemCategoriasDTO oResp = null;
            using (CategoriasLogic oCategoriasLogic = new CategoriasLogic())
            {
                oResp = oCategoriasLogic.CategoriasGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new CategoriasProductosViewModel();
                oItemViewModel.CodigoUnidadNegocio = oResp.Item.CodigoUnidadNegocio;
                oItemViewModel.CodigoSede = oResp.Item.CodigoSede;
                oItemViewModel.CodigoMenu = oResp.Item.CodigoMenu;
                oItemViewModel.CodigoMenuSuperior = oResp.Item.CodigoMenuSuperior;
                oItemViewModel.Descripcion = oResp.Item.Descripcion;
                oItemViewModel.UrlUbicacion = oResp.Item.UrlUbicacion;
                oItemViewModel.UrlImagen = oResp.Item.UrlImagen;
                oItemViewModel.Tipo = oResp.Item.Tipo;
                oItemViewModel.Orden = oResp.Item.Orden;
                oItemViewModel.Estado = oResp.Item.Estado;

            }

            return oItemViewModel;

        }

        public CategoriasProductosViewModel ecommerce_uspBuscarCategoriasTiendaVirtual(int CodigoUnidadNegocio, int CodigoSede, string CodigoImagenPortada)
        {
            CategoriasProductosViewModel oItemViewModel = null;

            CategoriasDTO oCategoriasDTO = new CategoriasDTO();
            oCategoriasDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oCategoriasDTO.CodigoSede = CodigoSede;
            oCategoriasDTO.CodigoImagenPortada = CodigoImagenPortada;

            ReqFilterCategoriasDTO oReq = new ReqFilterCategoriasDTO()
            {
                FilterCase = filterCaseCategorias.ecommerce_uspBuscarCategoriasTiendaVirutal,
                Item = oCategoriasDTO,
                User = "appsfit"
            };
            RespItemCategoriasDTO oResp = null;
            using (CategoriasLogic oCategoriasLogic = new CategoriasLogic())
            {
                oResp = oCategoriasLogic.CategoriasGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new CategoriasProductosViewModel();
                oItemViewModel.CodigoUnidadNegocio = oResp.Item.CodigoUnidadNegocio;
                oItemViewModel.CodigoSede = oResp.Item.CodigoSede;
                oItemViewModel.CodigoMenu = oResp.Item.CodigoMenu;
                oItemViewModel.CodigoMenuSuperior = oResp.Item.CodigoMenuSuperior;
                oItemViewModel.Descripcion = oResp.Item.Descripcion;
                oItemViewModel.UrlUbicacion = oResp.Item.UrlUbicacion;
                oItemViewModel.UrlImagen = oResp.Item.UrlImagen;
                oItemViewModel.Tipo = oResp.Item.Tipo;
                oItemViewModel.Orden = oResp.Item.Orden;
                oItemViewModel.Estado = oResp.Item.Estado;

                oItemViewModel.listaItemsVenta = oResp.Item.listaItemsVenta;
            }

            return oItemViewModel;

        }


        public string ecommerce_uspRegistrarCategorias(CategoriasProductosViewModel oItem)
        {
            string mensaje = string.Empty;

            List<CategoriasDTO> list = new List<CategoriasDTO>();

            list.Add(new CategoriasDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoMenu = oItem.CodigoMenu,
                CodigoMenuSuperior = oItem.CodigoMenuSuperior,
                Descripcion = oItem.Descripcion,
                UrlUbicacion = oItem.UrlUbicacion == null ? string.Empty : oItem.UrlUbicacion,
                UrlImagen = oItem.UrlImagen == null ? string.Empty : oItem.UrlImagen,
                Tipo = string.Empty,//oItem.Tipo,
                Orden = oItem.Orden,
                Estado = oItem.Estado,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = oItem.Accion == "N" ? Operation.Create : Operation.Update,
            });

            ReqCategoriasDTO oReq = new ReqCategoriasDTO()
            {
                List = list,
                User = "admin"
            };
            RespCategoriasDTO oResp = null;
            using (CategoriasLogic oCategoriasLogic = new CategoriasLogic())
            {
                oResp = oCategoriasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }

            return mensaje;
        }

        public string ecommerce_uspActualizarCatalogoPortadaFoto(CategoriasProductosViewModel oItem)
        {
            string mensaje = string.Empty;

            List<CategoriasDTO> list = new List<CategoriasDTO>();

            list.Add(new CategoriasDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoMenu = oItem.CodigoMenu,
                UrlImagen = oItem.UrlImagen == null ? string.Empty : oItem.UrlImagen,
                CodigoImagenPortada = oItem.CodigoImagenPortada,
                Operation = Operation.UpdateFoto
            });

            ReqCategoriasDTO oReq = new ReqCategoriasDTO()
            {
                List = list,
                User = "appfit"
            };
            RespCategoriasDTO oResp = null;
            using (CategoriasLogic oCategoriasLogic = new CategoriasLogic())
            {
                oResp = oCategoriasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }

            return mensaje;
        }

        public string ecommerce_uspEliminarCategorias(CategoriasProductosViewModel oItem)
        {
            string mensaje = string.Empty;

            List<CategoriasDTO> list = new List<CategoriasDTO>();

            list.Add(new CategoriasDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoMenu = oItem.CodigoMenu,
                CodigoMenuSuperior = oItem.CodigoMenuSuperior,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = Operation.Delete,
            });

            ReqCategoriasDTO oReq = new ReqCategoriasDTO()
            {
                List = list,
                User = "admin"
            };
            RespCategoriasDTO oResp = null;
            using (CategoriasLogic oCategoriasLogic = new CategoriasLogic())
            {
                oResp = oCategoriasLogic.ExecuteTransac(oReq);
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