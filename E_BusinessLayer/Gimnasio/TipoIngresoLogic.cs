
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
	//Archivo     : TipoIngresoLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 17/09/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class TipoIngresoLogic: IDisposable
	{
		TipoIngresoData oTipoIngresoData = null;
		public TipoIngresoLogic()
		{
			oTipoIngresoData = new TipoIngresoData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TipoIngresoGetList
		//Objetivo: Retorna una colección de registros de tipo TipoIngresoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListTipoIngresoDTO TipoIngresoGetList(ReqFilterTipoIngresoDTO oReqFilterTipoIngresoDTO)
		{
		
			RespListTipoIngresoDTO oRespListTipoIngresoDTO = new RespListTipoIngresoDTO();
		
			oRespListTipoIngresoDTO.List = new List<TipoIngresoDTO>();
			oRespListTipoIngresoDTO.User = oReqFilterTipoIngresoDTO.User;
			oRespListTipoIngresoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterTipoIngresoDTO.User))
            {
                oRespListTipoIngresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoIngreso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterTipoIngresoDTO.Paging == null)
            {
                oRespListTipoIngresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListTipoIngresoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterTipoIngresoDTO.Paging.All && oReqFilterTipoIngresoDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterTipoIngresoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<TipoIngresoDTO> TipoIngresoDTOList = new List<TipoIngresoDTO>();

                    switch (oReqFilterTipoIngresoDTO.FilterCase)
                    {
                        default:
                            {
                                TipoIngresoDTOList = oTipoIngresoData.Listar(oReqFilterTipoIngresoDTO.Item);
                            }
                            break;
                    }

                    oRespListTipoIngresoDTO.List = TipoIngresoDTOList;
                    oRespListTipoIngresoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListTipoIngresoDTO.Success = false;
                    oRespListTipoIngresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListTipoIngresoDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TipoIngresoGetItem
		//Objetivo: Retorna un registro de tipo TipoIngresoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemTipoIngresoDTO TipoIngresoGetItem(ReqFilterTipoIngresoDTO oReqFilterTipoIngresoDTO)
		{
			RespItemTipoIngresoDTO oRespItemTipoIngresoDTO = new RespItemTipoIngresoDTO();

            oRespItemTipoIngresoDTO.Success = false;
            oRespItemTipoIngresoDTO.Item = null;
            oRespItemTipoIngresoDTO.User = oReqFilterTipoIngresoDTO.User;
            oRespItemTipoIngresoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterTipoIngresoDTO.User))
            {
                oRespItemTipoIngresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoIngreso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemTipoIngresoDTO.MessageList.Count == 0)
            {
                TipoIngresoDTO oTipoIngresoDTO = null;
                try
                {
                    switch (oReqFilterTipoIngresoDTO.FilterCase)
                    {
                       
                        case filterCaseTipoIngreso.porCodigo:
                            {
                                oTipoIngresoDTO = new TipoIngresoDTO();
                                oTipoIngresoDTO = oTipoIngresoData.BuscarPorCodigoTipoIngreso(oReqFilterTipoIngresoDTO.Item);
                            }
                            break;
                        default:
                            {
                                oTipoIngresoDTO = new TipoIngresoDTO();
                            }
                            break;
                    }

                    oRespItemTipoIngresoDTO.Item = new TipoIngresoDTO();
                    oRespItemTipoIngresoDTO.Item = oTipoIngresoDTO;
                    oRespItemTipoIngresoDTO.Success = true;
                    oRespItemTipoIngresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemTipoIngresoDTO.Success = false;
                    oRespItemTipoIngresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemTipoIngresoDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo TipoIngresoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespTipoIngresoDTO ExecuteTransac(ReqTipoIngresoDTO oReqTipoIngresoDTO)
		{
			RespTipoIngresoDTO oRespTipoIngresoDTO = new RespTipoIngresoDTO();

            oRespTipoIngresoDTO.MessageList = new List<Mensaje>();
            oRespTipoIngresoDTO.User = oReqTipoIngresoDTO.User;
            
            if (String.IsNullOrEmpty(oReqTipoIngresoDTO.User))
            {
                oRespTipoIngresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoIngreso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespTipoIngresoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (TipoIngresoDTO item in oReqTipoIngresoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oTipoIngresoData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oTipoIngresoData.Actualizar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespTipoIngresoDTO.Success = true;
                        oRespTipoIngresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespTipoIngresoDTO.Success = false;
                        oRespTipoIngresoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespTipoIngresoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
