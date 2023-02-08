
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
	//Archivo     : TipoEgresoLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 24/12/2015
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class TipoEgresoLogic: IDisposable
	{
		TipoEgresoData oTipoEgresoData = null;
		public TipoEgresoLogic()
		{
			oTipoEgresoData = new TipoEgresoData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TipoEgresoGetList
		//Objetivo: Retorna una colección de registros de tipo TipoEgresoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListTipoEgresoDTO TipoEgresoGetList(ReqFilterTipoEgresoDTO oReqFilterTipoEgresoDTO)
		{
		
			RespListTipoEgresoDTO oRespListTipoEgresoDTO = new RespListTipoEgresoDTO();
		
			oRespListTipoEgresoDTO.List = new List<TipoEgresoDTO>();
			oRespListTipoEgresoDTO.User = oReqFilterTipoEgresoDTO.User;
			oRespListTipoEgresoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterTipoEgresoDTO.User))
            {
                oRespListTipoEgresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoEgreso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterTipoEgresoDTO.Paging == null)
            {
                oRespListTipoEgresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListTipoEgresoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    if (!oReqFilterTipoEgresoDTO.Paging.All && oReqFilterTipoEgresoDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterTipoEgresoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<TipoEgresoDTO> TipoEgresoDTOList = new List<TipoEgresoDTO>();

                    switch (oReqFilterTipoEgresoDTO.FilterCase)
                    {
                        default:
                            {
                                TipoEgresoDTOList = oTipoEgresoData.Listar(oReqFilterTipoEgresoDTO.Item);//new List<TipoEgresoDTO>();
                            }
                            break;
                    }

                    oRespListTipoEgresoDTO.List = TipoEgresoDTOList;
                    oRespListTipoEgresoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListTipoEgresoDTO.Success = false;
                    oRespListTipoEgresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListTipoEgresoDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TipoEgresoGetItem
		//Objetivo: Retorna un registro de tipo TipoEgresoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemTipoEgresoDTO TipoEgresoGetItem(ReqFilterTipoEgresoDTO oReqFilterTipoEgresoDTO)
		{
			RespItemTipoEgresoDTO oRespItemTipoEgresoDTO = new RespItemTipoEgresoDTO();

            oRespItemTipoEgresoDTO.Success = false;
            oRespItemTipoEgresoDTO.Item = null;
            oRespItemTipoEgresoDTO.User = oReqFilterTipoEgresoDTO.User;
            oRespItemTipoEgresoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterTipoEgresoDTO.User))
            {
                oRespItemTipoEgresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoEgreso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemTipoEgresoDTO.MessageList.Count == 0)
            {
                TipoEgresoDTO oTipoEgresoDTO = null;
                try
                {
                    switch (oReqFilterTipoEgresoDTO.FilterCase)
                    {
                       
                        case filterCaseTipoEgreso.porCodigo:
                            {
                                oTipoEgresoDTO = new TipoEgresoDTO();
                                oTipoEgresoDTO = oTipoEgresoData.BuscarPorCodigoTipoEgreso(oReqFilterTipoEgresoDTO.Item);
                            }
                            break;
                        default:
                            {
                                oTipoEgresoDTO = new TipoEgresoDTO();
                            }
                            break;
                    }

                    oRespItemTipoEgresoDTO.Item = new TipoEgresoDTO();
                    oRespItemTipoEgresoDTO.Item = oTipoEgresoDTO;
                    oRespItemTipoEgresoDTO.Success = true;
                    oRespItemTipoEgresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemTipoEgresoDTO.Success = false;
                    oRespItemTipoEgresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemTipoEgresoDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo TipoEgresoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespTipoEgresoDTO ExecuteTransac(ReqTipoEgresoDTO oReqTipoEgresoDTO)
		{
			RespTipoEgresoDTO oRespTipoEgresoDTO = new RespTipoEgresoDTO();

            oRespTipoEgresoDTO.MessageList = new List<Mensaje>();
            oRespTipoEgresoDTO.User = oReqTipoEgresoDTO.User;
            
            if (String.IsNullOrEmpty(oReqTipoEgresoDTO.User))
            {
                oRespTipoEgresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoEgreso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespTipoEgresoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (TipoEgresoDTO item in oReqTipoEgresoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oTipoEgresoData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oTipoEgresoData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oTipoEgresoData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespTipoEgresoDTO.Success = true;
                        oRespTipoEgresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespTipoEgresoDTO.Success = false;
                        oRespTipoEgresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespTipoEgresoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
