using E_DataLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace E_BusinessLayer.Gimnasio
{
    public class SalaLogic:IDisposable
    {
        SalaData oSalaData = null;
        public SalaLogic()
        {
            oSalaData = new SalaData();
        }

        public RespListSalaDTO SalaGetList(ReqFilterSalaDTO oReqFilterSalaDTO)
        {

            RespListSalaDTO oRespListSalaDTO = new RespListSalaDTO();

            oRespListSalaDTO.List = new List<SalaDTO>();
            oRespListSalaDTO.User = oReqFilterSalaDTO.User;
            oRespListSalaDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterSalaDTO.User))
            {
                oRespListSalaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de SalaHorario no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterSalaDTO.Paging == null)
            {
                oRespListSalaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespListSalaDTO.MessageList.Count == 0)
            {

                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterSalaDTO.Paging.All && oReqFilterSalaDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterSalaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<SalaDTO> SalaDTOList = new List<SalaDTO>();

                    switch (oReqFilterSalaDTO.FilterCase)
                    {
                        default:
                            {
                                SalaDTOList = oSalaData.Listar(oReqFilterSalaDTO.Item);
                            }
                            break;
                    }

                    oRespListSalaDTO.List = SalaDTOList;
                    oRespListSalaDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListSalaDTO.Success = false;
                    oRespListSalaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespListSalaDTO;

        }

        //-------------------------------------------------------------------
        //Nombre:	SalaHorarioGetItem
        //Objetivo: Retorna un registro de tipo SalaHorarioDTO
        //Valores Prueba:
        //-------------------------------------------------------------------
        public RespItemSalaDTO SalaGetItem(ReqFilterSalaDTO oReqFilterSalaDTO)
        {
            RespItemSalaDTO oRespItemSalaDTO = new RespItemSalaDTO();

            oRespItemSalaDTO.Success = false;
            oRespItemSalaDTO.Item = null;
            oRespItemSalaDTO.User = oReqFilterSalaDTO.User;
            oRespItemSalaDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterSalaDTO.User))
            {
                oRespItemSalaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de SalaHorario no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemSalaDTO.MessageList.Count == 0)
            {
                SalaDTO oSalaDTO = null;
                try
                {
                    switch (oReqFilterSalaDTO.FilterCase)
                    {

                        case filterCaseSala.PorCodigo:
                            {
                                oSalaDTO = new SalaDTO();
                                oSalaDTO = oSalaData.BuscarPorCodigoSala(oReqFilterSalaDTO.Item);
                            }
                            break;
                        default:
                            {
                                oSalaDTO = new SalaDTO();
                            }
                            break;
                    }

                    oRespItemSalaDTO.Item = new SalaDTO();
                    oRespItemSalaDTO.Item = oSalaDTO;
                    oRespItemSalaDTO.Success = true;
                    oRespItemSalaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemSalaDTO.Success = false;
                    oRespItemSalaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemSalaDTO;
        }

        //-------------------------------------------------------------------
        //Nombre:	ExecuteTransac
        //Objetivo: Almacena el registro de un objeto de tipo SalaHorarioDTO
        //Valores Prueba:
        //-------------------------------------------------------------------
        public RespSalaDTO ExecuteTransac(ReqSalaDTO oReqSalaDTO)
        {
            RespSalaDTO oRespSalaDTO = new RespSalaDTO();

            oRespSalaDTO.MessageList = new List<Mensaje>();
            oRespSalaDTO.User = oReqSalaDTO.User;

            if (String.IsNullOrEmpty(oReqSalaDTO.User))
            {
                oRespSalaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de SalaHorario no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespSalaDTO.MessageList.Count == 0)
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (SalaDTO item in oReqSalaDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                   item.Codigo= oSalaData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oSalaData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oSalaData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespSalaDTO.Success = true;
                        
                        oRespSalaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });

                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespSalaDTO.Success = false;
                        oRespSalaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespSalaDTO;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
