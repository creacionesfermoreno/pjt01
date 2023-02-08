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
	//Archivo     : TipoAgendaClienteLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 13/10/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class TipoAgendaClienteLogic: IDisposable
	{
		TipoAgendaClienteData oTipoAgendaClienteData = null;
		public TipoAgendaClienteLogic()
		{
			oTipoAgendaClienteData = new TipoAgendaClienteData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TipoAgendaClienteGetList
		//Objetivo: Retorna una colección de registros de tipo TipoAgendaClienteDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListTipoAgendaClienteDTO TipoAgendaClienteGetList(ReqFilterTipoAgendaClienteDTO oReqFilterTipoAgendaClienteDTO)
		{
		
			RespListTipoAgendaClienteDTO oRespListTipoAgendaClienteDTO = new RespListTipoAgendaClienteDTO();
		
			oRespListTipoAgendaClienteDTO.List = new List<TipoAgendaClienteDTO>();
			oRespListTipoAgendaClienteDTO.User = oReqFilterTipoAgendaClienteDTO.User;
			oRespListTipoAgendaClienteDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterTipoAgendaClienteDTO.User))
            {
                oRespListTipoAgendaClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoAgendaCliente no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterTipoAgendaClienteDTO.Paging == null)
            {
                oRespListTipoAgendaClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListTipoAgendaClienteDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    if (!oReqFilterTipoAgendaClienteDTO.Paging.All && oReqFilterTipoAgendaClienteDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterTipoAgendaClienteDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<TipoAgendaClienteDTO> TipoAgendaClienteDTOList = new List<TipoAgendaClienteDTO>();
                    switch (oReqFilterTipoAgendaClienteDTO.FilterCase)
                    {
                        case filterCaseTipoAgendaCliente.Filter_uspListarTipoAgendaCliente:
                            TipoAgendaClienteDTOList = oTipoAgendaClienteData.uspListarTipoAgendaCliente();
                            break;
                    }

                    oRespListTipoAgendaClienteDTO.List = TipoAgendaClienteDTOList;
                    oRespListTipoAgendaClienteDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListTipoAgendaClienteDTO.Success = false;
                    oRespListTipoAgendaClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListTipoAgendaClienteDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TipoAgendaClienteGetItem
		//Objetivo: Retorna un registro de tipo TipoAgendaClienteDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemTipoAgendaClienteDTO TipoAgendaClienteGetItem(ReqFilterTipoAgendaClienteDTO oReqFilterTipoAgendaClienteDTO)
		{
			RespItemTipoAgendaClienteDTO oRespItemTipoAgendaClienteDTO = new RespItemTipoAgendaClienteDTO();

            oRespItemTipoAgendaClienteDTO.Success = false;
            oRespItemTipoAgendaClienteDTO.Item = null;
            oRespItemTipoAgendaClienteDTO.User = oReqFilterTipoAgendaClienteDTO.User;
            oRespItemTipoAgendaClienteDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterTipoAgendaClienteDTO.User))
            {
                oRespItemTipoAgendaClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoAgendaCliente no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemTipoAgendaClienteDTO.MessageList.Count == 0)
            {
                TipoAgendaClienteDTO oTipoAgendaClienteDTO = null;
                try
                {
                    switch (oReqFilterTipoAgendaClienteDTO.FilterCase)
                    {
                       
                        case filterCaseTipoAgendaCliente.Filter_uspBuscarTipoAgendaClientePorCodigo:
                            {
                                oTipoAgendaClienteDTO = new TipoAgendaClienteDTO();
                                oTipoAgendaClienteDTO = oTipoAgendaClienteData.uspBuscarTipoAgendaClientePorCodigo(oReqFilterTipoAgendaClienteDTO.Item);
                            }
                            break;
                        default:
                            {
                                oTipoAgendaClienteDTO = new TipoAgendaClienteDTO();
                            }
                            break;
                    }

                    oRespItemTipoAgendaClienteDTO.Item = new TipoAgendaClienteDTO();
                    oRespItemTipoAgendaClienteDTO.Item = oTipoAgendaClienteDTO;
                    oRespItemTipoAgendaClienteDTO.Success = true;
                    oRespItemTipoAgendaClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemTipoAgendaClienteDTO.Success = false;
                    oRespItemTipoAgendaClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemTipoAgendaClienteDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo TipoAgendaClienteDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespTipoAgendaClienteDTO ExecuteTransac(ReqTipoAgendaClienteDTO oReqTipoAgendaClienteDTO)
		{
			RespTipoAgendaClienteDTO oRespTipoAgendaClienteDTO = new RespTipoAgendaClienteDTO();

            oRespTipoAgendaClienteDTO.MessageList = new List<Mensaje>();
            oRespTipoAgendaClienteDTO.User = oReqTipoAgendaClienteDTO.User;
            
            if (String.IsNullOrEmpty(oReqTipoAgendaClienteDTO.User))
            {
                oRespTipoAgendaClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoAgendaCliente no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespTipoAgendaClienteDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (TipoAgendaClienteDTO item in oReqTipoAgendaClienteDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oTipoAgendaClienteData.Registrar(item);
                                    break;
                                case Operation.Delete:
                                    oTipoAgendaClienteData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespTipoAgendaClienteDTO.Success = true;
                        oRespTipoAgendaClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespTipoAgendaClienteDTO.Success = false;
                        oRespTipoAgendaClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespTipoAgendaClienteDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
