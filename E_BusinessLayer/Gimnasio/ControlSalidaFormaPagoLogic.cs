
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
	//Archivo     : ControlSalidaFormaPagoLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 21/10/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class ControlSalidaFormaPagoLogic: IDisposable
	{
		ControlSalidaFormaPagoData oControlSalidaFormaPagoData = null;
		public ControlSalidaFormaPagoLogic()
		{
			oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ControlSalidaFormaPagoGetList
		//Objetivo: Retorna una colección de registros de tipo ControlSalidaFormaPagoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListControlSalidaFormaPagoDTO ControlSalidaFormaPagoGetList(ReqFilterControlSalidaFormaPagoDTO oReqFilterControlSalidaFormaPagoDTO)
		{		
			RespListControlSalidaFormaPagoDTO oRespListControlSalidaFormaPagoDTO = new RespListControlSalidaFormaPagoDTO();
		
			oRespListControlSalidaFormaPagoDTO.List = new List<ControlSalidaFormaPagoDTO>();
			oRespListControlSalidaFormaPagoDTO.User = oReqFilterControlSalidaFormaPagoDTO.User;
			oRespListControlSalidaFormaPagoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterControlSalidaFormaPagoDTO.User))
            {
                oRespListControlSalidaFormaPagoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlSalidaFormaPago no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterControlSalidaFormaPagoDTO.Paging == null)
            {
                oRespListControlSalidaFormaPagoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListControlSalidaFormaPagoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    
                    if (!oReqFilterControlSalidaFormaPagoDTO.Paging.All && oReqFilterControlSalidaFormaPagoDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterControlSalidaFormaPagoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<ControlSalidaFormaPagoDTO> ControlSalidaFormaPagoDTOList = new List<ControlSalidaFormaPagoDTO>();

                    switch (oReqFilterControlSalidaFormaPagoDTO.FilterCase)
                    {
                        case filterCaseControlSalidaFormaPago.Filter_uspListarControlSalidaFormaPago:
                            ControlSalidaFormaPagoDTOList = oControlSalidaFormaPagoData.uspListarControlSalidaFormaPago(oReqFilterControlSalidaFormaPagoDTO.Item);
                            break;
                    }

                    oRespListControlSalidaFormaPagoDTO.List = ControlSalidaFormaPagoDTOList;
                    oRespListControlSalidaFormaPagoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListControlSalidaFormaPagoDTO.Success = false;
                    oRespListControlSalidaFormaPagoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListControlSalidaFormaPagoDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ControlSalidaFormaPagoGetItem
		//Objetivo: Retorna un registro de tipo ControlSalidaFormaPagoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemControlSalidaFormaPagoDTO ControlSalidaFormaPagoGetItem(ReqFilterControlSalidaFormaPagoDTO oReqFilterControlSalidaFormaPagoDTO)
		{
			RespItemControlSalidaFormaPagoDTO oRespItemControlSalidaFormaPagoDTO = new RespItemControlSalidaFormaPagoDTO();

            oRespItemControlSalidaFormaPagoDTO.Success = false;
            oRespItemControlSalidaFormaPagoDTO.Item = null;
            oRespItemControlSalidaFormaPagoDTO.User = oReqFilterControlSalidaFormaPagoDTO.User;
            oRespItemControlSalidaFormaPagoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterControlSalidaFormaPagoDTO.User))
            {
                oRespItemControlSalidaFormaPagoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlSalidaFormaPago no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemControlSalidaFormaPagoDTO.MessageList.Count == 0)
            {
                ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = null;
                try
                {
                    //switch (oReqFilterControlSalidaFormaPagoDTO.FilterCase)
                    //{
                       
                    //    case filterCaseControlSalidaFormaPago.porCodigo:
                    //        {
                    //            oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                    //            oControlSalidaFormaPagoDTO = oControlSalidaFormaPagoData.BuscarPorCodigoControlSalidaFormaPago(oReqFilterControlSalidaFormaPagoDTO.Item);
                    //        }
                    //        break;
                    //    default:
                    //        {
                    //            oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                    //        }
                    //        break;
                    //}

                    oRespItemControlSalidaFormaPagoDTO.Item = new ControlSalidaFormaPagoDTO();
                    oRespItemControlSalidaFormaPagoDTO.Item = oControlSalidaFormaPagoDTO;
                    oRespItemControlSalidaFormaPagoDTO.Success = true;
                    oRespItemControlSalidaFormaPagoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemControlSalidaFormaPagoDTO.Success = false;
                    oRespItemControlSalidaFormaPagoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemControlSalidaFormaPagoDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo ControlSalidaFormaPagoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespControlSalidaFormaPagoDTO ExecuteTransac(ReqControlSalidaFormaPagoDTO oReqControlSalidaFormaPagoDTO)
		{
			RespControlSalidaFormaPagoDTO oRespControlSalidaFormaPagoDTO = new RespControlSalidaFormaPagoDTO();

            oRespControlSalidaFormaPagoDTO.MessageList = new List<Mensaje>();
            oRespControlSalidaFormaPagoDTO.User = oReqControlSalidaFormaPagoDTO.User;
            
            if (String.IsNullOrEmpty(oReqControlSalidaFormaPagoDTO.User))
            {
                oRespControlSalidaFormaPagoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlSalidaFormaPago no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespControlSalidaFormaPagoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (ControlSalidaFormaPagoDTO item in oReqControlSalidaFormaPagoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oControlSalidaFormaPagoData.Registrar(item);
                                    break;
                               
                            }
                        }
                        tx.Complete();
                        oRespControlSalidaFormaPagoDTO.Success = true;
                        oRespControlSalidaFormaPagoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespControlSalidaFormaPagoDTO.Success = false;
                        oRespControlSalidaFormaPagoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespControlSalidaFormaPagoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
