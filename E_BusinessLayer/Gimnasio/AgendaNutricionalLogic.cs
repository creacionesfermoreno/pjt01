
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
	//Archivo     : AgendaNutricionalLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 22/08/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class AgendaNutricionalLogic: IDisposable
	{
		AgendaNutricionalData oAgendaNutricionalData = null;
		public AgendaNutricionalLogic()
		{
			oAgendaNutricionalData = new AgendaNutricionalData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	AgendaNutricionalGetList
		//Objetivo: Retorna una colección de registros de tipo AgendaNutricionalDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListAgendaNutricionalDTO AgendaNutricionalGetList(ReqFilterAgendaNutricionalDTO oReqFilterAgendaNutricionalDTO)
		{
		
			RespListAgendaNutricionalDTO oRespListAgendaNutricionalDTO = new RespListAgendaNutricionalDTO();
		
			oRespListAgendaNutricionalDTO.List = new List<AgendaNutricionalDTO>();
			oRespListAgendaNutricionalDTO.User = oReqFilterAgendaNutricionalDTO.User;
			oRespListAgendaNutricionalDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterAgendaNutricionalDTO.User))
            {
                oRespListAgendaNutricionalDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AgendaNutricional no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterAgendaNutricionalDTO.Paging == null)
            {
                oRespListAgendaNutricionalDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListAgendaNutricionalDTO.MessageList.Count == 0)
            {
                
                try
                {
                   
                    List<AgendaNutricionalDTO> AgendaNutricionalDTOList = new List<AgendaNutricionalDTO>();
                    switch (oReqFilterAgendaNutricionalDTO.FilterCase)
                    {
                        case filterCaseAgendaNutricional.uspValidarHorariosOcupadosCitasNutricionales:
                            oReqFilterAgendaNutricionalDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarAgendaNutricionalGeneral_NumeroRegistros"]);
                            AgendaNutricionalDTOList = oAgendaNutricionalData.uspValidarHorariosOcupadosCitasNutricionales(oReqFilterAgendaNutricionalDTO.Item);
                            break;
                        case filterCaseAgendaNutricional.uspListarAgendaNutricionalGeneral_Paginacion:
                            oReqFilterAgendaNutricionalDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarAgendaNutricionalGeneral_NumeroRegistros"]);
                            AgendaNutricionalDTOList = oAgendaNutricionalData.uspListarAgendaNutricionalGeneral_Paginacion(oReqFilterAgendaNutricionalDTO.Item, oReqFilterAgendaNutricionalDTO.Paging);
                            break;
                   
                        case filterCaseAgendaNutricional.uspListarAgendaNutricionalPorCliente:
                            //oReqFilterAgendaNutricionalDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            AgendaNutricionalDTOList = oAgendaNutricionalData.uspListarAgendaNutricionalPorCliente(oReqFilterAgendaNutricionalDTO.Item);
                            break;
                    }
                    
                    oRespListAgendaNutricionalDTO.List = AgendaNutricionalDTOList;
                    oRespListAgendaNutricionalDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListAgendaNutricionalDTO.Success = false;
                    oRespListAgendaNutricionalDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListAgendaNutricionalDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	AgendaNutricionalGetItem
		//Objetivo: Retorna un registro de tipo AgendaNutricionalDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemAgendaNutricionalDTO AgendaNutricionalGetItem(ReqFilterAgendaNutricionalDTO oReqFilterAgendaNutricionalDTO)
		{
			RespItemAgendaNutricionalDTO oRespItemAgendaNutricionalDTO = new RespItemAgendaNutricionalDTO();

            oRespItemAgendaNutricionalDTO.Success = false;
            oRespItemAgendaNutricionalDTO.Item = null;
            oRespItemAgendaNutricionalDTO.User = oReqFilterAgendaNutricionalDTO.User;
            oRespItemAgendaNutricionalDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterAgendaNutricionalDTO.User))
            {
                oRespItemAgendaNutricionalDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AgendaNutricional no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemAgendaNutricionalDTO.MessageList.Count == 0)
            {
                AgendaNutricionalDTO oAgendaNutricionalDTO = null;
                try
                {
                    switch (oReqFilterAgendaNutricionalDTO.FilterCase)
                    {
                       
                        case filterCaseAgendaNutricional.uspBuscarAgendaNutricionalPorCodigo:
                            {
                                oAgendaNutricionalDTO = new AgendaNutricionalDTO();
                                oAgendaNutricionalDTO = oAgendaNutricionalData.uspBuscarAgendaNutricionalPorCodigo(oReqFilterAgendaNutricionalDTO.Item);
                            }
                            break;
                        case filterCaseAgendaNutricional.uspListarAgendaNutricionalGeneral_NumeroRegistros:
                            {
                                oAgendaNutricionalDTO = new AgendaNutricionalDTO();
                                oAgendaNutricionalDTO = oAgendaNutricionalData.uspListarAgendaNutricionalGeneral_NumeroRegistros(oReqFilterAgendaNutricionalDTO.Item);
                            }
                            break;                    
                        default:
                            {
                                oAgendaNutricionalDTO = new AgendaNutricionalDTO();
                            }
                            break;
                    }

                    oRespItemAgendaNutricionalDTO.Item = new AgendaNutricionalDTO();
                    oRespItemAgendaNutricionalDTO.Item = oAgendaNutricionalDTO;
                    oRespItemAgendaNutricionalDTO.Success = true;
                    oRespItemAgendaNutricionalDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemAgendaNutricionalDTO.Success = false;
                    oRespItemAgendaNutricionalDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemAgendaNutricionalDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo AgendaNutricionalDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespAgendaNutricionalDTO ExecuteTransac(ReqAgendaNutricionalDTO oReqAgendaNutricionalDTO)
		{
			RespAgendaNutricionalDTO oRespAgendaNutricionalDTO = new RespAgendaNutricionalDTO();

            oRespAgendaNutricionalDTO.MessageList = new List<Mensaje>();
            oRespAgendaNutricionalDTO.User = oReqAgendaNutricionalDTO.User;
            
            if (String.IsNullOrEmpty(oReqAgendaNutricionalDTO.User))
            {
                oRespAgendaNutricionalDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AgendaNutricional no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespAgendaNutricionalDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (AgendaNutricionalDTO item in oReqAgendaNutricionalDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oAgendaNutricionalData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oAgendaNutricionalData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oAgendaNutricionalData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespAgendaNutricionalDTO.Success = true;
                        oRespAgendaNutricionalDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespAgendaNutricionalDTO.Success = false;
                        oRespAgendaNutricionalDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespAgendaNutricionalDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
