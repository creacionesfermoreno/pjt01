
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
	//Archivo     : TipoContratoLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 22/07/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class TipoContratoLogic: IDisposable
	{
		TipoContratoData oTipoContratoData = null;
		public TipoContratoLogic()
		{
			oTipoContratoData = new TipoContratoData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TipoContratoGetList
		//Objetivo: Retorna una colección de registros de tipo TipoContratoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListTipoContratoDTO TipoContratoGetList(ReqFilterTipoContratoDTO oReqFilterTipoContratoDTO)
		{
		
			RespListTipoContratoDTO oRespListTipoContratoDTO = new RespListTipoContratoDTO();
		
			oRespListTipoContratoDTO.List = new List<TipoContratoDTO>();
			oRespListTipoContratoDTO.User = oReqFilterTipoContratoDTO.User;
			oRespListTipoContratoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterTipoContratoDTO.User))
            {
                oRespListTipoContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoContrato no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterTipoContratoDTO.Paging == null)
            {
                oRespListTipoContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListTipoContratoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    if (!oReqFilterTipoContratoDTO.Paging.All && oReqFilterTipoContratoDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterTipoContratoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<TipoContratoDTO> TipoContratoDTOList = new List<TipoContratoDTO>();

                    switch (oReqFilterTipoContratoDTO.FilterCase)
                    {
                        case filterCaseTipoContrato.uspListarCompromiso:
                            {
                                TipoContratoDTOList = oTipoContratoData.ListarCompromiso(oReqFilterTipoContratoDTO.Item);
                            }
                            break;
                        default:
                            {
                                TipoContratoDTOList = oTipoContratoData.Listar(oReqFilterTipoContratoDTO.Item);
                            }
                            break;
                    }

                    oRespListTipoContratoDTO.List = TipoContratoDTOList;
                    oRespListTipoContratoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListTipoContratoDTO.Success = false;
                    oRespListTipoContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListTipoContratoDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TipoContratoGetItem
		//Objetivo: Retorna un registro de tipo TipoContratoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemTipoContratoDTO TipoContratoGetItem(ReqFilterTipoContratoDTO oReqFilterTipoContratoDTO)
		{
			RespItemTipoContratoDTO oRespItemTipoContratoDTO = new RespItemTipoContratoDTO();

            oRespItemTipoContratoDTO.Success = false;
            oRespItemTipoContratoDTO.Item = null;
            oRespItemTipoContratoDTO.User = oReqFilterTipoContratoDTO.User;
            oRespItemTipoContratoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterTipoContratoDTO.User))
            {
                oRespItemTipoContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoContrato no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemTipoContratoDTO.MessageList.Count == 0)
            {
                TipoContratoDTO oTipoContratoDTO = null;
                try
                {
                    switch (oReqFilterTipoContratoDTO.FilterCase)
                    {
                        case filterCaseTipoContrato.uspBuscarCompromiso:
                            {
                                oTipoContratoDTO = new TipoContratoDTO();
                                oTipoContratoDTO = oTipoContratoData.uspBuscarCompromiso(oReqFilterTipoContratoDTO.Item);
                            }
                            break;
                        case filterCaseTipoContrato.porCodigo:
                            {
                                oTipoContratoDTO = new TipoContratoDTO();
                                oTipoContratoDTO = oTipoContratoData.BuscarPorCodigoTipoContrato(oReqFilterTipoContratoDTO.Item);
                            }
                            break;
                        default:
                            {
                                oTipoContratoDTO = new TipoContratoDTO();
                            }
                            break;
                    }

                    oRespItemTipoContratoDTO.Item = new TipoContratoDTO();
                    oRespItemTipoContratoDTO.Item = oTipoContratoDTO;
                    oRespItemTipoContratoDTO.Success = true;
                    oRespItemTipoContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemTipoContratoDTO.Success = false;
                    oRespItemTipoContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemTipoContratoDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo TipoContratoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespTipoContratoDTO ExecuteTransac(ReqTipoContratoDTO oReqTipoContratoDTO)
		{
			RespTipoContratoDTO oRespTipoContratoDTO = new RespTipoContratoDTO();

            oRespTipoContratoDTO.MessageList = new List<Mensaje>();
            oRespTipoContratoDTO.User = oReqTipoContratoDTO.User;
            
            if (String.IsNullOrEmpty(oReqTipoContratoDTO.User))
            {
                oRespTipoContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoContrato no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespTipoContratoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (TipoContratoDTO item in oReqTipoContratoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oTipoContratoData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oTipoContratoData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oTipoContratoData.Eliminar(item);
                                    break;
                                case Operation.uspRegistrarCompromiso:
                                    oTipoContratoData.uspRegistrarCompromiso(item);
                                    break;
                                case Operation.uspActualizarCompromiso:
                                    oTipoContratoData.uspActualizarCompromiso(item);
                                    break;
                                case Operation.uspEliminarCompromiso:
                                    oTipoContratoData.uspEliminarCompromiso(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespTipoContratoDTO.Success = true;
                        oRespTipoContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespTipoContratoDTO.Success = false;
                        oRespTipoContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespTipoContratoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
