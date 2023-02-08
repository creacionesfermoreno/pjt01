
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_DataModel.Common;


namespace E_BusinessLayer.Gimnasio
{
    public class AgendaPostVentaLogic : IDisposable
    {
        AgendaPostVentaData oAgendaPostVentaData = null;
        public AgendaPostVentaLogic()
        {
            oAgendaPostVentaData = new AgendaPostVentaData();
        }

        //-------------------------------------------------------------------
        //Nombre:	AgendaSeguimientoGetList
        //Objetivo: Retorna una colección de registros de tipo AgendaSeguimientoDTO
        //Valores Prueba:
        //Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //-------------------------------------------------------------------
        public RespListAgendaPostVentaDTO AgendaPostVentaGetList(ReqFilterAgendaPostVentaDTO oReqFilterAgendaPostVentaDTO)
        {
            RespListAgendaPostVentaDTO oRespListAgendaPostVentaDTO = new RespListAgendaPostVentaDTO();

            oRespListAgendaPostVentaDTO.List = new List<AgendaPostVentaDTO>();
            oRespListAgendaPostVentaDTO.User = oReqFilterAgendaPostVentaDTO.User;
            oRespListAgendaPostVentaDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterAgendaPostVentaDTO.User))
            {
                oRespListAgendaPostVentaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AgendaSeguimiento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterAgendaPostVentaDTO.Paging == null)
            {
                oRespListAgendaPostVentaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespListAgendaPostVentaDTO.MessageList.Count == 0)
            {

                try
                {
                    
                    List<AgendaPostVentaDTO> AgendaPostVentaDTOList = new List<AgendaPostVentaDTO>();

                    switch (oReqFilterAgendaPostVentaDTO.FilterCase)
                    {
                        case filterCaseAgendaPostVenta.uspListarAgendaPostVentaSeguimiento_Paginacion:
                            {
                                if (!oReqFilterAgendaPostVentaDTO.Paging.All && oReqFilterAgendaPostVentaDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterAgendaPostVentaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_Todos"]);
                                }
                                AgendaPostVentaDTOList = oAgendaPostVentaData.uspListarAgendaPostVentaSeguimiento_Paginacion(oReqFilterAgendaPostVentaDTO.Item, oReqFilterAgendaPostVentaDTO.Paging);
                            }
                            break;
                        case filterCaseAgendaPostVenta.uspListarCalificacionPostVenta:
                            {
                                AgendaPostVentaDTOList = oAgendaPostVentaData.uspListarCalificacionPostVenta();
                            }
                            break;
                        case filterCaseAgendaPostVenta.uspListarTipoAgendaPostVenta:
                            {
                                AgendaPostVentaDTOList = oAgendaPostVentaData.uspListarTipoAgendaPostVenta();
                            }
                            break;
                        case filterCaseAgendaPostVenta.uspListarAgendaPostVentaSeguimientoSocios_Mensajes:
                            {
                                AgendaPostVentaDTOList = oAgendaPostVentaData.uspListarAgendaPostVentaSeguimientoSocios_Mensajes(oReqFilterAgendaPostVentaDTO.Item);
                            }
                            break;
                        case filterCaseAgendaPostVenta.uspListarAgendaPostVentaSeguimientoReferido_Mensajes:
                            {
                                AgendaPostVentaDTOList = oAgendaPostVentaData.uspListarAgendaPostVentaSeguimientoReferido_Mensajes(oReqFilterAgendaPostVentaDTO.Item);
                            }
                            break;
                        case filterCaseAgendaPostVenta.uspListarAgendaPostVentaSeguimientoProspectos_Mensajes:
                            {
                                AgendaPostVentaDTOList = oAgendaPostVentaData.uspListarAgendaPostVentaSeguimientoProspectos_Mensajes(oReqFilterAgendaPostVentaDTO.Item);
                            }
                            break;
                        case filterCaseAgendaPostVenta.uspListarAgendaPostVentaSeguimientoLlamadaEntrante_Mensajes:
                            {
                                AgendaPostVentaDTOList = oAgendaPostVentaData.uspListarAgendaPostVentaSeguimientoLlamadaEntrante_Mensajes(oReqFilterAgendaPostVentaDTO.Item);
                            }
                            break;
                        case filterCaseAgendaPostVenta.uspListarAgendaPostVentaSeguimientoInvitado_Mensajes:
                            {
                                AgendaPostVentaDTOList = oAgendaPostVentaData.uspListarAgendaPostVentaSeguimientoInvitado_Mensajes(oReqFilterAgendaPostVentaDTO.Item);
                            }
                            break;
                        case filterCaseAgendaPostVenta.uspListarCantidadSeguimiento:
                            {
                                AgendaPostVentaDTOList = oAgendaPostVentaData.uspListarCantidadSeguimiento(oReqFilterAgendaPostVentaDTO.Item);
                            }
                            break;
                        default:
                            {
                                AgendaPostVentaDTOList = null;
                            }
                            break;

                    }

                    oRespListAgendaPostVentaDTO.List = AgendaPostVentaDTOList;
                    oRespListAgendaPostVentaDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListAgendaPostVentaDTO.Success = false;
                    oRespListAgendaPostVentaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespListAgendaPostVentaDTO;

        }

        //-------------------------------------------------------------------
        //Nombre:	AgendaSeguimientoGetItem
        //Objetivo: Retorna un registro de tipo AgendaSeguimientoDTO
        //Valores Prueba:
        //Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //-------------------------------------------------------------------
        public RespItemAgendaPostVentaDTO AgendaPostVentaGetItem(ReqFilterAgendaPostVentaDTO oReqFilterAgendaPostVentaDTO)
        {
            RespItemAgendaPostVentaDTO oRespItemAgendaPostVentaDTO = new RespItemAgendaPostVentaDTO();

            oRespItemAgendaPostVentaDTO.Success = false;
            oRespItemAgendaPostVentaDTO.Item = null;
            oRespItemAgendaPostVentaDTO.User = oReqFilterAgendaPostVentaDTO.User;
            oRespItemAgendaPostVentaDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterAgendaPostVentaDTO.User))
            {
                oRespItemAgendaPostVentaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AgendaSeguimiento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemAgendaPostVentaDTO.MessageList.Count == 0)
            {
                AgendaPostVentaDTO oAgendaPostVentaDTO = null;
                try
                {
                    switch (oReqFilterAgendaPostVentaDTO.FilterCase)
                    {

                        case filterCaseAgendaPostVenta.uspListarAgendaPostVentaSeguimiento_NumeroRegistros:
                            {
                                oAgendaPostVentaDTO = new AgendaPostVentaDTO();
                                oAgendaPostVentaDTO = oAgendaPostVentaData.uspListarAgendaPostVentaSeguimiento_NumeroRegistros(oReqFilterAgendaPostVentaDTO.Item);
                            }
                            break;
                        default:
                            {
                                oAgendaPostVentaDTO = new AgendaPostVentaDTO();
                            }
                            break;
                    }

                    oRespItemAgendaPostVentaDTO.Item = new AgendaPostVentaDTO();
                    oRespItemAgendaPostVentaDTO.Item = oAgendaPostVentaDTO;
                    oRespItemAgendaPostVentaDTO.Success = true;
                    oRespItemAgendaPostVentaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemAgendaPostVentaDTO.Success = false;
                    oRespItemAgendaPostVentaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemAgendaPostVentaDTO;
        }

        //-------------------------------------------------------------------
        //Nombre:	ExecuteTransac
        //Objetivo: Almacena el registro de un objeto de tipo AgendaNutricionalDTO
        //Valores Prueba:
        //Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //-------------------------------------------------------------------
        public RespAgendaPostVentaDTO ExecuteTransac(ReqAgendaPostVentaDTO oReqAgendaPostVentaDTO)
        {
            RespAgendaPostVentaDTO oRespAgendaPostVentaDTO = new RespAgendaPostVentaDTO();

            oRespAgendaPostVentaDTO.MessageList = new List<Mensaje>();
            oRespAgendaPostVentaDTO.User = oReqAgendaPostVentaDTO.User;

            if (String.IsNullOrEmpty(oReqAgendaPostVentaDTO.User))
            {
                oRespAgendaPostVentaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AgendaNutricional no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespAgendaPostVentaDTO.MessageList.Count == 0)
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (AgendaPostVentaDTO item in oReqAgendaPostVentaDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oAgendaPostVentaData.Registrar(item);
                                    break;
                                case Operation.Update:
                                  //  oAgendaNutricionalData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                  //  oAgendaNutricionalData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespAgendaPostVentaDTO.Success = true;
                        oRespAgendaPostVentaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });

                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespAgendaPostVentaDTO.Success = false;
                        oRespAgendaPostVentaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespAgendaPostVentaDTO;
        }

       
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
