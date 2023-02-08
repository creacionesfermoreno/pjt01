using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Corporativo;
using E_DataModel.Corporativo;
using E_DataModel.Common;

namespace E_BusinessLayer.Corporativo
{
    public class EmpresaLogic : IDisposable
    {

        EmpresaData oEmpresaData = null;
        public EmpresaLogic()
        {
            oEmpresaData = new EmpresaData();
        }

        public RespListEmpresaDTO EmpresaGetList(ReqFilterEmpresaDTO oReqFilterEmpresaDTO)
        {

            RespListEmpresaDTO oRespListEmpresaDTO = new RespListEmpresaDTO();

            oRespListEmpresaDTO.List = new List<EmpresaDTO>();
            oRespListEmpresaDTO.User = oReqFilterEmpresaDTO.User;
            oRespListEmpresaDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterEmpresaDTO.User))
            {
                oRespListEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Categoria no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterEmpresaDTO.Paging == null)
            {
                oRespListEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
            

            if (oRespListEmpresaDTO.MessageList.Count == 0)
            {

                try
                {

                    if (!oReqFilterEmpresaDTO.Paging.All && oReqFilterEmpresaDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterEmpresaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<EmpresaDTO> CategoriaDTOList = new List<EmpresaDTO>();

                    switch (oReqFilterEmpresaDTO.FilterCase)
                    {
                        case filterCaseEmpresa.ecommerce_uspListarEmpresas_Paginacion:

                            if (!oReqFilterEmpresaDTO.Paging.All && oReqFilterEmpresaDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterEmpresaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oEmpresaData.ecommerce_uspListarEmpresas_Paginacion(oReqFilterEmpresaDTO.Paging);
                            break;
                        case filterCaseEmpresa.ecommerce_uspObtenerEmpresaPorDominio:
                            {
                                CategoriaDTOList = oEmpresaData.ecommerce_uspObtenerEmpresaPorDominio(oReqFilterEmpresaDTO.Item);
                            }
                            break;            
                        case filterCaseEmpresa.ecommerce_uspObtenerEmpresaPorDominio_AppFitness:
                            {
                                CategoriaDTOList = oEmpresaData.ecommerce_uspObtenerEmpresaPorDominio_AppFitness(oReqFilterEmpresaDTO.Item);
                            }
                            break;       
                        case filterCaseEmpresa.appsfit_uspAspNetUsersCentroFit_Listar:
                            {
                                CategoriaDTOList = oEmpresaData.appsfit_uspAspNetUsersCentroFit_Listar(oReqFilterEmpresaDTO.Item);
                            }
                            break;                    
                        default:
                            {
                               // CategoriaDTOList = oEmpresaData.uspListarSocios_PorVendedor_Paginacion(oReqFilterEmpresaDTO.Paging);
                            }
                            break;
                    }

                    oRespListEmpresaDTO.List = CategoriaDTOList;
                    oRespListEmpresaDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListEmpresaDTO.Success = false;
                    oRespListEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespListEmpresaDTO;

        }

        public RespItemEmpresaDTO EmpresaGetItem(ReqFilterEmpresaDTO oReqFilter)
        {
            RespItemEmpresaDTO oRespItem = new RespItemEmpresaDTO();

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
                EmpresaDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseEmpresa.ecommerce_uspBuscarEmpresas:
                            {
                                oItem = new EmpresaDTO();
                                oItem = oEmpresaData.ecommerce_uspBuscarEmpresas(oReqFilter.Item);
                            }
                            break;
                     
                        default:
                            {
                                oItem = new EmpresaDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new EmpresaDTO();
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

        public RespEmpresaDTO ExecuteTransac(ReqEmpresaDTO oReqEmpresaDTO)
        {
            RespEmpresaDTO oRespEmpresaDTO = new RespEmpresaDTO();

            oRespEmpresaDTO.MessageList = new List<Mensaje>();
            oRespEmpresaDTO.User = oReqEmpresaDTO.User;

            if (String.IsNullOrEmpty(oReqEmpresaDTO.User))
            {
                oRespEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Emnpresa no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespEmpresaDTO.MessageList.Count == 0)
            {
                string ID = string.Empty;
                int Validador = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (EmpresaDTO item in oReqEmpresaDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    ID = oEmpresaData.RegistrarEmpresa(item);
                                    break;
                                case Operation.Update:
                                    oEmpresaData.ActualizarEmpresa(item);
                                    break;
                                case Operation.appsfit_uspAspNetUsersCentroFit_AgregarFavorito:
                                    Validador = oEmpresaData.appsfit_uspAspNetUsersCentroFit_AgregarFavorito(item);
                                    break;
                                case Operation.Delete:
                                    //oEmpresaData.Eliminar(item);
                                    break;
                            }
                        }
                        if (ID != String.Empty)
                        {
                            tx.Complete();
                            oRespEmpresaDTO.Success = true;
                            oRespEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                            {
                                Codigo = 100,
                                Detalle = ID,
                                Tipo = TipoMensaje.Informacion
                            });

                        }
                        else
                        {
                            tx.Complete();
                            oRespEmpresaDTO.Success = true;
                            oRespEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                            {
                                Codigo = Validador,
                                Detalle = String.Empty,
                                Tipo = TipoMensaje.Informacion
                            });
                        }

                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespEmpresaDTO.Success = false;
                        oRespEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespEmpresaDTO;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
