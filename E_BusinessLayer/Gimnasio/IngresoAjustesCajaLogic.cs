
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
	//Archivo     : IngresoAjustesCajaLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 15/03/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class IngresoAjustesCajaLogic: IDisposable
	{
		IngresoAjustesCajaData oIngresoAjustesCajaData = null;
		public IngresoAjustesCajaLogic()
		{
			oIngresoAjustesCajaData = new IngresoAjustesCajaData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	IngresoAjustesCajaGetList
		//Objetivo: Retorna una colección de registros de tipo IngresoAjustesCajaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListIngresoAjustesCajaDTO IngresoAjustesCajaGetList(ReqFilterIngresoAjustesCajaDTO oReqFilterIngresoAjustesCajaDTO)
		{
		
			RespListIngresoAjustesCajaDTO oRespListIngresoAjustesCajaDTO = new RespListIngresoAjustesCajaDTO();
		
			oRespListIngresoAjustesCajaDTO.List = new List<IngresoAjustesCajaDTO>();
			oRespListIngresoAjustesCajaDTO.User = oReqFilterIngresoAjustesCajaDTO.User;
			oRespListIngresoAjustesCajaDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterIngresoAjustesCajaDTO.User))
            {
                oRespListIngresoAjustesCajaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de IngresoAjustesCaja no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterIngresoAjustesCajaDTO.Paging == null)
            {
                oRespListIngresoAjustesCajaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListIngresoAjustesCajaDTO.MessageList.Count == 0)
            {
                
                try
                {
                   // uint recordCount = 0;
                    
                    if (!oReqFilterIngresoAjustesCajaDTO.Paging.All && oReqFilterIngresoAjustesCajaDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterIngresoAjustesCajaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<IngresoAjustesCajaDTO> IngresoAjustesCajaDTOList = new List<IngresoAjustesCajaDTO>();

                    switch (oReqFilterIngresoAjustesCajaDTO.FilterCase)
                    {
                          case filterCaseIngresoAjustesCaja.ListarDetalleAjustesIngresoCaja:
                            {
                                IngresoAjustesCajaDTOList = oIngresoAjustesCajaData.ListarDetalleAjustesIngresoCaja(oReqFilterIngresoAjustesCajaDTO.Item);
                            }
                            break;
                      
                    }

                    oRespListIngresoAjustesCajaDTO.List = IngresoAjustesCajaDTOList;
                    oRespListIngresoAjustesCajaDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListIngresoAjustesCajaDTO.Success = false;
                    oRespListIngresoAjustesCajaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListIngresoAjustesCajaDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	IngresoAjustesCajaGetItem
		//Objetivo: Retorna un registro de tipo IngresoAjustesCajaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemIngresoAjustesCajaDTO IngresoAjustesCajaGetItem(ReqFilterIngresoAjustesCajaDTO oReqFilterIngresoAjustesCajaDTO)
		{
			RespItemIngresoAjustesCajaDTO oRespItemIngresoAjustesCajaDTO = new RespItemIngresoAjustesCajaDTO();

            oRespItemIngresoAjustesCajaDTO.Success = false;
            oRespItemIngresoAjustesCajaDTO.Item = null;
            oRespItemIngresoAjustesCajaDTO.User = oReqFilterIngresoAjustesCajaDTO.User;
            oRespItemIngresoAjustesCajaDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterIngresoAjustesCajaDTO.User))
            {
                oRespItemIngresoAjustesCajaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de IngresoAjustesCaja no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemIngresoAjustesCajaDTO.MessageList.Count == 0)
            {
                IngresoAjustesCajaDTO oIngresoAjustesCajaDTO = null;
                try
                {
                    switch (oReqFilterIngresoAjustesCajaDTO.FilterCase)
                    {

                        case filterCaseIngresoAjustesCaja.uspBuscarMontoDeAbrirCaja:
                            {
                                oIngresoAjustesCajaDTO = new IngresoAjustesCajaDTO();
                            }
                            break;
                        default:
                            {
                                oIngresoAjustesCajaDTO = new IngresoAjustesCajaDTO();
                            }
                            break;
                    }

                    oRespItemIngresoAjustesCajaDTO.Item = new IngresoAjustesCajaDTO();
                    oRespItemIngresoAjustesCajaDTO.Item = oIngresoAjustesCajaDTO;
                    oRespItemIngresoAjustesCajaDTO.Success = true;
                    oRespItemIngresoAjustesCajaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemIngresoAjustesCajaDTO.Success = false;
                    oRespItemIngresoAjustesCajaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemIngresoAjustesCajaDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo IngresoAjustesCajaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespIngresoAjustesCajaDTO ExecuteTransac(ReqIngresoAjustesCajaDTO oReqIngresoAjustesCajaDTO)
		{
			RespIngresoAjustesCajaDTO oRespIngresoAjustesCajaDTO = new RespIngresoAjustesCajaDTO();

            oRespIngresoAjustesCajaDTO.MessageList = new List<Mensaje>();
            oRespIngresoAjustesCajaDTO.User = oReqIngresoAjustesCajaDTO.User;
            
            if (String.IsNullOrEmpty(oReqIngresoAjustesCajaDTO.User))
            {
                oRespIngresoAjustesCajaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de IngresoAjustesCaja no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespIngresoAjustesCajaDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (IngresoAjustesCajaDTO item in oReqIngresoAjustesCajaDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oIngresoAjustesCajaData.Registrar(item);
                                    break;                              
                            }
                        }
                        tx.Complete();
                        oRespIngresoAjustesCajaDTO.Success = true;
                        oRespIngresoAjustesCajaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespIngresoAjustesCajaDTO.Success = false;
                        oRespIngresoAjustesCajaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespIngresoAjustesCajaDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
