
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
	//-------------------------------------------------------------------
	//Archivo     : HorarioPersonalEventualsLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 15/09/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class HorarioPersonalEventualsLogic: IDisposable
	{
		HorarioPersonalEventualsData oHorarioPersonalEventualsData = null;
		public HorarioPersonalEventualsLogic()
		{
			oHorarioPersonalEventualsData = new HorarioPersonalEventualsData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	HorarioPersonalEventualsGetList
		//Objetivo: Retorna una colección de registros de tipo HorarioPersonalEventualsDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListHorarioPersonalEventualsDTO HorarioPersonalEventualsGetList(ReqFilterHorarioPersonalEventualsDTO oReqFilterHorarioPersonalEventualsDTO)
		{
		
			RespListHorarioPersonalEventualsDTO oRespListHorarioPersonalEventualsDTO = new RespListHorarioPersonalEventualsDTO();
		
			oRespListHorarioPersonalEventualsDTO.List = new List<HorarioPersonalEventualsDTO>();
			oRespListHorarioPersonalEventualsDTO.User = oReqFilterHorarioPersonalEventualsDTO.User;
			oRespListHorarioPersonalEventualsDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterHorarioPersonalEventualsDTO.User))
            {
                oRespListHorarioPersonalEventualsDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioPersonalEventuals no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterHorarioPersonalEventualsDTO.Paging == null)
            {
                oRespListHorarioPersonalEventualsDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListHorarioPersonalEventualsDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterHorarioPersonalEventualsDTO.Paging.All && oReqFilterHorarioPersonalEventualsDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterHorarioPersonalEventualsDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<HorarioPersonalEventualsDTO> HorarioPersonalEventualsDTOList = new List<HorarioPersonalEventualsDTO>();

                    switch (oReqFilterHorarioPersonalEventualsDTO.FilterCase)
                    {
                        default:
                            {
                               HorarioPersonalEventualsDTOList = oHorarioPersonalEventualsData.Listar(oReqFilterHorarioPersonalEventualsDTO.Item);
                            }
                            break;
                    }

                    oRespListHorarioPersonalEventualsDTO.List = HorarioPersonalEventualsDTOList;
                    oRespListHorarioPersonalEventualsDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListHorarioPersonalEventualsDTO.Success = false;
                    oRespListHorarioPersonalEventualsDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListHorarioPersonalEventualsDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	HorarioPersonalEventualsGetItem
		//Objetivo: Retorna un registro de tipo HorarioPersonalEventualsDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemHorarioPersonalEventualsDTO HorarioPersonalEventualsGetItem(ReqFilterHorarioPersonalEventualsDTO oReqFilterHorarioPersonalEventualsDTO)
		{
			RespItemHorarioPersonalEventualsDTO oRespItemHorarioPersonalEventualsDTO = new RespItemHorarioPersonalEventualsDTO();

            oRespItemHorarioPersonalEventualsDTO.Success = false;
            oRespItemHorarioPersonalEventualsDTO.Item = null;
            oRespItemHorarioPersonalEventualsDTO.User = oReqFilterHorarioPersonalEventualsDTO.User;
            oRespItemHorarioPersonalEventualsDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterHorarioPersonalEventualsDTO.User))
            {
                oRespItemHorarioPersonalEventualsDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioPersonalEventuals no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemHorarioPersonalEventualsDTO.MessageList.Count == 0)
            {
                HorarioPersonalEventualsDTO oHorarioPersonalEventualsDTO = null;
                try
                {
                    switch (oReqFilterHorarioPersonalEventualsDTO.FilterCase)
                    {
                       
                        case filterCaseHorarioPersonalEventuals.PorCodigo:
                            {
                                oHorarioPersonalEventualsDTO = new HorarioPersonalEventualsDTO();
                              //  oHorarioPersonalEventualsDTO = oHorarioPersonalEventualsData.BuscarPorCodigoHorarioPersonalEventuals(oReqFilterHorarioPersonalEventualsDTO.Item);
                            }
                            break;
                        default:
                            {
                                oHorarioPersonalEventualsDTO = new HorarioPersonalEventualsDTO();
                            }
                            break;
                    }

                    oRespItemHorarioPersonalEventualsDTO.Item = new HorarioPersonalEventualsDTO();
                    oRespItemHorarioPersonalEventualsDTO.Item = oHorarioPersonalEventualsDTO;
                    oRespItemHorarioPersonalEventualsDTO.Success = true;
                    oRespItemHorarioPersonalEventualsDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemHorarioPersonalEventualsDTO.Success = false;
                    oRespItemHorarioPersonalEventualsDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemHorarioPersonalEventualsDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo HorarioPersonalEventualsDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespHorarioPersonalEventualsDTO ExecuteTransac(ReqHorarioPersonalEventualsDTO oReqHorarioPersonalEventualsDTO)
		{
			RespHorarioPersonalEventualsDTO oRespHorarioPersonalEventualsDTO = new RespHorarioPersonalEventualsDTO();

            oRespHorarioPersonalEventualsDTO.MessageList = new List<Mensaje>();
            oRespHorarioPersonalEventualsDTO.User = oReqHorarioPersonalEventualsDTO.User;
            
            if (String.IsNullOrEmpty(oReqHorarioPersonalEventualsDTO.User))
            {
                oRespHorarioPersonalEventualsDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioPersonalEventuals no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespHorarioPersonalEventualsDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (HorarioPersonalEventualsDTO item in oReqHorarioPersonalEventualsDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oHorarioPersonalEventualsData.Registrar(item);
                                    break;
                                //case Operation.Update:
                                //    oHorarioPersonalEventualsData.Actualizar(item);
                                //    break;
                                //case Operation.Delete:
                                //    oHorarioPersonalEventualsData.Eliminar(item);
                                //    break;
                            }
                        }
                        tx.Complete();
                        oRespHorarioPersonalEventualsDTO.Success = true;
                        oRespHorarioPersonalEventualsDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespHorarioPersonalEventualsDTO.Success = false;
                        oRespHorarioPersonalEventualsDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespHorarioPersonalEventualsDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
