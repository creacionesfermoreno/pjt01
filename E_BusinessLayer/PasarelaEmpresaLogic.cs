using E_DataLayer;
using E_DataLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace E_BusinessLayer.Gimnasio
{
    public class PasarelaEmpresaLogic : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        private PasarelaEmpresaData oPasarelaEmpresaData = null;

        public PasarelaEmpresaLogic()
        {
            oPasarelaEmpresaData = new PasarelaEmpresaData();
        }


        public RespListPasarelaEmpresaDTO GetList(ReqFilterPasarelaEmpresaDTO oReqFilterPasarelaEmpresaDTO)
        {
            RespListPasarelaEmpresaDTO oRespListPasarelaEmpresaDTO = new RespListPasarelaEmpresaDTO();

            oRespListPasarelaEmpresaDTO.List = new List<PasarelaEmpresaDTO>();
            oRespListPasarelaEmpresaDTO.User = oReqFilterPasarelaEmpresaDTO.User;
            oRespListPasarelaEmpresaDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterPasarelaEmpresaDTO.User))
            {
                oRespListPasarelaEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterPasarelaEmpresaDTO.Paging == null)
            {
                oRespListPasarelaEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
            if (oRespListPasarelaEmpresaDTO.MessageList.Count == 0)
            {
                try
                {
                    if (!oReqFilterPasarelaEmpresaDTO.Paging.All && oReqFilterPasarelaEmpresaDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterPasarelaEmpresaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<PasarelaEmpresaDTO> NotificacionDTOList = new List<PasarelaEmpresaDTO>();

                    switch (oReqFilterPasarelaEmpresaDTO.FilterCase)
                    {
                        case FilterCasePasarelaEmpresa.List:
                            NotificacionDTOList = oPasarelaEmpresaData.List(oReqFilterPasarelaEmpresaDTO.Item);
                            break; 
                        
                        case FilterCasePasarelaEmpresa.ListApi:
                            NotificacionDTOList = oPasarelaEmpresaData.ListPasarelaByEM(oReqFilterPasarelaEmpresaDTO.Item);
                            break;                      
                    }

                    oRespListPasarelaEmpresaDTO.List = NotificacionDTOList;
                    oRespListPasarelaEmpresaDTO.Success = true;
                }
                catch (Exception ex)
                {
                    oRespListPasarelaEmpresaDTO.Success = false;
                    oRespListPasarelaEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }
            return oRespListPasarelaEmpresaDTO;
        }

        public RespItemPasarelaEmpresaDTO GetItem(ReqFilterPasarelaEmpresaDTO oReqFilterPasarelaEmpresaDTO)
        {
            RespItemPasarelaEmpresaDTO oRespItemNotificacionDTO = new RespItemPasarelaEmpresaDTO();

            oRespItemNotificacionDTO.Success = false;
            oRespItemNotificacionDTO.Item = null;
            oRespItemNotificacionDTO.User = oReqFilterPasarelaEmpresaDTO.User;
            oRespItemNotificacionDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterPasarelaEmpresaDTO.User))
            {
                oRespItemNotificacionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemNotificacionDTO.MessageList.Count == 0)
            {
                PasarelaEmpresaDTO oNotificacionDTO = null;
                try
                {
                    switch (oReqFilterPasarelaEmpresaDTO.FilterCase)
                    {
                        case FilterCasePasarelaEmpresa.SearchByCode:
                            {
                                oNotificacionDTO = new PasarelaEmpresaDTO();
                                oNotificacionDTO = oPasarelaEmpresaData.SearchByCode(oReqFilterPasarelaEmpresaDTO.Item);
                            }
                            break;   
                        case FilterCasePasarelaEmpresa.ListActive:
                            {
                                oNotificacionDTO = new PasarelaEmpresaDTO();
                                oNotificacionDTO = oPasarelaEmpresaData.ListPasarelaEMActive(oReqFilterPasarelaEmpresaDTO.Item);
                            }
                            break; 
                        
                        case FilterCasePasarelaEmpresa.SearchCodeApi:
                            {
                                oNotificacionDTO = new PasarelaEmpresaDTO();
                                oNotificacionDTO = oPasarelaEmpresaData.GetItemPasarelaByCode(oReqFilterPasarelaEmpresaDTO.Item);
                            }
                            break;

                        default:
                            {
                                oNotificacionDTO = new PasarelaEmpresaDTO();
                            }
                            break;
                    }

                    oRespItemNotificacionDTO.Item = new PasarelaEmpresaDTO();
                    oRespItemNotificacionDTO.Item = oNotificacionDTO;
                    oRespItemNotificacionDTO.Success = true;
                    oRespItemNotificacionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });
                }
                catch (Exception ex)
                {
                    oRespItemNotificacionDTO.Success = false;
                    oRespItemNotificacionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }
            return oRespItemNotificacionDTO;
        }

        public RespPasarelaEmpresaDTO ExecuteTransac(ReqPasarelaEmpresaDTO oReqPasarelaEmpresaDTO)
        {
            RespPasarelaEmpresaDTO oRespPasarelaEmpresaDTO = new RespPasarelaEmpresaDTO();

            oRespPasarelaEmpresaDTO.MessageList = new List<Mensaje>();
            oRespPasarelaEmpresaDTO.User = oReqPasarelaEmpresaDTO.User;

            if (String.IsNullOrEmpty(oReqPasarelaEmpresaDTO.User))
            {
                oRespPasarelaEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespPasarelaEmpresaDTO.MessageList.Count == 0)
            {
                int Estado = 0;
                string code = "";
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (PasarelaEmpresaDTO item in oReqPasarelaEmpresaDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.RegisterPEmpresa:
                                    oPasarelaEmpresaData.Register(item);
                                    break;

                                case Operation.UpdatePEmpresa:
                                    oPasarelaEmpresaData.Update(item);
                                    break;

                                case Operation.DestroyPEmpresa:
                                    oPasarelaEmpresaData.Destroy(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespPasarelaEmpresaDTO.Success = true;
                        oRespPasarelaEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Estado,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion,
                            Code = code,
                        });
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespPasarelaEmpresaDTO.Success = false;
                        oRespPasarelaEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }
            }

            return oRespPasarelaEmpresaDTO;
        }

    }
}
