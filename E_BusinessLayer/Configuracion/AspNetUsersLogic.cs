using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Configuracion;
using E_DataModel.Configuracion;
using E_DataModel.Common;

namespace E_BusinessLayer.Configuracion
{
    public class AspNetUsersLogic : IDisposable
    {
        AspNetUsersData oAspNetUsersData = null;
        public AspNetUsersLogic()
        {
            oAspNetUsersData = new AspNetUsersData();
        }

        public RespItemAspNetUsersDTO AspNetUsersGetItem(ReqFilterAspNetUsersDTO oReqFilter)
        {
            RespItemAspNetUsersDTO oRespItem = new RespItemAspNetUsersDTO();

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
                AspNetUsersDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseAspNetUsers.ecommerce_AspNetUsers_ValidarUsuarioBusiness:
                            {
                                oItem = new AspNetUsersDTO();
                                oItem = oAspNetUsersData.ecommerce_AspNetUsers_ValidarUsuarioBusiness(oReqFilter.Item);

                            }
                            break;
                        case filterCaseAspNetUsers.ecommerce_AspNetUsers_ValidarUsuarioPersonaFit:
                            {
                                oItem = new AspNetUsersDTO();
                                oItem = oAspNetUsersData.ecommerce_AspNetUsers_ValidarUsuarioPersonaFit(oReqFilter.Item);

                            }
                            break;
                        case filterCaseAspNetUsers.ecommerce_AspNetUsers_ValidarUsuarioPersonaFit_AppFitness:
                            {
                                oItem = new AspNetUsersDTO();
                                oItem = oAspNetUsersData.ecommerce_AspNetUsers_ValidarUsuarioPersonaFit_AppFitness(oReqFilter.Item);

                            }
                            break;
                        case filterCaseAspNetUsers.ecommerce_AspNetUsers_Buscar:
                            {
                                oItem = new AspNetUsersDTO();
                                oItem = oAspNetUsersData.ecommerce_AspNetUsers_Buscar(oReqFilter.Item);

                            }
                            break;
                        case filterCaseAspNetUsers.ecommerce_uspRecuperarClave_AspNetUsers_AppFitness:
                            {
                                oItem = new AspNetUsersDTO();
                                oItem = oAspNetUsersData.ecommerce_uspRecuperarClave_AspNetUsers_AppFitness(oReqFilter.Item);

                            }
                            break;
                        default:
                            {
                                oItem = new AspNetUsersDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new AspNetUsersDTO();
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

        public RespListAspNetUsersDTO AspNetUsersGetList(ReqFilterAspNetUsersDTO oReqFilter)
        {

            RespListAspNetUsersDTO oRespList = new RespListAspNetUsersDTO();

            oRespList.List = new List<AspNetUsersDTO>();
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

                    List<AspNetUsersDTO> CategoriaDTOList = new List<AspNetUsersDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseAspNetUsers.ecommerce_uspListarAspNetUsers_Paginacion:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oAspNetUsersData.ecommerce_uspListarAspNetUsers_Paginacion(oReqFilter.Item, oReqFilter.Paging);
                            break;
                        default:
                            {
                                // CategoriaDTOList = oAspNetUsersData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
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
        public AspNetUsersDTO resetPass(AspNetUsersDTO aspNetUsersDTO)
        {
            var res = oAspNetUsersData.ecommerce_uspRecuperarClave_AspNetUsers_AppFitness(aspNetUsersDTO);
            return res;
        }
        
        public RespAspNetUsersDTO ExecuteTransac(ReqAspNetUsersDTO oReq)
        {
            RespAspNetUsersDTO oResp = new RespAspNetUsersDTO();

            oResp.MessageList = new List<Mensaje>();
            oResp.User = oReq.User;

            if (String.IsNullOrEmpty(oReq.User))
            {
                oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Usuario no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oResp.MessageList.Count == 0)
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    int validacionCambioClave = 0;
                    int LoginValidation = 0;
                    int TipoValidacion = 0;
                    string strValidacion = string.Empty;
                    try
                    {                       
                        foreach (AspNetUsersDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.ecommerce_uspRegistrar_AspNetUsers_AppFitness:
                                    TipoValidacion = 3;
                                    strValidacion = oAspNetUsersData.ecommerce_uspRegistrar_AspNetUsers_AppFitness(item);
                                    break;
                                case Operation.ecommerce_uspValidarCorreo_AspNetUsers_AppFitness:
                                    TipoValidacion = 0;
                                    oAspNetUsersData.ecommerce_uspValidarCorreo_AspNetUsers_AppFitness(item);
                                    break;
                                case Operation.ecommerce_uspRegistrar_AspNetUsersToken_AppFitness:
                                   
                                    oAspNetUsersData.ecommerce_uspRegistrar_AspNetUsersToken_AppFitness(item);
                                    break;
                                case Operation.Create:
                                    TipoValidacion = 1;
                                    LoginValidation = oAspNetUsersData.ecommerce_uspRegistrar_AspNetUsers(item);
                                    break;
                                case Operation.ecommerce_uspRegistrar_AspNetUsersTiendaVirtual:
                                    TipoValidacion = 1;
                                    LoginValidation = oAspNetUsersData.ecommerce_uspRegistrar_AspNetUsersTiendaVirtual(item);
                                    break;
                                case Operation.Update:
                                
                                    break;
                                case Operation.UpdateClave:
                                    TipoValidacion = 2;
                                    validacionCambioClave =  oAspNetUsersData.ecommerce_uspActualizarCambiarClave_AspNetUsers(item);                                    
                                    break;

                                case Operation.UpdatePass:
                                    TipoValidacion = 2;
                                    validacionCambioClave = oAspNetUsersData.ecommerce_uspCambiarClave_AspNetUsers_AppFitness(item);
                                    break;
                                case Operation.Delete:
                                 
                                    break;
                            }
                        }
                        tx.Complete();
                        oResp.Success = true;
                        if (TipoValidacion == 1)
                        {
                            oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                            {
                                Codigo = LoginValidation,
                                Detalle = "Proceso Grabado Correctamente.",
                                Tipo = TipoMensaje.Informacion
                            });
                        }
                        else if (TipoValidacion == 2)
                        {
                            oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                            {
                                Codigo = validacionCambioClave,
                                Detalle = "Proceso Grabado Correctamente.",
                                Tipo = TipoMensaje.Informacion
                            });

                        }
                        else if (TipoValidacion == 3)
                        {
                            oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                            {
                                Codigo = 100,
                                Detalle = strValidacion,
                                Tipo = TipoMensaje.Informacion
                            });

                        }
                        else
                        {
                            oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                            {
                                Codigo = 100,
                                Detalle = "Proceso Grabado Correctamente.",
                                Tipo = TipoMensaje.Informacion
                            });
                        }
                        
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
