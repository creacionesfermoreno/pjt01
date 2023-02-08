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
	//Archivo     : ControlMedidasLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 16/08/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class ControlMedidasLogic: IDisposable
	{
		ControlMedidasData oControlMedidasData = null;
		public ControlMedidasLogic()
		{
			oControlMedidasData = new ControlMedidasData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ControlMedidasGetList
		//Objetivo: Retorna una colección de registros de tipo ControlMedidasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListControlMedidasDTO ControlMedidasGetList(ReqFilterControlMedidasDTO oReqFilterControlMedidasDTO)
		{
		
			RespListControlMedidasDTO oRespListControlMedidasDTO = new RespListControlMedidasDTO();
		
			oRespListControlMedidasDTO.List = new List<ControlMedidasDTO>();
			oRespListControlMedidasDTO.User = oReqFilterControlMedidasDTO.User;
			oRespListControlMedidasDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterControlMedidasDTO.User))
            {
                oRespListControlMedidasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlMedidas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterControlMedidasDTO.Paging == null)
            {
                oRespListControlMedidasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListControlMedidasDTO.MessageList.Count == 0)
            {
                
                try
                {                    
                    
                    List<ControlMedidasDTO> ControlMedidasDTOList = new List<ControlMedidasDTO>();
                    switch (oReqFilterControlMedidasDTO.FilterCase)
                    {
                        case filterCaseControlMedidas.uspListarControlMedidas_Paginacion:
                            oReqFilterControlMedidasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarControlMedidas_NumeroRegistros"]);
                            ControlMedidasDTOList = oControlMedidasData.uspListarControlMedidas_Paginacion(oReqFilterControlMedidasDTO.Item, oReqFilterControlMedidasDTO.Paging);
                            break;
                        case filterCaseControlMedidas.uspListarControlMedidasActivas_Paginacion:
                            oReqFilterControlMedidasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarControlMedidasActivas_NumeroRegistros"]);
                            ControlMedidasDTOList = oControlMedidasData.uspListarControlMedidasActivas_Paginacion(oReqFilterControlMedidasDTO.Item, oReqFilterControlMedidasDTO.Paging);
                            break;
                        case filterCaseControlMedidas.uspListarControlMedidasInactivas_Paginacion:
                            oReqFilterControlMedidasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarControlMedidasActivas_NumeroRegistros"]);
                            ControlMedidasDTOList = oControlMedidasData.uspListarControlMedidasInactivas_Paginacion(oReqFilterControlMedidasDTO.Item, oReqFilterControlMedidasDTO.Paging);
                            break;
                        case filterCaseControlMedidas.uspListarControlMedidasRenovaciones_Paginacion:
                            oReqFilterControlMedidasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarControlMedidasActivas_NumeroRegistros"]);
                            ControlMedidasDTOList = oControlMedidasData.uspListarControlMedidasRenovaciones_Paginacion(oReqFilterControlMedidasDTO.Item, oReqFilterControlMedidasDTO.Paging);
                            break;
                        case filterCaseControlMedidas.uspListarControlMedidasSinRutina_Paginacion:
                            oReqFilterControlMedidasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarControlMedidasActivas_NumeroRegistros"]);
                            ControlMedidasDTOList = oControlMedidasData.uspListarControlMedidasSinRutina_Paginacion(oReqFilterControlMedidasDTO.Item, oReqFilterControlMedidasDTO.Paging);
                            break;
                        case filterCaseControlMedidas.uspListarAgendaNutricionalGeneralHistorial_Paginacion:
                            oReqFilterControlMedidasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarControlMedidasActivas_NumeroRegistros"]);
                            ControlMedidasDTOList = oControlMedidasData.uspListarAgendaNutricionalGeneralHistorial_Paginacion(oReqFilterControlMedidasDTO.Item, oReqFilterControlMedidasDTO.Paging);
                            break;
                    }
                    
                    oRespListControlMedidasDTO.List = ControlMedidasDTOList;
                    oRespListControlMedidasDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListControlMedidasDTO.Success = false;
                    oRespListControlMedidasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListControlMedidasDTO;	           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ControlMedidasGetItem
		//Objetivo: Retorna un registro de tipo ControlMedidasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemControlMedidasDTO ControlMedidasGetItem(ReqFilterControlMedidasDTO oReqFilterControlMedidasDTO)
		{
			RespItemControlMedidasDTO oRespItemControlMedidasDTO = new RespItemControlMedidasDTO();

            oRespItemControlMedidasDTO.Success = false;
            oRespItemControlMedidasDTO.Item = null;
            oRespItemControlMedidasDTO.User = oReqFilterControlMedidasDTO.User;
            oRespItemControlMedidasDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterControlMedidasDTO.User))
            {
                oRespItemControlMedidasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlMedidas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemControlMedidasDTO.MessageList.Count == 0)
            {
                ControlMedidasDTO oControlMedidasDTO = null;
                try
                {
                    switch (oReqFilterControlMedidasDTO.FilterCase)
                    {
                       
                        case filterCaseControlMedidas.uspListarControlMedidas_NumeroRegistros:
                            {
                                oControlMedidasDTO = new ControlMedidasDTO();
                                oControlMedidasDTO = oControlMedidasData.uspListarControlMedidas_NumeroRegistros(oReqFilterControlMedidasDTO.Item);
                            }
                            break;
                        case filterCaseControlMedidas.uspListarControlMedidasActivas_NumeroRegistros:
                            {
                                oControlMedidasDTO = new ControlMedidasDTO();
                                oControlMedidasDTO = oControlMedidasData.uspListarControlMedidasActivas_NumeroRegistros(oReqFilterControlMedidasDTO.Item);
                            }
                            break;
                        case filterCaseControlMedidas.uspListarControlMedidasInactivas_NumeroRegistros:
                            {
                                oControlMedidasDTO = new ControlMedidasDTO();
                                oControlMedidasDTO = oControlMedidasData.uspListarControlMedidasInactivas_NumeroRegistros(oReqFilterControlMedidasDTO.Item);
                            }
                            break;
                        case filterCaseControlMedidas.uspListarControlMedidasRenovaciones_NumeroRegistros:
                            {
                                oControlMedidasDTO = new ControlMedidasDTO();
                                oControlMedidasDTO = oControlMedidasData.uspListarControlMedidasRenovaciones_NumeroRegistros(oReqFilterControlMedidasDTO.Item);
                            }
                            break;
                        case filterCaseControlMedidas.uspListarControlMedidasSinRutina_NumeroRegistros:
                            {
                                oControlMedidasDTO = new ControlMedidasDTO();
                                oControlMedidasDTO = oControlMedidasData.uspListarControlMedidasSinRutina_NumeroRegistros(oReqFilterControlMedidasDTO.Item);
                            }
                            break;
                        case filterCaseControlMedidas.uspBuscarControlMedidasPorCodigo:
                            {
                                oControlMedidasDTO = new ControlMedidasDTO();
                                oControlMedidasDTO = oControlMedidasData.uspBuscarControlMedidasPorCodigo(oReqFilterControlMedidasDTO.Item);
                            }
                            break;
                        case filterCaseControlMedidas.uspListarAgendaNutricionalGeneralHistorial_NumeroRegistros:
                            {
                                oControlMedidasDTO = new ControlMedidasDTO();
                                oControlMedidasDTO = oControlMedidasData.uspListarAgendaNutricionalGeneralHistorial_NumeroRegistros(oReqFilterControlMedidasDTO.Item);
                            }
                            break;
                    }

                    oRespItemControlMedidasDTO.Item = new ControlMedidasDTO();
                    oRespItemControlMedidasDTO.Item = oControlMedidasDTO;
                    oRespItemControlMedidasDTO.Success = true;
                    oRespItemControlMedidasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemControlMedidasDTO.Success = false;
                    oRespItemControlMedidasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemControlMedidasDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo ControlMedidasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespControlMedidasDTO ExecuteTransac(ReqControlMedidasDTO oReqControlMedidasDTO)
		{
			RespControlMedidasDTO oRespControlMedidasDTO = new RespControlMedidasDTO();

            oRespControlMedidasDTO.MessageList = new List<Mensaje>();
            oRespControlMedidasDTO.User = oReqControlMedidasDTO.User;
            
            if (String.IsNullOrEmpty(oReqControlMedidasDTO.User))
            {
                oRespControlMedidasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlMedidas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespControlMedidasDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (ControlMedidasDTO item in oReqControlMedidasDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oControlMedidasData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oControlMedidasData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oControlMedidasData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespControlMedidasDTO.Success = true;
                        oRespControlMedidasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespControlMedidasDTO.Success = false;
                        oRespControlMedidasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespControlMedidasDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
